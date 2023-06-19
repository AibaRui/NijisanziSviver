using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExperienceManager : MonoBehaviour
{
    /// <summary>現在のレベル</summary>
    private int _level;

    /// <summary>現在の経験値</summary>
    private float _experience;

    /// <summary>次のレベルまでに必要な経験値</summary>
    private float _nextLevelUpExeperience = 10;

    /// <summary>次のレベルまでに必要な経験値にかける倍率</summary>
    private float _nextLevelUpExeperienceAddParsentage = 10;

    [Header("レベルアップのパネル")]
    [Tooltip("レベルアップのパネル")] [SerializeField] GameObject _levelUpPanel;

    /// <summary>次に取得するアイテムを出すパネル</summary>
    [SerializeField] LayoutGroup _deck = null;



    void Start()
    {

    }

    void Update()
    {

    }



    void LevelUp()
    {
        _level++;

        _levelUpPanel.SetActive(true);
    }


    public void AddExeperience(float exeperience)
    {
        _experience += exeperience;
        while (_nextLevelUpExeperience <= _experience)
        {
            //次の必要経験値は、{ 現在の必要経験値　*　必要経験値の増加倍率 }
            _nextLevelUpExeperience = _nextLevelUpExeperience * _nextLevelUpExeperienceAddParsentage;

        }
    }




   enum NowSituasion
    {
        /// <summary>ゲーム中ではない。</summary>
        OutGame,
        /// <summary>ゲーム中</summary>
        InGame,
        /// <summary>ポーズかレベルアップ中</summary>
        PauseOrLevelUp, 
    }


}
