using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string _nextSceneName;

    public void NextScene()
    {
        SceneManager.LoadScene(_nextSceneName);
    }
}
