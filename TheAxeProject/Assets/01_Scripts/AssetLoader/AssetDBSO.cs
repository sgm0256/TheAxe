using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

[CreateAssetMenu(menuName = "SO/AssetDB")]
public class AssetDBSO : ScriptableObject
{
    public List<AssetReference> gameObjectAsset;
    public List<AssetReferenceT<AudioClip>> audioAssets;

    private Dictionary<string, AssetReference> _db;

    public event Action<int> LoadCountEvent;
    public event Action<int, string> LoadMessageEvent;

    public void Initialize()
    {
        LoadNonPoolingAssets();
    }

    private async void LoadNonPoolingAssets()
    {
        _db = new Dictionary<string, AssetReference>();

        LoadCountEvent?.Invoke(gameObjectAsset.Count);
        LoadCountEvent?.Invoke(audioAssets.Count);

        await Task.WhenAll(gameObjectAsset.Select(
            asset => asset.LoadAssetAsync<GameObject>().Task));
        LoadMessageEvent?.Invoke(gameObjectAsset.Count, "GameObject asset loaded");

        await Task.WhenAll(audioAssets.Select(
            asset => asset.LoadAssetAsync<AudioClip>().Task));
        LoadMessageEvent?.Invoke(audioAssets.Count, "Audio asset loaded");

        gameObjectAsset.ForEach(x => _db.Add(x.AssetGUID, x));
        audioAssets.ForEach(x => _db.Add(x.AssetGUID, x));
    }

    public T GetAsset<T>(string guid) where T : Object
    {
        if (_db.TryGetValue(guid, out AssetReference assetRef))
        {
            return assetRef.Asset as T;
        }
        return default;
    }

    public AssetReference GetAssetRef(string guid)
    {
        return _db.GetValueOrDefault(guid);
    }
}
