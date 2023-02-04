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

    [Header("���x���A�b�v��ʂ̃��C�A�E�g�O���[�v")]
    [Tooltip("���x���A�b�v��ʂ̃��C�A�E�g�O���[�v")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    [Header("�Q�[����ʂ̃��C�A�E�g�O���[�v")]
    [Tooltip("�Q�[����ʂ̃��C�A�E�g�O���[�v")] [SerializeField] LayoutGroup _gameSceneLayoutGroup;


    [Header("����̖��O")]
    [Tooltip("����̖��O")] [SerializeField] protected string _weaponName = "";

    [Header("����̍ő僌�x��")]
    [Tooltip("����̍ő僌�x��")] [SerializeField] protected int _maxLevel = 0;

    [Header("�Η�")]
    [Tooltip("�Η�")] [SerializeField] protected float _attackPower;

    [Header("1��U�����Ă��玟�̍U��������܂ł̃N�[������")]
    [Tooltip("������o����")] [SerializeField] protected float _coolTime = 1;

    [Header("�͈�")]
    [Tooltip("�͈�")] [SerializeField] protected float _eria;

    [Header("���x")]
    [Tooltip("���x")] [SerializeField] protected float _speed = 6;

    [Header("������o����")]
    [Tooltip("������o����")] [SerializeField] protected int _number;


    protected MainStatas _mainStatas;

    /// <summary>���x���A�b�v�e�[�u����ǂݍ��ނ���</summary>
    protected WeaponData _weaponManager = default;

    [SerializeField] LevelUpController _levelUpController;

     [SerializeField] BoxControl _boxControl;

    [SerializeField] protected ObjectPool _objectPool;
    /// <summary>����̃��x��</summary>
    protected int _level = 0;



    /// <summary>�v���C���[�̃p�����[�^�[</summary>
    public WeaponStats _weaponStats = default;


    /// <summary>�Ղꂢ��[�́@�I�u�W�F�N�g</summary>
    protected GameObject _player;
    /// <summary>�N�[���^�C�����v�Z����悤</summary>
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
    /// ���x���A�b�v����
    /// </summary>
    /// <param name="level">���x���A�b�v�����������x����</param>
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

        Debug.Log(_weaponName + "���x���A�b�v�I���݂̃��x����" + _level);
        _levelUpController.WeaponLevelUp(_weaponName, _level);
    }



    // protected abstract void SetPower();


    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
        _anim = gameObject.GetComponent<Animator>();
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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
