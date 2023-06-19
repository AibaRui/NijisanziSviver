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

    [Header("Box�̉��o�p�̃A�C�R��")]
    [SerializeField] GameObject _iconUseBox;

    [Header("UI�p�̃A�C�R��")]
    [SerializeField] GameObject _iconUseUI;

    [Header("���x���A�b�v�p�̏ڍ׃p�l��")]
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
