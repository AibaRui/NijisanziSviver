using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class InstantiateWeaponBase : MonoBehaviour
{
    [Header("����̖��O")]
    [Tooltip("����̖��O")] protected string _weaponName = "";

    [Header("����̍ő僌�x��")]
    [Tooltip("����̍ő僌�x��")] protected int _maxLevel = 0;

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

    protected AudioSource _aud;

    protected IEnumerator _instantiateCorutin;


    protected ObjectPool _objectPool;
    protected BoxControl _boxControl;
    protected MainStatas _mainStatas;
    protected LevelUpController _levelUpController;
    protected WeaponData _weaponData;
    protected PauseManager _pauseManager = default;

    public GameObject Player { get => _player; set => _player = value; }

    public ObjectPool ObjectPool { get => _objectPool; set => _objectPool = value; }
    public BoxControl BoxControl { get => _boxControl; set => _boxControl = value; }
    public LevelUpController LevelUpController { get => _levelUpController; set => _levelUpController = value; }
    public PauseManager PauseManager { get => _pauseManager; set => _pauseManager = value; }

    public MainStatas MainStatas { get => _mainStatas; set => _mainStatas = value; }

    public WeaponData WeaponData { get => _weaponData; set => _weaponData = value; }


    public void Init(string name, int maxLevel)
    {
        _weaponName = name;
        _maxLevel = maxLevel;

        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _anim = gameObject.GetComponent<Animator>();
    }

    /// <summary>
    /// ���x���A�b�v����
    /// </summary>
    /// <param name="level">���x���A�b�v�����������x����</param>
    public void LevelUp()
    {
        if (_aud == null)
            _aud = GetComponent<AudioSource>();

        _level++;
        _weaponStats = _weaponData.GetData(_level, _weaponName);

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


    private void Awake()
    {

    }

    void OnEnable()
    {

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
