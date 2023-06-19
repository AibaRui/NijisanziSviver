using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    /// <summary>レベルアップテーブルが CSV 形式で格納されているテキスト</summary>
    List<TextAsset> _levelUpTable = new List<TextAsset>();

    List<TextAsset> _itemInforMaitionText = new List<TextAsset>();


    public List<TextAsset> LevelTable { get => _levelUpTable; set => _levelUpTable = value; }
    public List<TextAsset> InfoTable { get => _itemInforMaitionText; set => _itemInforMaitionText = value; }


    /// <summary>武器の詳細が 入っている Dictionary</summary>
    Dictionary<string, ItemInforMaition> _itemInforMaitionData = new Dictionary<string, ItemInforMaition>();

    /// <summary>レベルアップテーブルが入っている Dictionary</summary>
    Dictionary<string, ItemStats> _levelData = new Dictionary<string, ItemStats>();

    ItemStats _itemStats;

    private ItemManager _itemManager;

    public void Init(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }



    /// <summary>
    /// _levelUpTable で指定されたテキストアセットから CSV データを読み取り、
    /// レベルアップテーブルを作る
    /// </summary>
    public void BuildLevelUpTable(string itemName, TextAsset itemStatas, TextAsset itemInfo)
    {

        // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
        System.IO.StringReader sr = new System.IO.StringReader(itemStatas.text);

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

            ItemStats stats = new ItemStats(itemName + i.ToString(), float.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                float.Parse(values[4]), float.Parse(values[5]), float.Parse(values[6]), float.Parse(values[7]), float.Parse(values[8]),
                float.Parse(values[9]), float.Parse(values[10]));
            _levelData.Add(itemName + i.ToString(), stats);
            i++;
        }


        // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
        System.IO.StringReader sr2 = new System.IO.StringReader(itemInfo.text);

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

            ItemInforMaition wI = new ItemInforMaition(itemName + k.ToString(), values[1]);
            _itemInforMaitionData.Add(itemName + k.ToString(), wI);
            k++;
        }
        Debug.Log("Finished BuildLevelUpTable");
    }



    /// <summary>
    /// レベルを指定して、レベルアップテーブルからデータを取得する
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public ItemStats GetData(int level, string itemName)
    {
        if (_levelData.ContainsKey(itemName + level.ToString()))
        {
            return _levelData[itemName + level.ToString()];
        }
        Debug.LogError($"指定されたレベルは存在しません。name:{itemName + level.ToString()}/　level: {level} ");
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
            Debug.Log("鍵"+a.Key);
            }

        }


        Debug.LogError($"指定されたレベルは存在しません。name:{itemName + level.ToString()}/　level: {level} ");
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
/// プレイヤーのパラメーターを格納する構造体
/// </summary>
public struct ItemStats
{
    /// <summary>名前</summary>
    public string Name;
    /// <summary>ステータス</summary>
    public float AttackPower;
    /// <summary>ステータス</summary>
    public float AttackSpeed;
    /// <summary>ステータス</summary>
    public float Number;
    /// <summary>ステータス</summary>
    public float AttackEria;
    /// <summary>ステータス</summary>
    public float CoolTime;
    /// <summary>ステータス</summary>
    public float MaxHp;
    /// <summary>ステータス</summary>
    public float Dex;
    /// <summary>ステータス</summary>
    public float MoveSpeed;
    /// <summary>ステータス</summary>
    public float Exp;
    /// <summary>ステータス</summary>
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
