#region

using UnityEngine;

#endregion

public class LoadGuiCommand : BaseCommand
{
    private const string _guiLoadTag = "gui";

    public LoadGuiCommand(string layoutName, bool isInitialLoad = false)
    {
        LayoutName = layoutName;
        IsInitialLoad = isInitialLoad;
    }

    public string LayoutName { get; set; }
    public bool IsInitialLoad { get; private set; }

    public override void Execute()
    {
        if (string.IsNullOrEmpty(LayoutName))
        {
            Debug.Log("Loading GUI but got empty string. Ignoring.");
            Globals.EventBus.Dispatch(new GuiLoadedEvent(null));
            return;
        }

        Globals.Get<ResourceLoader>()
            .LoadAsset<GameObject>(LayoutName, "guis/" + LayoutName.ToLower(), OnAssetLoaded);
        InProgress = true;
    }

    private void OnAssetLoaded(GameObject asset)
    {
        Object.Instantiate(asset);
        InProgress = false;
        if (!IsInitialLoad)
        {
            Globals.Get<ICommandProtocol>().SendDummyInputResponse();
        }
    }
}