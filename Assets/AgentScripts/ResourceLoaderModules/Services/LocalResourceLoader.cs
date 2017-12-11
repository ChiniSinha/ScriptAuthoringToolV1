#region

using System.Collections;
using System.IO;
using AssetBundles;
using UnityEngine;

#endregion

public class LocalResourceLoader : ResourceLoader
{
    public override void Initialize(string hostUrl)
    {
        DoInitialize();
    }

    public override void LoadAsset<T>(string assetName, string bundleName, LoadCallback<T> onLoad)
    {
        StartCoroutine(GetPrefabFromBundle(assetName, bundleName, onLoad));
    }

    public override void LoadScene(string environmentName, bool additive=true, SceneLoadCallback onLoad=null)
    {
        StartCoroutine(DoLoadScene(environmentName, additive, onLoad));
    }

    public override WWW GetImageLoader(string url)
    {
        return GetFileLoader(url);
    }

    public override WWW GetAudioLoader(string url)
    {
        return GetFileLoader(url);
    }

    private void DoInitialize()
    {
        
        Globals.EventBus.Dispatch(new ResourceLoaderReadyEvent());
    }

    protected IEnumerator GetPrefabFromBundle<T>(string assetName, string bundleName, LoadCallback<T> onLoad)
        where T : Object
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName, typeof(T));
        if (request == null)
        {
            yield break;
        }
        yield return StartCoroutine(request);

        T prefab = request.GetAsset<T>();
        onLoad(prefab);
    }

    protected IEnumerator DoLoadScene(string sceneName, bool additive, SceneLoadCallback onLoad)
    {
        AssetBundleLoadOperation request = AssetBundleManager.LoadLevelAsync("environments/" + sceneName.ToLower(),
            sceneName, additive);
        if (request == null)
        {
            yield break;
        }
        yield return StartCoroutine(request);
        if (onLoad != null)
        {
            onLoad();
        }
    }

    protected WWW GetFileLoader(string uri)
    {
        if (uri.Contains("://"))
        {
            return new WWW(uri);
        }
        if (Path.IsPathRooted(uri))
        {
            return new WWW("file://" + uri);
        }
        return new WWW("file://" + Path.Combine(Application.streamingAssetsPath, uri));
    }
}