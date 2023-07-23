using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEffectAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _aud;

    [SerializeField] private AudioClip _aud1;

    [SerializeField] private AudioClip _aud2;

    [SerializeField] private AudioClip _aud3;

    public void Play1()
    {
        _aud.PlayOneShot(_aud1);
    }

    public void Play2()
    {
        _aud.PlayOneShot(_aud2);
    }

    public void Play3()
    {
        _aud.PlayOneShot(_aud3);
    }

}
