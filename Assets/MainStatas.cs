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

    /// <summary>•Ší‚Ì“®‚­‘¬‚³</summary>
    [SerializeField] float _attackSpeed = 1;
    private float _attackSpeedOrizin = 0;
    private float _attackSpeedPercent = 1;

    public float AttackSpeed { get => _attackSpeed; }


    /// <summary>ƒN[ƒ‹ŠÔ</summary>
    [SerializeField] float _coolTime = 1;
    private float _coolTimeOrizin = 0;
    private float _coolTimePercent = 1;

    public float CoolTime { get => _coolTime; }

    /// <summary>”ÍˆÍ</summary>
    [SerializeField] float _eria = 1;
    private float _eriaOrizin = 0;
    private float _eriaPercent = 1;

    public float Eria { get => _eria; }


    /// <summary>”­Ë•¨‚Ì”</summary>
    [SerializeField] int _number = 0;
    private int _numberOrizin = 0;

    public int Number { get => _number; }

    /// <summary>ŒoŒ±’l‚Ì‰ÁZ”{—¦</summary>
    [SerializeField] float _expUpper = 1;
    private float _expUpperOrizin = 0;
    private float _expUpperPercent = 1;
    public float ExpUpper { get => _expUpper; }

    /// <summary>æ“¾”ÍˆÍ</summary>
    [SerializeField] float _getEria = 3;
    private float _getEriaOrizin = 0;
    private float _getEriaPercent = 1;

    public float GetEria { get => _getEria; }

    [Header("Šl“¾‚Ì‰~‚Ì‰ŠúƒTƒCƒY")]
    private Vector2 _getEriaStartSize;

    [Header("Šl“¾”ÍˆÍ‚Ì‰~")]
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

    //addPercent‚Ìg—p•û–@
    //Œ³‚ğ1‚Æ‚µ‚ÄA’Ç‰Á‚·‚éê‡‚Í1.1‚â1.5‚Æ—ˆ‚é(‚»‚ê‚¼‚êA10%,50%ƒAƒbƒv‚Æ‚¢‚¤ˆÓ–¡)
    //—á)1.1‚¾‚Á‚½‚ç 0.1’Ç‰ÁB0.9‚¾‚Á‚½‚ç0.1ˆø‚­‚Æ‚¢‚¤‚±‚Æ‚É‚È‚é


    public void SetGetEria(Vector3 _size)
    {
        _getEriaStartSize = _size;
    }



    public void ChangeAttackPower(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _powerPercent += (percent - 1);
        else if (percent < 0) _powerPercent -= (1 - percent);

        if (set > 0) _powerOrizin = set;
        _power = _powerOrizin * _powerPercent + add;
        Debug.Log("AttackPower‚ª:" + _power);
    }


    public void ChangeAttackSpeed(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _attackSpeedPercent += (percent - 1);
        else if (percent < 0) _attackSpeedPercent -= (1 - percent);

        if (set > 0) _attackSpeedOrizin = set;
        _attackSpeed = _attackSpeedOrizin * _attackSpeedPercent + add;
        Debug.Log("AttackSpeed‚ª:" + _attackSpeed);
    }

    public void ChangeCoolTIme(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _coolTimePercent += (percent - 1);
        else if (percent < 0) _coolTimePercent -= (1 - percent);

        if (set > 0) _coolTimeOrizin = set;
        _coolTime = _coolTimeOrizin * _coolTimePercent + add;
        Debug.Log("CoolTime‚ª:" + _coolTime);
    }

    public void ChangeAttackEria(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _eriaPercent += (percent - 1);
        else if (percent < 0) _eriaPercent -= (1 - percent);

        if (set > 0) _eriaOrizin = set;
        _eria = _eriaOrizin * _eriaPercent + add;
        Debug.Log("AttackEria‚ª:" + _eria);
    }

    public void ChangeNumber(int set, int add)
    {
        if (set > 0) _numberOrizin += set;
        _number = _numberOrizin + add;
    }

    public void ChamgeExp(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _expUpperPercent += (percent - 1);
        else if (percent < 0) _expUpperPercent -= (1 - percent);

        if (set > 0) _expUpperOrizin = set;
        _expUpper = _expUpperOrizin * _expUpperPercent + add;
        Debug.Log("Exp‚ª:" + _expUpper);
    }


    public void ChangeGetEria(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _getEriaPercent += (percent - 1);
        else if (percent < 0) _getEriaPercent -= (1 - percent);

        if (set > 0) _getEriaOrizin = set;
        _getEria = _getEriaOrizin * _getEriaPercent + add;

        //Šl“¾”ÍˆÍ‚Ì‰~‚Ì‘å‚«‚³‚ğ•Ï‚¦‚é
        Vector2 size = _getEriaStartSize * _getEria;
        _getEriaCircle.transform.localScale = size;

        Debug.Log("GetEria‚ª:" + _getEria);
    }

    public void ChangeMaxHp(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _maxHpPercent += (percent - 1);
        else if (percent < 0) _maxHpPercent -= (1 - percent);

        if (set > 0) _maxHpOrizin = set;
        _maxHp = (int)_maxHpOrizin * (int)_maxHpPercent + (int)add;
        Debug.Log("MaxHp‚ª:" + _maxHp);
        _playerHp.HpSlider.maxValue = _maxHp;
    }

    public void ChangeDex(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _dexPercent += (percent - 1);
        else if (percent < 0) _dexPercent -= (1 - percent);

        if (set > 0) _dexOrizin = set;
        _dex = _dexOrizin * _dexPercent + add;
        Debug.Log("Dex‚ª:" + _dex);
    }

    public void ChangeMoveSpeed(float set, float percent, float add)
    {
        //%‚ÌŒvZ
        if (percent > 0) _moveSpeedPercent += (percent - 1);
        else if (percent < 0) _moveSpeedPercent -= (1 - percent);

        if (set > 0) _moveSpeedOrizin = set;
        _moveSpeed = _moveSpeedOrizin * _moveSpeedPercent + add;
        Debug.Log("MoveSpeed‚ª:" + _moveSpeed);
    }

    public void Update()
    {

    }
}
