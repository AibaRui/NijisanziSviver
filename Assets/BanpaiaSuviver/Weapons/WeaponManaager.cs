using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �Q�[�����Ŏg�������S�Ď����Ă���N���X
/// </summary>
public class WeaponManaager : MonoBehaviour
{
    [Header("�Q�[�����ł�������")]
    [SerializeField] private List<ScritablWeapon> _useWeapons = new List<ScritablWeapon>();

    [Header("�����̕���")]
    [SerializeField] private ScritablWeapon _charactorOnlrWeapon;

    [Header("�v���C���[")]
    [SerializeField] private GameObject _player;


    /// <summary>���ݎg���Ă��镐��̖��O</summary>
    private List<string> _onUseWeapons = new List<string>();

    /// <summary>����̖��O</summary>
    private List<string> _weaponNameT = new List<string>();

    /// <summary>���݂̎�������Ă��镐��Ƃ��̃��x��������</summary>
    Dictionary<string, WeaponLevelData> _weaponsLevel = new Dictionary<string, WeaponLevelData>();

    public Dictionary<string, WeaponLevelData> WeaponLevels { get => _weaponsLevel; set => _weaponsLevel = value; }
    public List<string> WeaponNames { get => _weaponNameT; set => _weaponNameT = value; }

    public List<string> OnUseWeapons { get => _onUseWeapons; set => _onUseWeapons = value; }

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

        //�d�l�L�����̌ŗL�����o�^
        WeaponSetting(_charactorOnlrWeapon);

        //�g������̏���o�^���Ă���
        foreach (var a in _useWeapons)
        {
            WeaponSetting(a);
        }
        _firstWeapon.LevelUp();
    }

    private void WeaponSetting(ScritablWeapon weapon)
    {
        //�g������𐶐�
        var go = Instantiate(weapon.InstanceObject);

        //�ϐ��̏�����
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


        //�{�^���̐ݒ�
        var panel = Instantiate(weapon.LevelUpButtun);
        panel.TryGetComponent<Button>(out Button button);
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        //����̃��x���A�b�v�̏������{�^���ɓo�^
        button.onClick.AddListener(weaponBase.LevelUp);
        //���x���A�b�v�I���̏������{�^���ɓo�^
        button.onClick.AddListener(_levelUpController.EndLevelUp);

        panel.SetActive(false);

        //Box�p�̃A�C�R���𐶐�
        var boxIcon = Instantiate(weapon.IconBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //UI�p�̃A�C�R���𐶐�
        var icon = Instantiate(weapon.IconInGameUI);

        //�A�C�R���̐ݒ�
        _canvasManager.NameOfIconPanelUseBox.Add(weapon.WeaponName, boxIcon);
        _canvasManager.NameOfIconPanelUseUI.Add(weapon.WeaponName, icon);
        _canvasManager.NameOfInformationPanel.Add(weapon.WeaponName, panel);


        _levelUpController.SetWeaponData(weapon.WeaponName, weapon.MaxLevel);

        _boxControl.SetWeapon(weapon.WeaponName, weaponBase);
    }

}
