using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpUpperItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("ExpUppersは" + _statas +"倍");
        _mainStatas.ChamgeExp(0,_statas, 0);
    }

    public override void LevelUp()
    {
        _level++;

        _itemStats = _itemData.GetData(_level, _itemName);

            _thisStatas = _itemStats.Exp;
            LevelUpStatas(_thisStatas);
            Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);

        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }


}
