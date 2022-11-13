using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>実際に動いている武器たちの基底クラス</summary>
public class WeaponBase : MonoBehaviour
{

    [Header("出してから消すまでの時間")]
    [Tooltip("出してから消すまでの時間")] [SerializeField] float _lifeTime = 3;

    /// <summary>カウント用の変数</summary>
    float _countTime = 0;

    [SerializeField] bool _isDestroy;

    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
    protected bool _isLevelUpPause = false;


    float _angularVelocityOfLevelUpPause;
    Vector2 _velocityOfLevelUpPause;

    float _angularVelocity;
    Vector2 _velocity;



    Rigidbody2D _rb;
    AudioSource _aud;
    Animator _anim;

    PauseManager _pauseManager = default;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }

    void Update()
    {
        CountDestroyTime();
    }

    void CountDestroyTime()
    {
        if (!_isPause && !_isLevelUpPause)
        {
            _countTime += Time.deltaTime;

            if (_countTime >= _lifeTime)
            {
                if (_isDestroy)
                {
                    Destroy(gameObject);
                }

            }
        }
    }


    ///////Parse処理/////

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _anim = gameObject.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _aud = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
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
        if (_rb)
        {
            // 速度・回転を保存し、Rigidbody を停止する
            _angularVelocityOfLevelUpPause = _rb.angularVelocity;
            _velocityOfLevelUpPause = _rb.velocity;
            _rb.Sleep();
            _rb.isKinematic = true;
        }

        if (_anim)
        {
            _anim.enabled = false;
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_rb)
        {
            // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
            _rb.WakeUp();
            _rb.angularVelocity = _angularVelocityOfLevelUpPause;
            _rb.velocity = _velocityOfLevelUpPause;
            _rb.isKinematic = false;
        }

        if (_anim)
        {
            _anim.enabled = true;
        }
    }

    public void Pause()
    {
        if (!_isLevelUpPause)
        {
            _isPause = true;
            // 速度・回転を保存し、Rigidbody を停止する
            _angularVelocity = _rb.angularVelocity;
            _velocity = _rb.velocity;
            _rb.Sleep();
            _rb.isKinematic = true;

            if (_anim)
            {
                _anim.enabled = false;
            }
        }
    }

    public void Resume()
    {
        if (!_isLevelUpPause)
        {
            _isPause = false;
            // Rigidbody の活動を再開し、保存しておいた速度・回転を戻す
            _rb.WakeUp();
            _rb.angularVelocity = _angularVelocity;
            _rb.velocity = _velocity;
            _rb.isKinematic = false;

            if (_anim)
            {
                _anim.enabled = true;
            }
        }
    }

}
