using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatasUIControl : MonoBehaviour
{
    [Header("�X�e�[�^�X������UI")]
    [SerializeField] private GameObject _statasUI;

    /// <summary>UI���J�����Ƃ�����</summary>
    private int _onUIcount;

    public void ShowStatasUI()
    {
        if (_onUIcount == 0)
            _statasUI.SetActive(true);

        _onUIcount++;
    }

    public void CloseUI()
    {
        _onUIcount--;

        if (_onUIcount == 0)
            _statasUI.SetActive(false);
    }


}
