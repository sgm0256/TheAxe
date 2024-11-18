using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    private bool _isPress;
    [SerializeField] private AssetLoader _loader;

    private void Awake()
    {
        _loader.AssetLoadedEvent += HandleLoad;
    }

    private void HandleLoad()
    {
        _isPress = true;
    }

    private void Update()
    {
        if (_isPress)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
