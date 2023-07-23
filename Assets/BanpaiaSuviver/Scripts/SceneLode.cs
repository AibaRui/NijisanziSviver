using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLode : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;


    public void GoNextScene()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
