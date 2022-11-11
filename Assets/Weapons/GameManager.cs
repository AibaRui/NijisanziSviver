using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _panel = new List<GameObject>();

    [SerializeField] GameObject _basePanel;
    [SerializeField] LayoutGroup _basePanelLayoutGroup;

    [SerializeField] int weaponMaxLevel;

    [SerializeField] int _playerLevel = 1;

    [SerializeField] string[] weapons = new string[2];

    float _exp;
    float _nextLevelExp = 7;
    float nextLevelUpPer = 1.5f;

    //加算されてるレベル
    int levelTass = 0;

    Dictionary<string, int> weaponsLevel = new Dictionary<string, int>();

    public WeaponInforMaition _weaponInforMaition = default;

    MainStatas _mainStatas;
    WEaponManger weaponManger;

    List<GameObject> instanciatePanels = new List<GameObject>();


    GameSituation _gameSituation = GameSituation.InGame;


   public void AddExp(int exp)
    {
        
        var addExp = exp * _mainStatas.ExpUpper;
        _exp += addExp;

 

        while(_exp>=_nextLevelExp)
        {
            _nextLevelExp = _nextLevelExp * nextLevelUpPer;
            levelTass++;
        }

        Debug.Log("次までの必要経験値は" + _nextLevelExp);
        Debug.Log("現在の経験値は" + _exp);

        if(levelTass>0)
        {
            playerLevelUp();
            levelTass--;
        }
    }

    public void playerLevelUp()
    {
        Debug.Log("プレイヤーのレベルが上がった！！！現在のレベル:" + _playerLevel);

        Dictionary<string, int> dic = new Dictionary<string, int>();

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

            if (dic.Count > 0)
            {
                var r = Random.Range(0, ns.Count);


                //武器の強化情報を持ってくる。
                WeaponInforMaition weaponInformaition = weaponManger.GetInfomaitionData((dic[ns[r]]+1), ns[r]);

                //レベルアップする武器のパネルを編集してだす
                GameObject panel = _panel.Where(i => i.name.Contains(ns[r])).First();
                //var go = Instantiate(panel);

                //レベルアップ情報のTextを更新
                var text = panel.transform.GetChild(0).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                var text2 = panel.transform.GetChild(1).GetComponent<Text>();


                //レベルの表記を更新
                if (weaponsLevel[ns[r]] > 0)
                {
                    text2.text = "Level:" + (weaponsLevel[ns[r]]+1);
                }
                else
                {

                }

                panel.transform.SetParent(_basePanelLayoutGroup.transform);
                instanciatePanels.Add(panel);

                dic.Remove(ns[r]);

                ns.RemoveAt(r);
            }
        }
    }

    /// <summary>レベルアップが終わった時に呼ぶ。パネルにつけて</summary>
    public void EndLevelUp()
    {
        _gameSituation = GameSituation.InGame;
        instanciatePanels.ForEach(i => i.transform.position = new Vector3(3000, 3000));
        _basePanel.SetActive(false);
        _basePanelLayoutGroup.transform.DetachChildren();
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
        }
    }


    void Start()
    {
        _mainStatas = GameObject.FindObjectOfType<MainStatas>();
        weaponManger = GameObject.FindObjectOfType<WEaponManger>();
        SetWeapon();
    }


    void Update()
    {

    }
    void SetWeapon()
    {
        foreach (var n in weapons)
        {
            weaponsLevel.Add(n, 0);
        }
    }
    /// <summary>各武器から呼ぶ。武器のレベルをアップ</summary>
    public void WeaponLevelUp(string name, int level)
    {
        Debug.Log(name + "はレベル" + level);
        weaponsLevel[name] = level;
    }




    enum GameSituation
    {
        LevlUp,
        InGame,
    }
}
