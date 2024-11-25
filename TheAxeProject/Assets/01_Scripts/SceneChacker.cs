using MKDir;
using UnityEngine;

public class SceneChacker : MonoSingleton<SceneChacker>
{
    [SerializeField] private FirstLoading _firstLoading;
    public FirstLoading FirstLod;

    protected override void Awake()
    {
        base.Awake();
        FirstLod = _firstLoading.Clone();
    }
}
