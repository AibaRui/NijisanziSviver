using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour, IDamageble, IHealble
{

    [SerializeField] Text _text;

    [SerializeField] private Slider _slider;


    public Slider HpSlider{ get => _slider; }

    MainStatas _mainStatas;

    private int _nowHp;

    public int NowHp { get => _nowHp; }


    [SerializeField] AudioSource _aud;
    private void Awake()
    {
        _mainStatas = FindObjectOfType<MainStatas>();
    }
    void Start()
    {
        _nowHp = _mainStatas.MaxHp;
        _text.text = "Hp:" + _nowHp;
        _slider.maxValue = _mainStatas.MaxHp;
        _slider.value = _nowHp;
    }


    public void SetValue()
    {
        _slider.maxValue = _mainStatas.MaxHp;
        _slider.value = _nowHp;
    }


    void IDamageble.AddDamage(float damage)
    {
        _nowHp -= (int)damage;

        if (_nowHp < 0)
        {
            _nowHp = 0;
        }
        _aud.Play();
        _text.text = "Hp:" + _nowHp;
    }

    public void AddHeal(float heal)
    {
        _nowHp += (int)heal;

        if (_nowHp > _mainStatas.MaxHp)
        {
            _nowHp = _mainStatas.MaxHp;
        }
        _text.text = "Hp:" + _nowHp;
    }
}
