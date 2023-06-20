using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpController : MonoBehaviour
{
    [Header("����A�A�C�e���̍ő及����")]
    [SerializeField] private int _maxItemAndWeaponNumbers = 0;

    /// <summary>
    /// ���x���A�b�v���ɑI������
    /// ���������p�l�����ꎞ�ۑ����Ă������X�g</summary>
    private List<GameObject> instanciatePanels = new List<GameObject>();

    /// <summary>�v���C���[�̃��x��</summary>
    private int _playerLevel = 1;
    /// <summary>���݂̑��o���l</summary>
    private float _exp;
    /// <summary>���̃��x���A�b�v�ɕK�v�Ȍo���l</summary>
    private float _nextLevelExp = 7;

    private float nextLevelUpPer = 1.5f;
    /// <summary>���x���̉��o�������</summary>
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

    // [Header("���x���A�b�v�̃A�C�e���̃��C�A�E�g�O���[�v")]
    // [SerializeField] LayoutGroup _itemLayoutGroupOnLevelUp;
    // public LayoutGroup ItemLayoutGroupOnLvelUp { get => _itemLayoutGroupOnLevelUp; }

    // [Header("���x���A�b�v�̕���̃��C�A�E�g�O���[�v")]
    // [SerializeField] LayoutGroup _weaponLayoutGroupOnLevelUp;
    // public LayoutGroup WeaponLayoutGroupOnLevelUp { get => _weaponLayoutGroupOnLevelUp; }

    private void Awake()
    {
        _aud = GetComponent<AudioSource>();
        _canvasManager.LevelUpSlider.maxValue = _nextLevelExp;
        _canvasManager.LevelUpSlider.minValue = 0;
    }

    /// <summary>�e���킩��ĂԁB���O�ƍō����x�����m�F����</summary>
    public void SetWeaponData(string weaponName, int maxLevel)
    {
        WeaponLevelData data = new WeaponLevelData(maxLevel, 0);
        weaponManger.WeaponNames.Add(weaponName);
        weaponManger.WeaponLevels.Add(weaponName, data);
    }

    /// <summary>�e�A�C�e������ĂԁB���O�ƍō����x�����m�F����</summary>
    public void SetItemData(string itemName, int maxLevel)
    {
        ItemLevelDate data = new ItemLevelDate(maxLevel, 0);
        itemData.ItemNames.Add(itemName);
        itemData.ItemLevels.Add(itemName, data);
    }

    /// <summary>�e���킩��ĂԁB����̃��x�����Đݒ�</summary>
    public void WeaponLevelUp(string name, int level)
    {
        weaponManger.WeaponLevels[name].NowLevel += 1;
        //�������킪���Q�b�g��������A���ݎg���Ă��镐��Ƃ��ċL�^����B
        if (weaponManger.WeaponLevels[name].NowLevel == 1)
        {
            //���ݎg���Ă��镐����L�^
            weaponManger.OnUseWeapons.Add(name);

            //���ݎg���Ă���A�C�R����ݒ�
            _canvasManager.NameOfIconPanelUseUI[name].transform.SetParent(_canvasManager.WeaponLayoutGroup.transform);
        }
    }

    /// <summary>�e�A�C�e������ĂԁB�A�C�e���̃��x�����Đݒ�</summary>
    public void ItemLevelUp(string name, int level)
    {
        itemData.ItemLevels[name].NowLevel += 1;
        //�����A�C�e�������Q�b�g��������A���ݎg���Ă���A�C�e���Ƃ��ċL�^����B
        if (itemData.ItemLevels[name].NowLevel == 1)
        {
            itemData.OnUseItems.Add(name);

            //���ݎg���Ă���A�C�R����ݒ�
            _canvasManager.NameOfIconPanelUseUI[name].transform.SetParent(_canvasManager.ItemLayoutGroup.transform);
        }
    }

    /// <summary>�o���l�Q�b�g</summary>
    /// <param name="exp">�����o���l�̗�</param>
    public void AddExp(float exp)
    {
        // Debug.Log("Get�����o���l��" + exp);
        //�����o���l�ɁA�o���l�{���������āA���݂̑��o���l�ɓ����o���l�𑫂�   
        var addExp = exp * _mainStatas.ExpUpper;
        _exp += addExp;

        //_sliderLevelUp.value += addExp;
        //���o���l���A���̃��x���A�b�v�o���l�𒴂�����A
        //�E���̃��x���A�b�v�o���l���グ�ă��x���A�b�v�̏����̉񐔂𑫂�
        while (_exp >= _nextLevelExp)
        {
            _exp -= _nextLevelExp;
            _nextLevelExp = _nextLevelExp * nextLevelUpPer;
            levelTass++;

            _canvasManager.LevelUpSlider.maxValue = _nextLevelExp;

        }

        //���x���A�b�v�̏����̉񐔂�0�ȏゾ�����珈�������s����B
        if (levelTass > 0)
        {
            playerLevelUp();
            levelTass--;
            Debug.Log("���܂ł̕K�v�o���l��" + _nextLevelExp);
        }

        _canvasManager.LevelUpSlider.value = _exp;
        //  Debug.Log("���݂̌o���l��" + _exp);
    }



    /// <summary>���x���A�b�v�̏���</summary>
    public void playerLevelUp()
    {
        if (!_pauseManager._isLevelUp)
        {
            _pauseManager.PauseResumeLevelUp();
        }

        _playerLevel++;
        _canvasManager.LevelText.text = "Lv:" + _playerLevel.ToString();
        Debug.Log("�v���C���[�̃��x�����オ�����I�I�I���݂̃��x��:" + _playerLevel);

        //���x��Max�ȊO�̕�������Ă�����Dictionary
        Dictionary<string, int> dicWeapon = new Dictionary<string, int>();

        //���x��Max�ȊO�̕���̖��O�������Ă���List
        List<string> nameWeapon = new List<string>();

        //���x��Max�ȊO�̃A�C�e�������Ă�����Dictionary
        Dictionary<string, int> dicItem = new Dictionary<string, int>();

        //���x��Max�ȊO�̃A�C�e���̖��O�������Ă���List
        List<string> nameItem = new List<string>();

        _canvasManager.LevelUpPanel.SetActive(true);

        //�g�ɋ󂫂���������ALevelMax�ȊO�̂��ׂĂ�I�o
        if (_canvasManager.WeaponLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //���ׂĂ̕���̃��x�������āAMax���x���ȊO�̂��̂�I�o
            foreach (var n in weaponManger.WeaponNames)
            {
                if (weaponManger.WeaponLevels[n].NowLevel < weaponManger.WeaponLevels[n].MaxLevel)
                {
                    dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }
        else//�󂫂�����������ALevelMax�ȊO��"�g���Ă�������̂���"��I�o
        {
            //"���ݎg�p���Ă���A�C�e��"�̃A�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
            foreach (var n in weaponManger.OnUseWeapons)
            {
                if (weaponManger.WeaponLevels[n].NowLevel < weaponManger.WeaponLevels[n].MaxLevel)
                {
                    dicWeapon.Add(n, weaponManger.WeaponLevels[n].NowLevel);
                    nameWeapon.Add(n);
                }
            }
        }

        //�g�ɋ󂫂���������ALevelMax�ȊO�̂��ׂĂ�I�o
        if (_canvasManager.ItemLayoutGroup.transform.childCount < _maxItemAndWeaponNumbers)
        {
            //���ׂẴA�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
            foreach (var n in itemData.ItemNames)
            {
                if (itemData.ItemLevels[n].NowLevel < itemData.ItemLevels[n].MaxLevel)
                {
                    dicItem.Add(n, itemData.ItemLevels[n].NowLevel);
                    nameItem.Add(n);
                }
            }
        }
        else//�󂫂�����������ALevelMax�ȊO��"�g���Ă�������̂���"��I�o
        {
            //"���ݎg�p���Ă���A�C�e��"�̃A�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
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

        //�R�I�������o��
        for (int i = 0; i < 3; i++)
        {
            //����A�A�C�e���ǂ��炩���A�J���X�g�����ꍇ�̏����B
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

                //����̎��̃X�e�[�^�X�������Ă���B
                WeaponInforMaition weaponInformaition = weaponManger.weaponData.GetInfomaitionData((dicWeapon[nameWeapon[r]] + 1), nameWeapon[r]);

                //���x���A�b�v���镐��̃p�l��������
                GameObject panel = _canvasManager.NameOfInformationPanel[nameWeapon[r]];
                panel.SetActive(true);


                //����̃p�l����Text���X�V
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                //����̃p�l���̃��x���̕\�LTextt���X�V
                if (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();

                    if (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel + 1 == weaponManger.WeaponLevels[nameWeapon[r]].MaxLevel) text2.text = "Level:Max";
                    else text2.text = "Level:" + (weaponManger.WeaponLevels[nameWeapon[r]].NowLevel + 1).ToString();
                }

                //����̃p�l�������C�A�E�g�O���[�v�̎q�I�u�W�F�N�g�ɂ���
                panel.transform.SetParent(_canvasManager.BasePanelLayoutGroup.transform);
                //�o�����p�l����ۑ����Ă������X�g�ɒǉ�(��ŏ�������)
                instanciatePanels.Add(panel);

                //�I���̏d���𖳂���
                dicWeapon.Remove(nameWeapon[r]);
                nameWeapon.RemoveAt(r);
            }
            else if ((randamItemOrWeapon == 1 || nameWeapon.Count == 0) && nameItem.Count != 0)
            {
                var r = Random.Range(0, nameItem.Count);

                //�A�C�e���̎��̃X�e�[�^�X�������Ă���B
                ItemInforMaition itemInformaition = itemData.ItemData.GetInfomaitionData(dicItem[nameItem[r]] + 1, nameItem[r]);

                //���x���A�b�v���镐��̃p�l��������
                GameObject panel = _canvasManager.NameOfInformationPanel[nameItem[r]];
                panel.SetActive(true);

                //����̃p�l����Text���X�V
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = itemInformaition.Te;

                //����̃p�l���̃��x���̕\�LTextt���X�V
                if (itemData.ItemLevels[nameItem[r]].NowLevel > 0)
                {
                    Debug.Log(itemData.ItemLevels[nameItem[r]].NowLevel);
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (itemData.ItemLevels[nameItem[r]].NowLevel + 1).ToString();
                }

                //����̃p�l�������C�A�E�g�O���[�v�̎q�I�u�W�F�N�g�ɂ���
                panel.transform.SetParent(_canvasManager.BasePanelLayoutGroup.transform);

                //�o�����p�l����ۑ����Ă������X�g�ɒǉ�(��ŏ�������)
                instanciatePanels.Add(panel);

                //�I���̏d���𖳂���
                dicItem.Remove(nameItem[r]);
                nameItem.RemoveAt(r);
            }
        }
        _aud.Play();
    }



    /// <summary>���x���A�b�v���I��������ɌĂԁB�p�l���ɂ���</summary>
    public void EndLevelUp()
    {
        _canvasManager.BasePanelLayoutGroup.transform.DetachChildren();

        //�X�e�[�g���u�Q�[�����v�ɕύX
        _gm._gameSituation = GameManager.GameSituation.InGame;
        //���o�p�̃p�l�����\���ɂ���
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


        //���x�����܂��o�����������x
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