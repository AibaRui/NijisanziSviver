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

    [Header("Boxの演出用のアイコン")]
    [SerializeField] GameObject _iconUseBox;

    [Header("UI用のアイコン")]
    [SerializeField] GameObject _iconUseUI;

    [Header("レベルアップ用の詳細パネル")]
    [SerializeField] GameObject _panel;

    public TextAsset Statas => _statasTextAssets;

    public TextAsset Info => _infoTextAssets;

    public GameObject IconUseBox => _iconUseBox;

    public GameObject IconUseUI => _iconUseUI;

    public GameObject LevelUpButtun => _panel;

    public int MaxLevel => _maxLevel;
    public string ItemName => _itemName;
    public GameObject ItemPrefab => _itemPrefab;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
