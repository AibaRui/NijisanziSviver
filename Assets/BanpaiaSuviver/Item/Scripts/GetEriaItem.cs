using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEriaItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("取得範囲は" + _statas);
        _mainStatas.ChangeGetEria(0,_statas, 0);
    }

    public override void LevelUp()
    {
        //レベルアップ
        _level++;

        //ステータス更新
        _itemStats = _itemData.GetData(_level, _itemName);
        _thisStatas = _itemStats.GetEria;
        LevelUpStatas(_thisStatas);


        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }

}
