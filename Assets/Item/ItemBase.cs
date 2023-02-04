using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [Header("Box�̉��o�p�̃A�C�R��")]
    [SerializeField] GameObject _iconUseBox;

    [Header("UI�p�̃A�C�R��")]
    [SerializeField] GameObject _iconUseUI;

    [Header("���x���A�b�v�p�̏ڍ׃p�l��")]
    [SerializeField] GameObject _panel;

    [Header("���x���A�b�v��ʂ̃��C�A�E�g�O���[�v")]
    [Tooltip("���x���A�b�v��ʂ̃��C�A�E�g�O���[�v")] [SerializeField] LayoutGroup _basePanelLayoutGroup;

    [Header("�Q�[����ʂ̃��C�A�E�g�O���[�v")]
    [Tooltip("�Q�[����ʂ̃��C�A�E�g�O���[�v")] [SerializeField] LayoutGroup _gameSceneLayoutGroup;

    [SerializeField] protected string _itemName;

    [SerializeField] protected float _thisStatas;

    [SerializeField] protected MainStatas _mainStatas;

    /// <summary>���x���A�b�v�e�[�u����ǂݍ��ނ���</summary>
    [SerializeField] protected ItemData _itemData = default;

    protected ItemStats _itemStats;

    [SerializeField] protected GameManager _gm;

    [SerializeField] protected int _maxLevel = 5;

    [SerializeField] protected LevelUpController _levelUpController;

    [SerializeField] protected BoxControl _boxControl;

   protected int _level = 0;

    private void Awake()
    {
        _levelUpController.SetItemData(_itemName, _maxLevel,_iconUseBox,_panel,_iconUseUI);
        _boxControl.SetItem(_itemName, this);
        _itemData = _itemData.GetComponent<ItemData>();
        _mainStatas = _mainStatas.GetComponent<MainStatas>();
        _gm = _gm.GetComponent<GameManager>();
    }

    protected abstract void LevelUpStatas(float _statas);


    public abstract void LevelUp(int level = 1);

    //public void LevelUp(int level = 1)
    //{
    //    if (_level < _maxLevel)
    //    {
    //        _level += level;

    //        _thisStatas = ReturnStatas(_level);
    //        LevelUpStatas(_thisStatas);
    //        Debug.Log(_itemName + "���x���A�b�v�I���݂̃��x����" + _level);
    //    }
    //    _mainStatas.SetStatsText();
    //    GameManager gm = FindObjectOfType<GameManager>();
    //    gm.ItemLevelUp(_itemName, _level);
    //}
}
