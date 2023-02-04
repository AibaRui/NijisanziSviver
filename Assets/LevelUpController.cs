using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [Header("詳細パネルの元のCanvas")]
    [SerializeField] Transform _orizinCanvus;

    [Header("ゲーム画面のアイテムのレイアウトグループ")]
    [SerializeField] LayoutGroup _itemLayoutGroup;
    public LayoutGroup ItemLayoutGroup { get => _itemLayoutGroup; }

    [Header("ゲーム画面の武器のレイアウトグループ")]
    [SerializeField] LayoutGroup _weaponLayoutGroup;
    public LayoutGroup WeaponLayoutGroup { get => _weaponLayoutGroup; }

    [SerializeField] Transform _endPos;

    // [Header("レベルアップのアイテムのレイアウトグループ")]
    // [SerializeField] LayoutGroup _itemLayoutGroupOnLevelUp;
    // public LayoutGroup ItemLayoutGroupOnLvelUp { get => _itemLayoutGroupOnLevelUp; }

    // [Header("レベルアップの武器のレイアウトグループ")]
    // [SerializeField] LayoutGroup _weaponLayoutGroupOnLevelUp;
    // public LayoutGroup WeaponLayoutGroupOnLevelUp { get => _weaponLayoutGroupOnLevelUp; }

    [SerializeField] Slider _sliderLevelUp;

    [Tooltip("武器、アイテムの最大所持数")]
    [SerializeField] int _maxItemAndWeaponNumbers = 0;

    public int MaxItemAndWeaponNumbers { get => _maxItemAndWeaponNumbers; }

    [Header("お金と回復のパネル")]
    [Tooltip("お金と回復のパネル")] [SerializeField] List<GameObject> _lastPanel = new List<GameObject>();

    [Header("レベルアップのおおもとのパネル")]
    [Tooltip("レベルアップのおおもとのパネル")] [SerializeField] GameObject _basePanel;

    [Header("レイアウトグループ")]
    [Tooltip("レイアウトグループ")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    List<GameObject> instanciatePanels = new List<GameObject>();

    [Tooltip("現在使っている武器の名前")]
    private List<string> _onUseWeapons = new List<string>();
    public List<string> OnUseWeapons { get => _onUseWeapons; set => _onUseWeapons = value; }

    [Tooltip("現在使っているアイテムの名前")]
    private List<string> _onUseItems = new List<string>();
    public List<string> OnUseItems { get => _onUseItems; set => _onUseItems = value; }

    private List<string> _weaponNameT = new List<string>();

    /// <summary>武器の名前</summary>
    public List<string> WeaponNames { get => _weaponNameT; set => _weaponNameT = value; }

    /// <summary>アイテムの名前</summary>
    private List<string> _itemNameT = new List<string>();

    public List<string> ItemNames { get => _itemNameT; set => _itemNameT = value; }

    /// <summary>プレイヤーのレベル</summary>
    private int _playerLevel = 1;

    /// <summary>現在の総経験値</summary>
    private float _exp;
    /// <summary>次のレベルアップに必要な経験値</summary>
    private float _nextLevelExp = 7;

    [SerializeField] Text _levelText;

    private float nextLevelUpPer = 1.5f;

    /// <summary>レベルの演出をする回数</summary>
    int levelTass = 0;

    /// <summary>Box用。武器、アイテムの名前をKeyにとりアイコンの画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseBox = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> NameOfIconPanelUseBox { get => _nameOfIconPaneUseBox; }

    /// <summary>。武器、アイテムの名前をKeyにとりアイコンの画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseUI = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> NameOfIconPanelUseUI { get => _nameOfIconPaneUseUI; }

    /// <summary>武器、アイテムの名前をKeyにとりパネル画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfInformationPanel = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> NameOfInformationPanel { get => _nameOfInformationPanel; }

    /// <summary>現在の実装されている武器とそのレベルを入れる</summary>
    Dictionary<string, WeaponLevelData> _weaponsLevel = new Dictionary<string, WeaponLevelData>();

    public Dictionary<string, WeaponLevelData> WeaponLevels { get => _weaponsLevel; set => _weaponsLevel = value; }

    /// <summary>現在の実装されている武器とそのレベルを入れる</summary>
    Dictionary<string, ItemLevelDate> _itemsLevel = new Dictionary<string, ItemLevelDate>();

    public Dictionary<string, ItemLevelDate> ItemLevels { get => _itemsLevel; set => _itemsLevel = value; }

    public WeaponInforMaition _weaponInforMaition = default;

    [SerializeField] GameManager _gm;
    [SerializeField] MainStatas _mainStatas;
    [SerializeField] WeaponData weaponManger;
    [SerializeField] ItemData itemData;

    [SerializeField] PauseManager _pauseManager = default;

    AudioSource _aud;
    private void Awake()
    {
        _aud = GetComponent<AudioSource>();
        _sliderLevelUp.maxValue = _nextLevelExp;
        _sliderLevelUp.minValue = 0;
    }

    /// <summary>各武器から呼ぶ。名前と最高レベルを確認する</summary>
    public void SetWeaponData(string weaponName, int maxLevel, GameObject iconUseBox, GameObject panel, GameObject iconUseUI)
    {
        _weaponNameT.Add(weaponName);
        WeaponLevelData data = new WeaponLevelData(maxLevel, 0);
        _weaponsLevel.Add(weaponName, data);
        _nameOfIconPaneUseBox.Add(weaponName, iconUseBox);
        _nameOfIconPaneUseUI.Add(weaponName, iconUseUI);
        _nameOfInformationPanel.Add(weaponName, panel);
    }
    /// <summary>各アイテムから呼ぶ。名前と最高レベルを確認する</summary>
    public void SetItemData(string itemName, int maxLevel, GameObject iconUseBox, GameObject panel, GameObject iconUseUI)
    {
        _itemNameT.Add(itemName);
        ItemLevelDate data = new ItemLevelDate(maxLevel, 0);
        _itemsLevel.Add(itemName, data);
        _nameOfIconPaneUseBox.Add(itemName, iconUseBox);
        _nameOfIconPaneUseUI.Add(itemName, iconUseUI);
        _nameOfInformationPanel.Add(itemName, panel);
    }

    /// <summary>各武器から呼ぶ。武器のレベルを再設定</summary>
    public void WeaponLevelUp(string name, int level)
    {
        _weaponsLevel[name].NowLevel += 1;
        //もし武器が初ゲットだったら、現在使っている武器として記録する。
        if (_weaponsLevel[name].NowLevel == 1)
        {
            _onUseWeapons.Add(name);
            var go = Instantiate(_nameOfIconPaneUseUI[name]);
            var go2 = Instantiate(_nameOfIconPaneUseUI[name]);
            go.transform.SetParent(_weaponLayoutGroup.transform);
            // go2.transform.SetParent(_weaponLayoutGroupOnLevelUp.transform);
        }
    }

    /// <summary>各アイテムから呼ぶ。アイテムのレベルを再設定</summary>
    public void ItemLevelUp(string name, int level)
    {
        _itemsLevel[name].NowLevel += 1;
        //もしアイテムが初ゲットだったら、現在使っているアイテムとして記録する。
        if (_itemsLevel[name].NowLevel == 1)
        {
            _onUseItems.Add(name);

            var go = Instantiate(_nameOfIconPaneUseUI[name]);
            var go2 = Instantiate(_nameOfIconPaneUseUI[name]);
            go.transform.SetParent(_itemLayoutGroup.transform);
            // go2.transform.SetParent(_itemLayoutGroupOnLevelUp.transform);
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

            _sliderLevelUp.maxValue = _nextLevelExp;

        }

        //レベルアップの処理の回数が0以上だったら処理を実行する。
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
            Debug.Log("次までの必要経験値は" + _nextLevelExp);
        }

        _sliderLevelUp.value = _exp;
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
        _levelText.text = "Lv:" + _playerLevel.ToString();
        Debug.Log("プレイヤーのレベルが上がった！！！現在のレベル:" + _playerLevel);

        //レベルMax以外の武器を入れておく仮Dictionary
        Dictionary<string, int> dicWeapon = new Dictionary<string, int>();

        //レベルMax以外の武器の名前が入っているList
        List<string> nameWeapon = new List<string>();

        //レベルMax以外のアイテムを入れておく仮Dictionary
        Dictionary<string, int> dicItem = new Dictionary<string, int>();

        //レベルMax以外のアイテムの名前が入っているList
        List<string> nameItem = new List<string>();



        _basePanel.SetActive(true);

        //枠に空きがあったら、LevelMax以外のすべてを選出
        if (_weaponLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //すべての武器のレベルを見て、Maxレベル以外のものを選出
            foreach (var n in _weaponNameT)
            {
                if (_weaponsLevel[n].NowLevel < _weaponsLevel[n].MaxLevel)
                {
                    dicWeapon.Add(n, _weaponsLevel[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }
        else//空きが無かったら、LevelMax以外の"使ってい居るものから"を選出
        {
            //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in _onUseWeapons)
            {
                if (_weaponsLevel[n].NowLevel < _weaponsLevel[n].MaxLevel)
                {
                    dicWeapon.Add(n, _weaponsLevel[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }

        //枠に空きがあったら、LevelMax以外のすべてを選出
        if (_itemLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //すべてのアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in _itemNameT)
            {
                if (_itemsLevel[n].NowLevel < _itemsLevel[n].MaxLevel)
                {
                    dicItem.Add(n, _itemsLevel[n].NowLevel);
                    nameItem.Add(n);
                }
            }
        }
        else//空きが無かったら、LevelMax以外の"使ってい居るものから"を選出
        {
            //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
            foreach (var n in _onUseItems)
            {
                if (_itemsLevel[n].NowLevel < _itemsLevel[n].MaxLevel)
                {
                    dicItem.Add(n, _itemsLevel[n].NowLevel);
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
            else if (nameWeapon.Count == 0 && nameItem.Count == 0 && count < _lastPanel.Count)
            {
                _lastPanel[count].transform.SetParent(_basePanelLayoutGroup.transform);
                _lastPanel[count].SetActive(true);
                instanciatePanels.Add(_lastPanel[count]);
                Debug.Log("HHPP");
                count++;
                break;
            }
            else if (nameWeapon.Count == 0 && nameItem.Count == 0 && count == _lastPanel.Count)
            {
                return;
            }


            int randamItemOrWeapon = Random.Range(0, 2);



            if ((randamItemOrWeapon == 0 || nameItem.Count == 0) && nameWeapon.Count!=0)
            {
                int r = Random.Range(0, nameWeapon.Count);

                //武器の次のステータスを持ってくる。
                WeaponInforMaition weaponInformaition = weaponManger.GetInfomaitionData((dicWeapon[nameWeapon[r]] + 1), nameWeapon[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _nameOfInformationPanel[nameWeapon[r]];
                panel.SetActive(true);


                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (_weaponsLevel[nameWeapon[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();

                    if (_weaponsLevel[nameWeapon[r]].NowLevel + 1 == _weaponsLevel[nameWeapon[r]].MaxLevel) text2.text = "Level:Max";
                    else text2.text = "Level:" + (_weaponsLevel[nameWeapon[r]].NowLevel + 1).ToString();
                }

                //武器のパネルをレイアウトグループの子オブジェクトにする
                panel.transform.SetParent(_basePanelLayoutGroup.transform);
                //出したパネルを保存しておくリストに追加(後で消すため)
                instanciatePanels.Add(panel);

                //選択の重複を無くす
                dicWeapon.Remove(nameWeapon[r]);
                nameWeapon.RemoveAt(r);
            }
            else if ((randamItemOrWeapon == 1 || nameWeapon.Count == 0) && nameItem.Count!=0)
            {
                var r = Random.Range(0, nameItem.Count);

                //アイテムの次のステータスを持ってくる。
                ItemInforMaition itemInformaition = itemData.GetInfomaitionData(dicItem[nameItem[r]] + 1, nameItem[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _nameOfInformationPanel[nameItem[r]];
                panel.SetActive(true);

                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = itemInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (_itemsLevel[nameItem[r]].NowLevel > 0)
                {
                    Debug.Log(_itemsLevel[nameItem[r]].NowLevel);
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (_itemsLevel[nameItem[r]].NowLevel + 1).ToString();
                }

                //武器のパネルをレイアウトグループの子オブジェクトにする
                panel.transform.SetParent(_basePanelLayoutGroup.transform);

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
        _basePanelLayoutGroup.transform.DetachChildren();

        //ステートを「ゲーム中」に変更
        _gm._gameSituation = GameManager.GameSituation.InGame;
        //演出用のパネルを非表示にする
        foreach (var p in instanciatePanels)
        {
            p.transform.position = _endPos.position;
            p.SetActive(false);
            p.transform.SetParent(_orizinCanvus);
        }

        foreach (var p in _lastPanel)
        {
            p.transform.position = _endPos.position;
            p.SetActive(false);
            p.transform.SetParent(_orizinCanvus);
        }

        _basePanel.SetActive(false);


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

public class WeaponLevelData
{
    private int _maxLevel;

    private int _nowLevel;

    public int MaxLevel { get => _maxLevel; }

    public int NowLevel { get => _nowLevel; set => _nowLevel = value; }

    public WeaponLevelData(int maxLevel, int nowLevel)
    {
        this._maxLevel = maxLevel;
        this._nowLevel = nowLevel;
    }
}