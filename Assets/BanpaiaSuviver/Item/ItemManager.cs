using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("�Q�[�����ł�������")]
    [SerializeField] private List<ScritableItem> _useItems = new List<ScritableItem>();

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

        //�{�^���̐ݒ�
        var panel = Instantiate(item.LevelUpButtun);
        panel.TryGetComponent<Button>(out Button button);
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        //����̃��x���A�b�v�̏������{�^���ɓo�^
        button.onClick.AddListener(itemBase.LevelUp);
        //���x���A�b�v�I���̏������{�^���ɓo�^
        button.onClick.AddListener(_levelUpController.EndLevelUp);

        panel.SetActive(false);

        //Box�p��Icon��ݒ�
        var boxIcon = Instantiate(item.IconUseBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //UI�p�̃A�C�R���𐶐�
        var icon = Instantiate(item.IconUseUI);

        //�A�C�R���̐ݒ�
        _canvasManager.NameOfIconPanelUseBox.Add(item.ItemName, boxIcon);
        _canvasManager.NameOfIconPanelUseUI.Add(item.ItemName, icon);
        _canvasManager.NameOfInformationPanel.Add(item.ItemName, panel);

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
