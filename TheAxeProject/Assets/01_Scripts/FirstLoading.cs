using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/FirstLoading")]
public class FirstLoading : ScriptableObject
{
    public bool _isLoaded = false;
    
    public FirstLoading Clone()
    {
        return new FirstLoading();
    }
}
