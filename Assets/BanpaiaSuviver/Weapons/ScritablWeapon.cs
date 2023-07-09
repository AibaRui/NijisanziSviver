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

    [Header("�A�C�R���p�̃X�v���C�g")]
    [SerializeField] private Sprite _sprite;



    [Header("���x���f�[�^��TextAsset")]
    [SerializeField] private TextAsset _levelData;

    [Header("����TextAsset")]
    [SerializeField] private TextAsset _infoData;

    [Header("======�i��======")]

    [Header("�i����̖��O")]
    [Tooltip("�i����̖��O")] [SerializeField] protected string _evolutionWeaponName = "";

    [Header("�i���ɕK�v�ȃA�C�e��")]
    [SerializeField] private ScritableItem _evolutionItem;

    [Header("�i����̃A�C�R����Sprite")]
    [SerializeField] Sprite _evolutionSprite;


    public Sprite Sprite => _sprite;
    public ScritableItem NeedEvolutionItem => _evolutionItem;
    public string WeaponName => _weaponName;

    public int MaxLevel => _maxLevel;


    public GameObject InstanceObject => _instanceObject;
    public TextAsset LevelData => _levelData;
    public TextAsset InfoData => _infoData;


    public Sprite EvolutionSprite => _evolutionSprite;
    public string evolutionWeaponName => _evolutionWeaponName;
}
