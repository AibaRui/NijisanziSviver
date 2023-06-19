using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("�Q�[�����Ԃ�\������Text")]
    [SerializeField] Text _timeText;

    [Header("�Q�[�����ԁB��")]
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
            //1�b�҂�
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
