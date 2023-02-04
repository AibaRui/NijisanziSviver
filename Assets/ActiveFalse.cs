using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveFalse : MonoBehaviour
{
    [SerializeField] float _lifeTime = 0;

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    private bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    private bool _isLevelUpPause = false;


    Animator _anim;
    PauseManager _pauseManager = default;


    IEnumerator _col;
    
    IEnumerator CountTime()
    {
        yield return new WaitForSeconds(_lifeTime);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {

        _pauseManager = FindObjectOfType<PauseManager>();

        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _col = CountTime();
        StartCoroutine(_col);
    }
    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
    }

    ///////Parse����/////
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

        StopCoroutine(_col);
        if (_anim)
        {
            _anim.enabled = false;
        }

    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        StartCoroutine(_col);

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

            StopCoroutine(_col);

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

            StartCoroutine(_col);

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }


}
