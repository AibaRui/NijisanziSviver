using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerInTown : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;



    private void Update()
    {
        _playerMove.Move();
    }

}
