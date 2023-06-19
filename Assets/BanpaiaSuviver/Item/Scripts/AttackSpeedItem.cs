using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("攻撃速度は" + _statas + "倍");
        _mainStatas.ChangeAttackSpeed(0,_statas, 0);
    }

    public override void LevelUp()
    {
        _level ++;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.AttackSpeed;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
