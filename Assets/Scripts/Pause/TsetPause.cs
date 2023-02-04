using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TsetPause : MonoBehaviour
{

    public bool PauseFlg { get; private set; } = false;

    event Action<bool> _onPauseResume = default;




    /// <summary>
    /// 一時停止・再開を切り替える
    /// </summary>
    public void PauseResume()
    {
        PauseFlg = !PauseFlg;
        _onPauseResume(PauseFlg);  // これで変数に代入した関数を全て呼び出せる
    }

    /// <summary>
    /// 一時停止、再開時の関数をデリゲートに登録する関数
    /// 
    /// 一時停止を実装したいスクリプトから呼び出す
    /// 
    /// OnEnable関数で PauseManager.Instance.SetEvent(this); と記述される
    /// </summary>
    /// <param name="pauseable"></param>
    public void SetEvent(IPauseable pauseable)
    {
        _onPauseResume += pauseable.PauseResume;
    }

    /// <summary>
    /// 一時停止、再開時の関数をデリゲートから登録を解除する関数
    /// 
    /// 一時停止を実装したいスクリプトから呼び出す
    /// 
    /// OnDisable関数でPauseManager.Instance.RemoveEvent(this); と記述される
    /// </summary>
    /// <param name="pauseable"></param>
    public void RemoveEvent(IPauseable pauseable)
    {
        _onPauseResume -= pauseable.PauseResume;
    }
}
