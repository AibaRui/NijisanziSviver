using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMove
{
    [Header("�X�ł̈ړ����ǂ���")]
    [Tooltip("�X�ł̈ړ����ǂ���")] [SerializeField] private bool _isTown;

    [Header("�ړ����x")]
    [Tooltip("�ړ����x")] [SerializeField] float _speed = 7;

    private PlayerControl _playerControl;

    /// <summary>
    ///������
    /// </summary>
    /// <param name="playerControl"></param>
    public void Init(PlayerControl playerControl)
    {
        _playerControl = playerControl;
    }

    public void Move()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 dir = default;

        if (_isTown)
        {
            dir = new Vector2(h, 0).normalized;

            if (h != 0)
            {
                _playerControl.PlayerAnim.SetBool("Move", true);
            }
            else if (h == 0)
            {
                _playerControl.PlayerAnim.SetBool("Move", false);
            }
        }
        else
        {
            dir = new Vector2(h, v).normalized;

            if (h != 0 || v != 0)
            {
                _playerControl.PlayerAnim.SetBool("Move", true);
            }
            else if (h == 0 && v == 0)
            {
                _playerControl.PlayerAnim.SetBool("Move", false);
            }
        }

        _playerControl.Rb.velocity = dir * _speed;



        if (h > 0)
        {
            _playerControl.PalyerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0)
        {
            _playerControl.PalyerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
    }

}
