using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateIceMagic : InstantiateWeaponBase//,IPausebleGetBox
{
    [SerializeField] float _spownAddPos = 4;

    [Header("進化後の力一のオブジェクト")]
    [SerializeField] private GameObject _rikiiti;

    [Header("進化後の舞元のオブジェクト")]
    [SerializeField] private GameObject _maimoto;

    bool _isAttack = false;
    bool _isAttackNow = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    private GameObject _rikiitiSpownObject;
    private GameObject _maimotoSpownObject;

    private EvolutionIce _rikiitiEvo = null;
    private EvolutionIce _maimotoEvo = null;

    private Rigidbody2D _rbRikiiti = null;
    private Rigidbody2D _rbMaimoto = null;

    void Start()
    {

    }

    void Update()
    {
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            if (_level > 0)
            {
                if (_isAttack)
                {
                    if (_isEvolution)
                    {
                        if (_rikiitiSpownObject != null && _maimotoSpownObject != null)
                        {
                            if (!_rikiitiSpownObject.activeSelf && !_maimotoSpownObject.activeSelf)
                            {
                                _instantiateCorutin = Attack();
                                StartCoroutine(_instantiateCorutin);
                            }
                        }
                    }
                    else
                    {
                        _instantiateCorutin = Attack();
                        StartCoroutine(_instantiateCorutin);
                    }

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
            _isInstanciateEnd = false;
        }
    }

    IEnumerator Attack()
    {
        //_isAttackNow = true;
        _isAttack = false;

        if (_isEvolution)
        {
            SpownEvoluitonIce();
        }
        else
        {
            var num = _number + _mainStatas.Number;

            for (int i = 0; i < num; i++)
            {
                SpownIce(i, _player.transform.localScale.x);

                yield return new WaitForSeconds(0.1f);
            }
        }

        // _isAttackNow = false;
        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }

    public override void SetEvolutionSystem()
    {
        //オブジェクトを生成
        _maimotoSpownObject = Instantiate(_maimoto);
        _rikiitiSpownObject = Instantiate(_rikiiti);

        _maimotoEvo = _maimotoSpownObject.GetComponent<EvolutionIce>();
        _rikiitiEvo = _rikiitiSpownObject.GetComponent<EvolutionIce>();

        _rbMaimoto = _maimotoSpownObject.GetComponent<Rigidbody2D>();
        _rbRikiiti = _rikiitiSpownObject.GetComponent<Rigidbody2D>();

        _maimotoSpownObject.SetActive(false);
        _rikiitiSpownObject.SetActive(false);
    }

    private void SpownEvoluitonIce()
    {
        _maimotoSpownObject.transform.position = _player.transform.position;
        _rikiitiSpownObject.transform.position = _player.transform.position;

        _maimotoEvo.Power = _attackPower * _mainStatas.Power;
        _rikiitiEvo.Power = _attackPower * _mainStatas.Power;
        _maimotoSpownObject.transform.localScale = new Vector3(_mainStatas.Eria * _eria, _mainStatas.Eria * _eria, 1);
        _rikiitiSpownObject.transform.localScale = new Vector3(_mainStatas.Eria * _eria, _mainStatas.Eria * _eria, 1);

        _maimotoSpownObject.SetActive(true);
        _rikiitiSpownObject.SetActive(true);

        float speed = _weaponManaager.weaponData.GetData(_maxLevel + 1, _weaponName).Speed;

        _rbRikiiti.velocity = -Vector2.right * speed * _mainStatas.AttackSpeed;
        _rbMaimoto.velocity = Vector2.right * speed * _mainStatas.AttackSpeed;

    }

    private void SpownIce(int i, float playerLocalX)
    {
        var go = _objectPool.UseObject(_player.transform.position, PoolObjectType.IceMagick);

        if (i % 2 == 0)
        {
            Vector3 pos = default;

            if (i == 0) pos = new Vector3(-_spownAddPos, 0, 0);
            else pos = new Vector3(-_spownAddPos * i, 0, 0);

            go.transform.position = _player.transform.position + pos;
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            go.gameObject.transform.localScale = new Vector3(-_mainStatas.Eria * _eria, _mainStatas.Eria * _eria, 1);
        }
        else if (i % 2 != 0)
        {
            Vector3 pos = default;

            if (i == 0) pos = new Vector3(_spownAddPos, 0, 0);
            else pos = new Vector3(_spownAddPos * i, 0, 0);

            go.transform.position = _player.transform.position + pos;
            go.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
            go.gameObject.transform.localScale = new Vector3(_mainStatas.Eria * _eria, _mainStatas.Eria * _eria, 1);
        }

    }


}
