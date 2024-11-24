using ObjectPooling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string _nextSceneName;

    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_nextSceneName);
    }

    public void AllPushNextScene()
    {
        Time.timeScale = 1;
        SingletonPoolManager.Instance?.AllPush();
        SceneManager.LoadScene(_nextSceneName);
    }
}