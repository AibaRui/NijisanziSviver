using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaitasManager : MonoBehaviour
{
    /// <summary>”‚ª‘‚¦‚é‚Æ”­Ë•¨‚Ì”‚ª‘‚¦‚é</summary>
    [SerializeField]  float _weaponAdd = 0;

    /// <summary>”{—¦‚ªã‚ª‚é‚Æ‰Î—Í‚ªã‚ª‚é</summary>
    [SerializeField] float _attackPower = 1;

    /// <summary>”{—¦‚ª‰º‚ª‚é‚ÆƒŒ[ƒg‚ª‘‚­‚È‚é</summary>
    [SerializeField] float _attackLate = 1;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>”­Ë•¨‚ğ‘‚â‚·</summary>
    public void AddWepon(int add)
    {
        _weaponAdd+=add;
    }

    public void AttackPowerUp(float s)
    {
        _attackPower += _attackPower * s;
    }


    public void AttackLateUp(float s)
    {
        _attackLate -= _attackLate * s;
    }

}
