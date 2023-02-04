using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>�v�[������Text�I�u�W�F�N�g���i�[�����X�N���v�^�u���I�u�W�F�N�g</summary>
[CreateAssetMenu(fileName = "TextPoolData")]

public class TextPoolData : ScriptableObject
{
    public ObjectData[] Data => data;

    [SerializeField]
    private ObjectData[] data = default;

    /// <summary>�v�[������I�u�W�F�N�g�̈��f�[�^���i�[�����N���X</summary>

    [Serializable]
    public class ObjectData
    {
        public GameObject Prefab { get => objectPrefab; }
        public TextType Type { get => textType; }
        public int MaxCount { get => objectMaxCount; }

        [SerializeField]
        private string Name;
        [SerializeField]
        private TextType textType;
        [SerializeField]
        private GameObject objectPrefab;
        [SerializeField]
        private int objectMaxCount;
    }
}
