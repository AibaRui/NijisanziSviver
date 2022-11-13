using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : InstantiateWeaponBase
{
    bool _isAttack = false;
    bool _isAttackNow = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    IEnumerator _instanciateCorutineBomb;

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
                    _instanciateCorutineBomb = Attack();
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

        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(_weaponObject);
            go.transform.position = _player.transform.position;

            yield return new WaitForSeconds(0.5f);
        }
        // _isAttackNow = false;
        _isInstanciateEnd = true;
        _instanciateCorutineBomb = null;
    }

    public override void LevelUpPause()
    {
        _isLevelUpPause = true;
        if (_instanciateCorutineBomb != null)
        {
            StopCoroutine(_instanciateCorutineBomb);
        }
    }

    public override void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instanciateCorutineBomb != null)
        {
            StartCoroutine(_instanciateCorutineBomb);
        }
    }

    public override void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineBomb != null)
            {
                StopCoroutine(_instanciateCorutineBomb);
            }
        }
    }

    public override void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineBomb != null)
            {
                StartCoroutine(_instanciateCorutineBomb);
            }
        }
    }

}
