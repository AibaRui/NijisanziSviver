using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon")]
public class ScritablWeapon : ScriptableObject
{
    [Header("武器の名前")]
    [Tooltip("武器の名前")] [SerializeField] protected string _weaponName = "";

    [Header("武器の最大レベル")]
    [Tooltip("武器の最大レベル")] [SerializeField] protected int _maxLevel = 0;

    [Header("武器の生成処理を行うプレハブ")]
    [SerializeField] private GameObject _instanceObject;

    [Header("アイコン用のスプライト")]
    [SerializeField] private Sprite _sprite;



    [Header("レベルデータのTextAsset")]
    [SerializeField] private TextAsset _levelData;

    [Header("情報のTextAsset")]
    [SerializeField] private TextAsset _infoData;

    [Header("======進化======")]

    [Header("進化後の名前")]
    [Tooltip("進化後の名前")] [SerializeField] protected string _evolutionWeaponName = "";

    [Header("進化に必要なアイテム")]
    [SerializeField] private ScritableItem _evolutionItem;

    [Header("進化後のアイコンのSprite")]
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
