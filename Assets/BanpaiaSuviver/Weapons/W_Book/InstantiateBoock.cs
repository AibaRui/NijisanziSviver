using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBoock : InstantiateWeaponBase//,IPausebleGetBox
{
    [Header("本の動いている時間")]
    [SerializeField] float _bookMovingTime = 5;

    List<GameObject> go = new List<GameObject>();


    // 円運動周期(+と-で回転方向が変わる)
    public float _period;


    private bool _isAttackNow;
    private bool _isAttack;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {

            if (_level > 0)
            {
                if (_isAttack && _isAttackNow == false)
                {
                    _instantiateCorutin = Attack();
                    StartCoroutine(_instantiateCorutin);
                }
                else if (!_isAttack)
                {
                    AttackLate();
                }
            }
        }
    }


    void AttackLate()
    {
        _countTime -= Time.deltaTime;

        if (_countTime <= 0)
        {
            var setCoolTime = _coolTime * _mainStatas.CoolTime;
            _countTime = setCoolTime;
            _isAttack = true;
        }
    }

    IEnumerator Attack()
    {
        //現在攻撃中
        _isAttackNow = true;

        //発射物の数
        var num = _number + _mainStatas.Number;
        //オブジェクト間の角度差
        float angleDiff = 360f / num;
        //出す武器のリスト
        List<GameObject> books = new List<GameObject>();
        //リストに出す武器を登録する
        for (int i = 0; i < num; i++)
        {
            var go = _objectPool.UseObject(transform.position, PoolObjectType.Book);
            books.Add(go);
        }
        //武器をプールから借り、初期設定する
        for (int i = 0; i < books.Count; i++)
        {
            //各オブジェクトを円状に配置         
            Vector3 childPostion = _player.transform.position;
            float radius = _eria + _mainStatas.Eria;
            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            childPostion.x += radius * Mathf.Cos(angle);
            childPostion.y += radius * Mathf.Sin(angle);
            books[i].transform.position = childPostion;

            //初期設定
         //   books[i].transform.SetParent(_player.transform);
            books[i].gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            books[i].gameObject.GetComponent<BookMove>().Period = _speed * _mainStatas.AttackSpeed;
            books[i].gameObject.GetComponent<BookMove>().Radius = _eria + _mainStatas.Eria;
            books[i].gameObject.GetComponent<WeaponBase>().Level = _level;
        }
        yield return new WaitForSeconds(_bookMovingTime);
        //リストをリセット
        books.Clear();
        //現在は攻撃していない
        _isAttack = false;
        _isAttackNow = false;
        _instantiateCorutin = null;
    }

}
