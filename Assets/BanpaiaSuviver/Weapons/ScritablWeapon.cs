using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class ScritablWeapon : ScriptableObject
{
    [Header("����̖��O")]
    [Tooltip("����̖��O")] [SerializeField] protected string _weaponName = "";

    [Header("����̍ő僌�x��")]
    [Tooltip("����̍ő僌�x��")] [SerializeField] protected int _maxLevel = 0;

    [Header("����̐����������s���v���n�u")]
    [SerializeField] private GameObject _instanceObject;

    [Header("InGame�Ŏg���A�C�R��")]
    [SerializeField] private GameObject _iconUseUI;

    [Header("Box�Ŏg��Icon")]
    [SerializeField] GameObject _iconUseBox;

    [Header("���x���A�b�v���ɏo���{�^��")]
    [SerializeField] GameObject _levelUpButtun;

    [Header("���x���f�[�^��TextAsset")]
    [SerializeField] private TextAsset _levelData;

    [Header("����TextAsset")]
    [SerializeField] private TextAsset _infoData;

    public string WeaponName => _weaponName;

    public GameObject LevelUpButtun => _levelUpButtun;
    public int MaxLevel => _maxLevel;

    public GameObject IconInGameUI => _iconUseUI;
    public GameObject IconBox => _iconUseBox;

    public GameObject InstanceObject => _instanceObject;
    public TextAsset LevelData => _levelData;
    public TextAsset InfoData => _infoData;

}
