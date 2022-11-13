using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    [Header("各武器のパネル")]
    [Tooltip("各武器のパネル")] [SerializeField] List<GameObject> _panel = new List<GameObject>();

    [Header("レベルアップのおおもとのパネル")]
    [Tooltip("レベルアップのおおもとのパネル")] [SerializeField] GameObject _basePanel;

    [Header("レイアウトグループ")]
    [Tooltip("レイアウトグループ")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    List<GameObject> instanciatePanels = new List<GameObject>();

    [Header("実装されている武器の名前")]
    [Tooltip("実装されている武器の名前")] [SerializeField] string[] weapons = new string[2];


    [SerializeField] int weaponMaxLevel;

    /// <summary>プレイヤーのレベル</summary>
    private int _playerLevel = 1;

    /// <summary>現在の総経験値</summary>
    private float _exp;
    /// <summary>次のレベルアップに必要な経験値</summary>
    private float _nextLevelExp = 7;

    private float nextLevelUpPer = 1.5f;

    /// <summary>レベルの演出をする回数</summary>
    int levelTass = 0;

    /// <summary>現在の実装されている武器とそのレベルを入れる</summary>
    Dictionary<string, int> weaponsLevel = new Dictionary<string, int>();

    public WeaponInforMaition _weaponInforMaition = default;

    MainStatas _mainStatas;
    WeaponData weaponManger;


    GameSituation _gameSituation = GameSituation.InGame;
    PauseManager _pauseManager = default;
    private void Awake()
    {
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }
    /// <summary>経験値ゲット</summary>
    /// <param name="exp">得た経験値の量</param>
    public void AddExp(int exp)
    {
        //得た経験値に、経験値倍率をかけて、現在の総経験値に得た経験値を足す   
        var addExp = exp * _mainStatas.ExpUpper;
        _exp += addExp;

        //総経験値が、次のレベルアップ経験値を超えたら、
        //・次のレベルアップ経験値を上げてレベルアップの処理の回数を足す
        while (_exp >= _nextLevelExp)
        {
            _nextLevelExp = _nextLevelExp * nextLevelUpPer;
            levelTass++;
        }

        //レベルアップの処理の回数が0以上だったら処理を実行する。
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
        }

        Debug.Log("次までの必要経験値は" + _nextLevelExp);
        Debug.Log("現在の経験値は" + _exp);
    }

    /// <summary>レベルアップの処理</summary>
    public void playerLevelUp()
    {
        _pauseManager.PauseResumeLevelUp();
        Debug.Log("プレイヤーのレベルが上がった！！！現在のレベル:" + _playerLevel);

        //レベルMax以外のアイテムを入れておく仮Dictionary
        Dictionary<string, int> dic = new Dictionary<string, int>();

        //レベルMax以外のアイテムの名前が入っているList
        List<string> ns = new List<string>();

        _basePanel.SetActive(true);

        //すべての武器のレベルを見て、Maxレベル以外のものを選出
        foreach (var n in weapons)
        {
            if (weaponsLevel[n] != weaponMaxLevel)
            {
                dic.Add(n, weaponsLevel[n]);
                ns.Add(n);
            }
        }

        //３つ選択肢を出す
        for (int i = 0; i < 3; i++)
        {
            var r = Random.Range(0, ns.Count);

            //武器の次のステータスを持ってくる。
            WeaponInforMaition weaponInformaition = weaponManger.GetInfomaitionData((dic[ns[r]] + 1), ns[r]);

            //レベルアップする武器のパネルをだす
            GameObject panel = _panel.Where(i => i.name.Contains(ns[r])).First();

            //武器のパネルのTextを更新
            var text = panel.transform.GetChild(0).GetComponent<Text>();
            text.text = weaponInformaition.Te;

            //武器のパネルのレベルの表記Texttを更新
            if (weaponsLevel[ns[r]] > 0)
            {
                var text2 = panel.transform.GetChild(1).GetComponent<Text>();
                text2.text = "Level:" + (weaponsLevel[ns[r]] + 1);
            }

            //武器のパネルをレイアウトグループの子オブジェクトにする
            panel.transform.SetParent(_basePanelLayoutGroup.transform);
            //出したパネルを保存しておくリストに追加(後で消すため)
            instanciatePanels.Add(panel);

            //選択の重複を無くす
            dic.Remove(ns[r]);
            ns.RemoveAt(r);
        }
    }

    /// <summary>レベルアップが終わった時に呼ぶ。パネルにつけて</summary>
    public void EndLevelUp()
    {
        _pauseManager.PauseResumeLevelUp();
        //ステートを「ゲーム中」に変更
        _gameSituation = GameSituation.InGame;
        //演出用のパネルを非表示にする
        instanciatePanels.ForEach(i => i.transform.position = new Vector3(3000, 3000));
        _basePanel.SetActive(false);
        _basePanelLayoutGroup.transform.DetachChildren();

        //レベルがまだ出来たらもう一度
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
        }
    }


    void Start()
    {
        _mainStatas = GameObject.FindObjectOfType<MainStatas>();
        weaponManger = GameObject.FindObjectOfType<WeaponData>();
        SetWeapon();
    }


    void Update()
    {

    }

    /// <summary>武器を確認する</summary>
    void SetWeapon()
    {
        foreach (var n in weapons)
        {
            weaponsLevel.Add(n, 0);
        }
    }
    /// <summary>各武器から呼ぶ。武器のレベルを再設定</summary>
    public void WeaponLevelUp(string name, int level)
    {
        weaponsLevel[name] = level;
        Debug.Log(name + "はレベル" + level);
    }




    enum GameSituation
    {
        LevlUp,
        InGame,
    }
}
