using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("�Q�[�����ł�������")]
    [SerializeField] private List<ScritableItem> _useItems = new List<ScritableItem>();

    [SerializeField] private DebugMaker debugMaker;

    /// <summary>���ݎg���Ă���A�C�e���̖��O</summary>
    private List<string> _onUseItems = new List<string>();
    /// <summary>�A�C�e���̖��O</summary>
    private List<string> _itemNameT = new List<string>();
    /// <summary>���݂̎�������Ă��镐��Ƃ��̃��x��������</summary>
    Dictionary<string, ItemLevelDate> _itemsLevel = new Dictionary<string, ItemLevelDate>();

    public Dictionary<string, ItemLevelDate> ItemLevels { get => _itemsLevel; set => _itemsLevel = value; }
    public List<string> OnUseItems { get => _onUseItems; set => _onUseItems = value; }
    public List<string> ItemNames { get => _itemNameT; set => _itemNameT = value; }

    [SerializeField] private ItemData _itemData;
    [SerializeField] private LevelUpController _levelUpController;
    [SerializeField] private BoxControl _boxControl;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private MainStatas _mainStatas;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private UIMaker _uIMaker;

    public ItemData ItemData => _itemData;

    private void Awake()
    {
        //�g������̏���o�^���Ă���
        foreach (var a in _useItems)
        {
            ItemSetting(a);

            _itemData.LevelTable.Add(a.Statas);
            _itemData.InfoTable.Add(a.Info);
            _itemData.BuildLevelUpTable(a.ItemName, a.Statas, a.Info);
        }

        _itemData.Init(this);
    }

    void ItemSetting(ScritableItem item)
    {
        var go = Instantiate(item.ItemPrefab);

        go.TryGetComponent<ItemBase>(out ItemBase itemBase);

        itemBase.Init(item.ItemName, item.MaxLevel);

        itemBase.ItemData = _itemData;
        itemBase.GameManager = _gameManager;
        itemBase.MainStatas = _mainStatas;
        itemBase.BoxControl = _boxControl;
        itemBase.LevelUpController = _levelUpController;

        //�I�����̃p�l���̓o�^
        _uIMaker.PanelMake(item.ItemName,item.ItemSprite);
        _uIMaker.Panel[item.ItemName].TryGetComponent<Button>(out Button button);
        button.onClick.AddListener(itemBase.LevelUp); //����̃��x���A�b�v�̏������{�^���ɓo�^  
        button.onClick.AddListener(_levelUpController.EndLevelUp); //���x���A�b�v�I���̏������{�^���ɓo�^

        var debug = Instantiate(debugMaker._buttun);
        debug.transform.SetParent(debugMaker._itemLayoutGroup.transform);
        debug.onClick.AddListener(itemBase.LevelUpDebugButtun);
        debug.transform.GetChild(0).GetComponent<Text>().text = item.ItemName;



        //Box�p�̃A�C�R���𐶐�
        _uIMaker.BoxIconMake(item.ItemName, item.ItemSprite);

        //UI�p�̃A�C�R�����쐬
        _uIMaker.UIIconMake(item.ItemName, item.ItemSprite);

        _levelUpController.SetItemData(item.ItemName, item.MaxLevel);
        _boxControl.SetItem(item.ItemName, itemBase);
    }


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
