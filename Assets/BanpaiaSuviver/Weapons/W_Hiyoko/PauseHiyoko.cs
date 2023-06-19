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

    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _anim = gameObject.GetComponent<Animator>();
        _aud = GetComponent<AudioSource>();
        _animSprite = _animSprite.GetComponent<Animator>();
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
            // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
            if (_anim)
            {
                _animSprite.enabled = true;
                _anim.enabled = true;
            }
        }
    }

}
