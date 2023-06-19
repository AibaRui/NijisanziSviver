using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBoock : InstantiateWeaponBase//,IPausebleGetBox
{
    [Header("�{�̓����Ă��鎞��")]
    [SerializeField] float _bookMovingTime = 5;

    List<GameObject> go = new List<GameObject>();


    // �~�^������(+��-�ŉ�]�������ς��)
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
        //���ݍU����
        _isAttackNow = true;

        //���˕��̐�
        var num = _number + _mainStatas.Number;
        //�I�u�W�F�N�g�Ԃ̊p�x��
        float angleDiff = 360f / num;
        //�o������̃��X�g
        List<GameObject> books = new List<GameObject>();
        //���X�g�ɏo�������o�^����
        for (int i = 0; i < num; i++)
        {
            var go = _objectPool.UseObject(transform.position, PoolObjectType.Book);
            books.Add(go);
        }
        //������v�[������؂�A�����ݒ肷��
        for (int i = 0; i < books.Count; i++)
        {
            //�e�I�u�W�F�N�g���~��ɔz�u         
            Vector3 childPostion = _player.transform.position;
            float radius = _eria + _mainStatas.Eria;
            float angle = (90 - angleDiff * i) * Mathf.Deg2Rad;
            childPostion.x += radius * Mathf.Cos(angle);
            childPostion.y += radius * Mathf.Sin(angle);
            books[i].transform.position = childPostion;

            //�����ݒ�
         //   books[i].transform.SetParent(_player.transform);
            books[i].gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            books[i].gameObject.GetComponent<BookMove>().Period = _speed * _mainStatas.AttackSpeed;
            books[i].gameObject.GetComponent<BookMove>().Radius = _eria + _mainStatas.Eria;
            books[i].gameObject.GetComponent<WeaponBase>().Level = _level;
        }
        yield return new WaitForSeconds(_bookMovingTime);
        //���X�g�����Z�b�g
        books.Clear();
        //���݂͍U�����Ă��Ȃ�
        _isAttack = false;
        _isAttackNow = false;
        _instantiateCorutin = null;
    }

}
