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
        //デバッグ用
        //_pool.ForEach(x => Debug.Log($"オブジェクト名:{x.Object.name} 種類:{x.Type}"));
    }

    /// <summary>
    /// 設定したオブジェクトの種類,数だけプールにオブジェクトを生成して追加する
    /// 再帰呼び出しを用いている
    /// </summary>
    private void CreatePool()
    {
        if (_poolCountIndex >= _textPoolData.Data.Length)
        {
            //Debug.Log("すべてのオブジェクトを生成しました。");
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
    /// オブジェクトを使いたいときに呼び出す関数
    /// </summary>
    /// <param name="position">オブジェクトの位置を指定する</param>
    /// <param name="objectType">オブジェクトの種類</param>
    /// <returns>生成したオブジェクト</returns>
    public GameObject UseTextObject(Vector2 position, TextType objectType,float damage)
    {
        //if (PauseManager.Instance.PauseFlg == true)
        //{
        //    Debug.LogWarning($"{objectType}を使用するリクエストを受けました\nポーズ中なので使用しません");
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

        Debug.LogWarning($"{objectType}のプールのオブジェクト数が足りなかったため新たにオブジェクトを生成します" +
        $"\nこのオブジェクトはプールの最大値が少ない可能性があります" +
        $"現在{objectType}の数は{_pool.FindAll(x => x.Type == objectType).Count}です");

        return newObj;
    }

    /// <summary>オブジェクトをプールするためのクラス</summary>
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