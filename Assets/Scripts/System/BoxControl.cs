using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Playables;

public class BoxControl : MonoBehaviour
{
    [Header("���o���̃A�C�R�����o���ꏊ")]
    [Tooltip("���o���̃A�C�R�����o���ꏊ")] [SerializeField] Transform[] _boxIconPos = new Transform[5];

    [Header("�A�C�e�����̏ڍ׃p�l�����o���ꏊ")]
    [Tooltip("�A�C�e�����̏ڍ׃p�l�����o���ꏊ")] [SerializeField] Transform _boxInformationPanelPos;

    [Header("���o�̊��p�l��")]
    [Tooltip("���o�̊��p�l��")] [SerializeField] GameObject _boxpanel;

    [Header("�g���I������p�l����u����ʊO�̈ʒu")]
    [Tooltip("�g���I������p�l����u����ʊO�̈ʒu")] [SerializeField] Transform _posEndPos;

    [Header("�S�I����́A�񕜃p�l��")]
    [Tooltip("�S�I����́A�񕜃p�l��")] [SerializeField] GameObject _lastHealPanel;

    [Header("�S�I����́A�񕜃A�C�R��")]
    [Tooltip("�S�I����́A�񕜃A�C�R��")] [SerializeField] List<GameObject> _lastHealIcons = new List<GameObject>();


    //���ݎg�p���Ă���A�C�e�����̏ڍ׃p�l��
    List<GameObject> _choiseItemInformationPanel = new List<GameObject>();
    //���ݎg�p���Ă���A�C�e�����̖��O
    List<string> _nowChoiseBoxWeaponOrItemName = new List<string>();

    Dictionary<string, InstantiateWeaponBase> _levelUpBaseWeapons = new Dictionary<string, InstantiateWeaponBase>();
    Dictionary<string, ItemBase> _levelUpBaseItems = new Dictionary<string, ItemBase>();

    private bool _isGetBox = false;


    [SerializeField] List<GameObject> _timeline = new List<GameObject>();
    PlayableDirector _nowTimeLine = default;
    [SerializeField] WeaponData weaponManger;
    [SerializeField] ItemData itemData;
    [SerializeField] LevelUpController _levelUpController;
    [SerializeField] PauseManager _pauseManager;

    //���݊J���Ă��邩
    private bool _isOpen;
    //�̂���񂷉�
    private int _numRemaining;

    private List<int> _numRemainingGetNum = new List<int>();

    void OnEnable()
    {
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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

    /// <summary>�󔠂���������̏���</summary>
    public void GetBox(int num)
    {
        if (_isOpen)
        {
            _numRemaining++;
            _numRemainingGetNum.Add(num);
            return;
        }
        _isOpen = true;

        //���o�^�C�����C���������_���ōĐ�
        var randomT = UnityEngine.Random.Range(0, _timeline.Count);
        _timeline[randomT].SetActive(true);
        _nowTimeLine = _timeline[randomT].GetComponent<PlayableDirector>();

        _pauseManager.PauseResumeLevelUp();
        _boxpanel.SetActive(true);
        _isGetBox = true;

        //�󔠂̌���������
        for (int i = 0; i < num; i++)
        {
            //���x��Max�ȊO�̕�������Ă�����Dictionary
            Dictionary<string, int> dicWeapon = new Dictionary<string, int>();

            //���x��Max�ȊO�̕���̖��O�������Ă���List
            List<string> nameWeapon = new List<string>();

            //���x��Max�ȊO�̃A�C�e�������Ă�����Dictionary
            Dictionary<string, int> dicItem = new Dictionary<string, int>();

            //���x��Max�ȊO�̃A�C�e���̖��O�������Ă���List
            List<string> nameItem = new List<string>();


            //�g�ɋ󂫂���������ALevelMax�ȊO�̂��ׂĂ�I�o
            if (_levelUpController.WeaponLayoutGroup.transform.childCount < _levelUpController.MaxItemAndWeaponNumbers)
            {
                //���ׂĂ̕���̃��x�������āAMax���x���ȊO�̂��̂�I�o
                foreach (var n in _levelUpController.WeaponNames)
                {
                    if (_levelUpController.WeaponLevels[n].NowLevel + i < _levelUpController.WeaponLevels[n].MaxLevel)
                    {
                        dicWeapon.Add(n, _levelUpController.WeaponLevels[n].NowLevel);
                        nameWeapon.Add(n);
                    }
                }
            }
            else
            {
                //"���ݎg�p���Ă���A�C�e��"�̃A�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
                foreach (var n in _levelUpController.OnUseWeapons)
                {
                    if (_levelUpController.WeaponLevels[n].NowLevel + i < _levelUpController.WeaponLevels[n].MaxLevel)
                    {
                        dicWeapon.Add(n, _levelUpController.WeaponLevels[n].NowLevel);
                        nameWeapon.Add(n);
                    }
                }
            }

            //����܂ŃA�C�e����ۗL���Ă�����o���Ȃ�
            if (_levelUpController.ItemLayoutGroup.transform.childCount < _levelUpController.MaxItemAndWeaponNumbers)
            {
                //���ׂẴA�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
                foreach (var n in _levelUpController.ItemNames)
                {
                    if (_levelUpController.ItemLevels[n].NowLevel + i < _levelUpController.ItemLevels[n].MaxLevel)
                    {
                        dicItem.Add(n, _levelUpController.ItemLevels[n].NowLevel);
                        nameItem.Add(n);
                    }
                }
            }
            else
            {
                //"���ݎg�p���Ă���A�C�e��"�̃A�C�e���̃��x�������āAMax���x���ȊO�̂��̂�I�o
                foreach (var n in _levelUpController.OnUseItems)
                {
                    if (_levelUpController.ItemLevels[n].NowLevel + i < _levelUpController.ItemLevels[n].MaxLevel)
                    {
                        dicItem.Add(n, _levelUpController.ItemLevels[n].NowLevel);
                        nameItem.Add(n);
                    }
                }
            }


            //�A�C�e���ƕ��킪��������������A"���̑�(���A�̗͉񕜃A�C�e��)"���o��
            if (nameWeapon.Count == 0 && nameItem.Count == 0)
            {
                _choiseItemInformationPanel.Add(_lastHealPanel);
                _lastHealIcons[0].transform.position = _boxIconPos[i].position;
                return;
            }

            //�A�C�e���A����̂ǂ�����o���������߂�
            int randamItemOrWeapon = UnityEngine.Random.Range(0, 2);


            //�ڂ����킩�A�A�C�e����������
            if (randamItemOrWeapon == 0 || nameItem.Count == 0)
            {
                var r = UnityEngine.Random.Range(0, nameWeapon.Count);

                //����̎��̃X�e�[�^�X�������Ă���B
                WeaponInforMaition weaponInformaition = weaponManger.GetInfomaitionData((dicWeapon[nameWeapon[r]] + 1), nameWeapon[r]);

                //���x���A�b�v���镐��̃p�l��������
                GameObject panel = _levelUpController.NameOfInformationPanel[nameWeapon[r]];
                panel.SetActive(true);
                panel.GetComponent<Button>().enabled = false;

                //����̃p�l����Text���X�V
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = weaponInformaition.Te;

                //����̃p�l���̃��x���̕\�LTextt���X�V
                if (_levelUpController.WeaponLevels[nameWeapon[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (_levelUpController.WeaponLevels[nameWeapon[r]].NowLevel + 1).ToString();
                }

                _nowChoiseBoxWeaponOrItemName.Add(nameWeapon[r]);
                _levelUpController.NameOfIconPanelUseBox[nameWeapon[r]].transform.position = _boxIconPos[i].position;
                _choiseItemInformationPanel.Add(_levelUpController.NameOfInformationPanel[nameWeapon[r]]);

                Debug.Log("�l�������̂�+"+nameWeapon[r]);
            }
            else if (randamItemOrWeapon == 1 || nameWeapon.Count == 0)
            {
                var r = UnityEngine.Random.Range(0, nameItem.Count);

                //�A�C�e���̎��̃X�e�[�^�X�������Ă���B
                ItemInforMaition itemInformaition = itemData.GetInfomaitionData(dicItem[nameItem[r]] + 1, nameItem[r]);

                //���x���A�b�v���镐��̃p�l��������
                GameObject panel = _levelUpController.NameOfInformationPanel[nameItem[r]];
                panel.SetActive(true);
                panel.GetComponent<Button>().enabled = false;
                //����̃p�l����Text���X�V
                var text = panel.transform.GetChild(6).GetComponent<Text>();
                text.text = itemInformaition.Te;

                //����̃p�l���̃��x���̕\�LTextt���X�V
                if (_levelUpController.ItemLevels[nameItem[r]].NowLevel > 0)
                {
                    var text2 = panel.transform.GetChild(0).GetComponent<Text>();
                    text2.text = "Level:" + (_levelUpController.ItemLevels[nameItem[r]].NowLevel + 1).ToString();
                }
                _nowChoiseBoxWeaponOrItemName.Add(nameItem[r]);

                //�A�C�R���̈ʒu��ݒ�
                _levelUpController.NameOfIconPanelUseBox[nameItem[r]].transform.position = _boxIconPos[i].position;
                //�p�l����ݒ�
                _choiseItemInformationPanel.Add(_levelUpController.NameOfInformationPanel[nameItem[r]]);
                //�o�����p�l����ۑ����Ă������X�g�ɒǉ�(��ŏ�������)
                // instanciatePanels.Add(panel);

                Debug.Log("�l�������̂�+" + nameItem[r]);
            }
        }

    }

    public void EndMove()
    {
        _isGetBox = false;
        _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;
    }

    public void BoxButtunGetItem()
    {
        //�擪����ʊO�ɏo���A���X�g����폜
        _choiseItemInformationPanel[0].transform.position = _posEndPos.position;
        _choiseItemInformationPanel[0].GetComponent<Button>().enabled = true;
        _choiseItemInformationPanel[0].SetActive(false);
        _choiseItemInformationPanel.RemoveAt(0);

        //List�̒��g����ł͂Ȃ����ǂ����̊m�F
        if (_nowChoiseBoxWeaponOrItemName.Count > 0)
        {
            string n = _nowChoiseBoxWeaponOrItemName[0];
            _nowChoiseBoxWeaponOrItemName.RemoveAt(0);

            //���킩�A�C�e���̃��x���A�b�v
            if (_levelUpController.ItemNames.Contains(n))
            {
                _levelUpController.ItemLevelUp(n, 1);
                _levelUpBaseItems[n].LevelUp();
            }
            else if (_levelUpController.WeaponNames.Contains(n))
            {
                _levelUpController.WeaponLevelUp(n, 1);
                _levelUpBaseWeapons[n].LevelUp();
            }
        }




        //���o�𑱍s���I��肩���f
        if (_choiseItemInformationPanel.Count == 0)
        {
            _nowTimeLine = null;
            _pauseManager.PauseResumeLevelUp();
            _boxpanel.SetActive(false);
            //�A�C�R���̈ʒu��߂�
            foreach (var key in _levelUpController.NameOfIconPanelUseBox.Keys)
            {
                _levelUpController.NameOfIconPanelUseBox[key].transform.position = _posEndPos.position;
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
        else        //���̕����Z�b�g
        {
            _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;
        }
    }

    public void BoxButtunNoGetItem()
    {
        //�擪����ʊO�ɏo���A���X�g����폜
        _choiseItemInformationPanel[0].transform.position = _posEndPos.position;
        _choiseItemInformationPanel[0].GetComponent<Button>().enabled = true;
        _choiseItemInformationPanel[0].SetActive(false);
        _choiseItemInformationPanel.RemoveAt(0);

        if (_nowChoiseBoxWeaponOrItemName.Count > 0)  _nowChoiseBoxWeaponOrItemName.RemoveAt(0);

        //���o�𑱍s���I��肩���f
        if (_choiseItemInformationPanel.Count == 0)
        {
            _nowTimeLine = null;
            _pauseManager.PauseResumeLevelUp();
            _boxpanel.SetActive(false);

            //�A�C�R���̈ʒu��߂�
            foreach (var key in _levelUpController.NameOfIconPanelUseBox.Keys)
            {

                _levelUpController.NameOfIconPanelUseBox[key].transform.position = _posEndPos.position;
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
        else        //���̕����Z�b�g
        {
            _choiseItemInformationPanel[0].transform.position = _boxInformationPanelPos.transform.position;
        }
    }
}