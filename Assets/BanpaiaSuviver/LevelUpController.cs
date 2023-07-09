using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [Header("武器、アイテムの最大所持数")]
    [SerializeField] private int _maxItemAndWeaponNumbers = 0;

    /// <summary>
    /// レベルアップ時に選択肢の
    /// 生成したパネルを一時保存しておくリスト</summary>
    private List<GameObject> instanciatePanels = new List<GameObject>();

    /// <summary>プレイヤーのレベル</summary>
    private int _playerLevel = 1;
    /// <summary>現在の総経験値</summary>
    private float _exp;
    /// <summary>次のレベルアップに必要な経験値</summary>
    private float _nextLevelExp = 7;

    private float nextLevelUpPer = 1.5f;
    /// <summary>レベルの演出をする回数</summary>
    private int levelTass = 0;

    private AudioSource _aud;

    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private GameManager _gm;
    [SerializeField] private MainStatas _mainStatas;
    [SerializeField] private WeaponManaager weaponManger;
    [SerializeField] private ItemManager itemData;
    [SerializeField] private PauseManager _pauseManager = default;

    public WeaponInforMaition _weaponInforMaition = default;

    public int MaxItemAndWeaponNumbers { get => _maxItemAndWeaponNumbers; }

    // [Header("レベルアップのアイテムのレイアウトグループ")]
    // [SerializeField] LayoutGroup _itemLayoutGroupOnLevelUp;
    // public LayoutGroup ItemLayoutGroupOnLvelUp { get => _itemLayoutGroupOnLevelUp; }

    // [Header("レベルアップの武器のレイアウトグループ")]
    // [SerializeField] LayoutGroup _weaponLayoutGroupOnLevelUp;
    // public LayoutGroup WeaponLayoutGroupOnLevelUp { get => _weaponLayoutGroupOnLevelUp; }

    private void Awake()
    {
        _aud = GetComponent<AudioSource>();
        _canvasManager.LevelUpSlider.maxValue = _nextLevelExp;
        _canvasManager.LevelUpSlider.minValue = 0;
    }

    /// <summary>各武器から呼ぶ。名前と最高レベルを確認する</summary>
    public void SetWeaponData(string weaponName, int maxLevel)
    {
        WeaponLevelData data = new WeaponLevelData(maxLevel, 0);
        weaponManger.WeaponNames.Add(weaponName);
        weaponManger.WeaponLevels.Add(weaponName, data);
    }

    /// <summary>各アイテムから呼ぶ。名前と最高レベルを確認する</summary>
    public void SetItemData(string itemName, int maxLevel)
    {
        ItemLevelDate data = new ItemLevelDate(maxLevel, 0);
        itemData.ItemNames.Add(itemName);
        itemData.ItemLevels.Add(itemName, data);
    }

    /// <summary>各武器から呼ぶ。武器のレベルを再設定</summary>
    public void WeaponLevelUp(string name, int level)
    {
        weaponManger.WeaponLevels[name].NowLevel += 1;
        //もし武器が初ゲットだったら、現在使っている武器として記録する。
        if (weaponManger.WeaponLevels[name].NowLevel == 1)
        {
            //現在使っている武器を記録
            weaponManger.OnUseWeapons.Add(name);

            //現在使っているアイコンを設定
            _canvasManager.NameOfIconPanelUseUI[name].transform.SetParent(_canvasManager.WeaponLayoutGroup.transform);
        }
    }

    /// <summary>各アイテムから呼ぶ。アイテムのレベルを再設定</summary>
    public void ItemLevelUp(string name, int level)
    {
        itemData.ItemLevels[name].NowLevel += 1;
        //もしアイテムが初ゲットだったら、現在使っているアイテムとして記録する。
        if (itemData.ItemLevels[name].NowLevel == 1)
        {
            itemData.OnUseItems.Add(name);

            //現在使っているアイコンを設定
            _canvasManager.NameOfIconPanelUseUI[name].transform.SetParent(_canvasManager.ItemLayoutGroup.transform);
        }
    }

    /// <summary>経験値ゲット</summary>
    /// <param name="exp">得た経験値の量</param>
    public void AddExp(float exp)
    {
        // Debug.Log("Getした経験値は" + exp);
        //得た経験値に、経験値倍率をかけて、現在の総経験値に得た経験値を足す   
        var addExp = exp * _mainStatas.ExpUpper;
        _exp += addExp;

        //_sliderLevelUp.value += addExp;
        //総経験値が、次のレベルアップ経験値を超えたら、
        //・次のレベルアップ経験値を上げてレベルアップの処理の回数を足す
        while (_exp >= _nextLevelExp)
        {
            _exp -= _nextLevelExp;
            _nextLevelExp = _nextLevelExp * nextLevelUpPer;
            levelTass++;

            _canvasManager.LevelUpSlider.maxValue = _nextLevelExp;

        }

        //レベルアップの処理の回数が0以上だったら処理を実行する。
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
            Debug.Log("次までの必要経験値は" + _nextLevelExp);
        }

        _canvasManager.LevelUpSlider.value = _exp;
        //  Debug.Log("現在の経験値は" + _exp);
    }



    /// <summary>レベルアップの処理</summary>
    public void playerLevelUp()
    {
        if (!_pauseManager._isLevelUp)
        {
            _pauseManager.PauseResumeLevelUp();
        }

        _playerLevel++;
        _canvasManager.LevelText.text = "Lv:" + _playerLevel.ToString();
        Debug.Log("プレイヤーのレベルが上がった！！！現在のレベル:" + _playerLevel);

        //レベルMax以外の武器を入れておく仮Dictionary
        Dictionary<string, int> dicWeapon = new Dictionary<string, int>();

        //レベルMax以外の武器の名前が入っているList
        List<string> nameWeapon = new List<string>();

        //レベルMax以外のアイテムを入れておく仮Dictionary
        Dictionary<string, int> dicItem = new Dictionary<string, int>();

        //レベルMax以外のアイテムの名前が入っているList
        List<string> nameItem = new List<string>();

        _canvasManager.LevelUpPanel.SetActive(true);

        //枠に空きがあったら、LevelMax以外のすべてを選出
        if (_canvasManager.WeaponLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //すべての武器のレベルを見て、Maxレベル以外のものを選出
            foreach (var n in weaponManger.WeaponNames)
            {
                if (weaponManger.WeaponLevels[n].NowLevel < weaponManger.WeaponLevels[n].MaxLevel)
                {
                    dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }
        else//空きが無かったら、LevelMax以外の"使ってい居るものから"を選出
        {
            //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in weaponManger.OnUseWeapons)
            {
                if (weaponManger.WeaponLevels[n].NowLevel < weaponManger.WeaponLevels[n].MaxLevel)
                {
                    dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }

        //枠に空きがあったら、LevelMax以外のすべてを選出
        if (_canvasManager.ItemLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //すべてのアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in itemData.ItemNames)
            {
                if (itemData.ItemLevels[n].NowLevel < itemData.ItemLevels[n].MaxLevel)
                {
                    dicItem.Add(n, itemData.ItemLevels[n].NowLevel);
                    nameItem.Add(n);
                }
            }
        }
        else//空きが無かったら、LevelMax以外の"使ってい居るものから"を選出
        {
            //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in itemData.OnUseItems)
            {
                if (itemData.ItemLevels[n].NowLevel < itemData.ItemLevels[n].MaxLevel)
                {
                    dicItem.Add(n, itemData.ItemLevels[n].NowLevel);
                    nameItem.Add(n);
                }
            }
        }


        int count = 0;

        //３つ選択肢を出す
        for (int i = 0; i < 3; i++)
        {
            //武器、アイテムどちらかが、カンストした場合の処理。
            if (count == 3)
            {
                break;
            }
            else if (nameWeapon.Count == 0 && nameItem.Count == 0 && count < _canvasManager.LastPanel.Count)
            {
                _canvasManager.LastPanel[count].transform.SetParent(_canvasManager.BasePanelLayoutGroup.transform);
                _canvasManager.LastPanel[count].SetActive(true);
                instanciatePanels.Add(_canvasManager.LastPanel[count]);
                Debug.Log("HHPP");
                count++;
                break;
            }
            else if (nameWeapon.Count == 0 && nameItem.Count == 0 && count == _canvasManager.LastPanel.Count)
            {
                return;
            }


            int randamItemOrWeapon = Random.Range(0, 2);



            if ((randamItemOrWeapon == 0 || nameItem.Count == 0) && nameWeapon.Count != 0)
            {
                int r = Random.Range(0, nameWeapon.Count);

                //武器の次のステータスを持ってくる。
                WeaponInforMaition weaponInformaition = weaponManger.weaponData.GetInfomaitionData((dicWeapon[nameWeapon[r]] + 1), nameWeapon[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _canvasManager.NameOfInformationPanel[nameWeapon[r]];
                panel.SetActive(true);


                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();

                    if (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel + 1 == weaponManger.WeaponLevels[nameWeapon[r]].MaxLevel) text2.text = "Level:Max";
                    else text2.text = "Level:" + (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel + 1).ToString();
                }

                //武器のパネルをレイアウトグループの子オブジェクトにする
                panel.transform.SetParent(_canvasManager.BasePanelLayoutGroup.transform);
                //出したパネルを保存しておくリストに追加(後で消すため)
                instanciatePanels.Add(panel);

                //選択の重複を無くす
                dicWeapon.Remove(nameWeapon[r]);
                nameWeapon.RemoveAt(r);
            }
            else if ((randamItemOrWeapon == 1 || nameWeapon.Count == 0) && nameItem.Count != 0)
            {
                var r = Random.Range(0, nameItem.Count);

                //アイテムの次のステータスを持ってくる。
                ItemInforMaition itemInformaition = itemData.ItemData.GetInfomaitionData(dicItem[nameItem[r]] + 1, nameItem[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _canvasManager.NameOfInformationPanel[nameItem[r]];
                panel.SetActive(true);

                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = itemInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (itemData.ItemLevels[nameItem[r]].NowLevel > 0)
                {
                    Debug.Log(itemData.ItemLevels[nameItem[r]].NowLevel);
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (itemData.ItemLevels[nameItem[r]].NowLevel + 1).ToString();
                }

                //武器のパネルをレイアウトグループの子オブジェクトにする
                panel.transform.SetParent(_canvasManager.BasePanelLayoutGroup.transform);

                //出したパネルを保存しておくリストに追加(後で消すため)
                instanciatePanels.Add(panel);

                //選択の重複を無くす
                dicItem.Remove(nameItem[r]);
                nameItem.RemoveAt(r);
            }
        }
        _aud.Play();
    }



    /// <summary>レベルアップが終わった時に呼ぶ。パネルにつけて</summary>
    public void EndLevelUp()
    {
        _canvasManager.BasePanelLayoutGroup.transform.DetachChildren();

        //ステートを「ゲーム中」に変更
        _gm._gameSituation = GameManager.GameSituation.InGame;
        //演出用のパネルを非表示にする
        foreach (var p in instanciatePanels)
        {
            //p.transform.SetParent(_canvasManager.OrizinCanvus);
            p.transform.localPosition = _canvasManager.ButtunEndUsePos.position;
            p.SetActive(false);
        }

        foreach (var p in _canvasManager.LastPanel)
        {
            // p.transform.SetParent(_canvasManager.OrizinCanvus);
            p.transform.localPosition = _canvasManager.ButtunEndUsePos.position;
            p.SetActive(false);
        }

        _canvasManager.LevelUpPanel.SetActive(false);


        //レベルがまだ出来たらもう一度
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
        }
        else
        {
            _pauseManager.PauseResumeLevelUp();
        }
    }
}


public class ItemLevelDate
{
    private int _maxLevel;

    private int _nowLevel;

    public int MaxLevel { get => _maxLevel; }

    public int NowLevel { get => _nowLevel; set => _nowLevel = value; }

    public ItemLevelDate(int maxLevel, int nowLevel)
    {
        this._maxLevel = maxLevel;
        this._nowLevel = nowLevel;
    }

}

