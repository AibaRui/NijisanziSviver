using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionSlash : MonoBehaviour
{
    Quaternion setR = default;

    [SerializeField] private float _rotationSpeed = 100;

    [SerializeField] private Animator _anim;

    [SerializeField] private AudioSource _aud;


    [Header("たたく時の音1")]
    [SerializeField] private AudioClip _audioClip;

    [Header("たたく時の音2")]
    [SerializeField] private AudioClip _audioClip2;

    /// <summary>Pauseしているかどうか</summary>
    private bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
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

    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager = FindObjectOfType<PauseManager>();
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
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
