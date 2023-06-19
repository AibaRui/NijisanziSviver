using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.UI;

public class DamageTextPool : MonoBehaviour
{
    [SerializeField]
    private TextPoolData _textPoolData = default;

    [SerializeField] string _nameAnimRight;
    [SerializeField] string _nameAnimLeft;

    private List<TextPool> _pool = new List<TextPool>();
    private int _poolCountIndex = 0;

    void Awake()
    {
        _poolCountIndex = 0;
        CreatePool();
        //�f�o�b�O�p
        //_pool.ForEach(x => Debug.Log($"�I�u�W�F�N�g��:{x.Object.name} ���:{x.Type}"));
    }

    /// <summary>
    /// �ݒ肵���I�u�W�F�N�g�̎��,�������v�[���ɃI�u�W�F�N�g�𐶐����Ēǉ�����
    /// �ċA�Ăяo����p���Ă���
    /// </summary>
    private void CreatePool()
    {
        if (_poolCountIndex >= _textPoolData.Data.Length)
        {
            //Debug.Log("���ׂẴI�u�W�F�N�g�𐶐����܂����B");
            return;
        }

        for (int i = 0; i < _textPoolData.Data[_poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(_textPoolData.Data[_poolCountIndex].Prefab, this.transform);
            bullet.SetActive(false);
            _pool.Add(new TextPool { Object = bullet, Type = _textPoolData.Data[_poolCountIndex].Type });
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
    public GameObject UseTextObject(Vector2 position, TextType objectType,float damage)
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
                pool.Object.transform.GetChild(0).GetComponent<Text>().text = damage.ToString();

                pool.Object.transform.position = position;

                var x = UnityEngine.Random.Range(0, 2);

                if (x == 0) pool.Object.GetComponent<Animator>().Play(_nameAnimRight);
                else pool.Object.GetComponent<Animator>().Play(_nameAnimLeft);




                return pool.Object;
            }
        }


        var newObj = Instantiate(Array.Find(_textPoolData.Data, x => x.Type == objectType).Prefab, this.transform);
        newObj.transform.position = position;
        newObj.SetActive(true);
        _pool.Add(new TextPool { Object = newObj, Type = objectType });

        Debug.LogWarning($"{objectType}�̃v�[���̃I�u�W�F�N�g��������Ȃ��������ߐV���ɃI�u�W�F�N�g�𐶐����܂�" +
        $"\n���̃I�u�W�F�N�g�̓v�[���̍ő�l�����Ȃ��\��������܂�" +
        $"����{objectType}�̐���{_pool.FindAll(x => x.Type == objectType).Count}�ł�");

        return newObj;
    }

    /// <summary>�I�u�W�F�N�g���v�[�����邽�߂̃N���X</summary>
    private class TextPool
    {
        public GameObject Object { get; set; }
        public TextType Type { get; set; }
    }
}

public enum TextType
{
    Nomal,
    Cretical,s

}