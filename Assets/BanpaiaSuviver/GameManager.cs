using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("ゲーム時間を表示するText")]
    [SerializeField] Text _timeText;

    [Header("ゲーム時間。分")]
    [SerializeField] private float _maxGameTimeMiniutu = 20;

    private int _nowMiniutu = 0;

    private int _nowSecond = 0;

    public int NowMiniutu { get => _nowMiniutu; }
    public int NowSecond { get => NowSecond; }


    IEnumerator _countCorutin;

    private bool _isPauseLevelUp = false;

    public GameSituation _gameSituation = GameSituation.InGame;
    [SerializeField] PauseManager _pauseManager = default;
    [SerializeField] SceneLode _sceneLode;
    [SerializeField] SceneLode _sceneLodeGameOver;
    [SerializeField] PlayerHp _playerHp;
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

    private void Awake()
    {

    }


    void Start()
    {
        _countCorutin = CountTime();
        StartCoroutine(_countCorutin);
    }


    void Update()
    {

        if(_nowMiniutu==_maxGameTimeMiniutu)
        {
            _sceneLode.GoNextScene();
        }

        if(_playerHp.NowHp<=0)
        {
            _sceneLodeGameOver.GoNextScene();
        }
    }

    IEnumerator CountTime()
    {

        while (true)
        {
            //1秒待つ
            yield return new WaitForSeconds(1);
            _nowSecond++;

            if(_nowSecond==60)
            {
                _nowSecond = 0;
                _nowMiniutu++;
            }
            _timeText.text = _nowMiniutu.ToString("00") + ":" + _nowSecond.ToString("00");
        }
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
        _isPauseLevelUp = true;
        StopCoroutine(_countCorutin);
    }

    public void LevelUpResume()
    {
        _isPauseLevelUp = false;
        StartCoroutine(_countCorutin);
    }

    public void Pause()
    {
        if (!_isPauseLevelUp)
        {
            StopCoroutine(_countCorutin);
        }
    }

    public void Resume()
    {
        if (!_isPauseLevelUp)
        {
            StartCoroutine(_countCorutin);
        }
    }

    public enum GameSituation
    {
        LevlUp,
        InGame,
    }
}
