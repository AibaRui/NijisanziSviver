using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMaker : MonoBehaviour
{
    [Header("�p�l���̌�")]
    [SerializeField] private GameObject _panelBase;

    [Header("�i���p�l���̌�")]
    [SerializeField] private GameObject _evolutionPanelBase;

    [Header("UI�̃A�C�R���̌�")]
    [SerializeField] private GameObject _iconBase;

    [Header("Box�̃A�C�R���̌�")]
    [SerializeField] private GameObject _boxIconBase;

    [Header("����̕���A�A�C�e����Level������Text")]
    [SerializeField] private TextMeshProUGUI  _levelTextMeshPro;

    [Header("����̕���A�A�C�e����Level������Text��OffSet")]
    [SerializeField] private Vector2 _levelTextMeshProOffSet = new Vector2(0, -17);


    [SerializeField] private BoxControl _boxControl;
    [SerializeField] private CanvasManager _canvasManager;

    /// <summary>�I�����̃p�l��</summary>
    private Dictionary<string, GameObject> _panel = new Dictionary<string, GameObject>();
    /// <summary>UI�p�̃A�C�R��</summary>
    private Dictionary<string, GameObject> _uIIcon = new Dictionary<string, GameObject>();
    /// <summary>Box�p�̃A�C�R��</summary>
    private Dictionary<string, GameObject> _boxIcon = new Dictionary<string, GameObject>();

    public Dictionary<string, GameObject> Panel { get => _panel; set => _panel = value; }
    public Dictionary<string, GameObject> UIIcon { get => _uIIcon; set => _uIIcon = value; }
    public Dictionary<string, GameObject> BoxIcon { get => _boxIcon; set => _boxIcon = value; }

    void Start()
    {

    }


    void Update()
    {

    }

    public void MakeLevelText(bool isWepaon, int num,string name)
    {
        var go = Instantiate(_levelTextMeshPro);
        _canvasManager.LevelTextOnItemAndWeapon.Add(name, go);

        if (isWepaon)
        {
            go.transform.SetParent(_canvasManager.WeaponUIPos[num - 1]);
        }
        else
        {
            go.transform.SetParent(_canvasManager.ItemUIPos[num - 1]);
        }
        go.transform.localPosition = _levelTextMeshProOffSet;
    }


    public void PanelMake(string name, Sprite sprite)
    {
        //�{�^���̐ݒ�
        var panel = Instantiate(_panelBase);
        panel.transform.GetChild(4).GetComponent<Image>().sprite = sprite;
        panel.transform.GetChild(5).GetComponent<Text>().text = name;
        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        _canvasManager.NameOfInformationPanel.Add(name, panel);
        panel.SetActive(false);

        Panel.Add(name, panel);
    }

    public void UIIconMake(string name, Sprite sprite)
    {
        var icon = Instantiate(_iconBase);
        icon.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        _canvasManager.NameOfIconPanelUseUI.Add(name, icon);
    }


    public void BoxIconMake(string name, Sprite sprite)
    {
        var boxIcon = Instantiate(_boxIconBase);
        boxIcon.GetComponent<Image>().sprite = sprite;

        boxIcon.transform.SetParent(_boxControl.IconParentObject);

        //�A�C�R���̐ݒ�
        _canvasManager.NameOfIconPanelUseBox.Add(name, boxIcon);
    }

    public void EvolutionIcon(string name, Sprite sprite)
    {
        //Box�p�̐i���A�C�R���𐶐�
        var boxIconEvoluton = Instantiate(_boxIconBase);
        boxIconEvoluton.GetComponent<Image>().sprite = sprite;
        _canvasManager._NameOfEvolutionWeaponIconBox.Add(name, boxIconEvoluton);

        boxIconEvoluton.transform.SetParent(_boxControl.IconParentObject);
    }

    public void EvolutionPanel(string name, string weaponName, string data, Sprite sprite)
    {
        var panel = Instantiate(_evolutionPanelBase);
        panel.transform.GetChild(4).GetComponent<Image>().sprite = sprite;
        panel.transform.GetChild(5).GetComponent<Text>().text = weaponName;


        //����̃p�l����Text���X�V
        var text = panel.transform.GetChild(6).GetComponent<Text>();
        text.text = data;

        panel.transform.SetParent(_canvasManager.OrizinCanvus);
        _canvasManager.NameOfEvolutionWeaponPanel.Add(name, panel);
        panel.SetActive(false);
    }

}
