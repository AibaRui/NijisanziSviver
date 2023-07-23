using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>���ۂɓ����Ă��镐�킽���̊��N���X</summary>
public class WeaponBase : MonoBehaviour
{

    [Header("�o���Ă�������܂ł̎���")]
    [Tooltip("�o���Ă�������܂ł̎���")] [SerializeField] private float _lifeTime = 3;

    [Header("���Ԍo�߂ɂ���Ĕ�\���ɂ��邩�ǂ���")]
    [Tooltip("���Ԍo�߂ɂ���ď������ǂ���")] [SerializeField] private bool _isActiveFalse = true;


    public float LifeTime { get => _lifeTime; set => _lifeTime = value; }

    /// <summary>�J�E���g�p�̕ϐ�</summary>
    float _countTime = 0;

    protected float _power = 0;
    public float Power { set => _power = value; }

    protected int _level = 0;

    public int Level { set => _level = value; }

    /// <summary>Pause���Ă��邩�ǂ���</summary>
    protected bool _isPause = false;
    /// <summary>���x���A�b�v�����ǂ���</summary>
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


    ///////Parse����/////

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        _rb = GetComponent<Rigidbody2D>();
        _aud = GetComponent<AudioSource>();
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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
                // ���x�E��]��ۑ����ARigidbody ���~����
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
                // Rigidbody �̊������ĊJ���A�ۑ����Ă��������x�E��]��߂�
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
                // ���x�E��]��ۑ����ARigidbody ���~����
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
                // Rigidbody �̊������ĊJ���A�ۑ����Ă��������x�E��]��߂�
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
                // ���x�E��]��ۑ����ARigidbody ���~����
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
                // Rigidbody �̊������ĊJ���A�ۑ����Ă��������x�E��]��߂�
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
