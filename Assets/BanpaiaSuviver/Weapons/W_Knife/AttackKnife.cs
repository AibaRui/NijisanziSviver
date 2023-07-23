using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackKnife : WeaponBase//,IPausebleGetBox
{
    [Header("3回ヒットになるレベル")]
    [SerializeField] int _threeHitDestroyLevel = 4;
    [Header("無限回ヒットになるレベル")]
    [SerializeField] int _noDestroyLevel = 8;

    private int count = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemy))
            {
                count++;
                enemy.Damage(_power);

                if (_level >= _noDestroyLevel)
                {

                }
                else if (_level >= _threeHitDestroyLevel)
                {
                    if(count>=3)
                    {
                        this.gameObject.SetActive(false);
                    }

                }
                else
                {
                    this.gameObject.SetActive(false);
                }

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
