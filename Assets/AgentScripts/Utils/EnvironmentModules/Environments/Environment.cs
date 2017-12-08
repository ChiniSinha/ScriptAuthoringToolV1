#region

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

public abstract class Environment : MonoBehaviour
{
    [SerializeField] protected List<AudioSource> _audioSources;

    //[SerializeField] protected List<VirtualCamera> _cameras = new List<VirtualCamera>();

    [SerializeField] protected bool _default;

    //[SerializeField] protected VirtualCamera _defaultCamera;

    [SerializeField] protected string _name;

    protected List<AudioSource> _playingAudioSources = new List<AudioSource>();
    public Transform AgentStartPosition;

    public string Label
    {
        get { return _name; }
    }

    public bool IsDefault
    {
        get { return _default; }
    }

    public bool IsActive { get; protected set; }

    public void PlayAudio(string url, string playerName)
    {
        StartCoroutine(LoadAndPlayAudio(url, playerName));
    }

    private IEnumerator LoadAndPlayAudio(string url, string playerName)
    {
        WWW audioLoader = Globals.Get<ResourceLoader>().GetAudioLoader(url);
        yield return audioLoader;

        if (audioLoader.GetAudioClip() == null)
        {
            Debug.LogError("Audio file not found: " + url);
            yield break;
        }

        AudioSource player = null;
        if (string.IsNullOrEmpty(playerName))
        {
            player = _audioSources[0];
        }
        else
        {
            for (int i = 0; i < _audioSources.Count; i++)
            {
                if (_audioSources[i].name == playerName)
                {
                    player = _audioSources[i];
                    break;
                }
            }
        }

        if (player == null)
        {
            Debug.LogError("Player not found: " + url);
            yield break;
        }

        player.clip = audioLoader.GetAudioClip(false, false);
        while (player.clip.loadState != AudioDataLoadState.Loaded)
        {
            if (player.clip.loadState == AudioDataLoadState.Failed)
            {
                Debug.LogError("Audio loading failed: " + url);
                yield break;
            }
            yield return new WaitForEndOfFrame();
        }

        player.Play();
        _playingAudioSources.Add(player);
        Globals.EventBus.Dispatch(new AudioEvent(AudioEvent.Type.PLAYBACK_START));
    }

    private void Update()
    {
        for (int i = 0; i < _playingAudioSources.Count; i++)
        {
            if (!_playingAudioSources[i].isPlaying)
            {
                _playingAudioSources.RemoveAt(i);
                i--;
                Globals.EventBus.Dispatch(new AudioEvent(AudioEvent.Type.PLAYBACK_COMPLETE));
            }
        }
    }

    public void PositionAgent(Agent agent)
    {
        agent.transform.SetParent(null);
        agent.transform.rotation = AgentStartPosition.rotation;
        agent.transform.position = AgentStartPosition.position;
        agent.transform.localScale = AgentStartPosition.lossyScale;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        //_defaultCamera.Activate();
        IsActive = true;
        Globals.EventBus.Dispatch(new EnvironmentReadyEvent(this));
    }

    public void Deactivate()
    {
        IsActive = false;
        //for (int i = 0; i < _cameras.Count; i++)
        {
          //  _cameras[i].Deactivate();
        }
        gameObject.SetActive(false);
    }

    protected void OnDrawGizmos()
    {
        if (AgentStartPosition)
        {
            Gizmos.DrawIcon(AgentStartPosition.position, "agent_gizmo.tiff");
        }
    }
}