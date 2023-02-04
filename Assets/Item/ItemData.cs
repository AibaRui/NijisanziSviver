using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    /// <summary>���x���A�b�v�e�[�u���� CSV �`���Ŋi�[����Ă���e�L�X�g</summary>
    [SerializeField] TextAsset[] _levelUpTable = new TextAsset[1];

    /// <summary>���x���A�b�v�e�[�u���������Ă��� Dictionary</summary>
    Dictionary<string, ItemStats> _levelData = new Dictionary<string, ItemStats>();


    /// <summary>����̏ڍׂ� CSV �`���Ŋi�[����Ă���e�L�X�g</summary>
    [SerializeField] TextAsset[] _itemInforMaitionText = new TextAsset[1];


    /// <summary>����̏ڍׂ� �����Ă��� Dictionary</summary>
    Dictionary<string, ItemInforMaition> _itemInforMaitionData = new Dictionary<string, ItemInforMaition>();


    ItemStats _itemStats;

    void Awake()
    {
        BuildLevelUpTable();
    }

    /// <summary>
    /// _levelUpTable �Ŏw�肳�ꂽ�e�L�X�g�A�Z�b�g���� CSV �f�[�^��ǂݎ��A
    /// ���x���A�b�v�e�[�u�������
    /// </summary>
    void BuildLevelUpTable()
    {
        foreach (var text in _levelUpTable)
        {
            // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
            System.IO.StringReader sr = new System.IO.StringReader(text.text);

            // ��s�ڂ͗񖼂Ȃ̂Ŕ�΂�
            sr.ReadLine();

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

                ItemStats stats = new ItemStats(nameAndLevel, float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                    float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]), float.Parse(values[7]), float.Parse(values[8]),
                    float.Parse(values[9]), float.Parse(values[10]));
                _levelData.Add(nameAndLevel, stats);
            }
        }
        foreach (var text2 in _itemInforMaitionText)
        {
            // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
            System.IO.StringReader sr2 = new System.IO.StringReader(text2.text);

            // ��s�ڂ͗񖼂Ȃ̂Ŕ�΂�
            sr2.ReadLine();

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

                ItemInforMaition wI = new ItemInforMaition(nameAndLevel, values[1]);
                _itemInforMaitionData.Add(nameAndLevel, wI);
            }
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
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Blevel: {level}");
        return default;
    }


    public ItemInforMaition GetInfomaitionData(int level, string itemName)
    {
        if (_levelData.ContainsKey(itemName + level.ToString()))
        {
            return _itemInforMaitionData[itemName + level.ToString()];
        }
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Blevel: {level}");
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
