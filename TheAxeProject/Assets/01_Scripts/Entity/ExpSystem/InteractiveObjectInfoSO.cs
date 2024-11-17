using UnityEngine;

namespace Core.InteractiveObjects
{
    [CreateAssetMenu(menuName = "SO/InteractiveInfo")]
    public class InteractiveObjectInfoSO : ScriptableObject
    {
        [field: SerializeField] public InteractiveType Type { get; private set; }
        [field: SerializeField] public float Value { get; set; }
    }
}
