using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    /// <summary>���x���A�b�v�e�[�u���� CSV �`���Ŋi�[����Ă���e�L�X�g</summary>
    [SerializeField] TextAsset[] _levelUpTable = new TextAsset[1];

    /// <summary>���x���A�b�v�e�[�u���������Ă��� Dictionary</summary>
    Dictionary<string, WeaponStats> _levelData = new Dictionary<string, WeaponStats>();


    /// <summary>����̏ڍׂ� CSV �`���Ŋi�[����Ă���e�L�X�g</summary>
    [SerializeField] TextAsset[] _weaponInforMaitionText = new TextAsset[1];


    /// <summary>����̏ڍׂ� �����Ă��� Dictionary</summary>
    Dictionary<string, WeaponInforMaition> _weaponInforMaitionData = new Dictionary<string, WeaponInforMaition>();


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
                Debug.Log(nameAndLevel);
                WeaponStats stats = new WeaponStats(nameAndLevel, int.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                                                    float.Parse(values[4]), float.Parse(values[5]), int.Parse(values[6]));
                _levelData.Add(nameAndLevel, stats);
            }
        }
        foreach (var text2 in _weaponInforMaitionText)
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

                WeaponInforMaition wI = new WeaponInforMaition(nameAndLevel, values[1]);
                _weaponInforMaitionData.Add(nameAndLevel, wI);
            }
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
        if (_levelData.ContainsKey(weaponName + level.ToString()))
        {
            return _levelData[weaponName + level.ToString()];
        }
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Blevel: {level}");
        return default;
    }


    public WeaponInforMaition GetInfomaitionData(int level, string weaponName)
    {
        if (_levelData.ContainsKey(weaponName + level.ToString()))
        {
            return _weaponInforMaitionData[weaponName + level.ToString()];
        }
        Debug.LogError($"�w�肳�ꂽ���x���͑��݂��܂���Blevel: {level}");
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
    /// �v���C���[�̃p�����[�^�[���i�[����\����
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
