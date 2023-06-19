using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EnemySpownInformation : IComparable<EnemySpownInformation>
{
    [Header("出す敵達")]
    [SerializeField]
    public List<SpownEnemysData> _enemysData = new List<SpownEnemysData>();

    [Header("設定する時間")]
    [SerializeField] private int _setTimes;

    [Header("敵を生成するインターバル")]
    [SerializeField] private float _spawnInterval = 0.1f;

    public int SetTime { get => _setTimes; }

    public float SpawnInterval { get => _spawnInterval; }

    private TestSpown _spownser = null;

    /// <summary>StateMacineをセットする関数</summary>
    /// <param name="stateMachine"></param>
    public void Init(TestSpown spownser)
    {
        _spownser = spownser;
    }

    /// <summary>
    /// ID 順としてソートするように定義する
    /// </summary>
    /// <param name="other">ソート順を比較する相手</param>
    /// <returns>自分の方が小さい時は -1, 自分の方が大きい時は 1, 同値の時は 0</returns>
    public int CompareTo(EnemySpownInformation other)
    {
        if (this._setTimes < other._setTimes)
        {
            return -1;  // 自分の ID が小さい時は「自分の方が前」とする
        }
        else if (this._setTimes > other._setTimes)
        {
            return 1;  // 自分の ID が大きい時は「自分の方が後ろ」とする
        }
        else
        {
            return 0;   // ID が同じなら「同じ」とする
        }
    }
}

[Serializable]
public class SpownEnemysData
{
    [SerializeField]
    private string Name;
    [Header("出す敵")]
    [SerializeField] private PoolObjectType _enemy;

    [Header("敵の数")]
    [SerializeField]  private int _spownNum;


    public PoolObjectType Enemy { get => _enemy; }

    public int SpownNum { get => _spownNum; }

}