using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
	[Header("名前")]
	public string enemyName;
	[Header("1～３。最高3レベル。経験値を落とすかに作用する")]
	public int _level;
	[Header("体力")]
	public int maxHp;
	[Header("攻撃力")]
	public int atk;
	[Header("移動速度")]
	public float _speed;
	[Header("防御力")]
	public int def;
	public int gold;

	[SerializeField] public GameObject _lowExp;
	[SerializeField] public GameObject _midleExp;
	[SerializeField] public GameObject _hightExp;
}
