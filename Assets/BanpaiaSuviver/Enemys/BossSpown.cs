using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpown : MonoBehaviour
{

    [SerializeField] ObjectPool _objectPool;
    [SerializeField] GameManager _gm;
    [SerializeField] PauseManager _pauseManager = default;
    [SerializeField] MapSizeControl _mapSize;

    [Header("���Ԍo�߂ɂ��A�G�̐������ԂƏo���G�̏��")]
    [SerializeField] List<BossSpownTimes> _situationOfEnemysData = new List<BossSpownTimes>();


    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;

    private int _nowSituation = 0;


    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>���Ԍo�߂ɂ��A��(�G�̐��␶���Ԋu)��ς���</summary>
    void ChangeSituation()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            //���݂̌o�ߎ��Ԃ��A�ݒ莞�Ԃ𒴂��Ă����玟�̏󋵂Ɉڍs
            if ((_nowSituation < _situationOfEnemysData.Count && _situationOfEnemysData[_nowSituation].Minittu == _gm.NowMiniutu))
            {
                Spawn();
                _nowSituation++;
            }
        }
    }




    void Update()
    {
        //���݂̌o�ߎ��ԂƐݒ莞�Ԃ�����ׂ�
        ChangeSituation();
    }


    /// <summary>�G���o��</summary>
    void Spawn()
    {
        var go = Instantiate(_situationOfEnemysData[_nowSituation].BossPrefab);
        go.transform.position = SetPosition();
    }

    private Vector3 SetPosition()
    {
        //�v���X�����A�}�C�i�X�����A�ǂ���ɂ��邩�B[0���v���X����][1���}�C�i�X����]
        int randomUpOrDown = Random.Range(0, 2);

        //�Œ�ł��v���C���[���痣�������B�̐��̒l
        Vector3 minPosUp = new Vector3(10, 0, 0) + _player.transform.position;
        //�Œ�ł��v���C���[���痣�������B�̕��̒l
        Vector3 minPosDown = new Vector3(-10, 0, 0) + _player.transform.position;

        //�Œ�̈ʒu���烉���_���ɔz�u����ꏊ�B
        Vector3 randomAddPosUp = new Vector3(0, UnityEngine.Random.Range(-5, 5), 0);
        //�Œ�̈ʒu���烉���_���ɔz�u����ꏊ�B�̕��̒l
        Vector3 randomAddPosDown = new Vector3(0, UnityEngine.Random.Range(-5, 5), 0);

        if (randomUpOrDown == 0)//�G�𕦂�����ʒu�́A���̒l
        {
            Vector3 enemySpownPosUp = minPosUp + randomAddPosUp;
            //�}�b�v�̂͂��𒴂��Ȃ��悤�ɒ���
            //if (enemySpownPosUp.x > _mapSize.UpMapSize.x) enemySpownPosUp.x = _mapSize.UpMapSize.x;
            //if (enemySpownPosUp.y > _mapSize.UpMapSize.y) enemySpownPosUp.y = _mapSize.UpMapSize.y;
            return enemySpownPosUp;
        }
        else//�G�𕦂�����ʒu�́A���̒l
        {
            Vector3 enemySpownPosDown = minPosDown + randomAddPosDown;
            // if (enemySpownPosDown.x > _mapSize.MinussMapSize.x) enemySpownPosDown.x = _mapSize.MinussMapSize.x;
            //if (enemySpownPosDown.y > _mapSize.MinussMapSize.y) enemySpownPosDown.y = _mapSize.MinussMapSize.y;
            return enemySpownPosDown;
        }
    }


    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
        // PauseGetBox.Instance.SetEvent(this);
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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
