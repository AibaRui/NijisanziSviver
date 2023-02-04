using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealHpLatePanel : MonoBehaviour
{
    [SerializeField] int _healHp;
    
    [SerializeField] PlayerHp _playerHp;
        

    public void HealHp()
    {
        _playerHp.AddHeal(_healHp);
    }
}
