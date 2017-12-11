#region

using System;
using System.Collections;
using System.IO;
using AssetBundles;
using UnityEngine;
using Object = UnityEngine.Object;

#endregion

public class WebResourceLoader : ResourceLoader
{
    protected string _basePath;
    protected UriBuilder _builder;

    public override void Initialize(string baseUri)
    {
        DoInitialize(baseUri);
    }

    public override void LoadAsset<T>(string assetName, string bundleName, LoadCallback<T> onLoad)
    {
        StartCoroutine(GetPrefabFromBundle<T>(assetName, bundleName, onLoad));
    }

    public override void LoadScene(string environmentName, bool additive=true, SceneLoadCallback onLoad=null)
    {
        StartCoroutine(DoLoadScene(environmentName, additive, onLoad));
    }

    public override WWW GetImageLoader(string relativeUrl)
    {
        _builder.Path = _basePath + relativeUrl;
        return new WWW(_builder.Uri.AbsoluteUri);
    }

    public override WWW GetAudioLoader(string relativeUrl)
    {
        if (relativeUrl.Contains("://"))
        {
            return new WWW(relativeUrl);
        }
        _builder.Path = relativeUrl;
        return new WWW(_builder.Uri.AbsoluteUri);
    }

    protected void DoInitialize(string baseUri)
    {
        _builder = new UriBuilder(baseUri);
        _basePath = _builder.Path;

        
        Globals.EventBus.Dispatch(new ResourceLoaderReadyEvent());
    }

    protected IEnumerator GetPrefabFromBundle<T>(string assetName, string bundleName, LoadCallback<T> onLoad) where T : Object
    {
        AssetBundleLoadAssetOperation request = AssetBundleManager.LoadAssetAsync(bundleName, assetName, typeof(T));
        if (request == null)
        {
            yield break;
        }
        yield return StartCoroutine(request);

        T prefab = request.GetAsset<T>();
        if (onLoad != null)
        {
            onLoad(prefab);
        }
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
}