using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHiyoko : MonoBehaviour
{

    bool _isPause;
    bool _isLevelUpPause;

    AudioSource _aud;

   [SerializeField] Animator _animSprite;
    Animator _anim;

    PauseManager _pauseManager = default;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _anim = gameObject.GetComponent<Animator>();
        _aud = GetComponent<AudioSource>();
        _animSprite = _animSprite.GetComponent<Animator>();
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

    public void LevelUpPause()
    {
        _isLevelUpPause = true;
        if (_anim)
        {
            _animSprite.enabled = false;
            _anim.enabled = false;
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_anim)
        {
            _animSprite.enabled = true;
            _anim.enabled = true;
        }
    }

    public void Pause()
    {
        if (!_isLevelUpPause)
        {
            if (_anim)
            {
                _animSprite.enabled = false;
                _anim.enabled = false;
            }
        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause)
        {
            _isPause = false;
            // Rigidbody �̊������ĊJ���A�ۑ����Ă��������x�E��]��߂�
            if (_anim)
            {
                _animSprite.enabled = true;
                _anim.enabled = true;
            }
        }
    }

}
