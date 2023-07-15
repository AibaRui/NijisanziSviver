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

    [Header("スプライト")]
    [SerializeField] private GameObject _spriteObj;

    [Header("スプライトが右向きだったらTrue")]
    [SerializeField] private bool _spriteDirIsRight = true;

    [Header("Textを出す位置")]
    [SerializeField] private List<Transform> _textPos = new List<Transform>();

    [SerializeField] private Rigidbody2D _rb2D;

    private Text _text;

    private float _power;

    /// <summary> 移動場所 </summary>
    private Vector2 _nextMovePos = default;
    /// <summary> 移動が終わったかどうか</summary>
    private bool _isMoveEnd = false;

    /// <summary> プレイヤー </summary>
    private Transform _playerT;

    //Textの位置
    private int _setTextPos;

    /// <summary>Pauseしているかどうか</summary>
    protected bool _isPause = false;
    /// <summary>レベルアップ中かどうか</summary>
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

        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;

        SetNextMovePos();
        TalkTextSet();
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
            SetSpriteDir();
            TextMove();
        }
    }


    /// <summary>
    /// 移動処理
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
    /// 次に移動するべき場所を決める
    /// </summary>
    public void SetNextMovePos()
    {
        float offSetX = Random.Range(-_moveH, _moveH);
        float offSetY = Random.Range(-_moveH, _moveH);

        Vector2 randamV = new Vector2(offSetX, offSetY);
        _nextMovePos = (Vector2)_playerT.position + randamV;
    }

    /// <summary>
    /// Textの位置を設定
    /// </summary>
    public void TextMove()
    {
        _text.gameObject.transform.position = _textPos[_setTextPos].position;
    }

    /// <summary>
    /// Textの内容を設定する
    /// </summary>
    public void TalkTextSet()
    {
        int r = Random.Range(0, 3);

        if (r == 0)
        {
            _text.text = "すいませーん、やめてくださーい";
        }
        else if (r == 1)
        {
            _text.text = "アババババ";
        }
        else
        {
            _text.text = "お前ら俺を殺す気かぁ！";
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
