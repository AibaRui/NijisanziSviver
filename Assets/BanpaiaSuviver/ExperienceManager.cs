using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ExperienceManager : MonoBehaviour
{
    /// <summary>���݂̃��x��</summary>
    private int _level;

    /// <summary>���݂̌o���l</summary>
    private float _experience;

    /// <summary>���̃��x���܂łɕK�v�Ȍo���l</summary>
    private float _nextLevelUpExeperience = 10;

    /// <summary>���̃��x���܂łɕK�v�Ȍo���l�ɂ�����{��</summary>
    private float _nextLevelUpExeperienceAddParsentage = 10;

    [Header("���x���A�b�v�̃p�l��")]
    [Tooltip("���x���A�b�v�̃p�l��")] [SerializeField] GameObject _levelUpPanel;

    /// <summary>���Ɏ擾����A�C�e�����o���p�l��</summary>
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
            //���̕K�v�o���l�́A{ ���݂̕K�v�o���l�@*�@�K�v�o���l�̑����{�� }
            _nextLevelUpExeperience = _nextLevelUpExeperience * _nextLevelUpExeperienceAddParsentage;

        }
    }




   enum NowSituasion
    {
        /// <summary>�Q�[�����ł͂Ȃ��B</summary>
        OutGame,
        /// <summary>�Q�[����</summary>
        InGame,
        /// <summary>�|�[�Y�����x���A�b�v��</summary>
        PauseOrLevelUp, 
    }


}
