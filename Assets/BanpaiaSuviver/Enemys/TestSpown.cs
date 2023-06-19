using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestSpown : MonoBehaviour//,IPausebleGetBox
{

    [SerializeField] ObjectPool _objectPool;
    [SerializeField] GameManager _gm;
    [SerializeField] PauseManager _pauseManager = default;
    [SerializeField] MapSizeControl _mapSize;

    [Header("���Ԍo�߂ɂ��A�G�̐������ԂƏo���G�̏��")]
    [SerializeField] List<EnemySpownInformation> _situationOfEnemysData = new List<EnemySpownInformation>();

    [Header("�v���C���[����Œ�ł���������")]
    [SerializeField]
    Vector3 minSpawnPosition = default;

    [SerializeField]
    Vector3 maxSpawnPosition = default;


    /// <summary>�v���C���[�̃I�u�W�F�N�g</summary>
    private GameObject _player;

    private int _nowSituation = 0;

    private IEnumerator _instanciateCorutine;
    private WaitForSeconds spawnIntervalWait;

    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        foreach (var datas in _situationOfEnemysData)
        {
            datas.Init(this);
        }
        _situationOfEnemysData.Sort();

        _instanciateCorutine = SpawnTimer();
        spawnIntervalWait = new WaitForSeconds(_situationOfEnemysData[_nowSituation].SpawnInterval);
        StartCoroutine(_instanciateCorutine);
        Debug.Log($"�Q�[���J�n�B���݂̏󋵂́A{_nowSituation}/{_situationOfEnemysData.Count}");
    }

    /// <summary>���Ԍo�߂ɂ��A��(�G�̐��␶���Ԋu)��ς���</summary>
    void ChangeSituation()
    {
        //���݂̌o�ߎ��Ԃ��A�ݒ莞�Ԃ𒴂��Ă����玟�̏󋵂Ɉڍs
        if ((_nowSituation < _situationOfEnemysData.Count - 1 && _situationOfEnemysData[_nowSituation + 1].SetTime == _gm.NowMiniutu))
        {
            _nowSituation++;

            StopCoroutine(_instanciateCorutine);
            spawnIntervalWait = new WaitForSeconds(_situationOfEnemysData[_nowSituation].SpawnInterval);
            _instanciateCorutine = SpawnTimer();
            StartCoroutine(_instanciateCorutine);
            Debug.Log($"�G��ύX���܂��B���݂̏󋵂́A{_nowSituation}/{_situationOfEnemysData.Count}");
        }
    }


    IEnumerator SpawnTimer()
    {
        while (true)
        {
            Spawn();
            yield return spawnIntervalWait;
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
        foreach (var data in _situationOfEnemysData[_nowSituation]._enemysData)
        {
            for (int i = 0; i < data.SpownNum; i++)
            {
                //�G�𐶐�
                _objectPool.UseObject(SetPosition(), data.Enemy);
            }
        }
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

    //Vector3 test a ()
    //{
    //    Debug.Log(_player.transform.position);
    //    //�v���X�����A�}�C�i�X�����A�ǂ���ɂ��邩�B[0���v���X����][1���}�C�i�X����]
    //    int randomUpOrDown = Random.Range(0, 2);

    //    //�Œ�ł��v���C���[���痣�������B�̐��̒l
    //    Vector3 minPosUp = minSpawnPosition + _player.transform.position;
    //    //�Œ�ł��v���C���[���痣�������B�̕��̒l
    //    Vector3 minPosDown = -minSpawnPosition + _player.transform.position;

    //    //�Œ�̈ʒu���烉���_���ɔz�u����ꏊ�B
    //    Vector3 randomAddPosUp = new Vector3(Random.Range(0, 1), Random.Range(0, 10), 0);
    //    //�Œ�̈ʒu���烉���_���ɔz�u����ꏊ�B�̕��̒l
    //    Vector3 randomAddPosDown = new Vector3(Random.Range(0, -1), Random.Range(0, -10), 0);

    //    if (randomUpOrDown == 0)//�G�𕦂�����ʒu�́A���̒l
    //    {
    //        Debug.Log("AAAA");
    //        Vector3 enemySpownPosUp = minPosUp + randomAddPosUp;
    //        //�}�b�v�̂͂��𒴂��Ȃ��悤�ɒ���
    //        if (enemySpownPosUp.x > _mapSize.UpMapSize.x) enemySpownPosUp.x = _mapSize.UpMapSize.x;
    //        if (enemySpownPosUp.y > _mapSize.UpMapSize.y) enemySpownPosUp.y = _mapSize.UpMapSize.y;
    //        return enemySpownPosUp;
    //    }
    //    else//�G�𕦂�����ʒu�́A���̒l
    //    {
    //        Debug.Log("BBBB");
    //        Vector3 enemySpownPosDown = minPosDown + randomAddPosDown;
    //        if (enemySpownPosDown.x > _mapSize.MinussMapSize.x) enemySpownPosDown.x = _mapSize.MinussMapSize.x;
    //        if (enemySpownPosDown.y > _mapSize.MinussMapSize.y) enemySpownPosDown.y = _mapSize.MinussMapSize.y;
    //        return enemySpownPosDown;
    //    }
    //}


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
        if (_instanciateCorutine != null)
        {
            StopCoroutine(_instanciateCorutine);
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instanciateCorutine != null)
        {
            StartCoroutine(_instanciateCorutine);
        }
    }

    public void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_instanciateCorutine != null)
            {
                StopCoroutine(_instanciateCorutine);
            }
        }
    }

    public void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_instanciateCorutine != null)
            {
                StartCoroutine(_instanciateCorutine);
            }
        }
    }

    //void IPausebleGetBox.PauseResume(bool isPause)
    //{
    //    _isPauseGetBox = isPause;

    //    if(isPause)
    //    {
    //        if (_instanciateCorutine != null)
    //        {
    //            StopCoroutine(_instanciateCorutine);
    //        }
    //    }
    //    else
    //    {
    //        if (_instanciateCorutine != null)
    //        {
    //            StartCoroutine(_instanciateCorutine);
    //        }
    //    }
    //}
}
