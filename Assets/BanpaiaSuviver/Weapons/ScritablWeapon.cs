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

    [Header("InGameで使うアイコン")]
    [SerializeField] private GameObject _iconUseUI;

    [Header("Boxで使うIcon")]
    [SerializeField] GameObject _iconUseBox;

    [Header("レベルアップ時に出すボタン")]
    [SerializeField] GameObject _levelUpButtun;

    [Header("レベルデータのTextAsset")]
    [SerializeField] private TextAsset _levelData;

    [Header("情報のTextAsset")]
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
