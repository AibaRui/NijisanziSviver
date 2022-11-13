using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateKnife : InstantiateWeaponBase
{
    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    /// <summary>ナイフを飛ばす向きのベクトル</summary>
    Vector2 dirSave　=new Vector2(1, 0);

    IEnumerator _instanciateCorutineKnife;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WeaponData>();
    }

    void Update()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(h, v).normalized;
            if (h != 0 || v != 0)
            {
                dirSave = dir;
            }

            
            if (_level > 0)
            {

                if (_isAttack)
                {
                    _instanciateCorutineKnife = Attack();
                    StartCoroutine(_instanciateCorutineKnife);
                }
                else if (_isInstanciateEnd)
                {
                    AttackLate();
                }
            }
        }
    }

    void AttackLate()
    {
        _countTime -= Time.deltaTime;

        if (_countTime <= 0)
        {
            var setCoolTime = _coolTime * _mainStatas.CoolTime;
            _countTime = setCoolTime;
            _isAttack = true;
        }
    }

    IEnumerator Attack()
    {
        _isAttack = false;
        _isInstanciateEnd = false;

        //武器を出す回数を決める。(武器のステータスと、メインステータスの合計値)
        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            float randamX = Random.Range(0.2f, 1);
            float randamY = Random.Range(-1.3f, 1.3f);

            var go = Instantiate(_weaponObject);
            go.transform.position = _player.transform.position + new Vector3(randamX, randamY, 0);
            go.transform.up = dirSave;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = dirSave * _speed * _mainStatas.Speed;

            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instanciateCorutineKnife = null;
    }

    public override void LevelUpPause()
    {
        _isLevelUpPause = true;
        if (_instanciateCorutineKnife != null)
        {
            StopCoroutine(_instanciateCorutineKnife);
        }
    }

    public override void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instanciateCorutineKnife != null)
        {
            StartCoroutine(_instanciateCorutineKnife);
        }
    }

    public override void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineKnife != null)
            {
                StopCoroutine(_instanciateCorutineKnife);
            }
        }
    }

    public override void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineKnife != null)
            {
                StartCoroutine(_instanciateCorutineKnife);
            }
        }
    }

}
