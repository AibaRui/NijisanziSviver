using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionSlash : MonoBehaviour
{
    Quaternion setR = default;

    [SerializeField] private float _rotationSpeed = 100;

    [SerializeField] private Animator _anim;

    [SerializeField] private AudioSource _aud;


    [Header("���������̉�1")]
    [SerializeField] private AudioClip _audioClip;

    [Header("���������̉�2")]
    [SerializeField] private AudioClip _audioClip2;

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    private bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    private bool _isLevelUpPause = false;

    private bool _isPauseGetBox = false;

    private int _saveX = 1;

    private PauseManager _pauseManager;

    public PauseManager PauseManager { get => _pauseManager; set => _pauseManager = value; }

    void Start()
    {

    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        {
            _saveX = h > 0 ? 1 : -1;
        }

        transform.localScale = new Vector3(_saveX, 1, 1);
    }

    public void PlayerSound1()
    {
        _aud.PlayOneShot(_audioClip);
    }

    public void PlayerSound2()
    {
        _aud.PlayOneShot(_audioClip2);
    }

    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager = FindObjectOfType<PauseManager>();
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnLevelUp -= LevelUpPauseResume;
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

    public void LevelUpPause()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = true;

            if (_anim)
            {
                _anim.enabled = false;
            }
            _aud.UnPause();
        }
    }

    public void LevelUpResume()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = false;

            if (_anim)
            {
                _anim.enabled = true;
            }

            _aud.UnPause();
        }
    }

    public void Pause()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_anim)
            {
                _anim.enabled = false;
            }

            _aud.Pause();

        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _isPause = false;


            if (_anim)
            {
                _anim.enabled = true;
            }

            _aud.UnPause();
        }
    }
}
