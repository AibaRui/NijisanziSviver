using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Playables;

public class BoxControl : MonoBehaviour
{
    [Header("演出時のアイコンを出す場所")]
    [Tooltip("演出時のアイコンを出す場所")] [SerializeField] Transform[] _boxIconPos = new Transform[5];

    [Header("アイテム等の詳細パネルを出す場所")]
    [Tooltip("アイテム等の詳細パネルを出す場所")] [SerializeField] Transform _boxInformationPanelPos;

    [Header("演出の基底パネル")]
    [Tooltip("演出の基底パネル")] [SerializeField] GameObject _boxpanel;

    [Header("使い終わったパネルを置く画面外の位置")]
    [Tooltip("使い終わったパネルを置く画面外の位置")] [SerializeField] Transform _posEndPos;

    [Header("全選択後の、回復パネル")]
    [Tooltip("全選択後の、回復パネル")] [SerializeField] GameObject _lastHealPanel;

    [Header("全選択後の、回復アイコン")]
    [Tooltip("全選択後の、回復アイコン")] [SerializeField] List<GameObject> _lastHealIcons = new List<GameObject>();

    [Header("アイコンの親オブジェクト")]
    [SerializeField] private Transform _iconParent;

    [SerializeField] private GameObject _noGetButtun;

    //現在使用しているアイテム等の詳細パネル
    List<GameObject> _choiseItemInformationPanel = new List<GameObject>();
    //現在使用しているアイテム等の名前
    List<string> _nowChoiseBoxWeaponOrItemName = new List<string>();

    Dictionary<string, InstantiateWeaponBase> _levelUpBaseWeapons = new Dictionary<string, InstantiateWeaponBase>();
    Dictionary<string, ItemBase> _levelUpBaseItems = new Dictionary<string, ItemBase>();

    private bool _isGetBox = false;


    [SerializeField] List<GameObject> _timeline = new List<GameObject>();
    PlayableDirector _nowTimeLine = default;
    [SerializeField] WeaponManaager weaponManger;
    [SerializeField] ItemManager itemData;
    [SerializeField] LevelUpController _levelUpController;
    [SerializeField] PauseManager _pauseManager;
    [SerializeField] private CanvasManager _canvasManager;


    //現在開けているか
    private bool _isOpen;
    //のこり回す回数
    private int _numRemaining;

    /// <summary>アイテムを何回受け取ったかを数える</summary>
    private int _countGet = 0;

    private Dictionary<int, bool> _type = new Dictionary<int, bool>();

    private List<int> _numRemainingGetNum = new List<int>();

    public Transform IconParentObject => _iconParent;

    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }



    public void Pause()
    {
        if (_isGetBox && _nowTimeLine != null)
        {
            _nowTimeLine.Pause();
        }
    }

    public void Resume()
    {
        if (_nowTimeLine != null)
        {
            _nowTimeLine.Resume();
        }
    }

    private void Update()
    {
        if (_nowTimeLine != null)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _nowTimeLine.time += 10;
            }
        }
    }

    public void SetWeapon(string name, InstantiateWeaponBase weapon)
    {
        _levelUpBaseWeapons.Add(name, weapon);
    }

    public void SetItem(string name, ItemBase item)
    {
        _levelUpBaseItems.Add(name, item);
    }

    /// <summary>宝箱を取った時の処理</summary>
    public void GetBox(int num)
    {
        if (_isOpen)
        {
            _numRemaining++;
            _numRemainingGetNum.Add(num);
            return;
        }

        _type.Clear();

        _countGet = 0;

        _isOpen = true;

        //演出タイムラインをランダムで再生
        var randomT = UnityEngine.Random.Range(0, _timeline.Count);
        _timeline[randomT].SetActive(true);
        _nowTimeLine = _timeline[randomT].GetComponent<PlayableDirector>();

        _pauseManager.PauseResumeLevelUp();
        _boxpanel.SetActive(true);
        _isGetBox = true;

        var canEvolutionWeapon = weaponManger.CheckWeaponCanEvolution();

        //宝箱の個数分だけ回す
        for (int i = 0; i < num; i++)
        {
            if (canEvolutionWeapon.Length > i)
            {
                _nowChoiseBoxWeaponOrItemName.Add(canEvolutionWeapon[i]);
                _canvasManager._NameOfEvolutionWeaponIconBox[canEvolutionWeapon[i]].transform.position = _boxIconPos[i].position;
                _choiseItemInformationPanel.Add(_canvasManager.NameOfEvolutionWeaponPanel[canEvolutionWeapon[i]]);
                _type.Add(i, true);
                _noGetButtun.SetActive(false);
                continue;
            }
            _type.Add(i, false);
            _noGetButtun.SetActive(true);


            //レベルMax以外の武器を入れておく仮Dictionary
            Dictionary<string, int> dicWeapon = new Dictionary<string, int>();

            //レベルMax以外の武器の名前が入っているList
            List<string> nameWeapon = new List<string>();

            //レベルMax以外のアイテムを入れておく仮Dictionary
            Dictionary<string, int> dicItem = new Dictionary<string, int>();

            //レベルMax以外のアイテムの名前が入っているList
            List<string> nameItem = new List<string>();


            //枠に空きがあったら、LevelMax以外のすべてを選出
            if (_canvasManager.WeaponLayoutGroup.transform.childCount < _levelUpController.MaxItemAndWeaponNumbers)
            {
                //すべての武器のレベルを見て、Maxレベル以外のものを選出
                foreach (var n in weaponManger.WeaponNames)
                {
                    if (weaponManger.WeaponLevels[n].NowLevel + i < weaponManger.WeaponLevels[n].MaxLevel)
                    {
                        dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                        nameWeapon.Add(n);
                    }
                }
            }
            else
            {
                //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
                foreach (var n in weaponManger.OnUseWeapons)
                {
                    if (weaponManger.WeaponLevels[n].NowLevel + i < weaponManger.WeaponLevels[n].MaxLevel)
                    {
                        dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                        nameWeapon.Add(n);
                    }
                }
            }

            //上限までアイテムを保有していたら出さない
            if (_canvasManager.ItemLayoutGroup.transform.childCount < _levelUpController.MaxItemAndWeaponNumbers)
            {
                //すべてのアイテムのレベルを見て、Maxレベル以外のものを選出
                foreach (var n in itemData.ItemNames)
                {
                    if (itemData.ItemLevels[n].NowLevel + i < itemData.ItemLevels[n].MaxLevel)
                    {
                        dicItem.Add(n, itemData.ItemLevels[n].NowLevel);
                        nameItem.Add(n);
                    }
                }
            }
            else
            {
                //"現在使用しているアイテム"のアイテムのレベルを見て、Maxレベル以外のものを選出
                foreach (var n in itemData.OnUseItems)
                {
                    if (itemData.ItemLevels[n].NowLevel + i < itemData.ItemLevels[n].MaxLevel)
                    {
                        dicItem.Add(n, itemData.ItemLevels[n].NowLevel);
                        nameItem.Add(n);
                    }
                }
            }


            //アイテムと武器が何も無かったら、"その他(金、体力回復アイテム)"を出す
            if (nameWeapon.Count == 0 && nameItem.Count == 0)
            {
                _choiseItemInformationPanel.Add(_lastHealPanel);
                _lastHealIcons[0].transform.position = _boxIconPos[i].position;
                return;
            }

            //アイテム、武器のどちらを出すかを決める
            int randamItemOrWeapon = UnityEngine.Random.Range(0, 2);


            //目が武器か、アイテムが無い時
            if (randamItemOrWeapon == 0 || nameItem.Count == 0)
            {
                var r = UnityEngine.Random.Range(0, nameWeapon.Count);

                //武器の次のステータスを持ってくる。
                WeaponInforMaition weaponInformaition = weaponManger.weaponData.GetInfomaitionData((dicWeapon[nameWeapon[r]] + 1), nameWeapon[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _canvasManager.NameOfInformationPanel[nameWeapon[r]];
                panel.SetActive(true);
                panel.GetComponent<Button>().enabled = false;

                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel + 1).ToString();
                }

                _nowChoiseBoxWeaponOrItemName.Add(nameWeapon[r]);
                _canvasManager.NameOfIconPanelUseBox[nameWeapon[r]].transform.position = _boxIconPos[i].position;
                _choiseItemInformationPanel.Add(_canvasManager.NameOfInformationPanel[nameWeapon[r]]);

                Debug.Log("獲得したのは+" + nameWeapon[r]);
            }
            else if (randamItemOrWeapon == 1 || nameWeapon.Count == 0)
            {
                var r = UnityEngine.Random.Range(0, nameItem.Count);

                //アイテムの次のステータスを持ってくる。
                ItemInforMaition itemInformaition = itemData.ItemData.GetInfomaitionData(dicItem[nameItem[r]] + 1, nameItem[r]);

                //レベルアップする武器のパネルをだす
                GameObject panel = _canvasManager.NameOfInformationPanel[nameItem[r]];
                panel.SetActive(true);
                panel.GetComponent<Button>().enabled = false;
                //武器のパネルのTextを更新
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = itemInformaition.Te;

                //武器のパネルのレベルの表記Texttを更新
                if (itemData.ItemLevels[nameItem[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (itemData.ItemLevels[nameItem[r]].NowLevel + 1).ToString();
                }
                _nowChoiseBoxWeaponOrItemName.Add(nameItem[r]);

                //アイコンの位置を設定
                _canvasManager.NameOfIconPanelUseBox[nameItem[r]].transform.position = _boxIconPos[i].position;
                //パネルを設定
                _choiseItemInformationPanel.Add(_canvasManager.NameOfInformationPanel[nameItem[r]]);
                //出したパネルを保存しておくリストに追加(後で消すため)
                // instanciatePanels.Add(panel);

                Debug.Log("獲得したのは+" + nameItem[r]);
            }
        }

    }

    public void EndMove()
    {
        _isGetBox = false;
        _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;
        _choiseItemInformationPanel[0].SetActive(true);
    }

    public void BoxButtunGetItem()
    {
        //先頭を画面外に出し、リストから削除
        _choiseItemInformationPanel[0].transform.position = _posEndPos.position;


        if (!_type[_countGet])
        {
            _choiseItemInformationPanel[0].GetComponent<Button>().enabled = true;
        }


        _choiseItemInformationPanel[0].SetActive(false);
        _choiseItemInformationPanel.RemoveAt(0);


        //Listの中身が空ではないかどうかの確認
        if (_nowChoiseBoxWeaponOrItemName.Count > 0)
        {
            string n = _nowChoiseBoxWeaponOrItemName[0];
            _nowChoiseBoxWeaponOrItemName.RemoveAt(0);

            if (!_type[_countGet])
            {
                //武器かアイテムのレベルアップ
                if (itemData.ItemNames.Contains(n))
                {
                    _levelUpController.ItemLevelUp(n, 1);
                    _levelUpBaseItems[n].LevelUp();
                }
                else if (weaponManger.WeaponNames.Contains(n))
                {
                    _levelUpController.WeaponLevelUp(n, 1);
                    _levelUpBaseWeapons[n].LevelUp();
                }
            }
            else
            {
                weaponManger.Evolution(n);
            }
        }




        //演出を続行か終わりか判断
        if (_choiseItemInformationPanel.Count == 0)
        {
            _nowTimeLine = null;
            _pauseManager.PauseResumeLevelUp();
            _boxpanel.SetActive(false);
            //アイコンの位置を戻す
            foreach (var key in _canvasManager.NameOfIconPanelUseBox.Keys)
            {
                _canvasManager.NameOfIconPanelUseBox[key].transform.position = _posEndPos.position;
            }
            _lastHealIcons[0].transform.position = _posEndPos.position;

            _isOpen = false;
            if (_numRemaining > 0)
            {
                _numRemaining--;
                GetBox(_numRemainingGetNum[0]);
                _numRemainingGetNum.RemoveAt(0);
            }
        }
        else        //次の物をセット
        {
            _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;

            _countGet++;

            if (_type[_countGet])
            {
                _noGetButtun.SetActive(false);
            }
            else
            {
                _noGetButtun.SetActive(true);
            }
        }
    }

    public void BoxButtunNoGetItem()
    {
        //先頭を画面外に出し、リストから削除
        _choiseItemInformationPanel[0].transform.position = _posEndPos.position;
        _choiseItemInformationPanel[0].GetComponent<Button>().enabled = true;
        _choiseItemInformationPanel[0].SetActive(false);
        _choiseItemInformationPanel.RemoveAt(0);

        if (_nowChoiseBoxWeaponOrItemName.Count > 0) _nowChoiseBoxWeaponOrItemName.RemoveAt(0);

        //演出を続行か終わりか判断
        if (_choiseItemInformationPanel.Count == 0)
        {
            _nowTimeLine = null;
            _pauseManager.PauseResumeLevelUp();
            _boxpanel.SetActive(false);

            //アイコンの位置を戻す
            foreach (var key in _canvasManager.NameOfIconPanelUseBox.Keys)
            {

                _canvasManager.NameOfIconPanelUseBox[key].transform.position = _posEndPos.position;
            }
            _lastHealIcons[0].transform.position = _posEndPos.position;

            _choiseItemInformationPanel.Clear();
            _nowChoiseBoxWeaponOrItemName.Clear();

            _isOpen = false;
            if (_numRemaining > 0)
            {
                _numRemaining--;
                GetBox(_numRemainingGetNum[0]);
                _numRemainingGetNum.RemoveAt(0);
            }

        }
        else        //次の物をセット
        {
            _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;

            _countGet++;

            if (_type[_countGet])
            {
                _noGetButtun.SetActive(false);
            }
            else
            {
                _noGetButtun.SetActive(true);
            }
        }
    }
}
