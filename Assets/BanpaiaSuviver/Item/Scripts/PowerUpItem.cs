using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�U���͂��グ�A�h���������A�C�e��</summary>
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

        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
