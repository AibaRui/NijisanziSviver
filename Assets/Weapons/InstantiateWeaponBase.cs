using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class InstantiateWeaponBase : MonoBehaviour
{
    
    [Header("武器のオブジェクト")]
    [Tooltip("武器のオブジェクト")] [SerializeField] protected GameObject _weaponObject;

    [Header("武器の名前")]
    [Tooltip("武器の名前")] [SerializeField] protected string _weaponName = "";

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

    /// <summary>武器のレベル</summary>
    protected int _level = 0;

    [Header("武器の最大レベル")]
    [Tooltip("武器の最大レベル")] [SerializeField] protected int _maxLevel = 0;

    /// <summary>プレイヤーのパラメーター</summary>
    public WeaponStats _weaponStats = default;


    /// <summary>ぷれいやーの　オブジェクト</summary>
    protected GameObject _player;
    /// <summary>クールタイムを計算するよう</summary>
    protected float _countTime = 0;


    protected bool _isPause = false;
    protected bool _isLevelUpPause = false;

    Animator _anim;


    PauseManager _pauseManager = default;
    private void Awake()
    {
        _mainStatas = FindObjectOfType<MainStatas>();   
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    /// <summary>
    /// レベルアップする
    /// </summary>
    /// <param name="level">レベルアップさせたいレベル数</param>
    public void LevelUp(int level = 1)
    {
        _weaponManager = FindObjectOfType<WeaponData>();
        if (_level < _maxLevel)
        {
            _level += level;
            _weaponStats = _weaponManager.GetData(_level, _weaponName);

            _attackPower = _weaponStats.Power;
            _coolTime = _weaponStats.CoolTime;
            _eria = _weaponStats.Eria;
            _speed = _weaponStats.Speed;
            _number = _weaponStats.Number;

            Debug.Log(_weaponName + "レベルアップ！現在のレベルは" + _level);
        }
        GameManager gm = FindObjectOfType<GameManager>();
        gm.WeaponLevelUp(_weaponName,_level);
    }


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

    public abstract void LevelUpPause();


    public abstract void LevelUpResume();


    public abstract void Pause();

    public abstract void Resume(); 
}
