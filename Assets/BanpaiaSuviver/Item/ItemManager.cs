using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("ゲーム内でつかう武器")]
    [SerializeField] private List<ScritableItem> _useItems = new List<ScritableItem>();

    /// <summary>現在使っているアイテムの名前</summary>
    private List<string> _onUseItems = new List<string>();
    /// <summary>アイテムの名前</summary>
    private List<string> _itemNameT = new List<string>();
    /// <summary>現在の実装されている武器とそのレベルを入れる</summary>
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
        //使う武器の情報を登録していく
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

        //ボタンの設定
        var panel = Instantiate(item.LevelUpButtun);
        panel.TryGetComponent<Button>(out Button button);
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        //武器のレベルアップの処理をボタンに登録
        button.onClick.AddListener(itemBase.LevelUp);
        //レベルアップ終了の処理をボタンに登録
        button.onClick.AddListener(_levelUpController.EndLevelUp);

        panel.SetActive(false);

        //Box用のIconを設定
        var boxIcon = Instantiate(item.IconUseBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //UI用のアイコンを生成
        var icon = Instantiate(item.IconUseUI);

        //アイコンの設定
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
