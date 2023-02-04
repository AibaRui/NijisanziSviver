using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookMove : WeaponBase
{
    // 円運動周期(+と-で回転方向が変わる)
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
        //回転の中心点をプレイヤーの位置に設定
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

            // 回転のクォータニオン作成。
            // Quaternion.AngleAxis(何度回転させるか,回転軸)
            // 指定された角度と軸での回転を表すクォータニオンを取得するメソッド
            var angleAxis = Quaternion.AngleAxis(360 / _period*Time.deltaTime, Vector3.forward);

            // 円運動の位置計算
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
