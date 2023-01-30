using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create ExperiencePointData")]
public class ExperiencePointData : ScriptableObject
{
    /// <summary>�o���l�̗�</summary>
    [SerializeField] int _expPoint;

    public int ExpPoint { get => _expPoint; }

    private GameObject _player;

    public GameObject Player { get => _player; set => _player = value; }

    LevelUpController _levelUpController;

    public LevelUpController LevelUpController { get => _levelUpController; set => _levelUpController = value; }

}
