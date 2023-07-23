using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvolutionIce : WeaponBase//,IPausebleGetBox
{
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
