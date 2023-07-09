using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ObjectPool : MonoBehaviour
{
    private ObjectsPoolData _objectsPoolData = default;

    [SerializeField]
    private List<ObjectsPoolData> _poolDatas = new List<ObjectsPoolData>();

    private ObjectsPoolData _addObjectPoolData = default;

    private List<Pool> _pool = new List<Pool>();
    private int _poolCountIndex = 0;

    private int _poolAddCountIndex = 0;

    void Awake()
    {
        foreach (var data in _poolDatas)
        {
            _poolCountIndex = 0;
            _objectsPoolData = data;
            CreatePool();
        }


        //�f�o�b�O�p
        //_pool.ForEach(x => Debug.Log($"�I�u�W�F�N�g��:{x.Object.name} ���:{x.Type}"));
    }

    /// <summary>
    /// �ݒ肵���I�u�W�F�N�g�̎��,�������v�[���ɃI�u�W�F�N�g�𐶐����Ēǉ�����
    /// �ċA�Ăяo����p���Ă���
    /// </summary>
    private void CreatePool()
    {
        if (_poolCountIndex >= _objectsPoolData.Data.Length)
        {
            //Debug.Log("���ׂẴI�u�W�F�N�g�𐶐����܂����B");
            return;
        }

        for (int i = 0; i < _objectsPoolData.Data[_poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(_objectsPoolData.Data[_poolCountIndex].Prefab, this.transform);
            bullet.SetActive(false);
            _pool.Add(new Pool { Object = bullet, Type = _objectsPoolData.Data[_poolCountIndex].Type });
        }

        _poolCountIndex++;
        CreatePool();
    }



    /// <summary>
    /// �I�u�W�F�N�g���g�������Ƃ��ɌĂяo���֐�
    /// </summary>
    /// <param name="position">�I�u�W�F�N�g�̈ʒu���w�肷��</param>
    /// <param name="objectType">�I�u�W�F�N�g�̎��</param>
    /// <returns>���������I�u�W�F�N�g</returns>
    public GameObject UseObject(Vector2 position, PoolObjectType objectType)
    {
        //if (PauseManager.Instance.PauseFlg == true)
        //{
        //    Debug.LogWarning($"{objectType}���g�p���郊�N�G�X�g���󂯂܂���\n�|�[�Y���Ȃ̂Ŏg�p���܂���");
        //    return null;
        //}

        foreach (var pool in _pool)
        {
            if (pool.Object.activeSelf == false && pool.Type == objectType)
            {
                pool.Object.SetActive(true);
                pool.Object.transform.position = position;
                return pool.Object;
            }
        }

        GameObject newObj = default;

        foreach (var a in _poolDatas)
        {
            foreach (var t in a.Data)
            {
                if (t.Type == objectType)
                {
                    //newObj = Instantiate(Array.Find(a.Data, x => x.Type == objectType).Prefab, this.transform);
                    newObj = Instantiate(t.Prefab, this.transform);
                    break;
                }
            }
        }

       // Debug.Log(newObj.name);

        newObj.transform.position = position;
        newObj.SetActive(true);
        _pool.Add(new Pool { Object = newObj, Type = objectType });

        Debug.LogWarning($"{objectType}�̃v�[���̃I�u�W�F�N�g��������Ȃ��������ߐV���ɃI�u�W�F�N�g�𐶐����܂�" +
        $"\n���̃I�u�W�F�N�g�̓v�[���̍ő�l�����Ȃ��\��������܂�" +
        $"����{objectType}�̐���{_pool.FindAll(x => x.Type == objectType).Count}�ł�");

        return newObj;
    }

    /// <summary>�I�u�W�F�N�g���v�[�����邽�߂̃N���X</summary>
    private class Pool
    {
        public GameObject Object { get; set; }
        public PoolObjectType Type { get; set; }
    }
}

public enum PoolObjectType
{
    ///Exp
    LowExp,
    MidleExp,
    HightExp,


    //Enemy
    Mameneko,
    Gundou,
    Masyumaro,
    Kuzuha,


    //Weapon
    Knife,
    IceMagick,
    Bomb,
    Baran,
    Thunder,
    Book,
    Hiyoko,
    Ninniku,

    //EvolutionWeapon
    EW_Knife,
    EW_IceMagick,
    EW_Bomb,
    EW_Baran,
    EW_Thunder,
    EW_Book,
    EW_Hiyoko,
    EW_Ninniku,

    //Box
    Box,
    //
    HealItem,


}