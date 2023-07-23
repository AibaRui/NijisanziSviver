using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]

public class ScritableItem : ScriptableObject
{
    [Header("アイテムの名前")]
    [SerializeField] private string _itemName;

    [Header("最高レベル")]
    [SerializeField] protected int _maxLevel = 5;

    [Header("ステータスのTextAsstes")]
    [SerializeField] private TextAsset _statasTextAssets;

    [Header("詳細のTextAsstes")]
    [SerializeField] private TextAsset _infoTextAssets;

    [Header("アイテムのMonobehaviorプレハブ")]
    [SerializeField] private GameObject _itemPrefab;

    [Header("アイコン用のスプライト")]
    [SerializeField] Sprite _sprite;


    public TextAsset Statas => _statasTextAssets;

    public TextAsset Info => _infoTextAssets;


    public Sprite ItemSprite => _sprite;

    public int MaxLevel => _maxLevel;
    public string ItemName => _itemName;
    public GameObject ItemPrefab => _itemPrefab;

}
