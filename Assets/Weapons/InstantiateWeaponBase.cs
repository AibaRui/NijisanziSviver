using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class InstantiateWeaponBase : MonoBehaviour
{
    [SerializeField] GameObject _iconUseUI;

    [SerializeField] GameObject _iconUseBox;

    [SerializeField] GameObject _panel;

    [Header("レベルアップ画面のレイアウトグループ")]
    [Tooltip("レベルアップ画面のレイアウトグループ")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    [Header("ゲーム画面のレイアウトグループ")]
    [Tooltip("ゲーム画面のレイアウトグループ")] [SerializeField] LayoutGroup _gameSceneLayoutGroup;


    [Header("武器の名前")]
    [Tooltip("武器の名前")] [SerializeField] protected string _weaponName = "";

    [Header("武器の最大レベル")]
    [Tooltip("武器の最大レベル")] [SerializeField] protected int _maxLevel = 0;

    [Header("火力")]
    [Tooltip("火力")] [SerializeField] protected float _attackPower;

    [Header("1回攻撃してから次の攻撃をするまでのクール時間")]
    [Tooltip("武器を出す数")] [SerializeField] protected float _coolTime = 1;

    [Header("範囲")]
    [Tooltip("範囲")] [SerializeField] protected float _eria;

    [Header("速度")]
    [Tooltip("速度")] [SerializeField] protected float _speed = 6;

    [Header("武器を出す数")]
    [Tooltip("武器を出す数")] [SerializeField] protected int _number;


    protected MainStatas _mainStatas;

    /// <summary>レベルアップテーブルを読み込むため</summary>
    protected WeaponData _weaponManager = default;

    [SerializeField] LevelUpController _levelUpController;

     [SerializeField] BoxControl _boxControl;

    [SerializeField] protected ObjectPool _objectPool;
    /// <summary>武器のレベル</summary>
    protected int _level = 0;



    /// <summary>プレイヤーのパラメーター</summary>
    public WeaponStats _weaponStats = default;


    /// <summary>ぷれいやーの　オブジェクト</summary>
    protected GameObject _player;
    /// <summary>クールタイムを計算するよう</summary>
    protected float _countTime = 0;


    protected bool _isPause = false;
    protected bool _isLevelUpPause = false;
    protected bool _isPauseGetBox = false;

    Animator _anim;
  protected  AudioSource _aud;

    protected IEnumerator _instantiateCorutin;

    PauseManager _pauseManager = default;
    private void Awake()
    {
        
        _aud = GetComponent<AudioSource>();
        _levelUpController.SetWeaponData(_weaponName, _maxLevel, _iconUseBox, _panel, _iconUseUI);
        _boxControl.SetWeapon(_weaponName, this);
        _mainStatas = FindObjectOfType<MainStatas>();
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    private void SetIcon()
    {
        var go = Instantiate(_iconUseUI);
        var go2 = Instantiate(_iconUseUI);
        go.transform.SetParent(_basePanelLayoutGroup.transform);
        go2.transform.SetParent(_gameSceneLayoutGroup.transform);
    }

    /// <summary>
    /// レベルアップする
    /// </summary>
    /// <param name="level">レベルアップさせたいレベル数</param>
    public void LevelUp(int level = 1)
    {
        _weaponManager = FindObjectOfType<WeaponData>();

        _level += level;

        _weaponStats = _weaponManager.GetData(_level, _weaponName);

        _attackPower = _weaponStats.Power;
        _coolTime = _weaponStats.CoolTime;
        _eria = _weaponStats.Eria;
        _speed = _weaponStats.Speed;
        _number = _weaponStats.Number;

        Debug.Log(_weaponName + "レベルアップ！現在のレベルは" + _level);
        _levelUpController.WeaponLevelUp(_weaponName, _level);
    }



    // protected abstract void SetPower();


    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
        _anim = gameObject.GetComponent<Animator>();
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void LevelUpPauseResume(bool isPause)
    {
        if (isPause)
        {
            LevelUpPause();
        }
        else
        {
            LevelUpResume();
        }
    }

    void LevelUpPause()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = true;
            if (_instantiateCorutin != null)
            {
                StopCoroutine(_instantiateCorutin);
            }
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instantiateCorutin != null)
        {
            StartCoroutine(_instantiateCorutin);
        }
    }

    public void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_instantiateCorutin != null)
            {
                StopCoroutine(_instantiateCorutin);
            }
        }
    }

    public void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_instantiateCorutin != null)
            {
                StartCoroutine(_instantiateCorutin);
            }
        }
    }
}
