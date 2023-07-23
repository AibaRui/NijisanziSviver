using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// ゲーム内で使う武器を全て持っているクラス
/// </summary>
public class WeaponManaager : MonoBehaviour
{
    [Header("ゲーム内でつかう武器")]
    [SerializeField] private List<ScritablWeapon> _useWeapons = new List<ScritablWeapon>();

    [Header("初期の武器")]
    [SerializeField] private ScritablWeapon _charactorOnlrWeapon;

    [Header("プレイヤー")]
    [SerializeField] private GameObject _player;

    [SerializeField] private DebugMaker debugMaker;

    /// <summary>現在使っている武器の名前</summary>
    private List<string> _onUseWeapons = new List<string>();

    /// <summary>武器の名前</summary>
    private List<string> _weaponNameT = new List<string>();

    /// <summary>現在の実装されている武器とそのレベルを入れる</summary>
    Dictionary<string, WeaponLevelData> _weaponsLevel = new Dictionary<string, WeaponLevelData>();

    /// <summary>現在の実装されている武器とその生成処理のオブジェクトを入れてる</summary>
    Dictionary<string, InstantiateWeaponBase> _weapon = new Dictionary<string, InstantiateWeaponBase>();

    /// <summary>現在の実装されている武器とそのScritablオブジェクトを入れてる</summary>
    Dictionary<string, ScritablWeapon> _weaponScritables = new Dictionary<string, ScritablWeapon>();

    public Dictionary<string, WeaponLevelData> WeaponLevels { get => _weaponsLevel; set => _weaponsLevel = value; }
    public List<string> WeaponNames { get => _weaponNameT; set => _weaponNameT = value; }

    public List<string> OnUseWeapons { get => _onUseWeapons; set => _onUseWeapons = value; }

    [SerializeField] private ItemManager _itemMnager;
    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private LevelUpController _levelUpController;
    [SerializeField] private BoxControl _boxControl;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private MainStatas _mainStatas;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private UIMaker _uIMaker;
    public WeaponData weaponData => _weaponData;
    private InstantiateWeaponBase _firstWeapon;



    /// <summary>
    /// 武器の進化処理
    /// </summary>
    /// <param name="name">進化させる武器の名前</param>
    public void Evolution(string name)
    {
        _weapon[name].Evolution(name);

        //進化した武器の設定
        _canvasManager.NameOfIconPanelUseUI[name].transform.GetChild(0).GetComponent<Image>().sprite = _weaponScritables[name].EvolutionSprite;
    }

    /// <summary>
    /// 進化可能な武器があるかどうかを確認する
    /// </summary>
    public string[] CheckWeaponCanEvolution()
    {
        List<string> canEvolutionweapon = new List<string>();

        //進化にアイテムがいらない場合
        foreach (var a in _onUseWeapons)
        {
            //進化していたら飛ばす
            if (_weapon[a].IsEvolution) continue;

            //武器のレベルアップが最大 && 進化に必要なアイテムを持っている
            if (_weaponsLevel[a].MaxLevel == _weaponsLevel[a].NowLevel)
            {
                canEvolutionweapon.Add(a);
            }
        }


        //進化にアイテムが必要な場合
        //foreach (var a in _onUseWeapons)
        //{
        //    string needItemName = _weaponScritables[a].NeedEvolutionItem.ItemName;

        //    //進化していたら飛ばす
        //    if (_weapon[a].IsEvolution) continue;

        //    //武器のレベルアップが最大 && 進化に必要なアイテムを持っている
        //    if (_weaponsLevel[a].MaxLevel == _weaponsLevel[a].NowLevel && _itemMnager.OnUseItems.Contains(needItemName))
        //    {
        //        //必要なアイテムがLevelMax
        //        if (_itemMnager.ItemLevels[needItemName].MaxLevel == _itemMnager.ItemLevels[needItemName].NowLevel)
        //        {
        //            canEvolutionweapon.Add(a);
        //        }
        //    }
        //}

        return canEvolutionweapon.ToArray();
    }

    /// <summary>
    /// 武器が進化しているかどうかを確認する
    /// </summary>
    /// <param name="name">進化を確認したい武器</param>
    /// <returns>進化しているかどうか</returns>
    public bool CheckWeaponEvolution(string name)
    {
        return _weapon[name].IsEvolution;
    }

    /// <summary>
    /// 指定した武器がレベルアップMaxかどうかを返す
    /// </summary>
    /// <param name="name">確認したい武器の名前</param>
    /// <returns>レベルアップMaxかどうか</returns>
    public bool CheckLevel(string name)
    {
        if (_weaponsLevel[name].MaxLevel > _weaponsLevel[name].NowLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        _weaponData.Init(this);

        //仕様キャラの固有武器を登録
        WeaponSetting(_charactorOnlrWeapon);

        //使う武器の情報を登録していく
        foreach (var a in _useWeapons)
        {
            WeaponSetting(a);
        }
        _firstWeapon.LevelUp();
    }

    private void WeaponSetting(ScritablWeapon weapon)
    {
        //使う武器を生成
        var go = Instantiate(weapon.InstanceObject);

        //変数の初期化
        go.TryGetComponent<InstantiateWeaponBase>(out InstantiateWeaponBase weaponBase);

        if (weapon == _charactorOnlrWeapon)
        {
            _firstWeapon = weaponBase;
        }

        _weaponScritables.Add(weapon.WeaponName, weapon);
        _weapon.Add(weapon.WeaponName, weaponBase);

        weaponBase.WeaponManager = this;
        weaponBase.ObjectPool = _objectPool;
        weaponBase.PauseManager = _pauseManager;
        weaponBase.MainStatas = _mainStatas;
        weaponBase.WeaponData = _weaponData;
        weaponBase.BoxControl = _boxControl;
        weaponBase.LevelUpController = _levelUpController;
        weaponBase.Player = _player;

        weaponBase.Init(weapon.WeaponName, weapon.MaxLevel);

        _weaponData.BuildLevelUpTable(weapon.WeaponName, weapon.LevelData, weapon.InfoData);


        //選択肢のパネルの登録
        _uIMaker.PanelMake(weapon.WeaponName, weapon.Sprite);
        _uIMaker.Panel[weapon.WeaponName].TryGetComponent<Button>(out Button button);
        button.onClick.AddListener(weaponBase.LevelUp); //武器のレベルアップの処理をボタンに登録  
        button.onClick.AddListener(_levelUpController.EndLevelUp); //レベルアップ終了の処理をボタンに登録

        var debug = Instantiate(debugMaker._buttun);
        debug.transform.SetParent(debugMaker._weaponLayoutGroup.transform);
        debug.onClick.AddListener(weaponBase.DebugLevelUp); //武器のレベルアップの処理をボタンに登録  
        debug.transform.GetChild(0).GetComponent<Text>().text = weapon.WeaponName;

        //武器の次のステータスを持ってくる。
        WeaponInforMaition weaponInformaition = _weaponData.GetInfomaitionData(weapon.MaxLevel + 1, weapon.WeaponName);

        //進化後のパネルの登録
        _uIMaker.EvolutionPanel(weapon.WeaponName, weapon.evolutionWeaponName, weaponInformaition.Te, weapon.EvolutionSprite);

        //Box用のアイコンを生成
        _uIMaker.BoxIconMake(weapon.WeaponName, weapon.Sprite);

        //Box用の進化アイコンを生成
        _uIMaker.EvolutionIcon(weapon.WeaponName, weapon.EvolutionSprite);

        //UI用のアイコンを作成
        _uIMaker.UIIconMake(weapon.WeaponName, weapon.Sprite);





        _levelUpController.SetWeaponData(weapon.WeaponName, weapon.MaxLevel);

        _boxControl.SetWeapon(weapon.WeaponName, weaponBase);
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