using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "MyScriptable/Create ExperiencePointData")]
public class ExperiencePointData : ScriptableObject
{
    /// <summary>経験値の量</summary>
    public float _expPoint;

    /// <summary>経験値のイラスト</summary>
    public SpriteRenderer _sprite;
}
