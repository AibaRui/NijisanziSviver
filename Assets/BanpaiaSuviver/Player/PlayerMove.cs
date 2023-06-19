using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("街での移動かどうか")]
    [Tooltip("街での移動かどうか")] [SerializeField] private bool _isTown;

    [Header("移動速度")]
    [Tooltip("移動速度")] [SerializeField] float _speed = 7;

    [Header("プレイヤーのスプライト")]
    [Tooltip("プレイヤーのスプライト")] [SerializeField] GameObject _playerSprite;

    [SerializeField]
    private Animator _anim;
    //  public Animator Anim { get => _anim; set => value = _anim; }
    Rigidbody2D _rb;
    void Start()
    {
        //_anim = _anim.GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
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
                _anim.SetBool("Move", true);
            }
            else if (h == 0)
            {
                _anim.SetBool("Move", false);
            }
        }
        else
        {
            dir = new Vector2(h, v).normalized;

            if (h != 0 || v != 0)
            {
                _anim.SetBool("Move", true);
            }
            else if (h == 0 && v == 0)
            {
                _anim.SetBool("Move", false);
            }
        }

        _rb.velocity = dir * _speed;



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
