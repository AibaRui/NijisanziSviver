using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateKnife : InstantiateWeaponBase//,IPausebleGetBox
{
    /// <summary>�U���\���ǂ���</summary>
    bool _isAttack = false;

    /// <summary>���̍U���ŏo��������A�o���I�������ǂ���</summary>
    bool _isInstanciateEnd = true;

    /// <summary>�i�C�t���΂������̃x�N�g��</summary>
    Vector2 dirSave = new Vector2(1, 0);



    void Update()
    {
        if (_level == 0) return;

        //�ꎞ��~���łȂ���������s
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Vector2 dir = new Vector2(h, v).normalized;
            if (h != 0 || v != 0)
            {
                dirSave = dir;
            }

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
            _isInstanciateEnd = false;
            _isAttack = true;
        }
    }

    /// <summary>�U���̏���</summary>
    /// <returns></returns>
    IEnumerator Attack()
    {
        _isAttack = false;

        //������o���񐔂����߂�B(����̃X�e�[�^�X�ƁA���C���X�e�[�^�X�̍��v�l)
        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            float randamX = Random.Range(0.2f, 1);
            float randamY = Random.Range(-1.3f, 1.3f);

            var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.Knife);
            go.transform.position = _player.transform.position + new Vector3(randamX, randamY, 0);
            go.transform.up = dirSave;
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            go.gameObject.GetComponent<WeaponBase>().Level = _level;
            Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
            rb.velocity = dirSave * _speed * _mainStatas.AttackSpeed;
            _aud.Play();
            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }


    //private void OnEnable()
    //{
    //    //  _weaponManager = FindObjectOfType<WeaponData>();
    //}


    //private void OnDisable()
    //{
    //    PauseGetBox.Instance.RemoveEvent(this);
    //}

    //public void PauseResume(bool isPause)
    //{
    //    _isPauseGetBox = isPause;

    //    if(isPause)
    //    {
    //        if (_instantiateCorutin != null)
    //        {
    //            StopCoroutine(_instantiateCorutin);
    //        }
    //    }
    //    else
    //    {
    //        if (_instantiateCorutin != null)
    //        {
    //            StartCoroutine(_instantiateCorutin);
    //        }
    //    }
    //}
}
