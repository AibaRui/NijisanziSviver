using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    protected float _thisStatas;

    protected string _itemName;
    protected int _maxLevel = 5;

    protected ItemStats _itemStats;

    protected MainStatas _mainStatas;
    /// <summary>レベルアップテーブルを読み込むため</summary>
    protected ItemData _itemData = default;
    protected GameManager _gm;
    protected LevelUpController _levelUpController;
    protected BoxControl _boxControl;

    public BoxControl BoxControl { get => _boxControl; set => _boxControl = value; }
    public LevelUpController LevelUpController { get => _levelUpController; set => _levelUpController = value; }
    public MainStatas MainStatas { get => _mainStatas; set => _mainStatas = value; }
    public GameManager GameManager { get => _gm; set => _gm = value; }
    public ItemData ItemData { get => _itemData; set => _itemData = value; }

    protected int _level = 0;

    public void Init(string itemName, int maxLevel)
    {
        _itemName = itemName;
        _maxLevel = maxLevel;
    }


    protected abstract void LevelUpStatas(float _statas);

    public abstract void LevelUp();

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
