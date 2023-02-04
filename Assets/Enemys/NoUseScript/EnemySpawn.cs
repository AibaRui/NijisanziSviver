using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    EnemyPool poolManager;

    [SerializeField]
    public List<GameObject> prefab = new List<GameObject>();


    [SerializeField]
    int spawnCount = 1;

    [SerializeField]
    float spawnInterval = 0.1f;

    [SerializeField]
    Vector3 minSpawnPosition = Vector3.zero;

    [SerializeField]
    Vector3 maxSpawnPosition = Vector3.zero;

    [SerializeField]
    float destroyWaitTime = 3;

    WaitForSeconds spawnIntervalWait;

    GameObject _player;

    [SerializeField] float _gameTime;
    float _time = 0;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        Spawn(prefab[0]);
        spawnIntervalWait = new WaitForSeconds(spawnInterval);
        StartCoroutine(nameof(SpawnTimer));
    }


    void CountTime()
    {
        _time += Time.deltaTime;
    }

    IEnumerator SpawnTimer()
    {
        int i;
        while (true)
        {
            for (i = 0; i < spawnCount; i++)
            {
                var r = Random.Range(0, prefab.Count);
                Spawn(prefab[r]);
            }
            yield return spawnIntervalWait;
        }
    }


    void Spawn(GameObject prefab)
    {
        EnemyDestroy destroyer;

        //Y軸は上下の壁前の座標を制限、X軸はプレイヤーの位置からランダムに
        Vector3 pos = new Vector3(Random.Range(minSpawnPosition.x, maxSpawnPosition.x) + _player.transform.position.x,
                                     Random.Range(minSpawnPosition.y, maxSpawnPosition.y), 0);

        destroyer = poolManager.GetGameObject(prefab, pos, Quaternion.identity).GetComponent<EnemyDestroy>();
        destroyer.PoolManager = poolManager;
    }
}
