#region

using UnityEngine;

#endregion

public abstract class ResourceLoader : MonoBehaviour
{
    public delegate void LoadCallback<T>(T loadedObject) where T : Object;

    public delegate void SceneLoadCallback();

    public abstract void Initialize(string hostUrl);
    public abstract void LoadAsset<T>(string assetName, string bundleName, LoadCallback<T> onLoad) where T : Object;
    public abstract void LoadScene(string environmentName, bool additive = true, SceneLoadCallback onLoad = null);
    public abstract WWW GetImageLoader(string url);
    public abstract WWW GetAudioLoader(string url);
}