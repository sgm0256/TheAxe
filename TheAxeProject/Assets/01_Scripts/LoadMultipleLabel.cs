using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadMultipleLabel : MonoBehaviour
{
    [SerializeField] private List<string> _labelName = new List<string>() { "Audio" };

    private AsyncOperationHandle<IList<AudioClip>> _loadHandle;

    public List<AudioClip> clips = new List<AudioClip>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LoadAssets();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ReleaseAssets();
        }
    }

    private void ReleaseAssets()
    {
        clips.Clear();
        Addressables.Release(_loadHandle);
    }

    private void LoadAssets()
    {
        clips.Clear();
        
        _loadHandle = Addressables.LoadAssetsAsync<AudioClip>(_labelName, (asset) =>
        {
            clips.Add(asset);
        }, Addressables.MergeMode.Union);
    }
}
