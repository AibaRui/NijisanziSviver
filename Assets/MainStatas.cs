using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainStatas : MonoBehaviour
{
    [SerializeField] PlayerHp _playerHp;

    [SerializeField] int _maxHp = 10;
    private float _maxHpOrizin = 0;
    private float _maxHpPercent = 1;
    public int MaxHp { get => _maxHp; }



    [SerializeField] float _dex = 2;
    private float _dexOrizin = 0;
    private float _dexPercent = 1;
    public float Dex { get => _dex; }

    [SerializeField] float _moveSpeed = 10;
    private float _moveSpeedOrizin = 0;
    private float _moveSpeedPercent = 1;
    public float MoveSpeed { get => _moveSpeed; }

    [SerializeField] float _power = 1;
    private float _powerOrizin = 0;
    private float _powerPercent = 1;

    public float Power { get => _power; }

    /// <summary>武器の動く速さ</summary>
    [SerializeField] float _attackSpeed = 1;
    private float _attackSpeedOrizin = 0;
    private float _attackSpeedPercent = 1;

    public float AttackSpeed { get => _attackSpeed; }


    /// <summary>クール時間</summary>
    [SerializeField] float _coolTime = 1;
    private float _coolTimeOrizin = 0;
    private float _coolTimePercent = 1;

    public float CoolTime { get => _coolTime; }

    /// <summary>範囲</summary>
    [SerializeField] float _eria = 1;
    private float _eriaOrizin = 0;
    private float _eriaPercent = 1;

    public float Eria { get => _eria; }


    /// <summary>発射物の数</summary>
    [SerializeField] int _number = 0;
    private int _numberOrizin = 0;

    public int Number { get => _number; }

    /// <summary>経験値の加算倍率</summary>
    [SerializeField] float _expUpper = 1;
    private float _expUpperOrizin = 0;
    private float _expUpperPercent = 1;
    public float ExpUpper { get => _expUpper; }

    /// <summary>取得範囲</summary>
    [SerializeField] float _getEria = 3;
    private float _getEriaOrizin = 0;
    private float _getEriaPercent = 1;

    public float GetEria { get => _getEria; }

    [Header("獲得の円の初期サイズ")]
    private Vector2 _getEriaStartSize;

    [Header("獲得範囲の円")]
    [SerializeField] Transform _getEriaCircle;

    public Vector2 GetEriaStartSize { get => _getEriaStartSize; }


    StaitasManager _staitasManager = default;


    [SerializeField] Text _powerText;
    [SerializeField] Text _maxHpText;
    [SerializeField] Text _dexText;
    [SerializeField] Text _moveSpeedText;
    [SerializeField] Text _attackSpeedText;
    [SerializeField] Text _coolTimeText;
    [SerializeField] Text _eriaText;
    [SerializeField] Text _numberText;
    [SerializeField] Text _expText;
    [SerializeField] Text _getEriaText;

    public void SetStatsText()
    {
        _powerText.text = $"Power:{_power}";
        _maxHpText.text = $"MaxHp:{_maxHp}";
        _dexText.text = $"Dex:{_dex}";
        _moveSpeedText.text = $"MoveSpeed:{_moveSpeed}";
        _attackSpeedText.text = $"AtSpeed:{_attackSpeed}";
        _coolTimeText.text = $"CoolTime:{_coolTime}";
        _eriaText.text = $"Eria:{_eria}";
        _numberText.text = $"Num:{_number}";
        _expText.text = $"Exp:{_expUpper}";
        _getEriaText.text = $"GetEria:{_getEria}";

    }


    private void OnEnable()
    {
        _powerOrizin = _power;
        _maxHpOrizin = _maxHp;
        _dexOrizin = _dex;
        _moveSpeedOrizin = _moveSpeed;
        _attackSpeedOrizin = _attackSpeed;
        _coolTimeOrizin = _coolTime;
        _eriaOrizin = _eria;
        _numberOrizin = _number;
        _expUpperOrizin = _expUpper;
        _getEriaOrizin = _getEria;
    }

    void Start()
    {
        SetStatsText();
    }

    //addPercentの使用方法
    //元を1として、追加する場合は1.1や1.5と来る(それぞれ、10%,50%アップという意味)
    //例)1.1だったら 0.1追加。0.9だったら0.1引くということになる


    public void SetGetEria(Vector3 _size)
    {
        _getEriaStartSize = _size;
    }



    public void ChangeAttackPower(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _powerPercent += (percent - 1);
        else if (percent < 0) _powerPercent -= (1 - percent);

        if (set > 0) _powerOrizin = set;
        _power = _powerOrizin * _powerPercent + add;
        Debug.Log("AttackPowerが:" + _power);
    }


    public void ChangeAttackSpeed(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _attackSpeedPercent += (percent - 1);
        else if (percent < 0) _attackSpeedPercent -= (1 - percent);

        if (set > 0) _attackSpeedOrizin = set;
        _attackSpeed = _attackSpeedOrizin * _attackSpeedPercent + add;
        Debug.Log("AttackSpeedが:" + _attackSpeed);
    }

    public void ChangeCoolTIme(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _coolTimePercent += (percent - 1);
        else if (percent < 0) _coolTimePercent -= (1 - percent);

        if (set > 0) _coolTimeOrizin = set;
        _coolTime = _coolTimeOrizin * _coolTimePercent + add;
        Debug.Log("CoolTimeが:" + _coolTime);
    }

    public void ChangeAttackEria(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _eriaPercent += (percent - 1);
        else if (percent < 0) _eriaPercent -= (1 - percent);

        if (set > 0) _eriaOrizin = set;
        _eria = _eriaOrizin * _eriaPercent + add;
        Debug.Log("AttackEriaが:" + _eria);
    }

    public void ChangeNumber(int set, int add)
    {
        if (set > 0) _numberOrizin += set;
        _number = _numberOrizin + add;
    }

    public void ChamgeExp(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _expUpperPercent += (percent - 1);
        else if (percent < 0) _expUpperPercent -= (1 - percent);

        if (set > 0) _expUpperOrizin = set;
        _expUpper = _expUpperOrizin * _expUpperPercent + add;
        Debug.Log("Expが:" + _expUpper);
    }


    public void ChangeGetEria(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _getEriaPercent += (percent - 1);
        else if (percent < 0) _getEriaPercent -= (1 - percent);

        if (set > 0) _getEriaOrizin = set;
        _getEria = _getEriaOrizin * _getEriaPercent + add;

        //獲得範囲の円の大きさを変える
        Vector2 size = _getEriaStartSize * _getEria;
        _getEriaCircle.transform.localScale = size;

        Debug.Log("GetEriaが:" + _getEria);
    }

    public void ChangeMaxHp(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _maxHpPercent += (percent - 1);
        else if (percent < 0) _maxHpPercent -= (1 - percent);

        if (set > 0) _maxHpOrizin = set;
        _maxHp = (int)_maxHpOrizin * (int)_maxHpPercent + (int)add;
        Debug.Log("MaxHpが:" + _maxHp);
        _playerHp.HpSlider.maxValue = _maxHp;
    }

    public void ChangeDex(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _dexPercent += (percent - 1);
        else if (percent < 0) _dexPercent -= (1 - percent);

        if (set > 0) _dexOrizin = set;
        _dex = _dexOrizin * _dexPercent + add;
        Debug.Log("Dexが:" + _dex);
    }

    public void ChangeMoveSpeed(float set, float percent, float add)
    {
        //%の計算
        if (percent > 0) _moveSpeedPercent += (percent - 1);
        else if (percent < 0) _moveSpeedPercent -= (1 - percent);

        if (set > 0) _moveSpeedOrizin = set;
        _moveSpeed = _moveSpeedOrizin * _moveSpeedPercent + add;
        Debug.Log("MoveSpeedが:" + _moveSpeed);
    }

    public void Update()
    {

    }
}
