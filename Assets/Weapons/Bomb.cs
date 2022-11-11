using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : WeaponBase
{
    bool _isAttack = false;
    bool _isAttackNow = false;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WEaponManger>();
    }

    void Update()
    {
        if (_level > 0)
        {
            if (_isAttack && !_isAttackNow)
            {
                StartCoroutine(Attack());
            }
            else
            {
                AttackLate();
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
        _isAttackNow = true;
        _isAttack = false;

        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(_weaponObject);
            go.transform.position = _player.transform.position;

            yield return new WaitForSeconds(0.5f);
        }
        _isAttackNow = false;
    }
}
