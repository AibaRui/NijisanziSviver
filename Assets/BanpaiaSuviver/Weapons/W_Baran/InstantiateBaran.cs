using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBaran : InstantiateWeaponBase
{
    [Header("�ʏ�̃o�����̈ړ�")]
    [SerializeField] float _addPower;

    [Header("�i���̃o�����̈ړ����x")]
    [SerializeField] private float _moveSpeed = 5;


    [SerializeField] float _randamX;


    /// <summary>�U���\���ǂ���</summary>
    bool _isAttack = false;

    /// <summary>���̍U���ŏo��������A�o���I�������ǂ���</summary>
    bool _isInstanciateEnd = true;

    /// <summary>�i�C�t���΂������̃x�N�g��</summary>
    Vector2 dirSave = new Vector2(1, 0);

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_level == 0) return;

        //�ꎞ��~���łȂ���������s
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {

            if (_isAttack)
            {
                _instantiateCorutin = Attack();
                StartCoroutine(_instantiateCorutin);
            }
            else if (_isInstanciateEnd)
            {
                AttackLate();
            }

        }
    }

    /// <summary>�N�[���^�C���̏���</summary>
    void AttackLate()
    {
        _countTime -= Time.deltaTime;

        if (_countTime <= 0)
        {
            var setCoolTime = _coolTime * _mainStatas.CoolTime;
            _countTime = setCoolTime;
            _isAttack = true;
            _isInstanciateEnd = false;
        }
    }

    /// <summary>�U���̏���</summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        _isAttack = false;

        //������o���񐔂����߂�B(����̃X�e�[�^�X�ƁA���C���X�e�[�^�X�̍��v�l)
        var num = _number + _mainStatas.Number;

        if (_isEvolution)
        {
            for (int i = 0; i < 12; i++)
            {
                SpownEvolutionWeapon(i);
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            for (int i = 0; i < num; i++)
            {
                SpownNomalWeapon();
                yield return new WaitForSeconds(0.2f);
            }
        }

        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }

    void SpownEvolutionWeapon(int num)
    {
        //�����Ə����ݒ�
        var go = _objectPool.UseObject(transform.position, PoolObjectType.EW_Baran);
        go.transform.position = _player.transform.position;

        //����̑傫���̐ݒ�
        go.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 1);

        //���x���A�Η͂̐ݒ�
        go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
        go.gameObject.GetComponent<WeaponBase>().Level = _level;

        float oneA = 360 / 12;
        float angle = oneA * (num + 1);

        // �p�x�����W�A���ɕϊ�
        float radians = angle * Mathf.PI / 180f;

        // �x�N�g����x��y�������v�Z
        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        Vector2 vector = new Vector2(x, y);

        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.velocity = vector * _moveSpeed;

    }

    void SpownNomalWeapon()
    {
        //�����Ə����ݒ�
        var go = _objectPool.UseObject(transform.position, PoolObjectType.Baran);
        go.transform.position = _player.transform.position;

        go.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 1);

        go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
        go.gameObject.GetComponent<WeaponBase>().Level = _level;

        float randamX = Random.Range(-_randamX, _randamX);
        Vector2 addDir = new Vector2(randamX, 1);
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();

        rb.AddForce(addDir.normalized * _addPower, ForceMode2D.Impulse);
    }

}
