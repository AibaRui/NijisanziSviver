using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateHiyoko : InstantiateWeaponBase//,IPausebleGetBox
{
    [Header("Playerにつけるヒヨコのプレハブ")]
    [SerializeField] GameObject _hiyoko;
    [Header("ヒヨコのプレハブのBeamの出す場所の名前")]
    [SerializeField] string _beamPos;

    GameObject _instanciateHiyoko;

    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>既にヒヨコを出したかどうか</summary>
    bool _isInstance = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
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
        //ヒヨコを出す
        if (!_isInstance)
        {
            _isInstance = true;
            var hiyoko = Instantiate(_hiyoko);
            hiyoko.transform.SetParent(_player.transform);
            _instanciateHiyoko = hiyoko;

        }


        _isAttack = false;
        _isInstanciateEnd = false;


        //ビームを借りる
        var beam = _objectPool.UseObject(_instanciateHiyoko.transform.position, PoolObjectType.Hiyoko);

        //ビームの大きさを変更
        base.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 0);
        
        //ビームの位置を設定
        Transform pos = _instanciateHiyoko.transform.Find(_beamPos);
        beam.transform.position = pos.position;
        beam.transform.SetParent(pos.transform);

        //ビームのパワーを変更
        beam.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;

        //ビームを出す時間を決めて、ビームに設定。(武器のステータスと、メインステータスの合計値)
        var lifeTime = _number + _mainStatas.Number * 2;
        beam.gameObject.GetComponent<WeaponBase>().LifeTime = lifeTime;

        yield return new WaitForSeconds(lifeTime);

        _isInstanciateEnd = true;
        _instanciateCorutine = null;
    }


}
