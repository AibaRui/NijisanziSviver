using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EnemySpwnController : MonoBehaviour
{
    [SerializeField] UnityEvent _firstEnemy;


    [SerializeField] List<UnityEvent> _enemyChangeEvents = new List<UnityEvent>();



    [Header("�G��ς���^�C�~���O�̕b��")]
    [Tooltip("�G��ς���^�C�~���O�̕b��")] [SerializeField] List<float> _enemyChangeTime = new List<float>();

    int _changeEnemyCount = 0;


    [SerializeField] float _gameTime = 300;

    float _countTime = 0;


    void Start()
    {
        _firstEnemy.Invoke();
    }


    void Update()
    {

    }

    private void FixedUpdate()
    {
        EnemyChangeOfTime();
    }


    /// <summary>�o�ߎ��Ԃɂ���āA�G�̎�ނ�ς���֐�</summary>
    ///�o�ߎ��Ԃ̐ݒ肵�����X�g�̗v�f�ƁA�Ăяo��UnityEvents�̗v�f�����킹�Ă���B
    void EnemyChangeOfTime()
    {
        if (_changeEnemyCount < _enemyChangeEvents.Count)
        {
            _countTime += Time.deltaTime;

            //�o�ߎ��ԂɂȂ�����A�C�x���g�����s�B���̌o�ߎ��Ԃ̃��X�g���X�V
            if (_countTime >= _enemyChangeTime[_changeEnemyCount])
            {
                _enemyChangeEvents[_changeEnemyCount].Invoke();
                _changeEnemyCount++;
            }
        }
    }
}
