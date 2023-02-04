using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class WeaponSqritable : ScriptableObject
{
    public string weaponName;
    public int maxHp;
    public int atk;

    public int Attack { set => atk = value; get => atk; }

    public float _speed;
    public int def;
    public int gold;





}