using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomStatsUp : ItemBase
{
    [SerializeField] float _randomTime = 10;

    [SerializeField] Text _showText;

    [SerializeField] PauseManager _pauseManager;

    private bool _isPause = false;
    private bool _isLevelUpPause = false;
    private bool _isPauseGetBox = false;

    private IEnumerator _corutin;
    protected override void LevelUpStatas(float _statas)
    {

    }

    public override void LevelUp()
    {
        if (_level < _maxLevel)
        {
            _level++;

            _itemStats = _itemData.GetData(_level, _itemName);

            _thisStatas = _itemStats.MaxHp;
            LevelUpStatas(_thisStatas);
            Debug.Log(_itemName + "レベルアップ！現在のレベルは" + _level);

            if (_level == 1)
            {
                // _corutin = ChangeStatas();
                StartCoroutine(_corutin);
            }
        }
        _mainStatas.SetStatsText();
        LevelUpController _levelUpController = FindObjectOfType<LevelUpController>();
        _levelUpController.ItemLevelUp(_itemName, _level);
    }


    IEnumerator ChangeStatas()
    {
        while (true)
        {
            var r = Random.Range(0, 10);

            float power = 0;
            float attackSpeed = 0;
            float coolTime = 0;
            float attackEria = 0;
            int numbr = 0;
            float exp = 0;
            float getEria = 0;
            float maxHp = 0;
            float dex = 0;
            float moveSpeed = 0;

            if (r == 0)
            {
                power = _itemStats.AttackPower * _mainStatas.Power;
                _mainStatas.ChangeAttackPower(0, 1, power);
                if (_showText) _showText.text = "Power:+" + power.ToString();
            }
            else if (r == 1)
            {
                attackSpeed = _itemStats.AttackSpeed * _mainStatas.AttackSpeed;
                _mainStatas.ChangeAttackSpeed(0, 1, attackSpeed);
                if (_showText) _showText.text = "AttackSpeed:+" + attackSpeed.ToString();
            }
            else if (r == 2)
            {
                coolTime = -_itemStats.CoolTime * _mainStatas.CoolTime;
                _mainStatas.ChangeCoolTIme(0, 1, coolTime);
                if (_showText) _showText.text = "CoolTime:+" + coolTime.ToString();
            }
            else if (r == 3)
            {
                attackEria = _itemStats.AttackEria * _mainStatas.Eria;
                _mainStatas.ChangeAttackEria(0, 1, attackEria);
                if (_showText) _showText.text = "AttacEria:+" + attackEria.ToString();
            }
            else if (r == 4)
            {
                numbr = (int)_itemStats.Number;
                _mainStatas.ChangeNumber(0, numbr);
                if (_showText) _showText.text = "Number:+" + numbr.ToString();
            }
            else if (r == 5)
            {
                exp = _itemStats.Exp * _mainStatas.ExpUpper;
                _mainStatas.ChamgeExp(0, 1, exp);
                if (_showText) _showText.text = "Exp:+" + exp.ToString();
            }
            else if (r == 6)
            {
                getEria = _itemStats.GetEria * _mainStatas.GetEria;
                _mainStatas.ChangeGetEria(0, 1, getEria);
                if (_showText) _showText.text = "GetEria:+" + getEria.ToString();
            }
            else if (r == 7)
            {
                maxHp = _itemStats.MaxHp * _mainStatas.MaxHp;
                _mainStatas.ChangeMaxHp(0, 1, maxHp);
                if (_showText) _showText.text = "MaxHp:+" + maxHp.ToString();
            }
            else if (r == 8)
            {
                dex = _itemStats.Dex * _mainStatas.Dex;
                _mainStatas.ChangeDex(0, 1, dex);
                if (_showText) _showText.text = "Dex:+" + dex.ToString();
            }
            else if (r == 9)
            {
                moveSpeed = _itemStats.MoveSpeed * _mainStatas.MoveSpeed;
                _mainStatas.ChangeMoveSpeed(0, 1, moveSpeed);


                if (_showText) _showText.text = "MoveSpeed:+" + moveSpeed.ToString();
            }
            yield return new WaitForSeconds(10);

            //ステータスリセット
            _mainStatas.ChangeAttackPower(0, 1, -power);
            _mainStatas.ChangeAttackSpeed(0, 1, -attackSpeed);
            _mainStatas.ChangeCoolTIme(0, 1, -coolTime);
            _mainStatas.ChangeAttackEria(0, 1, -attackEria);
            _mainStatas.ChangeNumber(0, -numbr);
            _mainStatas.ChamgeExp(0, 1, -exp);
            _mainStatas.ChangeGetEria(0, 1, -getEria);
            _mainStatas.ChangeMaxHp(0, 1, -maxHp);
            _mainStatas.ChangeDex(0, 1, -dex);
            _mainStatas.ChangeMoveSpeed(0, 1, -moveSpeed);
        }
    }


    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
        _pauseManager.OnPauseResume += PauseResume;
        _pauseManager.OnLevelUp += LevelUpPauseResume;
        _corutin = ChangeStatas();
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
        _pauseManager.OnPauseResume -= LevelUpPauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    void LevelUpPauseResume(bool isPause)
    {
        if (isPause)
        {
            LevelUpPause();
        }
        else
        {
            LevelUpResume();
        }
    }

    void LevelUpPause()
    {
        if (!_isPauseGetBox)
        {
            _isLevelUpPause = true;
            if (_corutin != null)
            {
                StopCoroutine(_corutin);
            }
        }
    }

    public void LevelUpResume()
    {
        _isLevelUpPause = false;

        if (_corutin != null)
        {
            StartCoroutine(_corutin);
        }
    }

    public void Pause()
    {
        _isPause = true;
        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_corutin != null)
            {
                StopCoroutine(_corutin);
            }
        }
    }

    public void Resume()
    {
        _isPause = false;

        if (!_isLevelUpPause && !_isPauseGetBox)
        {
            if (_corutin != null)
            {
                StartCoroutine(_corutin);
            }
        }
    }
}
