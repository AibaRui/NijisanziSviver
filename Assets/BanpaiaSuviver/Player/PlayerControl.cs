using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("�o���l�l���͈�")]
    [SerializeField] private Transform _gtEria;

    [Header("�v���C���[�̃X�v���C�g")]
    [Tooltip("�v���C���[�̃X�v���C�g")] [SerializeField] GameObject _playerSprite;

    [Header("�v���C���[�̃A�j���[�V����")]
    [SerializeField] Animator _anim;

    [Header("===�ړ��ݒ�===")]
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
