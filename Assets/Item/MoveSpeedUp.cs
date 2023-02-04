using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�h��A�ړ����x�A�̗͂𑝉�����A�C�e��</summary>
public class MoveSpeedUp : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("Speed��" + _statas);
        _mainStatas.ChangeMoveSpeed(0,_statas, 0);
    }


    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.MoveSpeed;

        //�ړ����x�𑝉�
        LevelUpStatas(_thisStatas);
        //�h��͂𑝉�
        _mainStatas.ChangeDex(0, _itemStats.Dex, 0);
        //�ő�̗͂𑝉�
        _mainStatas.ChangeMaxHp(0, 0,_itemStats.MaxHp);

        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);

        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
