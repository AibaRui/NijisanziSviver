using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChange : MonoBehaviour
{
    [SerializeField] EnemySpawn _enemySpawn;

    [Header("出現させる敵")]
    [Tooltip("出現させる敵")] [SerializeField] List<GameObject> _enemys = new List<GameObject>();


    /// <summary>敵を入れ替える関数</summary>
    public void ChaneEnemy()
    {
        _enemySpawn.prefab.Clear();

        for (int i = 0; i < _enemys.Count; i++)
        {
            _enemySpawn.prefab.Add(_enemys[0]);
        }

    }


}
