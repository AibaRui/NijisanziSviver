using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create EnemyData")]
public class EnemyData : ScriptableObject
{
	[Header("���O")]
	public string enemyName;
	[Header("1�`�R�B�ō�3���x���B�o���l�𗎂Ƃ����ɍ�p����")]
	public int _level;
	[Header("�̗�")]
	public int maxHp;
	[Header("�U����")]
	public int atk;
	[Header("�ړ����x")]
	public float _speed;
	[Header("�h���")]
	public int def;
	public int gold;

	[SerializeField] public GameObject _lowExp;
	[SerializeField] public GameObject _midleExp;
	[SerializeField] public GameObject _hightExp;
}
