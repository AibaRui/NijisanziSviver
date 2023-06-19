using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpUpItem : ItemBase
{
    protected override void LevelUpStatas(float _statas)
    {
        Debug.Log("Life�̍ő�l��" + _statas);
        _mainStatas.ChangeMaxHp(_statas, 0, 0);
    }

    public override void LevelUp()
    {
        if (_level < _maxLevel)
        {
            _level++;

            _itemStats = _itemData.GetData(_level, _itemName);

            _thisStatas = _itemStats.MaxHp;
            LevelUpStatas(_thisStatas);
            Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);
        }
        _mainStatas.SetStatsText();
        LevelUpController _levelUpController = FindObjectOfType<LevelUpController>();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
