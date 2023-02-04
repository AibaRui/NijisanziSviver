using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackNinniku : MonoBehaviour
{
    [Header("ダメージ判定をする間隔")]
    [SerializeField] float _damageTime = 1;

    private float _nowTime = 0;

    /// <summary>攻撃可能かどうか</summary>
    private bool _isAttack = false;

    protected float _power = 0;
    public float Power { set => _power = value; }

    protected int _level = 0;

    public int Level { set => _level = value; }

    //Pause用
    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;

    public IEnumerator _instantiateCorutin;


    PauseManager _pauseManager;
    void OnEnable()
    {
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
