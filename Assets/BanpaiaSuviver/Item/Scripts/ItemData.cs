using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    /// <summary>���x���A�b�v�e�[�u���� CSV �`���Ŋi�[����Ă���e�L�X�g</summary>
    List<TextAsset> _levelUpTable = new List<TextAsset>();

    List<TextAsset> _itemInforMaitionText = new List<TextAsset>();


    public List<TextAsset> LevelTable { get => _levelUpTable; set => _levelUpTable = value; }
    public List<TextAsset> InfoTable { get => _itemInforMaitionText; set => _itemInforMaitionText = value; }


    /// <summary>����̏ڍׂ� �����Ă��� Dictionary</summary>
    Dictionary<string, ItemInforMaition> _itemInforMaitionData = new Dictionary<string, ItemInforMaition>();

    /// <summary>���x���A�b�v�e�[�u���������Ă��� Dictionary</summary>
    Dictionary<string, ItemStats> _levelData = new Dictionary<string, ItemStats>();

    ItemStats _itemStats;

    private ItemManager _itemManager;

    public void Init(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }



    /// <summary>
    /// _levelUpTable �Ŏw�肳�ꂽ�e�L�X�g�A�Z�b�g���� CSV �f�[�^��ǂݎ��A
    /// ���x���A�b�v�e�[�u�������
    /// </summary>
    public void BuildLevelUpTable(string itemName, TextAsset itemStatas, TextAsset itemInfo)
    {

        // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
        System.IO.StringReader sr = new System.IO.StringReader(itemStatas.text);

        // ��s�ڂ͗񖼂Ȃ̂Ŕ�΂�
        sr.ReadLine();

        int i = 1;
        while (true)
        {
            // ��s���ǂ݂���ŏ�������
            string line = sr.ReadLine();

            // line �ɉ��������Ă��Ȃ�������I������Ƃ݂Ȃ��ď������I���
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            // �s���J���}��؂�ŕ������ăf�[�^�ɕ�������
            string[] values = line.Split(',');
            string nameAndLevel = values[0];

            ItemStats stats = new ItemStats(itemName + i.ToString(), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]), float.Parse(values[7]), float.Parse(values[8]),
                float.Parse(values[9]), float.Parse(values[10]));
            _levelData.Add(itemName + i.ToString(), stats);
            i++;
        }


        // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
        System.IO.StringReader sr2 = new System.IO.StringReader(itemInfo.text);

        // ��s�ڂ͗񖼂Ȃ̂Ŕ�΂�
        sr2.ReadLine();

        int k = 1;
        while (true)
        {
            // ��s���ǂ݂���ŏ�������
            string line = sr2.ReadLine();

            // line �ɉ��������Ă��Ȃ�������I������Ƃ݂Ȃ��ď������I���
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            // �s���J���}��؂�ŕ������ăf�[�^�ɕ�������
            string[] values = line.Split(',');
            string nameAndLevel = values[0];

            ItemInforMaition wI = new ItemInforMaition(itemName + k.ToString(), values[1]);
            _itemInforMaitionData.Add(itemName + k.ToString(), wI);
            k++;
        }
        Debug.Log("Finished BuildLevelUpTable");
    }



    /// <summary>
    /// ���x�����w�肵�āA���x���A�b�v�e�[�u������f�[�^���擾����
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public ItemStats GetData(int level, string itemName)
    {
        if (_levelData.ContainsKey(itemName + level.ToString()))
        {
            return _levelData[itemName + level.ToString()];
        }
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Bname:{itemName + level.ToString()}/�@level: {level} ");
        return default;
    }


    public ItemInforMaition GetInfomaitionData(int level, string itemName)
    {
        if (_levelData.ContainsKey(itemName + level.ToString()))
        {
            return _itemInforMaitionData[itemName + level.ToString()];
        }

        foreach(var a in _itemInforMaitionData)
        {
            if(a.Key== itemName + level.ToString())
            {
            Debug.Log("��"+a.Key);
            }

        }


        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Bname:{itemName + level.ToString()}/�@level: {level} ");
        return default;
    }

}

public struct ItemInforMaition
{
    public string Name;
    public string Te;

    public ItemInforMaition(string name, string te)
    {
        this.Name = name;
        this.Te = te;
    }
}


/// <summary>
/// �v���C���[�̃p�����[�^�[���i�[����\����
/// </summary>
public struct ItemStats
{
    /// <summary>���O</summary>
    public string Name;
    /// <summary>�X�e�[�^�X</summary>
    public float AttackPower;
    /// <summary>�X�e�[�^�X</summary>
    public float AttackSpeed;
    /// <summary>�X�e�[�^�X</summary>
    public float Number;
    /// <summary>�X�e�[�^�X</summary>
    public float AttackEria;
    /// <summary>�X�e�[�^�X</summary>
    public float CoolTime;
    /// <summary>�X�e�[�^�X</summary>
    public float MaxHp;
    /// <summary>�X�e�[�^�X</summary>
    public float Dex;
    /// <summary>�X�e�[�^�X</summary>
    public float MoveSpeed;
    /// <summary>�X�e�[�^�X</summary>
    public float Exp;
    /// <summary>�X�e�[�^�X</summary>
    public float GetEria;



    public ItemStats(string name, float attackPower, float attackSpeed, float number,
        float attackEria, float coolTime, float maxHp, float dex, float moveSpeed,
        float exp, float getEria)
    {
        this.Name = name;
        this.AttackPower = attackPower;
        this.AttackSpeed = attackSpeed;
        this.Number = number;
        this.AttackEria = attackEria;
        this.CoolTime = coolTime;
        this.MaxHp = maxHp;
        this.Dex = dex;
        this.MoveSpeed = moveSpeed;
        this.Exp = exp;
        this.GetEria = getEria;



    }
}
