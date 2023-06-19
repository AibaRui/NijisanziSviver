using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEriaItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("�擾�͈͂�" + _statas);
        _mainStatas.ChangeGetEria(0,_statas, 0);
    }

    public override void LevelUp()
    {
        //���x���A�b�v
        _level++;

        //�X�e�[�^�X�X�V
        _itemStats = _itemData.GetData(_level, _itemName);
        _thisStatas = _itemStats.GetEria;
        LevelUpStatas(_thisStatas);


        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }

}
