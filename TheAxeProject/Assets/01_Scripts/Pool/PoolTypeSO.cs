using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ObjectPooling
{
    [CreateAssetMenu(menuName = "SO/Pool/Type")]
    public class PoolTypeSO : ScriptableObject
    {
        public string typeName;
        public AssetReference assetRef;
        public int initCount;
    }
}
