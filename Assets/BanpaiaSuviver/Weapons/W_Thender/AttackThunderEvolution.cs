using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackThunderEvolution : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float _moveSpeed = 5;

    [Header("移動の幅")]
    [SerializeField] private float _moveH = 5;

    [Header("移動完了とみなす距離")]
    [SerializeField] private float _endDis = 5;

    [Header("武器のアニメーション")]
    [SerializeField] private Animator _anim;


    private Text _text;

    private float _power;

    /// <summary> 移動場所 </summary>
    private Vector2 _nextMovePos = default;
    /// <summary> 移動が終わったかどうか</summary>
    private bool _isMoveEnd = false;


    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
    protected bool _isLevelUpPause = false;

    protected bool _isPauseGetBox = false;

    private MainStatas _mainStatas;

    private ObjectPool _objectPool;

    private PauseManager _pauseManager;

    /// <summary> プレイヤー </summary>
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

        // 呼んで欲しいメソッドを登録する。
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
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
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
    /// 移動処理
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
                _text.text = "すいませーん、やめてくださーい";
            }
            else if(r==1)
            {
                _text.text = "アババババ";
            }
            else
            {
                _text.text = "やめてくださーい";
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
