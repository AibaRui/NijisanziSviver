using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]

public class ScritableItem : ScriptableObject
{
    [Header("�A�C�e���̖��O")]
    [SerializeField] private string _itemName;

    [Header("�ō����x��")]
    [SerializeField] protected int _maxLevel = 5;

    [Header("�X�e�[�^�X��TextAsstes")]
    [SerializeField] private TextAsset _statasTextAssets;

    [Header("�ڍׂ�TextAsstes")]
    [SerializeField] private TextAsset _infoTextAssets;

    [Header("�A�C�e����Monobehavior�v���n�u")]
    [SerializeField] private GameObject _itemPrefab;

    [Header("�A�C�R���p�̃X�v���C�g")]
    [SerializeField] Sprite _sprite;


    public TextAsset Statas => _statasTextAssets;

    public TextAsset Info => _infoTextAssets;


    public Sprite ItemSprite => _sprite;

    public int MaxLevel => _maxLevel;
    public string ItemName => _itemName;
    public GameObject ItemPrefab => _itemPrefab;

}
