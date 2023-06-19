using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIceMagick : WeaponBase//,IPausebleGetBox
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
    //private void OnEnable()
    //{
    //    PauseGetBox.Instance.SetEvent(this);
    //}

    //private void OnDisable()
    //{
    //    PauseGetBox.Instance.RemoveEvent(this);
    //}

    //public void PauseResume(bool isPause)
    //{
    //    PauseResumeGetBox(isPause);
    //}
}
