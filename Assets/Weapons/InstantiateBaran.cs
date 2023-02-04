using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBaran : InstantiateWeaponBase
{
    [SerializeField] float _addPower;

    [SerializeField] float _randamX;


    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    /// <summary>ナイフを飛ばす向きのベクトル</summary>
    Vector2 dirSave = new Vector2(1, 0);

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WeaponData>();
    }

    void Update()
    {
        if (_level == 0) return;

        //一時停止中でなかったら実行
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
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

    /// <summary>クールタイムの処理</summary>
    void AttackLate()
    {
        _countTime -= Time.deltaTime;

        if (_countTime <= 0)
        {
            var setCoolTime = _coolTime * _mainStatas.CoolTime;
            _countTime = setCoolTime;
            _isAttack = true;
            _isInstanciateEnd = false;
        }
    }

    /// <summary>攻撃の処理</summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        _isAttack = false;

        //武器を出す回数を決める。(武器のステータスと、メインステータスの合計値)
        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            SpownWeapon();
            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }


    void SpownWeapon()
    {
        //生成と初期設定
        var go = _objectPool.UseObject(transform.position, PoolObjectType.Baran);
        go.transform.position = _player.transform.position;

        go.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 1);

        go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
        go.gameObject.GetComponent<WeaponBase>().Level = _level;

        float randamX = Random.Range(-_randamX, _randamX);
        Vector2 addDir = new Vector2(randamX, 1);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        rb.AddForce(addDir.normalized * _addPower,ForceMode2D.Impulse);
    }

}
