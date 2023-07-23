using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionBaran : WeaponBase
{
    [Header("スプライトのオブジェクト")]
    [SerializeField] private GameObject spriteObj;

    [Header("回転速度")]
    [SerializeField] private float _rotateSpeed = 5;

    private void FixedUpdate()
    {
        if (!_isPause && !_isLevelUpPause && !_isPauseGetBox)
        {
            Quaternion r = spriteObj.transform.rotation;
            r.z += _rotateSpeed;
            spriteObj.transform.rotation = r;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemy))
            {
                enemy.Damage(_power);
            }
        }
    }
}
