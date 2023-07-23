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

    [SerializeField] private DebugMaker debugMaker;

    /// <summary>���ݎg���Ă��镐��̖��O</summary>
    private List<string> _onUseWeapons = new List<string>();

    /// <summary>����̖��O</summary>
    private List<string> _weaponNameT = new List<string>();

    /// <summary>���݂̎�������Ă��镐��Ƃ��̃��x��������</summary>
    Dictionary<string, WeaponLevelData> _weaponsLevel = new Dictionary<string, WeaponLevelData>();

    /// <summary>���݂̎�������Ă��镐��Ƃ��̐��������̃I�u�W�F�N�g�����Ă�</summary>
    Dictionary<string, InstantiateWeaponBase> _weapon = new Dictionary<string, InstantiateWeaponBase>();

    /// <summary>���݂̎�������Ă��镐��Ƃ���Scritabl�I�u�W�F�N�g�����Ă�</summary>
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
    /// ����̐i������
    /// </summary>
    /// <param name="name">�i�������镐��̖��O</param>
    public void Evolution(string name)
    {
        _weapon[name].Evolution(name);

        //�i����������̐ݒ�
        _canvasManager.NameOfIconPanelUseUI[name].transform.GetChild(0).GetComponent<Image>().sprite = _weaponScritables[name].EvolutionSprite;
    }

    /// <summary>
    /// �i���\�ȕ��킪���邩�ǂ������m�F����
    /// </summary>
    public string[] CheckWeaponCanEvolution()
    {
        List<string> canEvolutionweapon = new List<string>();

        //�i���ɃA�C�e��������Ȃ��ꍇ
        foreach (var a in _onUseWeapons)
        {
            //�i�����Ă������΂�
            if (_weapon[a].IsEvolution) continue;

            //����̃��x���A�b�v���ő� && �i���ɕK�v�ȃA�C�e���������Ă���
            if (_weaponsLevel[a].MaxLevel == _weaponsLevel[a].NowLevel)
            {
                canEvolutionweapon.Add(a);
            }
        }


        //�i���ɃA�C�e�����K�v�ȏꍇ
        //foreach (var a in _onUseWeapons)
        //{
        //    string needItemName = _weaponScritables[a].NeedEvolutionItem.ItemName;

        //    //�i�����Ă������΂�
        //    if (_weapon[a].IsEvolution) continue;

        //    //����̃��x���A�b�v���ő� && �i���ɕK�v�ȃA�C�e���������Ă���
        //    if (_weaponsLevel[a].MaxLevel == _weaponsLevel[a].NowLevel && _itemMnager.OnUseItems.Contains(needItemName))
        //    {
        //        //�K�v�ȃA�C�e����LevelMax
        //        if (_itemMnager.ItemLevels[needItemName].MaxLevel == _itemMnager.ItemLevels[needItemName].NowLevel)
        //        {
        //            canEvolutionweapon.Add(a);
        //        }
        //    }
        //}

        return canEvolutionweapon.ToArray();
    }

    /// <summary>
    /// ���킪�i�����Ă��邩�ǂ������m�F����
    /// </summary>
    /// <param name="name">�i�����m�F����������</param>
    /// <returns>�i�����Ă��邩�ǂ���</returns>
    public bool CheckWeaponEvolution(string name)
    {
        return _weapon[name].IsEvolution;
    }

    /// <summary>
    /// �w�肵�����킪���x���A�b�vMax���ǂ�����Ԃ�
    /// </summary>
    /// <param name="name">�m�F����������̖��O</param>
    /// <returns>���x���A�b�vMax���ǂ���</returns>
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


        //�I�����̃p�l���̓o�^
        _uIMaker.PanelMake(weapon.WeaponName, weapon.Sprite);
        _uIMaker.Panel[weapon.WeaponName].TryGetComponent<Button>(out Button button);
        button.onClick.AddListener(weaponBase.LevelUp); //����̃��x���A�b�v�̏������{�^���ɓo�^  
        button.onClick.AddListener(_levelUpController.EndLevelUp); //���x���A�b�v�I���̏������{�^���ɓo�^

        var debug = Instantiate(debugMaker._buttun);
        debug.transform.SetParent(debugMaker._weaponLayoutGroup.transform);
        debug.onClick.AddListener(weaponBase.DebugLevelUp); //����̃��x���A�b�v�̏������{�^���ɓo�^  
        debug.transform.GetChild(0).GetComponent<Text>().text = weapon.WeaponName;

        //����̎��̃X�e�[�^�X�������Ă���B
        WeaponInforMaition weaponInformaition = _weaponData.GetInfomaitionData(weapon.MaxLevel + 1, weapon.WeaponName);

        //�i����̃p�l���̓o�^
        _uIMaker.EvolutionPanel(weapon.WeaponName, weapon.evolutionWeaponName, weaponInformaition.Te, weapon.EvolutionSprite);

        //Box�p�̃A�C�R���𐶐�
        _uIMaker.BoxIconMake(weapon.WeaponName, weapon.Sprite);

        //Box�p�̐i���A�C�R���𐶐�
        _uIMaker.EvolutionIcon(weapon.WeaponName, weapon.EvolutionSprite);

        //UI�p�̃A�C�R�����쐬
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