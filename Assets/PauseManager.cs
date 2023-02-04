using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] GameManager _gm;


    public bool _isPause = false;
    /// <summary>true の時は一時停止とする</summary>
    bool _pauseFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void Pause(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    Pause _onPauseResume = default;



    public bool _isLevelUp = false;
    /// <summary>true の時は一時停止とする</summary>
    bool _levelUpFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void LevelUp(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    LevelUp _onLevelUp = default;


    public bool _isGetBox = false;
    /// <summary>true の時は一時停止とする</summary>
    bool _getBoxFlg = false;
    /// <summary>一時停止・再開を制御する関数の型（デリゲート）を定義する</summary>
    public delegate void GetBox(bool isPause);
    /// <summary>デリゲートを入れておく変数</summary>
    GetBox _onGetBox = default;

    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public GetBox OnGetBox
    {
        get { return _onGetBox; }
        set { _onGetBox = value; }
    }

    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public LevelUp OnLevelUp
    {
        get { return _onLevelUp; }
        set { _onLevelUp = value; }
    }


    /// <summary>一時停止・再開を入れるデリゲートプロパティ</summary>
    public Pause OnPauseResume
    {
        get { return _onPauseResume; }
        set { _onPauseResume = value; }
    }

    void Update()
    {
        // ESC キーが押されたら一時停止・再開を切り替える
        if (Input.GetKeyDown(KeyCode.Escape) )
        {
            PauseResume();
        }



    }

    /// <summary>一時停止・再開を切り替える</summary>
    void PauseResume()
    {
        _pauseFlg = !_pauseFlg;
        _isPause = !_isPause;

        if (_onPauseResume != null)
        {
            _onPauseResume(_pauseFlg);  // これで変数に代入した関数を（全て）呼び出せる
        }

    }

   public void PauseResumeLevelUp()
    {
        _levelUpFlg = !_levelUpFlg;
        _isLevelUp = !_isLevelUp;

        if (_onLevelUp != null)
        {
            _onLevelUp(_levelUpFlg);  // これで変数に代入した関数を（全て）呼び出せる
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
