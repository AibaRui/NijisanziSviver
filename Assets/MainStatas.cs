using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStatas : MonoBehaviour
{

    [SerializeField]  int _hp;
    public int HP { get => _hp; set => _hp = value; }


    [SerializeField] int _dex;
    public int Dex { get => _dex; set => _dex = value; }

    [SerializeField]  float _moveSpeed;
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }

    [SerializeField]  float _power;

    public float Power { get => _power; set => _power = value; }

    /// <summary>クール時間</summary>
    [SerializeField]  float _coolTime;

    public float CoolTime { get => _coolTime; set => _coolTime = value; }

    /// <summary>範囲</summary>
    [SerializeField]  float _eria;

    public float Eria { get => _eria; set => _eria = value; }

    /// <summary>スピード</summary>
    [SerializeField]  float _speed;

    public float Speed { get => _speed; set => _speed = value; }

    /// <summary>段数</summary>
    [SerializeField]  int _number;

    public int Number { get => _number; set => _number = value; }

    [SerializeField] float _expUpper = 1;
    public float ExpUpper { get => _expUpper; set => _expUpper = value; }

    void Start()
    {

    }


    void Update()
    {

    }
}
