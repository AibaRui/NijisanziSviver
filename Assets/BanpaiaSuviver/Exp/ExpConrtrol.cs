using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpConrtrol : MonoBehaviour
{
    [SerializeField] ExperiencePointData _experiencePointData;

    AudioSource _aud;
    ExpPause _expPause;
    bool _isGet = false;

    private void OnEnable()
    {

    }

    private void Awake()
    {
       _aud =  GetComponent<AudioSource>();
        _expPause = FindObjectOfType<ExpPause>();
        _experiencePointData.Player = GameObject.FindGameObjectWithTag("Player");
        _experiencePointData.LevelUpController = GameObject.FindObjectOfType<LevelUpController>();
    }

    private void Update()
    {
        if (!_expPause.IsPause && !_expPause.IsPauseLevelUp)
        {
            if (_isGet)
            {
                transform.position = Vector2.MoveTowards(transform.position, _experiencePointData.Player.transform.position, 0.2f);
                float dir = Vector2.Distance(transform.position, _experiencePointData.Player.transform.position);
                if (dir <= 0.2f)
                {
                    _isGet = false;
                    _experiencePointData.LevelUpController.AddExp(_experiencePointData.ExpPoint);
                    transform.position = Camera.main.transform.position;
                    _aud.Play();
                    StartCoroutine(AudioPlaying());
                }
            }
        }
    }

    IEnumerator AudioPlaying()
    {

        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GetArea")
        {
            _isGet = true;
        }
    }



}
