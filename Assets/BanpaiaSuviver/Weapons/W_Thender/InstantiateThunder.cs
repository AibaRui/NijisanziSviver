using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstantiateThunder : InstantiateWeaponBase//,IPausebleGetBox
{
    /// <summary>���𗎂Ƃ��͈͂̔��aX</summary>
    [SerializeField] float _sizeX;
    /// <summary>���𗎂Ƃ��͈͂̔��aY</summary>
    [SerializeField] float _sizeY;

    [SerializeField] private Text _text;

    [SerializeField] private AttackThunderEvolution _evolutionWeapon;

    /// <summary>�U���\���ǂ���</summary>
    bool _isAttack = false;

    /// <summary>���̍U���ŏo��������A�o���I�������ǂ���</summary>
    bool _isInstanciateEnd = true;


    public override void SetEvolutionSystem()
    {
        var go = Instantiate(_evolutionWeapon);
        go.TryGetComponent<AttackThunderEvolution>(out AttackThunderEvolution attack);

        _text.gameObject.SetActive(true);
        attack.AttackPower = _attackPower;
        attack.MainStatas = _mainStatas;
        attack.ObjectPool = _objectPool;
        attack.PauseManager = _pauseManager;

        attack.Text = _text;
        attack.Init(_player.transform);
        go.transform.position = _player.transform.position;
    }

    void Update()
    {
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            if (_level > 0)
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
        _isAttack = false;
        _isInstanciateEnd = false;

        //������o���񐔂����߂�B(����̃X�e�[�^�X�ƁA���C���X�e�[�^�X�̍��v�l)
        var num = _number + _mainStatas.Number;

        for (int i = 0; i < num; i++)
        {
            float randamX = Random.Range(-_sizeX, _sizeX);
            float randamY = Random.Range(-_sizeY, _sizeY);

            var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.Thunder);
            go.transform.position = _player.transform.position + new Vector3(randamX, randamY, 0);
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            yield return new WaitForSeconds(0.2f);
        }
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }


}
