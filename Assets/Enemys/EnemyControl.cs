using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : EnemyDestroy
{
    [Header("ダメージ判定をする間隔")]
    [SerializeField] float _damageTime = 1;

    [Header("攻撃力")]
    [SerializeField] int _attackPower = 1;

    [Header("ボスかどうか")]
    [SerializeField] bool _isBoss = false;


    private float _nowTime = 0;

    bool _isAttack = true;

    bool _isAttacked = false;

    [SerializeField] EnemyMove _enemyMove;

    [SerializeField] EnemyData _enemyData;

    [SerializeField] EnemyHp _enemyHp;

    [Header("経験値を特定で落とすか、ランダムか")]
    [SerializeField] bool _isOnlyDrop = false;

    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
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
        //普通の敵なら
        if (!_isBoss)
        {
            //固定ドロップ。レベルに応じたものを落とす
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
            else//ランダムドロップ
            {
                if (_enemyData._level == 1)
                {
                    _objctPool.UseObject(transform.position, PoolObjectType.LowExp);
                }//レベル１は低いのだけ
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
                }//レベル2は低いのと中のランダム
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
                }//レベル3は中と高のランダム

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


    /// <summary>攻撃を喰らった時のダメージ処理</summary>
    /// <param name="damage">受けたダメージ</param>
    public void Damage(float damage)
    {
        //ダメージ処理をする。
        var hp = _enemyHp.OnDamage(damage);
        // Debug.Log("攻撃を受けた" + damage + "ダメージ" + "残りの体力は" + hp);

        _textPool.UseTextObject(transform.position, TextType.Nomal, damage);

        //HPが0以下だったら消す。
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



    ///////Parse処理/////
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
