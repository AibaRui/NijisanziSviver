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
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
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

