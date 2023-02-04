using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUpItem : ItemBase
{
    [Header("謎のみと")]
    [SerializeField] GameObject _nazonomito;

    [SerializeField] Vector3 _addPos;


    protected override void LevelUpStatas(float _statas)
    {
     //   _mainStatas.ChamgeExp(_statas, 1, 0);
    }
    public override void LevelUp(int level = 1)
    {
        _level += level;

        _itemStats = _itemData.GetData(_level, _itemName);

        _thisStatas = _itemStats.Exp;
        LevelUpStatas(_thisStatas);
        Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);

        _mainStatas.ChangeAttackPower(0, _itemStats.AttackPower, 0);
        _mainStatas.ChangeAttackSpeed(0, _itemStats.AttackSpeed, 0);
        _mainStatas.ChangeNumber(0, (int)_itemStats.Number);
        _mainStatas.ChangeCoolTIme(0, _itemStats.CoolTime, 0);
        _mainStatas.ChangeAttackEria(0, _itemStats.AttackEria, 0);
        _mainStatas.ChangeGetEria(0, _itemStats.MoveSpeed, 0);
        _mainStatas.ChangeDex(0, _itemStats.Dex, 0);
        _mainStatas.ChangeMaxHp(0, _itemStats.MaxHp, 0);
        _mainStatas.ChamgeExp(0, _itemStats.Exp, 0);

        var player = GameObject.FindGameObjectWithTag("Player");
        var go = Instantiate(_nazonomito);
        go.transform.position = player.transform.position + _addPos;


        _mainStatas.SetStatsText();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }
}
