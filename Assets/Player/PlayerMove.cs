using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 7;

    [Header("プレイヤーのスプライト")]
    [Tooltip("プレイヤーのスプライト")] [SerializeField] GameObject _playerSprite ;


    [SerializeField] Animator _anim;
    Rigidbody2D _rb;
    void Start()
    {
        _anim = _anim.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        Move();
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v).normalized;
        _rb.velocity = dir * _speed;

        if (h != 0 || v != 0)
        {
            _anim.SetBool("Move", true);
        }
        else if (h == 0 && v == 0)
        {
            _anim.SetBool("Move", false);
        }

        if (h > 0)
        {
           _playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h < 0)
        {
            _playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }


    }

}
