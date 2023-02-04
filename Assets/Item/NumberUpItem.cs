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
        Debug.Log("発射物の個数は、前" + _mainStatas.Number);
        _mainStatas.ChangeNumber((int)statas,0);
        Debug.Log("発射物の個数は、後" + _mainStatas.Number);
    }

    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.Number;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);
        AddRikiiti();
        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
