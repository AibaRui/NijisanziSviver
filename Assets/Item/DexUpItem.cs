using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DexUpItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("防御力は" + _statas);
        _mainStatas.ChangeDex(_statas, 0, 0);
    }

    public override void LevelUp(int level = 1)
    {

        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.Dex;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
