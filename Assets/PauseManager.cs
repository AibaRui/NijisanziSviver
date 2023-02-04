using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameManager _gm;


    public bool _isPause = false;
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _pauseFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void Pause(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    Pause _onPauseResume = default;



    public bool _isLevelUp = false;
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _levelUpFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void LevelUp(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    LevelUp _onLevelUp = default;


    public bool _isGetBox = false;
    /// <summary>true �̎��͈ꎞ��~�Ƃ���</summary>
    bool _getBoxFlg = false;
    /// <summary>�ꎞ��~�E�ĊJ�𐧌䂷��֐��̌^�i�f���Q�[�g�j���`����</summary>
    public delegate void GetBox(bool isPause);
    /// <summary>�f���Q�[�g�����Ă����ϐ�</summary>
    GetBox _onGetBox = default;

    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public GetBox OnGetBox
    {
        get { return _onGetBox; }
        set { _onGetBox = value; }
    }

    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public LevelUp OnLevelUp
    {
        get { return _onLevelUp; }
        set { _onLevelUp = value; }
    }


    /// <summary>�ꎞ��~�E�ĊJ������f���Q�[�g�v���p�e�B</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        // ESC �L�[�������ꂽ��ꎞ��~�E�ĊJ��؂�ւ���
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseResume();
        }



    }

    /// <summary>�ꎞ��~�E�ĊJ��؂�ւ���</summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _isPause = !_isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
        }

    }

   public void PauseResumeLevelUp()
    {
        _levelUpFlg = !_levelUpFlg;
        _isLevelUp = !_isLevelUp;

        if (_onLevelUp != null)
        {
            _onLevelUp(_levelUpFlg);  // ����ŕϐ��ɑ�������֐����i�S�āj�Ăяo����
        }

    }

    public void PauseResumeGetBox()
    {
        _getBoxFlg = !_getBoxFlg;
        _isGetBox = !_isGetBox;

        if(_onGetBox!=null)
        {
            _onGetBox(_getBoxFlg);
        }
    }


}
