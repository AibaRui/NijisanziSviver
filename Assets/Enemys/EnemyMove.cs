using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Header("Sprite‚ÌŒü‚«B‰EŒü‚«‚¾‚Á‚½‚çTrueB¶Œü‚«‚¾‚Á‚½‚çFalse")]
    [SerializeField] private bool _spriteDir;

    float _dir;

    GameObject _player;

    Rigidbody2D _rb;
    private void Awake()
    {
        _dir = transform.localScale.x;
        _rb = GetComponent<Rigidbody2D>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    public void Move(float speed)
    {
        Vector2 velo = _player.transform.position - transform.position;

        if (_spriteDir)
        {
            if (velo.x >= 0) transform.localScale = new Vector3(_dir, _dir, 1);
            else transform.localScale = new Vector3(-_dir, _dir, 1);
        }
        else
        {
            if (velo.x >= 0) transform.localScale = new Vector3(-_dir, _dir, 1);
            else transform.localScale = new Vector3(_dir, _dir, 1);
        }


        _rb.velocity = velo.normalized * speed;
    }

}
