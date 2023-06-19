using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHiyoko : InstantiateWeaponBase//,IPausebleGetBox
{
    [Header("Player�ɂ���q���R�̃v���n�u")]
    [SerializeField] GameObject _hiyoko;
    [Header("�q���R�̃v���n�u��Beam�̏o���ꏊ�̖��O")]
    [SerializeField] string _beamPos;

    GameObject _instanciateHiyoko;

    /// <summary>�U���\���ǂ���</summary>
    bool _isAttack = false;

    /// <summary>���Ƀq���R���o�������ǂ���</summary>
    bool _isInstance = false;

    /// <summary>���̍U���ŏo��������A�o���I�������ǂ���</summary>
    bool _isInstanciateEnd = true;


    IEnumerator _instanciateCorutine;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_level == 0) return;

        if (!_isLevelUpPause && !_isPause)
        {
            if (_isAttack)
            {
                _instanciateCorutine = Attack();
                StartCoroutine(_instanciateCorutine);
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
        }
    }

    IEnumerator Attack()
    {
        //�q���R���o��
        if (!_isInstance)
        {
            _isInstance = true;
            var hiyoko = Instantiate(_hiyoko);
            hiyoko.transform.SetParent(_player.transform);
            _instanciateHiyoko = hiyoko;

        }


        _isAttack = false;
        _isInstanciateEnd = false;


        //�r�[�����؂��
        var beam = _objectPool.UseObject(_instanciateHiyoko.transform.position, PoolObjectType.Hiyoko);

        //�r�[���̑傫����ύX
        base.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 0);
        
        //�r�[���̈ʒu��ݒ�
        Transform pos = _instanciateHiyoko.transform.Find(_beamPos);
        beam.transform.position = pos.position;
        beam.transform.SetParent(pos.transform);

        //�r�[���̃p���[��ύX
        beam.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;

        //�r�[�����o�����Ԃ����߂āA�r�[���ɐݒ�B(����̃X�e�[�^�X�ƁA���C���X�e�[�^�X�̍��v�l)
        var lifeTime = _number + _mainStatas.Number * 2;
        beam.gameObject.GetComponent<WeaponBase>().LifeTime = lifeTime;

        yield return new WaitForSeconds(lifeTime);

        _isInstanciateEnd = true;
        _instanciateCorutine = null;
    }


}
