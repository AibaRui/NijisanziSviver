using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBoock : InstantiateWeaponBase
{
    List<GameObject> go = new List<GameObject>();


    // ‰~‰^“®ŽüŠú(+‚Æ-‚Å‰ñ“]•ûŒü‚ª•Ï‚í‚é)
    public float _period;


    bool _isAttackNow;
    bool _isAttack;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (!_isLevelUpPause && !_isLevelUpPause)
        {
            _period = _speed;
            if (_level > 0)
            {
                if (_isAttack && _isAttackNow == false)
                {
                    StartCoroutine(Attack());
                }
                else if (!_isAttack)
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
        _isAttackNow = true;

        //”­ŽË•¨‚Ì”
        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            var g = Instantiate(_weaponObject);
            g.transform.SetParent(_player.transform);
            Vector2 pos = new Vector2(_player.transform.position.x + 1, _player.transform.position.y);
            g.transform.position = pos;
            go.Add(g);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(5);
        _isAttack = false;
        _isAttackNow = false;
    }

    public override void LevelUpPause()
    {
        _isLevelUpPause = true;
    }

    public override void LevelUpResume()
    {
        _isLevelUpPause = false;

    }

    public override void Pause()
    {
        _isPause = true;
    }

    public override void Resume()
    {
        _isPause = false;
    }
}
