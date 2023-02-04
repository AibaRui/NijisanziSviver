using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    /// <summary>レベルアップテーブルが CSV 形式で格納されているテキスト</summary>
    [SerializeField] TextAsset[] _levelUpTable = new TextAsset[1];

    /// <summary>レベルアップテーブルが入っている Dictionary</summary>
    Dictionary<string, WeaponStats> _levelData = new Dictionary<string, WeaponStats>();


    /// <summary>武器の詳細が CSV 形式で格納されているテキスト</summary>
    [SerializeField] TextAsset[] _weaponInforMaitionText = new TextAsset[1];


    /// <summary>武器の詳細が 入っている Dictionary</summary>
    Dictionary<string, WeaponInforMaition> _weaponInforMaitionData = new Dictionary<string, WeaponInforMaition>();


    void Awake()
    {
        BuildLevelUpTable();
    }

    /// <summary>
    /// _levelUpTable で指定されたテキストアセットから CSV データを読み取り、
    /// レベルアップテーブルを作る
    /// </summary>
    void BuildLevelUpTable()
    {
        foreach (var text in _levelUpTable)
        {

            // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
            System.IO.StringReader sr = new System.IO.StringReader(text.text);

            // 一行目は列名なので飛ばす
            sr.ReadLine();

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
                Debug.Log(nameAndLevel);
                WeaponStats stats = new WeaponStats(nameAndLevel, int.Parse(values[1]), float.Parse(values[2]), float.Parse(values[3]),
                                                    float.Parse(values[4]), float.Parse(values[5]), int.Parse(values[6]));
                _levelData.Add(nameAndLevel, stats);
            }
        }
        foreach (var text2 in _weaponInforMaitionText)
        {
            // C# の標準ライブラリを使って「一行ずつ読む」という処理をする
            System.IO.StringReader sr2 = new System.IO.StringReader(text2.text);

            // 一行目は列名なので飛ばす
            sr2.ReadLine();

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

                WeaponInforMaition wI = new WeaponInforMaition(nameAndLevel, values[1]);
                _weaponInforMaitionData.Add(nameAndLevel, wI);
            }
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
        if (_levelData.ContainsKey(weaponName + level.ToString()))
        {
            return _levelData[weaponName + level.ToString()];
        }
        Debug.LogError($"指定されたレベルは存在しません。level: {level}");
        return default;
    }


    public WeaponInforMaition GetInfomaitionData(int level, string weaponName)
    {
        if (_levelData.ContainsKey(weaponName + level.ToString()))
        {
            return _weaponInforMaitionData[weaponName + level.ToString()];
        }
        Debug.LogError($"指定されたレベルは存在しません。level: {level}");
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
    /// プレイヤーのパラメーターを格納する構造体
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
