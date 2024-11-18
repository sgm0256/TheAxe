using System;
using UnityEngine;

namespace Core.InteractiveObjects
{
    [CreateAssetMenu(menuName = "SO/InteractiveInfo")]
    public class InteractiveObjectInfoSO : ScriptableObject, ICloneable
    {
        [field: SerializeField] public InteractiveType Type { get; private set; }
        [field: SerializeField] public float BaseValue { get; set; }
        

        public object Clone()
        {
            return Instantiate(this);
        }
    }
}
