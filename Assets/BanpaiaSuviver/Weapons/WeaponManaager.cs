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

    [SerializeField] private WeaponData _weaponData;
    [SerializeField] private LevelUpController _levelUpController;
    [SerializeField] private BoxControl _boxControl;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private MainStatas _mainStatas;
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private ObjectPool _objectPool;

    public WeaponData weaponData => _weaponData;
    private InstantiateWeaponBase _firstWeapon;

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

        weaponBase.ObjectPool = _objectPool;
        weaponBase.PauseManager = _pauseManager;
        weaponBase.MainStatas = _mainStatas;
        weaponBase.WeaponData = _weaponData;
        weaponBase.BoxControl = _boxControl;
        weaponBase.LevelUpController = _levelUpController;
        weaponBase.Player = _player;

        weaponBase.Init(weapon.WeaponName, weapon.MaxLevel);

            _weaponData.BuildLevelUpTable(weapon.WeaponName, weapon.LevelData, weapon.InfoData);


        //ボタンの設定
        var panel = Instantiate(weapon.LevelUpButtun);
        panel.TryGetComponent<Button>(out Button button);
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        //武器のレベルアップの処理をボタンに登録
        button.onClick.AddListener(weaponBase.LevelUp);
        //レベルアップ終了の処理をボタンに登録
        button.onClick.AddListener(_levelUpController.EndLevelUp);

        //Box用のアイコンを生成
        var boxIcon = Instantiate(weapon.IconBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //アイコンの設定
        _canvasManager.NameOfIconPanelUseBox.Add(weapon.WeaponName, boxIcon);
        _canvasManager.NameOfIconPanelUseUI.Add(weapon.WeaponName, weapon.IconInGameUI);
        _canvasManager.NameOfInformationPanel.Add(weapon.WeaponName, panel);


        _levelUpController.SetWeaponData(weapon.WeaponName, weapon.MaxLevel);

        _boxControl.SetWeapon(weapon.WeaponName, weaponBase);
    }

}
