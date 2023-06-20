using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateNinniku : InstantiateWeaponBase
{
    [Header("�~�̍ŏ��̑傫��")]
    [SerializeField] float _baseCircleScale;

    private GameObject _instantiateNiniku;

    bool _isAttack = false;

    /// <summary>���̍U���ŏo��������A�o���I�������ǂ���</summary>
    bool _isInstanciateEnd = true;

    private float _saveEria;
    private float _savePower;

    void Start()
    {
        _saveEria = _mainStatas.Eria;
        _savePower = _mainStatas.Power;
    }

    private void Update()
    {
        if (_level > 0)
        {
            if (_isAttack)
            {
                Attack();
            }
            else if (_isInstanciateEnd)
            {
                AttackLate();
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
            _isInstanciateEnd = false;
        }
    }


    /// <summary>�U���̏�������̍X�V�B�{�^���ŌĂ�</summary>
    /// <returns></returns>
    public void Attack()
    {
        _isAttack = false;

        //�o���Ă���j���j�N�����Z�b�g
        if (_instantiateNiniku != null) _instantiateNiniku.SetActive(false);


        _instantiateNiniku = null;

        //�j���j�N�̍Đ����ƈʒu����
        var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.Ninniku);
        var scale = _eria * _mainStatas.Eria * _baseCircleScale;
        go.transform.localScale = new Vector3(scale, scale, 1);
        go.transform.position = _player.transform.position;
        go.transform.SetParent(_player.transform);
        go.gameObject.GetComponent<AttackNinniku>().Power = _attackPower * _mainStatas.Power;
        go.gameObject.GetComponent<AttackNinniku>().Level = _level;
        _instantiateNiniku = go;
        Debug.Log("NNNN");
        _isInstanciateEnd = true;
    }

}
