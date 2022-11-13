using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIceMagic : InstantiateWeaponBase
{
    bool _isAttack = false;
    bool _isAttackNow = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    IEnumerator _instanciateCorutine;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WeaponData>();
    }

    void Update()
    {
        if (!_isLevelUpPause && !_isLevelUpPause)
        {
            if (_level > 0)
            {
                if (_isAttack)
                {
                    _instanciateCorutine = Attack();
                    StartCoroutine(Attack());
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
        //_isAttackNow = true;
        _isAttack = false;
        if (_number > 2) _number = 2;

        for (int i = 0; i < _number; i++)
        {
            var go = Instantiate(_weaponObject);
            if(i==0)
            {
                Vector3 pos = new Vector3(-2, 0,0);
                go.transform.localScale = new Vector3(-1, 1, 1);
                go.transform.position = _player.transform.position+pos;
            }
            else
            {
                Vector3 pos = new Vector3(2, 0, 0);
                go.transform.position = _player.transform.position + pos;
            }
            yield return new WaitForSeconds(0.1f);
        }
        // _isAttackNow = false;
        _isInstanciateEnd = true;
        _instanciateCorutine = null;
    }

    public override void LevelUpPause()
    {
        _isLevelUpPause = true;
        if (_instanciateCorutine != null)
        {
            StopCoroutine(_instanciateCorutine);
        }
    }

    public override void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instanciateCorutine != null)
        {
            StartCoroutine(_instanciateCorutine);
        }
    }

    public override void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause)
        {
            if (_instanciateCorutine != null)
            {
                StopCoroutine(_instanciateCorutine);
            }
        }
    }

    public override void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause)
        {
            if (_instanciateCorutine != null)
            {
                StartCoroutine(_instanciateCorutine);
            }
        }
    }

}
