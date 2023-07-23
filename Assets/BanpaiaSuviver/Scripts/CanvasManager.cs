using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    [Header("�ڍ׃p�l���̌���Canvas")]
    [SerializeField] Transform _orizinCanvus;

    [Header("�Q�[����ʍ���̏����A�C�e����")]
    [SerializeField] private List<Transform> _itemUIPos = new List<Transform>();

    [Header("�Q�[����ʍ���̏������헓")]
    [SerializeField] private List<Transform> _weaponUIPos = new List<Transform>();

    [Header("����A�C�e���I���{�^�����A�g���I��������ɒu���Ă����ꏊ")]
    [SerializeField] Transform _endPos;

    [Header("���x����Text")]
    [SerializeField] Text _levelText;

    [Header("���x���A�b�v�̃X���C�_�[")]
    [SerializeField] Slider _sliderLevelUp;

    [Header("�����Ɖ񕜂̃p�l��")]
    [Tooltip("�����Ɖ񕜂̃p�l��")] [SerializeField] List<GameObject> _lastPanel = new List<GameObject>();

    [Header("���x���A�b�v�̂������Ƃ̃p�l��")]
    [Tooltip("���x���A�b�v�̂������Ƃ̃p�l��")] [SerializeField] GameObject _basePanel;

    [Header("���C�A�E�g�O���[�v")]
    [Tooltip("���C�A�E�g�O���[�v")] [SerializeField] LayoutGroup _basePanelLayoutGroup;


    /// <summary>Box�p�B����A�A�C�e���̖��O��Key�ɂƂ�A�C�R���̉摜������</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseBox = new Dictionary<string, GameObject>();

    /// <summary>�B����A�A�C�e���̖��O��Key�ɂƂ�A�C�R���̉摜������</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseUI = new Dictionary<string, GameObject>();
    /// <summary>����A�A�C�e���̖��O��Key�ɂƂ�p�l���摜������</summary>
    Dictionary<string, GameObject> _nameOfInformationPanel = new Dictionary<string, GameObject>();

    /// <summary>�i������̃p�l��</summary>
    Dictionary<string,GameObject> _nameOfEvolutionWeaponPanel = new Dictionary<string, GameObject>();
    /// <summary>Box�p�B�i������̃A�C�R��</summary>
    Dictionary<string, GameObject> _nameOfEvolutionWeaponIconBox = new Dictionary<string, GameObject>();


    Dictionary<string, TextMeshProUGUI> _levelTextOnItemAndWeapon = new Dictionary<string, TextMeshProUGUI>();

    public Dictionary<string, TextMeshProUGUI> LevelTextOnItemAndWeapon = new Dictionary<string, TextMeshProUGUI>();
    public Dictionary<string, GameObject> NameOfEvolutionWeaponPanel { get => _nameOfEvolutionWeaponPanel; set => _nameOfEvolutionWeaponPanel = value; }
    public Dictionary<string, GameObject> _NameOfEvolutionWeaponIconBox { get => _nameOfEvolutionWeaponIconBox; set => _nameOfEvolutionWeaponIconBox = value; }

    public Dictionary<string, GameObject> NameOfIconPanelUseBox { get => _nameOfIconPaneUseBox; set => _nameOfIconPaneUseBox = value; }
    public Dictionary<string, GameObject> NameOfIconPanelUseUI { get => _nameOfIconPaneUseUI; set => _nameOfIconPaneUseUI = value; }
    public Dictionary<string, GameObject> NameOfInformationPanel { get => _nameOfInformationPanel; }

    public Text LevelText { get => _levelText; set => _levelText = value; }
    public Transform OrizinCanvus => _orizinCanvus;

    public LayoutGroup BasePanelLayoutGroup => _basePanelLayoutGroup;

    /// <summary>�S�Ă̕���A�C�e�������I��������Ƃɏo���p�l��</summary>
    public List<GameObject> LastPanel { get => _lastPanel; set => _lastPanel = value; }

    /// <summary>���x���A�b�v���ɏo���p�l��</summary>
    public GameObject LevelUpPanel => _basePanel;

    /// <summary>�Q�[����ʍ���̏����A�C�e�����̃��C�A�E�g�O���[�v</summary>
    public List<Transform> WeaponUIPos => _weaponUIPos;

    /// <summary>�Q�[����ʍ���̏����A�C�e�����̃��C�A�E�g�O���[�v</summary>
    public List<Transform> ItemUIPos => _itemUIPos;

    public Transform ButtunEndUsePos => _endPos;

    public Slider LevelUpSlider => _sliderLevelUp;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
