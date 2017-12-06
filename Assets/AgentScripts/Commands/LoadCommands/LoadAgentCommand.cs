using UnityEngine;

public class LoadAgentCommand : BaseCommand
{
    private readonly string _characterAsset;

    public LoadAgentCommand(string characterAsset)
    {
        _characterAsset = characterAsset;
    }

    public override void Execute()
    {
        InProgress = true;
        Globals.Get<ResourceLoader>()
            .LoadAsset<GameObject>(_characterAsset, "agents/" + _characterAsset.ToLower(), OnAgentReady);
    }

    private void OnAgentReady(GameObject loadedObject)
    {
        Object.Instantiate(loadedObject);
        InProgress = false;
    }
}