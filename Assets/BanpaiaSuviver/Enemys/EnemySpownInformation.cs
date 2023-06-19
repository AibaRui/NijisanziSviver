using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class EnemySpownInformation : IComparable<EnemySpownInformation>
{
    [Header("�o���G�B")]
    [SerializeField]
    public List<SpownEnemysData> _enemysData = new List<SpownEnemysData>();

    [Header("�ݒ肷�鎞��")]
    [SerializeField] private int _setTimes;

    [Header("�G�𐶐�����C���^�[�o��")]
    [SerializeField] private float _spawnInterval = 0.1f;

    public int SetTime { get => _setTimes; }

    public float SpawnInterval { get => _spawnInterval; }

    private TestSpown _spownser = null;

    /// <summary>StateMacine���Z�b�g����֐�</summary>
    /// <param name="stateMachine"></param>
    public void Init(TestSpown spownser)
    {
        _spownser = spownser;
    }

    /// <summary>
    /// ID ���Ƃ��ă\�[�g����悤�ɒ�`����
    /// </summary>
    /// <param name="other">�\�[�g�����r���鑊��</param>
    /// <returns>�����̕������������� -1, �����̕����傫������ 1, ���l�̎��� 0</returns>
    public int CompareTo(EnemySpownInformation other)
    {
        if (this._setTimes < other._setTimes)
        {
            return -1;  // ������ ID �����������́u�����̕����O�v�Ƃ���
        }
        else if (this._setTimes > other._setTimes)
        {
            return 1;  // ������ ID ���傫�����́u�����̕������v�Ƃ���
        }
        else
        {
            return 0;   // ID �������Ȃ�u�����v�Ƃ���
        }
    }
}

[Serializable]
public class SpownEnemysData
{
    [SerializeField]
    private string Name;
    [Header("�o���G")]
    [SerializeField] private PoolObjectType _enemy;

    [Header("�G�̐�")]
    [SerializeField]  private int _spownNum;


    public PoolObjectType Enemy { get => _enemy; }

    public int SpownNum { get => _spownNum; }

}