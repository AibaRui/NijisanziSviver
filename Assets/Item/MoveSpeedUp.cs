using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>防御、移動速度、体力を増加するアイテム</summary>
public class MoveSpeedUp : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("Speedは" + _statas);
        _mainStatas.ChangeMoveSpeed(0,_statas, 0);
    }


    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.MoveSpeed;

        //移動速度を増加
        LevelUpStatas(_thisStatas);
        //防御力を増加
        _mainStatas.ChangeDex(0, _itemStats.Dex, 0);
        //最大体力を増加
        _mainStatas.ChangeMaxHp(0, 0,_itemStats.MaxHp);

        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);

        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
