using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackThunderEvolution : MonoBehaviour
{
    [Header("�ړ����x")]
    [SerializeField] private float _moveSpeed = 5;

    [Header("�ړ��̕�")]
    [SerializeField] private float _moveH = 5;

    [Header("�ړ������Ƃ݂Ȃ�����")]
    [SerializeField] private float _endDis = 5;

    [Header("����̃A�j���[�V����")]
    [SerializeField] private Animator _anim;


    private Text _text;

    private float _power;

    /// <summary> �ړ��ꏊ </summary>
    private Vector2 _nextMovePos = default;
    /// <summary> �ړ����I��������ǂ���</summary>
    private bool _isMoveEnd = false;


    /// <summary>Pause���Ă��邩�ǂ���</summary>
    protected bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;

    private MainStatas _mainStatas;

    private ObjectPool _objectPool;

    private PauseManager _pauseManager;

    /// <summary> �v���C���[ </summary>
    private Transform _playerT;

    public Text Text { get => _text; set => _text = value; }
    public float AttackPower { get => _power; set => _power = value; }
    public ObjectPool ObjectPool { get => _objectPool; set => _objectPool = value; }
    public MainStatas MainStatas { get => _mainStatas; set => _mainStatas = value; }
    public PauseManager PauseManager { get => _pauseManager; set => _pauseManager = value; }

    [SerializeField] private Rigidbody2D _rb2D;

    private Vector2 _off;

    public void Init(Transform playerT)
    {
        _playerT = playerT;

        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        float offSetX = Random.Range(-_moveH, _moveH);
        float offSetY = Random.Range(-_moveH, _moveH);

        Vector2 randamV = new Vector2(offSetX, offSetY);
        _nextMovePos = (Vector2)_playerT.position + randamV;
        _off = new Vector2(offSetX, offSetY);
    }
    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnLevelUp -= LevelUpPauseResume;

    }

    private void FixedUpdate()
    {
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            Move();
        }
    }


    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        if (_isMoveEnd)
        {
            float offSetX = Random.Range(-_moveH, _moveH);
            float offSetY = Random.Range(-_moveH, _moveH);

            Vector2 randamV = new Vector2(offSetX, offSetY);
            _nextMovePos = (Vector2)_playerT.position + randamV;
            _isMoveEnd = false;

            int r = Random.Range(0, 3);

            if(r==0)
            {
                _text.text = "�����܂��[��A��߂Ă������[��";
            }
            else if(r==1)
            {
                _text.text = "�A�o�o�o�o";
            }
            else
            {
                _text.text = "��߂Ă������[��";
            }
            _off = new Vector2(offSetX, offSetY);
        }
        else
        {
            if (_rb2D != null)
                _rb2D.velocity = (_nextMovePos - (Vector2)transform.position).normalized * _moveSpeed;

            if (_endDis > Vector2.Distance(transform.position, _nextMovePos))
            {
                _isMoveEnd = true;
            }

            _text.gameObject.transform.position = (Vector2)transform.position + _off;

            Debug.Log(Vector2.Distance(transform.position, _nextMovePos));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            var go = _objectPool.UseObject(transform.position, PoolObjectType.Thunder);
            go.gameObject.GetComponent<WeaponBase>().Power = _power * _mainStatas.Power;
        }

    }
    ///////Parse����/////
    ///
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

        if (_rb2D)
        {
            _rb2D.velocity = Vector2.zero;
        }

        if (_anim)
        {
            _anim.enabled = false;
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_anim)
        {
            _anim.enabled = true;
        }
    }

    public void Pause()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _isPause = true;

            if (_rb2D)
            {
                _rb2D.velocity = Vector2.zero;
            }
            if (_anim)
            {
                _anim.enabled = false;
            }
        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _isPause = false;

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }
}
