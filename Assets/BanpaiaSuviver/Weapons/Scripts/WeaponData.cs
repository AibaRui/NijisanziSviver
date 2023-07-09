using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    /// <summary>���x���A�b�v�e�[�u���������Ă��� Dictionary</summary>
    Dictionary<string, WeaponStats> _levelData = new Dictionary<string, WeaponStats>();
    /// <summary>����̏ڍׂ� �����Ă��� Dictionary</summary>
    Dictionary<string, WeaponInforMaition> _weaponInforMaitionData = new Dictionary<string, WeaponInforMaition>();

    private WeaponManaager _weaponManaager;

    public void Init(WeaponManaager weaponManaager)
    {
        _weaponManaager = weaponManaager;
    }

    /// <summary>
    /// _levelUpTable �Ŏw�肳�ꂽ�e�L�X�g�A�Z�b�g���� CSV �f�[�^��ǂݎ��A
    /// ���x���A�b�v�e�[�u�������
    /// </summary>
    public void BuildLevelUpTable(string weaponname, TextAsset weaponStatas, TextAsset weaponInfo)
    {
        // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
        System.IO.StringReader sr = new System.IO.StringReader(weaponStatas.text);

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

            string key = weaponname + i.ToString();
            WeaponStats stats = new WeaponStats(key, int.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                                                float.Parse(values[4]), float.Parse(values[5]), int.Parse(values[6]));
            _levelData.Add(key, stats);
            i++;
        }

        // C# �̕W�����C�u�������g���āu��s���ǂށv�Ƃ�������������
        System.IO.StringReader sr2 = new System.IO.StringReader(weaponInfo.text);

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

            string key = weaponname + k.ToString();

            WeaponInforMaition wI = new WeaponInforMaition(key, values[1]);
            _weaponInforMaitionData.Add(key, wI);
            k++;
        }

        Debug.Log("Finished BuildLevelUpTable");
    }

    /// <summary>
    /// ���x�����w�肵�āA���x���A�b�v�e�[�u������f�[�^���擾����
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public WeaponStats GetData(int level, string weaponName)
    {
        string key = weaponName + level.ToString();

        if (_levelData.ContainsKey(key))
        {
            return _levelData[key];
        }   //�ʏ�̃��x���A�b�v��Key��T��

        if (_levelData.ContainsKey(weaponName))
        {
            return _levelData[weaponName];  
        }   //�i����Key��T��


        Debug.LogError($"�w�肳�ꂽ���x���A�������͐i������͑��݂��܂���Bname:{key}/�@level: {level} ");
        return default;
    }


    public WeaponInforMaition GetInfomaitionData(int level, string weaponName)
    {
        string key = weaponName + level.ToString();

        if (_levelData.ContainsKey(key))
        {
            return _weaponInforMaitionData[weaponName + level.ToString()];
        }
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Bname:{key}/�@level: {level} ");
        return default;
    }

}

public struct WeaponInforMaition
{
    public string Name;
    public string Te;

    public WeaponInforMaition(string name, string te)
    {
        this.Name = name;
        this.Te = te;
    }
}


/// <summary>
/// ����̃p�����[�^�[���i�[����\����
/// </summary>
public struct WeaponStats
{
    /// <summary>���O</summary>
    public string Name;
    /// <summary>���x��</summary>
    public int Level;
    /// <summary>�Η�</summary>
    public float Power;
    /// <summary>�N�[������</summary>
    public float CoolTime;
    /// <summary>�͈�</summary>
    public float Eria;
    /// <summary>�X�s�[�h</summary>
    public float Speed;
    /// <summary>�i��</summary>
    public int Number;

    public WeaponStats(string name, int level, float power, float coolTime, float eria, float speed, int number)
    {
        this.Name = name;
        this.Level = level;
        this.Power = power;
        this.CoolTime = coolTime;
        this.Eria = eria;
        this.Speed = speed;
        this.Number = number;
    }
}
