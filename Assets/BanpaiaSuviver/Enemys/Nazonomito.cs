using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nazonomito : MonoBehaviour
{
    [Header("Sprite�̌����B�E������������True�B��������������False")]
    [SerializeField] private bool _spriteDir;

    [Header("�ړ����x")]
    [SerializeField] float _moveSpeed = 3;

    [Header("Player�Ƃ̕ۂ���")]
    [SerializeField] private float _dir = 7;

    [Header("�U�����x")]
    [SerializeField] float _attackTime = 3;

    [Header("�e")]
    [SerializeField] GameObject _bullet;

    [Header("�e�̑��x")]
    [SerializeField] float _bulletSpeed = 3;

    [Header("�}�Y��")]
    [SerializeField] Transform _muzluPos = default;


    [Header("�_���[�W���������Ԋu")]
    [SerializeField] float _damageTime = 1;

    [Header("�U����")]
    [SerializeField] int _attackPower = 1;

    private float _nowTime = 0;

    IEnumerator _attackCorutin;

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    protected bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;

    GameObject _player;

    Rigidbody2D _rb;

    [SerializeField] Animator _anim;

    PauseManager _pauseManager = default;
    private void Awake()
    {

    }

    void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();
        _attackCorutin = Attack();
        StartCoroutine(_attackCorutin);
        _pauseManager = FindObjectOfType<PauseManager>();

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
    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            Move();
        }

    }

    IEnumerator Attack()
    {
        while (true)
        {
            _anim.Play("Enemy_Nazonomito_Attack");
            Vector2 velo = _player.transform.position - transform.position;
            var go = Instantiate(_bullet);
            go.transform.position = _muzluPos.position;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = velo.normalized * _bulletSpeed;
            _rb.velocity = velo.normalized * _moveSpeed;
            yield return new WaitForSeconds(_attackTime);
        }
    }

    private void Move()
    {
        Vector2 velo = _player.transform.position - transform.position;

        if (_spriteDir)
        {
            if (velo.x >= 0) transform.localScale = new Vector3(1, 1, 1);
            else transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            if (velo.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!_isPause && !_isLevelUpPause)
            {
                if (_nowTime <= 0)
                {
                    if (collision.gameObject.TryGetComponent<IDamageble>(out IDamageble player))
                    {
                        player.AddDamage(_attackPower);
                        _nowTime = _damageTime;
                    }
                }
                else
                {
                    _nowTime -= Time.deltaTime;
                }
            }
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
        StopCoroutine(_attackCorutin);
        if (_rb)
        {
            _rb.velocity = Vector2.zero;
        }

        if (_anim)
        {
            _anim.enabled = false;
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;
        StartCoroutine(_attackCorutin);

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
            StopCoroutine(_attackCorutin);
            if (_rb)
            {
                _rb.velocity = Vector2.zero;
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
            StartCoroutine(_attackCorutin);
            _isPause = false;

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }
}
