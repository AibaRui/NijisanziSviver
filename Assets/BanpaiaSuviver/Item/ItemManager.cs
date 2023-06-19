using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("ゲーム内でつかう武器")]
    [SerializeField] private List<ScritableItem> _useItems = new List<ScritableItem>();


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
            _itemData.BuildLevelUpTable(a.ItemName,a.Statas,a.Info);
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


        //Box用のIconを設定
        var boxIcon = Instantiate(item.IconUseBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //アイコンの設定
        _canvasManager.NameOfIconPanelUseBox.Add(item.ItemName, boxIcon);
        _canvasManager.NameOfIconPanelUseUI.Add(item.ItemName, item.IconUseUI);
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
