using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSlash : InstantiateWeaponBase
{
    [Header("Slashのオブジェクト")]
    [SerializeField] private GameObject _slshObj;

    [Header("進化した時のオブジェクト")]
    [SerializeField] private GameObject _evolutionObj;

    private GameObject _slash;

    private GameObject _slashParent;

    bool _isInstantiate = false;

    /// <summary>攻撃可能かどうか</summary>
    bool _isAttack = false;

    /// <summary>一回の攻撃で出す武器を、出し終えたかどうか</summary>
    bool _isInstanciateEnd = true;

    /// <summary>1f前の入力</summary>
    Vector2 _dirSave = new Vector2(1, 0);

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (_level == 0) return;

        //一時停止中でなかったら実行
        if (!_isLevelUpPause && !_isPause && !_isPauseGetBox)
        {
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            if (_isAttack)
            {
                Attack();
            }
            else if (_isInstanciateEnd)
            {
                AttackLate();
            }

            if (h != 0 || v != 0)
            {
                _dirSave = new Vector2(h, v);
            }
        }
    }

    public override void SetEvolutionSystem()
    {
        var go = Instantiate(_evolutionObj);
        go.transform.SetParent(_player.transform);
        go.transform.localPosition = Vector2.zero;
        go.TryGetComponent<EvolutionSlash>(out EvolutionSlash slash);
       // slash.PauseManager = _pauseManager;
    }

    /// <summary>クールタイムの処理</summary>
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



    /// <summary>攻撃の処理</summary>
    /// <returns></returns>
    void Attack()
    {
        _isAttack = false;

        //生成と初期設定
        if (!_isInstantiate)
        {
            _slashParent = Instantiate(_slshObj);
            _slash = _slashParent.transform.GetChild(0).gameObject;
            _slashParent.transform.SetParent(_player.transform);
            _slashParent.transform.localPosition = new Vector3(0, 0, 0);
            _isInstantiate = true;
        }

        _slash.SetActive(true);


        //武器の大きさの設定
        _slash.transform.localScale = new Vector3(_eria * _mainStatas.Eria, _eria * _mainStatas.Eria, 1);

        //レベル、火力の設定
        _slash.gameObject.GetComponent<WeaponBase>().Power = _attackPower * _mainStatas.Power;
        _slash.gameObject.GetComponent<WeaponBase>().Level = _level;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(h, v);
        if (h == 0 && v == 0)
        {
            float angle = Vector2.SignedAngle(transform.right, _dirSave);
            // 0度から360度に変換
            if (angle < 0f)
                angle += 360f;
            _slashParent.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            float angle = Vector2.SignedAngle(transform.right, dir);
            // 0度から360度に変換
            if (angle < 0f)
                angle += 360f;
            _slashParent.transform.rotation = Quaternion.Euler(0, 0, angle);
        }


        _isInstanciateEnd = true;
        _instantiateCorutin = null;
    }
}
