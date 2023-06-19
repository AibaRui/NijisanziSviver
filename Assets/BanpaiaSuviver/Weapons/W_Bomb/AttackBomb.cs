using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBomb : WeaponBase//,IPausebleGetBox
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy"&&collision!=null)
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
