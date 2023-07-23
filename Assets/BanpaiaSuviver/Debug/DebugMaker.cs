using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMaker : MonoBehaviour
{
    [Header("ボタン")]
    [SerializeField] public  Button _buttun;

    [Header("武器のレイアウトグループ")]
    [SerializeField] public VerticalLayoutGroup _weaponLayoutGroup;

    [Header("アイテムのレイアウトグループ")]
    [SerializeField] public VerticalLayoutGroup _itemLayoutGroup;

    [SerializeField] public DebugMaker debugMaker;

}
