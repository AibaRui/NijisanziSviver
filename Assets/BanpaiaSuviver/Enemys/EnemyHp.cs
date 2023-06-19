using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHp : MonoBehaviour
{
    float _nowHp;

    /// <summary>���g��Hp���Z�b�g����֐�</summary>
    /// EnemyControl����Ă�
    /// <param name="hp"></param>
    public void SetHp(float hp)
    {
        _nowHp = hp;
    }

    public float OnDamage(float damage)
    {
        _nowHp -= damage;
        return _nowHp;
    }

}
