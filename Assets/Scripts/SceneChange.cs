using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public static string sceneName = "";

    public void changeScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneChange.sceneName = sceneName;
        SceneManager.LoadScene(sceneName);
    }
}
