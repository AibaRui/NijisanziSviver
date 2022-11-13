using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiyokoBeam : MonoBehaviour
{
    [SerializeField] float _lifeTime = 5;
    float _countTime = 0;


    // 円運動周期(+と-で回転方向が変わる)
    private float _period = 2;

    MainStatas _mainStatas;

    InstantiateBoock _boockWeapon;
    GameObject _player;
    Vector3 _center;
    protected bool _isPause = false;
    protected bool _isLevelUpPause = false;

    float _angularVelocity;
    Vector2 _velocity;

    InstantiateHiyoko _hiyoko;

    Rigidbody2D _rb;
    AudioSource _aud;
    Animator _anim;

    PauseManager _pauseManager = default;

    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        _hiyoko = GameObject.FindObjectOfType<InstantiateHiyoko>();
        _period = _hiyoko._period;

        _player = GameObject.FindGameObjectWithTag("Player");
        _center = _player.transform.position;
    }


    void Update()
    {
        if (!_isPause && !_isLevelUpPause)
        {
            _countTime += Time.deltaTime;

            if (_countTime >= _lifeTime)
            {

                Destroy(this.gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (!_isLevelUpPause && !_isPause)
        {    
            //transform.right = transform.parent.position;
            ////     _player =  GameObject.FindGameObjectWithTag("Player");
            //_center = transform.parent.position;

            //var tr = transform;
            //// 回転のクォータニオン作成
            //var angleAxis = Quaternion.AngleAxis(360 / _period * Time.deltaTime, transform.parent.forward);

            //// 円運動の位置計算
            //var pos = tr.position;

            //pos -= _center;
            //pos = angleAxis * pos.normalized * 5f;
            //pos += _center;

            //tr.position = pos;
            //tr.rotation = tr.rotation * angleAxis;
        }
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
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;
    }

    public void Pause()
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

    public void Resume()
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
