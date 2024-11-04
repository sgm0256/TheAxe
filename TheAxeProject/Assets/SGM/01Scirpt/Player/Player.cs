using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputCompo;

    private Dictionary<Type, IPlayerComponent> _components;

    private void Awake()
    {
        _components = new Dictionary<Type, IPlayerComponent>();
        GetComponentsInChildren<IPlayerComponent>()
            .ToList()
            .ForEach(compo => _components.Add(compo.GetType(), compo));
        _components.Add(_inputCompo.GetType(), _inputCompo);

        foreach (var compo in _components.Values)
        {
            compo.Initialize(this);
        }
    }

    public T GetCompo<T>() where T : class
    {
        if (_components.TryGetValue(typeof(T), out IPlayerComponent compo))
        {
            return compo as T;
        }
        return default;
    }
}
