using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPause : MonoBehaviour
{
    [SerializeField] PauseManager _pauseManager;

    private bool _isPause = false;

    public bool IsPause { get => _isPause; }

    private bool _isPauseLevelUp = false;

    public bool IsPauseLevelUp { get => _isPauseLevelUp; }
    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
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

    public  void LevelUpPause()
    {
        _isPauseLevelUp = true;
    }


    public  void LevelUpResume()
    {
        _isPauseLevelUp = false;
    }

    public  void Pause()
    {
        _isPause= true;
    }

    public  void Resume()
    {
        _isPause = false;
    }
}

