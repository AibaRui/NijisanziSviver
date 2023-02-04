using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItemControl : MonoBehaviour
{
    [SerializeField] float _healHp;

    GameObject _player;

    AudioSource _aud;
    ExpPause _expPause;
    bool _isGet = false;

    private void OnEnable()
    {
        _aud = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        _expPause = FindObjectOfType<ExpPause>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!_expPause.IsPause && !_expPause.IsPauseLevelUp)
        {
            if (_isGet)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 0.2f);
                float dir = Vector2.Distance(transform.position, _player.transform.position);
                if (dir <= 0.2f)
                {
                    _isGet = false;
                    PlayerHp playerHp = FindObjectOfType<PlayerHp>();
                    playerHp.AddHeal(_healHp);
                    _aud.Play();
                    this.gameObject.SetActive(false);
                }
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GetArea")
        {
            _isGet = true;
        }
    }



}
