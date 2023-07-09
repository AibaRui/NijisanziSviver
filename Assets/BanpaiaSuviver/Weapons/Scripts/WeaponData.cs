using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    /// <summary>レベルアップテーブルが入っている Dictionary</summary>
    Dictionary<string, WeaponStats> _levelData = new Dictionary<string, WeaponStats>();
    /// <summary>武器の詳細が 入っている Dictionary</summary>
    Dictionary<string, WeaponInforMaition> _weaponInforMaitionData = new Dictionary<string, WeaponInforMaition>();

    private WeaponManaager _weaponManaager;

    public void Init(WeaponManaager weaponManaager)
    {
        _weaponManaager = weaponManaager;
    }

    /// <summary>
    /// _levelUpTable で指定されたテキストアセットから CSV データを読み取り、
    /// レベルアップテーブルを作る
    /// </summary>
    public void BuildLevelUpTable(string weaponname, TextAsset weaponStatas, TextAsset weaponInfo)
    {
        // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
        System.IO.StringReader sr = new System.IO.StringReader(weaponStatas.text);

        // 一行目は列名なので飛ばす
        sr.ReadLine();

        int i = 1;
        while (true)
        {
            // 一行ずつ読みこんで処理する
            string line = sr.ReadLine();

            // line に何も入っていなかったら終わったとみなして処理を終わる
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            // 行をカンマ区切りで分割してデータに復元する
            string[] values = line.Split(',');
            string nameAndLevel = values[0];

            string key = weaponname + i.ToString();
            WeaponStats stats = new WeaponStats(key, int.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                                                float.Parse(values[4]), float.Parse(values[5]), int.Parse(values[6]));
            _levelData.Add(key, stats);
            i++;
        }

        // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
        System.IO.StringReader sr2 = new System.IO.StringReader(weaponInfo.text);

        // 一行目は列名なので飛ばす
        sr2.ReadLine();

        int k = 1;
        while (true)
        {
            // 一行ずつ読みこんで処理する
            string line = sr2.ReadLine();

            // line に何も入っていなかったら終わったとみなして処理を終わる
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            // 行をカンマ区切りで分割してデータに復元する
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
    /// レベルを指定して、レベルアップテーブルからデータを取得する
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public WeaponStats GetData(int level, string weaponName)
    {
        string key = weaponName + level.ToString();

        if (_levelData.ContainsKey(key))
        {
            return _levelData[key];
        }   //通常のレベルアップのKeyを探す

        if (_levelData.ContainsKey(weaponName))
        {
            return _levelData[weaponName];  
        }   //進化のKeyを探す


        Debug.LogError($"指定されたレベル、もしくは進化武器は存在しません。name:{key}/　level: {level} ");
        return default;
    }


    public WeaponInforMaition GetInfomaitionData(int level, string weaponName)
    {
        string key = weaponName + level.ToString();

        if (_levelData.ContainsKey(key))
        {
            return _weaponInforMaitionData[weaponName + level.ToString()];
        }
        Debug.LogError($"指定されたレベルは存在しません。name:{key}/　level: {level} ");
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
/// 武器のパラメーターを格納する構造体
/// </summary>
public struct WeaponStats
{
    /// <summary>名前</summary>
    public string Name;
    /// <summary>レベル</summary>
    public int Level;
    /// <summary>火力</summary>
    public float Power;
    /// <summary>クール時間</summary>
    public float CoolTime;
    /// <summary>範囲</summary>
    public float Eria;
    /// <summary>スピード</summary>
    public float Speed;
    /// <summary>段数</summary>
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
