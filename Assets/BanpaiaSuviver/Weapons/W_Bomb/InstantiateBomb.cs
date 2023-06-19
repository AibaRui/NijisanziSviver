using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBomb : InstantiateWeaponBase//,IPausebleGetBox
{

    bool _isAttack = false;

    bool _isAttackNow = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
 
    }

    void Update()
    {
        if (!_isLevelUpPause && !_isPause&&!_isPauseGetBox)
        {
            if (_level > 0)
            {
                if (_isAttack)
                {
                    _instantiateCorutin = Attack();
                    StartCoroutine(_instantiateCorutin);
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
            var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.Bomb);
            go.transform.position = _player.transform.position;
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            go.gameObject.transform.localScale = new Vector3(_mainStatas.Eria * _eria, _mainStatas.Eria * _eria, 1);
            yield return new WaitForSeconds(0.5f);
        }
        // _isAttackNow = false;
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }

   
}
