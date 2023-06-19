using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TsetPause : MonoBehaviour
{

    public bool PauseFlg { get; private set; } = false;

    event Action<bool> _onPauseResume = default;




    /// <summary>
    /// �ꎞ��~�E�ĊJ��؂�ւ���
    /// </summary>
    public void PauseResume()
    {
        PauseFlg = !PauseFlg;
        _onPauseResume(PauseFlg);  // ����ŕϐ��ɑ�������֐���S�ČĂяo����
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g�ɓo�^����֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnEnable�֐��� PauseManager.Instance.SetEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void SetEvent(IPauseable pauseable)
    {
        _onPauseResume += pauseable.PauseResume;
    }

    /// <summary>
    /// �ꎞ��~�A�ĊJ���̊֐����f���Q�[�g����o�^����������֐�
    /// 
    /// �ꎞ��~�������������X�N���v�g����Ăяo��
    /// 
    /// OnDisable�֐���PauseManager.Instance.RemoveEvent(this); �ƋL�q�����
    /// </summary>
    /// <param name="pauseable"></param>
    public void RemoveEvent(IPauseable pauseable)
    {
        _onPauseResume -= pauseable.PauseResume;
    }
}
