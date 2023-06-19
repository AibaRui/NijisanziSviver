using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>攻撃力を上げ、防御を下げるアイテム</summary>
public class PowerUpItem : ItemBase
{
    protected override void LevelUpStatas(float statas)
    {
        _mainStatas.ChangeAttackPower(0,statas,0);
    }

    private void LevelUpDex(float statas)
    {
        _mainStatas.ChangeDex(0, statas,0);
    }

    public override void LevelUp()
    {
        _level++;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.AttackPower;
        float _thisDex = _itemStats.Dex;

        LevelUpStatas(_thisStatas);
        LevelUpDex(_thisDex);

        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
