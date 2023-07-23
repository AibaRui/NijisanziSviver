using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>実際に動いている武器たちの基底クラス</summary>
public class WeaponBase : MonoBehaviour
{

    [Header("出してから消すまでの時間")]
    [Tooltip("出してから消すまでの時間")] [SerializeField] private float _lifeTime = 3;

    [Header("時間経過によって非表示にするかどうか")]
    [Tooltip("時間経過によって消すかどうか")] [SerializeField] private bool _isActiveFalse = true;


    public float LifeTime { get => _lifeTime; set => _lifeTime = value; }

    /// <summary>カウント用の変数</summary>
    float _countTime = 0;

    protected float _power = 0;
    public float Power { set => _power = value; }

    protected int _level = 0;

    public int Level { set => _level = value; }

    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;


    float _angularVelocityOfLevelUpPause;
    Vector2 _velocityOfLevelUpPause;

    float _angularVelocity;
    Vector2 _velocity;



    protected Rigidbody2D _rb;
    AudioSource _aud;


    [SerializeField] private List<Animator> _anims = new List<Animator>();

    PauseManager _pauseManager = default;
    private void Awake()
    {

    }

    void Update()
    {

        CountDestroyTime();
    }

    protected void CountDestroyTime()
    {
        if (!_isPause && !_isLevelUpPause)
        {
            if (_isActiveFalse)
            {
                _countTime += Time.deltaTime;

                if (_countTime >= _lifeTime)
                {
                    _countTime = 0;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemy))
    //        {

    //            enemy.Damage(_power);
    //        }
    //    }
    //}


    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _rb = GetComponent<Rigidbody2D>();
        _aud = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnLevelUp -= LevelUpPauseResume;
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
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = true;
            if (_rb)
            {
                // 速度・回転を保存し、Rigidbody を停止する
                _angularVelocityOfLevelUpPause = _rb.angularVelocity;
                _velocityOfLevelUpPause = _rb.velocity;
                _rb.Sleep();
                _rb.isKinematic = true;
            }

            foreach (var a in _anims)
            {
                a.enabled = false;
            }

        }
    }

    public void LevelUpResume()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = false;

            if (_rb)
            {
                // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
                _rb.isKinematic = false;
                _rb.WakeUp();
                _rb.angularVelocity = _angularVelocityOfLevelUpPause;
                _rb.velocity = _velocityOfLevelUpPause;
            }

            foreach (var a in _anims)
            {
                a.enabled = true;
            }
        }
    }

    public void Pause()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _isPause = true;
            if (_rb)
            {
                // 速度・回転を保存し、Rigidbody を停止する
                _angularVelocity = _rb.angularVelocity;
                _velocity = _rb.velocity;
                _rb.Sleep();
                _rb.isKinematic = true;
            }

            foreach (var a in _anims)
            {
                a.enabled = false;
            }

        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _isPause = false;
            if (_rb)
            {
                // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
                _rb.WakeUp();
                _rb.angularVelocity = _angularVelocity;
                _rb.velocity = _velocity;
                _rb.isKinematic = false;
            }

            foreach (var a in _anims)
            {
                a.enabled = true;
            }
        }
    }
    protected void PauseResumeGetBox(bool flg)
    {
        if (flg)
        {
            _isPauseGetBox = flg;
            if (_rb)
            {
                // 速度・回転を保存し、Rigidbody を停止する
                _angularVelocity = _rb.angularVelocity;
                _velocity = _rb.velocity;
                _rb.Sleep();
                _rb.isKinematic = true;

                foreach (var a in _anims)
                {
                    a.enabled = false;
                }
            }
        }
        else
        {
            _isPauseGetBox = flg;
            if (_rb)
            {
                // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
                _rb.WakeUp();
                _rb.angularVelocity = _angularVelocity;
                _rb.velocity = _velocity;
                _rb.isKinematic = false;
            }


            foreach (var a in _anims)
            {
                a.enabled = true;
            }
        }
    }

}
