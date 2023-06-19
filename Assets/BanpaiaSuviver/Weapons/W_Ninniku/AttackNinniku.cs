using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNinniku : MonoBehaviour
{
    [Header("�_���[�W���������Ԋu")]
    [SerializeField] float _damageTime = 1;

    private float _nowTime = 0;

    /// <summary>�U���\���ǂ���</summary>
    private bool _isAttack = false;

    protected float _power = 0;
    public float Power { set => _power = value; }

    protected int _level = 0;

    public int Level { set => _level = value; }

    //Pause�p
    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;

    public IEnumerator _instantiateCorutin;


    PauseManager _pauseManager;
    void OnEnable()
    {
        _pauseManager = FindObjectOfType<PauseManager>();
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!_isPause && !_isLevelUpPause)
            {
                if (_nowTime <= 0)
                {
                    if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemy))
                    {
                        enemy.Damage(_power);
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

        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;


    }

    public void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause && !_isPauseGetBox)
        {

        }
    }

    public void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause && !_isPauseGetBox)
        {

        }
    }
}
