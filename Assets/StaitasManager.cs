using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaitasManager : MonoBehaviour
{
    [SerializeField] public static int _hp;

    [SerializeField] public static int _dex;

    [SerializeField] public static float _moveSpeed;

    [SerializeField] public static float _power;

    /// <summary>クール時間</summary>
    [SerializeField] public static float _coolTime;

    /// <summary>範囲</summary>
    [SerializeField] public static float _eria;

    /// <summary>スピード</summary>
    [SerializeField] public static float _speed;

    /// <summary>段数</summary>
    [SerializeField] public static int _number;

    [SerializeField] public static float _expUpperOrigine = 1;

    /// <summary>数が増えると発射物の数が増える</summary>
    [SerializeField] public float _weaponAdd = 0;

    /// <summary>倍率が上がると火力が上がる</summary>
    [SerializeField] public float _attackPower = 1;

    /// <summary>倍率が下がるとレートが早くなる</summary>
    [SerializeField] public float _attackLate = 1;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>発射物を増やす</summary>
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
