using ObjectPooling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string _nextSceneName;
    [SerializeField] private FirstLoading _first;
    
    public void NextScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_nextSceneName);
    }

    public void AllPushNextScene()
    {
        Time.timeScale = 1;
        GameManager.Instance.SetGame();
        SingletonPoolManager.Instance?.AllPush();
        SceneManager.LoadScene(_nextSceneName);
    }
    
    public void PoolNextScene()
    {
        Time.timeScale = 1;
        if (SceneChacker.Instance.FirstLod._isLoaded)
        {
            SceneManager.LoadScene("Game");
            return;
        }
        SceneChacker.Instance.FirstLod._isLoaded = true;
        SceneManager.LoadScene(_nextSceneName);
    }
}