using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolTimeUpItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        _mainStatas.ChangeCoolTIme(_statas, 0, 0);
        Debug.Log("CoolTimeの倍率は" + _statas);

    }

    public override void LevelUp()
    {
        _level ++;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.CoolTime;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
