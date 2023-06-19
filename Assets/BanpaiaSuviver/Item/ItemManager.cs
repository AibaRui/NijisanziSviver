using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    [Header("�Q�[�����ł�������")]
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
        //�g������̏���o�^���Ă���
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

        //�{�^���̐ݒ�
        var panel = Instantiate(item.LevelUpButtun);
        panel.TryGetComponent<Button>(out Button button);
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        //����̃��x���A�b�v�̏������{�^���ɓo�^
        button.onClick.AddListener(itemBase.LevelUp);
        //���x���A�b�v�I���̏������{�^���ɓo�^
        button.onClick.AddListener(_levelUpController.EndLevelUp);


        //Box�p��Icon��ݒ�
        var boxIcon = Instantiate(item.IconUseBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //�A�C�R���̐ݒ�
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
