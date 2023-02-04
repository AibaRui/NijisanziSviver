using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nazonomito : MonoBehaviour
{
    [Header("Spriteの向き。右向きだったらTrue。左向きだったらFalse")]
    [SerializeField] private bool _spriteDir;

    [Header("移動速度")]
    [SerializeField] float _moveSpeed = 3;

    [Header("Playerとの保つ距離")]
    [SerializeField] private float _dir = 7;

    [Header("攻撃速度")]
    [SerializeField] float _attackTime = 3;

    [Header("弾")]
    [SerializeField] GameObject _bullet;

    [Header("弾の速度")]
    [SerializeField] float _bulletSpeed = 3;

    [Header("マズル")]
    [SerializeField] Transform _muzluPos = default;


    [Header("ダメージ判定をする間隔")]
    [SerializeField] float _damageTime = 1;

    [Header("攻撃力")]
    [SerializeField] int _attackPower = 1;

    private float _nowTime = 0;

    IEnumerator _attackCorutin;

    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
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

    ///////Parse処理/////
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
