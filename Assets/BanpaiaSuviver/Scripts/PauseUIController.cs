using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUIController : MonoBehaviour
{
    [Header("PauseéûÇ…èoÇ∑UI")]
    [SerializeField] GameObject _pauseUI;

    [SerializeField] private StatasUIControl _statasUIControl;

    bool _isPause = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPause = !_isPause;
            _pauseUI.SetActive(_isPause);

            if(_isPause)
            {
                _statasUIControl.ShowStatasUI();
            }
            else
            {
                _statasUIControl.CloseUI();
            }

        }
    }


   

}
