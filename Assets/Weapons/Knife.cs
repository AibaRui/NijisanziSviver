using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : WeaponBase
{

    bool _isAttack = false;

    Vector2 dirSave;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _weaponManager = FindObjectOfType<WEaponManger>();
        dirSave = new Vector2(transform.localScale.x, 0);
    }

    void Update()
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
            var setCoolTime = _coolTime*_mainStatas.CoolTime;
            _countTime = setCoolTime;
            _isAttack = true;
        }
    }

    IEnumerator Attack()
    {
        _isAttack = false;

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
            yield return new WaitForSeconds(0.1f);
        }
    }
}
