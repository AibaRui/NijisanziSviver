using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateThunder : InstantiateWeaponBase
{
    /// <summary>雷を落とす範囲の半径X</summary>
    [SerializeField] float _sizeX;
    /// <summary>雷を落とす範囲の半径Y</summary>
    [SerializeField] float _sizeY;

    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    IEnumerator _instanciateCorutineThunder;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WeaponData>();
    }

    void Update()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            if (_level > 0)
            {

                if (_isAttack)
                {
                    _instanciateCorutineThunder = Attack();
                    StartCoroutine(_instanciateCorutineThunder);
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
            float randamX = Random.Range(-_sizeX, _sizeX);
            float randamY = Random.Range(-_sizeY, _sizeY);

            var go = Instantiate(_weaponObject);
            go.transform.position = _player.transform.position + new Vector3(randamX, randamY, 0);
            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instanciateCorutineThunder = null;
    }

    public override void LevelUpPause()
    {
        _isLevelUpPause = true;
        if (_instanciateCorutineThunder != null)
        {
            StopCoroutine(_instanciateCorutineThunder);
        }
    }

    public override void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_instanciateCorutineThunder != null)
        {
            StartCoroutine(_instanciateCorutineThunder);
        }
    }

    public override void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineThunder != null)
            {
                StopCoroutine(_instanciateCorutineThunder);
            }
        }
    }

    public override void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause)
        {
            if (_instanciateCorutineThunder != null)
            {
                StartCoroutine(_instanciateCorutineThunder);
            }
        }
    }

}
