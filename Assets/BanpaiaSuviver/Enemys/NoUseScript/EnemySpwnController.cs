using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpwnController : MonoBehaviour
{
    [SerializeField] UnityEvent _firstEnemy;


    [SerializeField] List<UnityEvent> _enemyChangeEvents = new List<UnityEvent>();



    [Header("敵を変えるタイミングの秒数")]
    [Tooltip("敵を変えるタイミングの秒数")] [SerializeField] List<float> _enemyChangeTime = new List<float>();

    int _changeEnemyCount = 0;


    [SerializeField] float _gameTime = 300;

    float _countTime = 0;


    void Start()
    {
        _firstEnemy.Invoke();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        EnemyChangeOfTime();
    }


    /// <summary>経過時間によって、敵の種類を変える関数</summary>
    ///経過時間の設定したリストの要素と、呼び出すUnityEventsの要素を合わせている。
    void EnemyChangeOfTime()
    {
        if (_changeEnemyCount < _enemyChangeEvents.Count)
        {
            _countTime += Time.deltaTime;

            //経過時間になったら、イベントを実行。次の経過時間のリストを更新
            if (_countTime >= _enemyChangeTime[_changeEnemyCount])
            {
                _enemyChangeEvents[_changeEnemyCount].Invoke();
                _changeEnemyCount++;
            }
        }
    }
}
