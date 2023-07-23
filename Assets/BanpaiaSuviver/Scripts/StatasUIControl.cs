using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatasUIControl : MonoBehaviour
{
    [Header("ステータスを示すUI")]
    [SerializeField] private GameObject _statasUI;

    /// <summary>UIを開こうとした回数</summary>
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
