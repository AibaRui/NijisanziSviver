using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaitasManager : MonoBehaviour
{
    [SerializeField] public static int _hp;

    [SerializeField] public static int _dex;

    [SerializeField] public static float _moveSpeed;

    [SerializeField] public static float _power;

    /// <summary>�N�[������</summary>
    [SerializeField] public static float _coolTime;

    /// <summary>�͈�</summary>
    [SerializeField] public static float _eria;

    /// <summary>�X�s�[�h</summary>
    [SerializeField] public static float _speed;

    /// <summary>�i��</summary>
    [SerializeField] public static int _number;

    [SerializeField] public static float _expUpperOrigine = 1;

    /// <summary>����������Ɣ��˕��̐���������</summary>
    [SerializeField] public float _weaponAdd = 0;

    /// <summary>�{�����オ��ƉΗ͂��オ��</summary>
    [SerializeField] public float _attackPower = 1;

    /// <summary>�{����������ƃ��[�g�������Ȃ�</summary>
    [SerializeField] public float _attackLate = 1;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>���˕��𑝂₷</summary>
    public void AddWepon(int add)
    {
        _weaponAdd+=add;
    }

    public void AttackPowerUp(float s)
    {
        _attackPower += _attackPower * s;
    }


    public void AttackLateUp(float s)
    {
        _attackLate -= _attackLate * s;
    }

}
