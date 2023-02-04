using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberUpItem : ItemBase
{
    [SerializeField] List <GameObject> _rikiiti = new List<GameObject>();

    private int _count = 0;
    
    private void AddRikiiti()
    {
        for(int i =0;i<2;i++)
        {
            int add = (2 * _count)+i;
            _rikiiti[add].SetActive(true);
        }
        _count++;
    }

    protected override void LevelUpStatas(float statas)
    {
        Debug.Log("���˕��̌��́A�O" + _mainStatas.Number);
        _mainStatas.ChangeNumber((int)statas,0);
        Debug.Log("���˕��̌��́A��" + _mainStatas.Number);
    }

    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.Number;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);
        AddRikiiti();
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
