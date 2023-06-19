using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("詳細パネルの元のCanvas")]
    [SerializeField] Transform _orizinCanvus;

    [Header("ゲーム画面左上の所持アイテム欄のレイアウトグループ")]
    [SerializeField] LayoutGroup _itemLayoutGroup;

    [Header("ゲーム画面左上の所持武器欄のレイアウトグループ")]
    [SerializeField] LayoutGroup _weaponLayoutGroup;

    [Header("武器アイテム選択ボタンを、使い終わった時に置いておく場所")]
    [SerializeField] Transform _endPos;

    [Header("レベルのText")]
    [SerializeField] Text _levelText;

    [Header("レベルアップのスライダー")]
    [SerializeField] Slider _sliderLevelUp;

    [Header("お金と回復のパネル")]
    [Tooltip("お金と回復のパネル")] [SerializeField] List<GameObject> _lastPanel = new List<GameObject>();

    [Header("レベルアップのおおもとのパネル")]
    [Tooltip("レベルアップのおおもとのパネル")] [SerializeField] GameObject _basePanel;

    [Header("レイアウトグループ")]
    [Tooltip("レイアウトグループ")] [SerializeField] LayoutGroup _basePanelLayoutGroup;


    /// <summary>Box用。武器、アイテムの名前をKeyにとりアイコンの画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseBox = new Dictionary<string, GameObject>();

    /// <summary>。武器、アイテムの名前をKeyにとりアイコンの画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfIconPaneUseUI = new Dictionary<string, GameObject>();
    /// <summary>武器、アイテムの名前をKeyにとりパネル画像を持つ</summary>
    Dictionary<string, GameObject> _nameOfInformationPanel = new Dictionary<string, GameObject>();


    public Dictionary<string, GameObject> NameOfIconPanelUseBox { get => _nameOfIconPaneUseBox; set => _nameOfIconPaneUseBox = value; }
    public Dictionary<string, GameObject> NameOfIconPanelUseUI { get => _nameOfIconPaneUseUI; set => _nameOfIconPaneUseUI = value; }
    public Dictionary<string, GameObject> NameOfInformationPanel { get => _nameOfInformationPanel; }

    public Text LevelText { get => _levelText; set => _levelText = value; }
    public Transform OrizinCanvus => _orizinCanvus;

    public LayoutGroup BasePanelLayoutGroup => _basePanelLayoutGroup;

    /// <summary>全ての武器アイテムを取り終わったあとに出すパネル</summary>
    public List<GameObject> LastPanel { get => _lastPanel; set => _lastPanel = value; }

    /// <summary>レベルアップ時に出すパネル</summary>
    public GameObject LevelUpPanel => _basePanel;

    /// <summary>ゲーム画面左上の所持アイテム欄のレイアウトグループ</summary>
    public LayoutGroup WeaponLayoutGroup { get => _weaponLayoutGroup; }

    /// <summary>ゲーム画面左上の所持アイテム欄のレイアウトグループ</summary>
    public LayoutGroup ItemLayoutGroup { get => _itemLayoutGroup; }

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
