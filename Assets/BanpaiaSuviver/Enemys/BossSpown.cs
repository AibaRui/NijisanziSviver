using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpown : MonoBehaviour
{

    [SerializeField] ObjectPool _objectPool;
    [SerializeField] GameManager _gm;
    [SerializeField] PauseManager _pauseManager = default;
    [SerializeField] MapSizeControl _mapSize;

    [Header("時間経過による、敵の生成時間と出す敵の情報")]
    [SerializeField] List<BossSpownTimes> _situationOfEnemysData = new List<BossSpownTimes>();


    /// <summary>プレイヤーのオブジェクト</summary>
    private GameObject _player;

    private int _nowSituation = 0;


    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>時間経過により、状況(敵の数や生成間隔)を変える</summary>
    void ChangeSituation()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            //現在の経過時間が、設定時間を超えていたら次の状況に移行
            if ((_nowSituation < _situationOfEnemysData.Count && _situationOfEnemysData[_nowSituation].Minittu == _gm.NowMiniutu))
            {
                Spawn();
                _nowSituation++;
            }
        }
    }




    void Update()
    {
        //現在の経過時間と設定時間を見比べる
        ChangeSituation();
    }


    /// <summary>敵を出す</summary>
    void Spawn()
    {
        var go = Instantiate(_situationOfEnemysData[_nowSituation].BossPrefab);
        go.transform.position = SetPosition();
    }

    private Vector3 SetPosition()
    {
        //プラス方向、マイナス方向、どちらにするか。[0がプラス方向][1がマイナス方向]
        int randomUpOrDown = Random.Range(0, 2);

        //最低でもプレイヤーから離す距離。の正の値
        Vector3 minPosUp = new Vector3(10, 0, 0) + _player.transform.position;
        //最低でもプレイヤーから離す距離。の負の値
        Vector3 minPosDown = new Vector3(-10, 0, 0) + _player.transform.position;

        //最低の位置からランダムに配置する場所。
        Vector3 randomAddPosUp = new Vector3(0, UnityEngine.Random.Range(-5, 5), 0);
        //最低の位置からランダムに配置する場所。の負の値
        Vector3 randomAddPosDown = new Vector3(0, UnityEngine.Random.Range(-5, 5), 0);

        if (randomUpOrDown == 0)//敵を沸かせる位置の、正の値
        {
            Vector3 enemySpownPosUp = minPosUp + randomAddPosUp;
            //マップのはじを超えないように調整
            //if (enemySpownPosUp.x > _mapSize.UpMapSize.x) enemySpownPosUp.x = _mapSize.UpMapSize.x;
            //if (enemySpownPosUp.y > _mapSize.UpMapSize.y) enemySpownPosUp.y = _mapSize.UpMapSize.y;
            return enemySpownPosUp;
        }
        else//敵を沸かせる位置の、正の値
        {
            Vector3 enemySpownPosDown = minPosDown + randomAddPosDown;
            // if (enemySpownPosDown.x > _mapSize.MinussMapSize.x) enemySpownPosDown.x = _mapSize.MinussMapSize.x;
            //if (enemySpownPosDown.y > _mapSize.MinussMapSize.y) enemySpownPosDown.y = _mapSize.MinussMapSize.y;
            return enemySpownPosDown;
        }
    }


    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
        // PauseGetBox.Instance.SetEvent(this);
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
        //  PauseGetBox.Instance.RemoveEvent(this);
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
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;

    }
}


[System.Serializable]
public class BossSpownTimes
{

   [SerializeField] private GameObject _bossPrefeb;
    public GameObject BossPrefab { get => _bossPrefeb; set => _bossPrefeb = value; }


    [SerializeField] private int _minittu;

    public int Minittu { get => _minittu; set => _minittu = value; }

}
