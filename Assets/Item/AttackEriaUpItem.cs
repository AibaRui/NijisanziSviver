using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEriaUpItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("�U���͈͂̔{����" + _statas);
        _mainStatas.ChangeAttackEria(_statas, 0, 0);
    }

    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.AttackEria;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);

        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}

