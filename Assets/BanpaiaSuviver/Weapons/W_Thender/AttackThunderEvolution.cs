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

    [Header("�X�v���C�g")]
    [SerializeField] private GameObject _spriteObj;

    [Header("�X�v���C�g���E������������True")]
    [SerializeField] private bool _spriteDirIsRight = true;

    [Header("Text���o���ʒu")]
    [SerializeField] private List<Transform> _textPos = new List<Transform>();

    [SerializeField] private Rigidbody2D _rb2D;

    private Text _text;

    private float _power;

    /// <summary> �ړ��ꏊ </summary>
    private Vector2 _nextMovePos = default;
    /// <summary> �ړ����I��������ǂ���</summary>
    private bool _isMoveEnd = false;

    /// <summary> �v���C���[ </summary>
    private Transform _playerT;

    //Text�̈ʒu
    private int _setTextPos;

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    protected bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;

    private MainStatas _mainStatas;
    private ObjectPool _objectPool;
    private PauseManager _pauseManager;



    public Text Text { get => _text; set => _text = value; }
    public float AttackPower { get => _power; set => _power = value; }
    public ObjectPool ObjectPool { get => _objectPool; set => _objectPool = value; }
    public MainStatas MainStatas { get => _mainStatas; set => _mainStatas = value; }
    public PauseManager PauseManager { get => _pauseManager; set => _pauseManager = value; }





    public void Init(Transform playerT)
    {
        _playerT = playerT;

        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        SetNextMovePos();
        TalkTextSet();
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
            SetSpriteDir();
            TextMove();
        }
    }


    /// <summary>
    /// �ړ�����
    /// </summary>
    private void Move()
    {
        if (_isMoveEnd)
        {
            _isMoveEnd = false;

            SetNextMovePos();
            TalkTextSet();
        }
        else
        {
            if (_rb2D != null)
                _rb2D.velocity = (_nextMovePos - (Vector2)transform.position).normalized * _moveSpeed;

            if (_endDis > Vector2.Distance(transform.position, _nextMovePos))
            {
                _isMoveEnd = true;
            }
        }
    }

    public void SetSpriteDir()
    {
        if (_spriteDirIsRight)
        {
            if (_rb2D.velocity.x > 0)
            {
                _spriteObj.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                _spriteObj.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else
        {
            if (_rb2D.velocity.x > 0)
            {
                _spriteObj.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                _spriteObj.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    /// <summary>
    /// ���Ɉړ�����ׂ��ꏊ�����߂�
    /// </summary>
    public void SetNextMovePos()
    {
        float offSetX = Random.Range(-_moveH, _moveH);
        float offSetY = Random.Range(-_moveH, _moveH);

        Vector2 randamV = new Vector2(offSetX, offSetY);
        _nextMovePos = (Vector2)_playerT.position + randamV;
    }

    /// <summary>
    /// Text�̈ʒu��ݒ�
    /// </summary>
    public void TextMove()
    {
        _text.gameObject.transform.position = _textPos[_setTextPos].position;
    }

    /// <summary>
    /// Text�̓��e��ݒ肷��
    /// </summary>
    public void TalkTextSet()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            _text.text = "�����܂��[��A��߂Ă������[��";
        }
        else if (r == 1)
        {
            _text.text = "�A�o�o�o�o";
        }
        else
        {
            _text.text = "���O�牴���E���C�����I";
        }

        _setTextPos = Random.Range(0, _textPos.Count);
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
