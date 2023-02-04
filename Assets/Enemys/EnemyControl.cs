using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : EnemyDestroy
{
    [Header("�_���[�W���������Ԋu")]
    [SerializeField] float _damageTime = 1;

    [Header("�U����")]
    [SerializeField] int _attackPower = 1;

    [Header("�{�X���ǂ���")]
    [SerializeField] bool _isBoss = false;


    private float _nowTime = 0;

    bool _isAttack = true;

    bool _isAttacked = false;

    [SerializeField] EnemyMove _enemyMove;

    [SerializeField] EnemyData _enemyData;

    [SerializeField] EnemyHp _enemyHp;

    [Header("�o���l�����ŗ��Ƃ����A�����_����")]
    [SerializeField] bool _isOnlyDrop = false;

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    protected bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;

    float _angularVelocityOfLevelUpPause;
    Vector2 _velocityOfLevelUpPause;

    float _angularVelocity;
    Vector2 _velocity;


    DamageTextPool _textPool;

    ObjectPool _objctPool;
    Rigidbody2D _rb;

    [SerializeField] Animator _anim;

    PauseManager _pauseManager = default;




    private void Awake()
    {
        _textPool = GameObject.FindObjectOfType<DamageTextPool>();
        _objctPool = GameObject.FindObjectOfType<ObjectPool>();
    }

    void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _enemyHp.SetHp(_enemyData.maxHp);
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
            _enemyMove.Move(_enemyData._speed);
        }

    }

    void InstanceExp()
    {
        //���ʂ̓G�Ȃ�
        if (!_isBoss)
        {
            //�Œ�h���b�v�B���x���ɉ��������̂𗎂Ƃ�
            if (_isOnlyDrop)
            {
                if (_enemyData._level == 1)
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.LowExp);
                }
                else if (_enemyData._level == 2)
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.MidleExp);
                }
                else
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.HightExp);
                }
            }
            else//�����_���h���b�v
            {
                if (_enemyData._level == 1)
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.LowExp);
                }//���x���P�͒Ⴂ�̂���
                else if (_enemyData._level == 2)
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        _objctPool.UseObject(transform.position, PoolObjectType.LowExp);
                    }
                    else
                    {
                        _objctPool.UseObject(transform.position, PoolObjectType.MidleExp);
                    }
                }//���x��2�͒Ⴂ�̂ƒ��̃����_��
                else
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        _objctPool.UseObject(transform.position, PoolObjectType.MidleExp);
                    }
                    else
                    {
                        _objctPool.UseObject(transform.position, PoolObjectType.HightExp);
                    }
                }//���x��3�͒��ƍ��̃����_��

                var randomHp = Random.Range(0, 100);
                if (randomHp <= 1)
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.HealItem);
                }

            }
        }
        else
        {
            _objctPool.UseObject(transform.position, PoolObjectType.Box);
        }

    }


    /// <summary>�U�������������̃_���[�W����</summary>
    /// <param name="damage">�󂯂��_���[�W</param>
    public void Damage(float damage)
    {
        //�_���[�W����������B
        var hp = _enemyHp.OnDamage(damage);
        // Debug.Log("�U�����󂯂�" + damage + "�_���[�W" + "�c��̗̑͂�" + hp);

        _textPool.UseTextObject(transform.position, TextType.Nomal, damage);

        //HP��0�ȉ�������������B
        if (hp <= 0)
        {
            _enemyHp.SetHp(_enemyData.maxHp);
            InstanceExp();
            this.gameObject.SetActive(false);
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
            _isPause = false;

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }



}
