using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [Header("Boxの演出用のアイコン")]
    [SerializeField] GameObject _iconUseBox;

    [Header("UI用のアイコン")]
    [SerializeField] GameObject _iconUseUI;

    [Header("レベルアップ用の詳細パネル")]
    [SerializeField] GameObject _panel;

    [Header("レベルアップ画面のレイアウトグループ")]
    [Tooltip("レベルアップ画面のレイアウトグループ")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    [Header("ゲーム画面のレイアウトグループ")]
    [Tooltip("ゲーム画面のレイアウトグループ")] [SerializeField] LayoutGroup _gameSceneLayoutGroup;

    [SerializeField] protected string _itemName;

    [SerializeField] protected float _thisStatas;

    [SerializeField] protected MainStatas _mainStatas;

    /// <summary>レベルアップテーブルを読み込むため</summary>
    [SerializeField] protected ItemData _itemData = default;

    protected ItemStats _itemStats;

    [SerializeField] protected GameManager _gm;

    [SerializeField] protected int _maxLevel = 5;

    [SerializeField] protected LevelUpController _levelUpController;

    [SerializeField] protected BoxControl _boxControl;

   protected int _level = 0;

    private void Awake()
    {
        _levelUpController.SetItemData(_itemName, _maxLevel,_iconUseBox,_panel,_iconUseUI);
        _boxControl.SetItem(_itemName, this);
        _itemData = _itemData.GetComponent<ItemData>();
        _mainStatas = _mainStatas.GetComponent<MainStatas>();
        _gm = _gm.GetComponent<GameManager>();
    }

    protected abstract void LevelUpStatas(float _statas);


    public abstract void LevelUp(int level = 1);

    //public void LevelUp(int level = 1)
    //{
    //    if (_level < _maxLevel)
    //    {
    //        _level += level;

    //        _thisStatas = ReturnStatas(_level);
    //        LevelUpStatas(_thisStatas);
    //        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
    //    }
    //    _mainStatas.SetStatsText();
    //    GameManager gm = FindObjectOfType<GameManager>();
    //    gm.ItemLevelUp(_itemName, _level);
    //}
}
