#region



#endregion

using System;

public class BasicEnvironmentMediator : IEnvironmentMediator
{
    private readonly Environment _environment;

    public BasicEnvironmentMediator(Environment env)
    {
        _environment = env;
    }

    public void Setup()
    {
        Globals.EventBus.Register<AgentReadyEvent>(OnAgentReady);
        Globals.EventBus.Register<PlayAudioRequestEvent>(OnPlayAudioRequest);
        Globals.EventBus.Register<ChangeEnvironmentEvent>(OnEnvironmentChange);
        Globals.EventBus.Register<EnvironmentTransitionCompleteEvent>(OnTransitionComplete);
        Globals.EventBus.Register<StartEnvironmentTransitionEvent>(OnTransitonStart);
    }

    public void Cleanup()
    {
        Globals.EventBus.Unregister<AgentReadyEvent>(OnAgentReady);
        Globals.EventBus.Unregister<PlayAudioRequestEvent>(OnPlayAudioRequest);
        Globals.EventBus.Unregister<ChangeEnvironmentEvent>(OnEnvironmentChange);
        Globals.EventBus.Unregister<EnvironmentTransitionCompleteEvent>(OnTransitionComplete);
        Globals.EventBus.Unregister<StartEnvironmentTransitionEvent>(OnTransitonStart);
    }

    private void OnPlayAudioRequest(PlayAudioRequestEvent e)
    {
        _environment.PlayAudio(e.AudioUrl, e.PlayerName);
    }

    private void OnAgentReady(AgentReadyEvent e)
    {
        if (_environment.IsActive)
        {
            _environment.PositionAgent(e.Agent);
        }
    }

    private void OnTransitonStart(StartEnvironmentTransitionEvent e)
    {
        if (e.EnvironmentName.Equals(_environment.Label, StringComparison.CurrentCultureIgnoreCase))
        {
            _environment.gameObject.SetActive(true);
        }
    }

    private void OnTransitionComplete(EnvironmentTransitionCompleteEvent e)
    {
        if (!e.NewEnvironmentName.Equals(_environment.Label, StringComparison.CurrentCultureIgnoreCase))
        {
            _environment.Deactivate();
        }
    }

    private void OnEnvironmentChange(ChangeEnvironmentEvent e)
    {
        if (e.EnvironmentName.Equals(_environment.Label, StringComparison.CurrentCultureIgnoreCase))
        {
            _environment.Activate();
        }
    }
}