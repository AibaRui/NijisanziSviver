using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PauseGetBox : SingletonMonoBehaviour<PauseGetBox>
{

    public bool PauseGetBoxFlg { get; private set; } = false;

    event Action<bool> _onPauseResumeGetBox = default;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            PauseResume();
        }
    }


    /// <summary>
    /// �ꎞ��~�E�ĊJ��؂�ւ���
    /// </summary>
    public void PauseResume()
    {
        PauseGetBoxFlg = !PauseGetBoxFlg;
        _onPauseResumeGetBox(PauseGetBoxFlg);  // ����ŕϐ��ɑ�������֐���S�ČĂяo����
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g�ɓo�^����֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnEnable�֐��� PauseManager.Instance.SetEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void SetEvent(IPausebleGetBox pauseable)
    {
        _onPauseResumeGetBox += pauseable.PauseResume;
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g����o�^����������֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnDisable�֐���PauseManager.Instance.RemoveEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void RemoveEvent(IPausebleGetBox pauseable)
    {
        _onPauseResumeGetBox -= pauseable.PauseResume;
    }
}
