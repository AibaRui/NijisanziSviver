using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse : MonoBehaviour
{
    [SerializeField] float _lifeTime = 0;

    private float _countTime;

    /// <summary>Pauseしているかどうか</summary>
    private bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
    private bool _isLevelUpPause = false;

    [SerializeField] Animator _anim;
    PauseManager _pauseManager = default;



    private void Update()
    {
        if (_isPause || _isLevelUpPause) return;

        _countTime += Time.deltaTime;

        if (_countTime > _lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        _pauseManager = FindObjectOfType<PauseManager>();

        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _countTime = 0;
    }
    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
        _pauseManager.OnLevelUp -= LevelUpPauseResume;

    }

    ///////Parse処理/////
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
            _anim.enabled = false;
        }

    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_anim)
        {
            _anim.enabled = true;
        }


    }

    public void Pause()
    {
        if (!_isLevelUpPause)
        {
            _isPause = true;

            if (_anim)
            {
                _anim.enabled = false;
            }
        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause)
        {
            _isPause = false;

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }


}
