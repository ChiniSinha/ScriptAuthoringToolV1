#region

using UnityEngine;

#endregion

public abstract class ExternalConnection : MonoBehaviour
{
    public string RemoteHost { get; protected set; }
    public abstract bool Authenticated { get; }
    // Use this to communicate with the rest of the client

    public virtual void Setup(string remoteHost = "localhost")
    {
        RemoteHost = remoteHost;
    }
}