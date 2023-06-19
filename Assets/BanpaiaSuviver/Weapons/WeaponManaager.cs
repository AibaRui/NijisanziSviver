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

        //Box�p�̃A�C�R���𐶐�
        var boxIcon = Instantiate(weapon.IconBox);
        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //�A�C�R���̐ݒ�
        _canvasManager.NameOfIconPanelUseBox.Add(weapon.WeaponName, boxIcon);
        _canvasManager.NameOfIconPanelUseUI.Add(weapon.WeaponName, weapon.IconInGameUI);
        _canvasManager.NameOfInformationPanel.Add(weapon.WeaponName, panel);


        _levelUpController.SetWeaponData(weapon.WeaponName, weapon.MaxLevel);

        _boxControl.SetWeapon(weapon.WeaponName, weaponBase);
    }

}
