using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMove : WeaponBase
{
    // �~�^������(+��-�ŉ�]�������ς��)
    private float _period = 2;
    public float Period { set => _period = value; }

   private float _radius;

    public float Radius { set => _radius = value; }

    GameObject _player;
    Vector3 _center;

    public Vector3 Center { set => _center = value; }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");  
        //��]�̒��S�_���v���C���[�̈ʒu�ɐݒ�
    }



    void Update()
    {
        if (!_isPause && !_isLevelUpPause && !_isPauseGetBox)
        {
            CountDestroyTime();        
        }
    }

    void FixedUpdate()
    {
        if (!_isPause && !_isLevelUpPause && !_isPauseGetBox)
        {
            Move();
        }

    }

    void Move()
    {
        if (!_isLevelUpPause && !_isPause)
        {
            _center = _player.transform.position;

            var tr = transform;

            // ��]�̃N�H�[�^�j�I���쐬�B
            // Quaternion.AngleAxis(���x��]�����邩,��]��)
            // �w�肳�ꂽ�p�x�Ǝ��ł̉�]��\���N�H�[�^�j�I�����擾���郁�\�b�h
            var angleAxis = Quaternion.AngleAxis(360 / _period*Time.deltaTime, Vector3.forward);

            // �~�^���̈ʒu�v�Z
            var pos = tr.position;

            pos -= _center;

            pos = angleAxis * pos.normalized*_radius;
            pos += _center;

            tr.position = pos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (collision.gameObject.TryGetComponent<EnemyControl>(out EnemyControl enemy))
            {
                enemy.Damage(_power);
            }
        }
    }
}
