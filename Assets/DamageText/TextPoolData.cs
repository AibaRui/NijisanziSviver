using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>プールするTextオブジェクトを格納したスクリプタブルオブジェクト</summary>
[CreateAssetMenu(fileName = "TextPoolData")]

public class TextPoolData : ScriptableObject
{
    public ObjectData[] Data => data;

    [SerializeField]
    private ObjectData[] data = default;

    /// <summary>プールするオブジェクトの一つ一つデータを格納したクラス</summary>

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
