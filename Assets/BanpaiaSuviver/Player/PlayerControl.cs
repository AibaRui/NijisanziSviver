using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("経験値獲得範囲")]
    [SerializeField] private Transform _gtEria;

    [Header("プレイヤーのスプライト")]
    [Tooltip("プレイヤーのスプライト")] [SerializeField] GameObject _playerSprite;

    [Header("プレイヤーのアニメーション")]
    [SerializeField] Animator _anim;

    [Header("===移動設定===")]
    [SerializeField] private PlayerMove _playerMove;

    [SerializeField] private PlayerHp _playrHp;

    [SerializeField] private MainStatas _mainStatas;
    [SerializeField] private PauseManager _pauseManager;
    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;

    private Rigidbody2D _rb;

    public Rigidbody2D Rb => _rb;

    public Animator PlayerAnim { get => _anim; set => _anim = value; }
    public GameObject PalyerSprite { get => _playerSprite; set => _playerSprite = value; }

    void Start()
    {
        _playerMove.Init(this);
        _rb = GetComponent<Rigidbody2D>();
        _mainStatas.SetGetEria(_gtEria.transform.localScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            _playerMove.Move();
            _playrHp.SetValue();
        }
    }

    void OnEnable()
    {
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

    void LevelUpPause()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = true;
            _rb.velocity = Vector2.zero;
            _anim.enabled = false;
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;
        _anim.enabled = true;

    }

    public void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _anim.enabled = false;
            _rb.velocity = Vector2.zero;
        }
    }

    public void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            _anim.enabled = true;
        }
    }
}
