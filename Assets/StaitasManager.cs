using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaitasManager : MonoBehaviour
{
    /// <summary>数が増えると発射物の数が増える</summary>
    [SerializeField]  float _weaponAdd = 0;

    /// <summary>倍率が上がると火力が上がる</summary>
    [SerializeField] float _attackPower = 1;

    /// <summary>倍率が下がるとレートが早くなる</summary>
    [SerializeField] float _attackLate = 1;


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
