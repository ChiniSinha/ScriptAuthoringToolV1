#region

using System;
using System.Collections;
using System.Xml;
using UnityEngine;

#endregion

public class ExternalTTSServer : MonoBehaviour
{
    //Network information
    private string _baseUrl;
    private CommandQueue _commandQueue;
    private string _communicationSuffix = "agent";
    protected bool _connectionEstablished;
    private readonly IMessageDeserializer _deserializer = new DefaultRAG2Deserializer();
    private string _pass;

    private string _user;
    private readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    //private ErrorText _errorText;

    public void Awake()
    {
        _commandQueue = gameObject.AddComponent<CommandQueue>();
        _user = Globals.Config.Tts.User;
        _pass = Globals.Config.Tts.Pass;
        _baseUrl = Globals.Config.Tts.Url;
        StartCoroutine(FetchEndpoint());

        //if (Globals.HasRegistered<ErrorText>())
        //    _errorText = Globals.Get<ErrorText>();
    }

    protected IEnumerator FetchEndpoint()
    {
        WWWForm postData = new WWWForm();
        postData.AddField("user", _user);
        postData.AddField("pass", _pass);
        postData.AddField("action", "start");
        string url = _baseUrl + _communicationSuffix;
        WWW www = new WWW(url, postData);
        yield return www;
        XmlDocument response;
        while (www.error != null)
        {
            Debug.LogError ("Failed to FetchEndpoint, retrying");
            //Globals.EventBus.Dispatch(new NetworkStatusEvent(NetworkStatusEvent.AuthStatus.NO_TTS_CONNECTION));
            //ExternalConnection.SetNetworkStatus(ExternalConnection.AuthStatus.NO_TTS_CONNECTION);
            yield return new WaitForSeconds (5f);
            www = new WWW(url, postData);
            yield return www;
        }
        if (www.responseHeaders["STATUS"].Contains("200"))
        {
            response = CleanedXml(www.text);
            XmlNode endpointNode = response.SelectSingleNode("AGENT");
            if (endpointNode == null)
            {
                endpointNode = response.SelectSingleNode("//CONVERSATION");
            }
            if (endpointNode == null)
            {
                Debug.LogError("Could not get endpoint");
                yield break;
            }

            _communicationSuffix = endpointNode.AttributeCaseInsensitive("url");
            if (string.IsNullOrEmpty(_communicationSuffix))
            {
                Debug.LogError("Could not get endpoint");
                yield break;
            }

            _connectionEstablished = true;
        }
    }

    public void RequestUtterance(string input)
    {
        Debug.Log("Requesting utterance: " + input);
        input = input.Replace("\n", "");
        input = input.Trim();
        StartCoroutine(StartUtteranceGeneration(input));
    }

    protected IEnumerator StartUtteranceGeneration(string input)
    {
        while (!_connectionEstablished)
        {
            yield return _waitForEndOfFrame;
        }

        WWWForm postData = new WWWForm();
        postData.AddField("user", _user);
        postData.AddField("pass", _pass);
        postData.AddField("action", "speak");
        postData.AddField("utterance", input);
        string url = _baseUrl + _communicationSuffix;
        WWW www = new WWW(url, postData);
        yield return www;
        while (www.error != null)
        {
            Debug.Log ("Failed to Generate Speech, Retrying");
            //Globals.EventBus.Dispatch(new SetErrorTextEvent("Failed to generate to Generate Speech, Retrying..."));
            //_errorText.Text.text = "Failed to generate to Generate Speech, Retrying...";
            //_errorText.gameObject.SetActive(true);
            yield return new WaitForSeconds (5f);
            www = new WWW(url, postData);
            yield return www;
        }
        //Globals.EventBus.Dispatch(new SetErrorTextEvent(""));
        //_errorText.Text.text = "";
        //_errorText.gameObject.SetActive(false);

        yield return RetrieveLastUtterance();
    }

    private IEnumerator RetrieveLastUtterance()
    {
        WWWForm postData = new WWWForm();
        postData.AddField("user", _user);
        postData.AddField("pass", _pass);
        postData.AddField("action", "performComplete");
        string url = _baseUrl + _communicationSuffix;
        WWW www = new WWW(url, postData);
        yield return www;

        while (www.error != null)
        {
            Debug.Log ("Failed to Retrieve Speech, Retrying");
            //Globals.EventBus.Dispatch(new SetErrorTextEvent("Failed to generate to Retrieve Speech, Retrying..."));
            //_errorText.Text.text = "Failed to generate to Retrieve Speech, Retrying...";
            //_errorText.gameObject.SetActive(true);
            yield return new WaitForSeconds (5f);
            www = new WWW(url, postData);
            yield return www;
        }
        //Globals.EventBus.Dispatch(new SetErrorTextEvent(""));
        //_errorText.Text.text = "";
        //_errorText.gameObject.SetActive(false);

        XmlDocument response = CleanedXml(www.text);
        XmlNode node;
        for (int i = 0; i < response.ChildNodes.Count; i++)
        {
            node = response.ChildNodes[i];
            if (node.Name.Equals("perform", StringComparison.CurrentCultureIgnoreCase))
            {
                ParsePerformBlock(node);
            }
        }
    }

    protected XmlDocument CleanedXml(string serverMessage)
    {
        serverMessage = serverMessage.Replace("xml:base=\"../../\"", "");
        serverMessage = serverMessage.Replace("&", "&amp;");

        XmlDocument xml = new XmlDocument();
        xml.LoadXml(serverMessage);
        return xml;
    }

    protected void ParsePerformBlock(XmlNode performBlock)
    {
        XmlNode node;
        CommandQueue actionQueueRef = Globals.CommandQueue;
        for (int i = 0; i < performBlock.ChildNodes.Count; i++)
        {
            node = performBlock.ChildNodes[i];
            if (node.Name.Equals("unsync", StringComparison.CurrentCultureIgnoreCase))
            {
                ParseUnsyncBlock(node);
            }
            else
            {
                BaseCommand command = _deserializer.DeserializeActionXml(node);
                _commandQueue.Enqueue(command);
            }
        }
    }

    protected void ParseUnsyncBlock(XmlNode unsyncBlock)
    {
        XmlNode node;
        for (int i = 0; i < unsyncBlock.ChildNodes.Count; i++)
        {
            node = unsyncBlock.ChildNodes[i];

            BaseCommand command = _deserializer.DeserializeActionXml(node);
            _commandQueue.Enqueue(command);
        }
    }
}