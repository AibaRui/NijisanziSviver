using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLifeTime : MonoBehaviour
{
    [SerializeField] float _lifeTime = 3;
    float _countTime = 0;


    AudioSource _aud;
    void Start()
    {

    }




    void Update()
    {
        _countTime += Time.deltaTime;

        if (_countTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }



}
