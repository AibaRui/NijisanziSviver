using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateKnife : InstantiateWeaponBase//,IPausebleGetBox
{
    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    /// <summary>ナイフを飛ばす向きのベクトル</summary>
    Vector2 dirSave = new Vector2(1, 0);



    void Update()
    {
        if (_level == 0) return;

        //一時停止中でなかったら実行
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(h, v).normalized;
            if (h != 0 || v != 0)
            {
                dirSave = dir;
            }

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
            _isInstanciateEnd = false;
            _isAttack = true;
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
            float randamX = Random.Range(0.2f, 1);
            float randamY = Random.Range(-1.3f, 1.3f);

            var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.Knife);
            go.transform.position = _player.transform.position + new Vector3(randamX, randamY, 0);
            go.transform.up = dirSave;
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            go.gameObject.GetComponent<WeaponBase>().Level = _level;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = dirSave * _speed * _mainStatas.AttackSpeed;
            _aud.Play();
            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }


    //private void OnEnable()
    //{
    //    //  _weaponManager = FindObjectOfType<WeaponData>();
    //}


    //private void OnDisable()
    //{
    //    PauseGetBox.Instance.RemoveEvent(this);
    //}

    //public void PauseResume(bool isPause)
    //{
    //    _isPauseGetBox = isPause;

    //    if(isPause)
    //    {
    //        if (_instantiateCorutin != null)
    //        {
    //            StopCoroutine(_instantiateCorutin);
    //        }
    //    }
    //    else
    //    {
    //        if (_instantiateCorutin != null)
    //        {
    //            StartCoroutine(_instantiateCorutin);
    //        }
    //    }
    //}
}
