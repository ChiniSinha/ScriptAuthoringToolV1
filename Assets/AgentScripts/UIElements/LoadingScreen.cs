#region


#endregion

public class LoadingScreen : LoadingIndicator
{
    protected EventBus _eventBus;

    private void Awake()
    {
        _eventBus = Globals.EventBus;
        _eventBus.Register<GraphicalAssetsLoadedEvent>(OnGraphicsReady);

        _dotCount = 3;
    }

    private void OnGraphicsReady(GraphicalAssetsLoadedEvent e)
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        _eventBus.Unregister<GraphicalAssetsLoadedEvent>(OnGraphicsReady);
    }
}