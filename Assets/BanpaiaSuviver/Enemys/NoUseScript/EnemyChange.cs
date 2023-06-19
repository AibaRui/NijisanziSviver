using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChange : MonoBehaviour
{
    [SerializeField] EnemySpawn _enemySpawn;

    [Header("oŒ»‚³‚¹‚é“G")]
    [Tooltip("oŒ»‚³‚¹‚é“G")] [SerializeField] List<GameObject> _enemys = new List<GameObject>();


    /// <summary>“G‚ğ“ü‚ê‘Ö‚¦‚éŠÖ”</summary>
    public void ChaneEnemy()
    {
        _enemySpawn.prefab.Clear();

        for (int i = 0; i < _enemys.Count; i++)
        {
            _enemySpawn.prefab.Add(_enemys[0]);
        }

    }


}
