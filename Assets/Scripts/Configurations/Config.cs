using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;

[Serializable]
public class Config : ICloneable {

    public enum CommandMode
    {
        NONE,
        WEB,
        SOCKET,
        STANDALONE,
        PRESENTATION
    }

    public enum VisemeMapping
    {
        SAPI,
        CRAPI
    }

    public enum DatabaseModes
    {
        NONE,
        LOCAL,
        NETWORK,
        LITEBODY,
        HYBRID
    }

    public enum TtsMode
    {
        NATIVE,
        WEB_CEREVOICE,
        LOCAL_CEREVOICE
    }

    public ProjectData projectData;
    public List<ProjectListData> projectList;

    public AgentSection Agent;
    public DatabaseSection Database;
    public GuiSection Gui;
    public NetworkSection Network;
    public ScriptSection Script;
    public SpeechRecognitionSection SpeechRecognition;

    public SystemSection System;
    public TtsSection Tts;

    public Config()
    {
        projectData = new ProjectData();
        projectList = new List<ProjectListData>();

        System = new SystemSection();
        Agent = new AgentSection();
        Database = new DatabaseSection();
        Network = new NetworkSection();
        Tts = new TtsSection();
        SpeechRecognition = new SpeechRecognitionSection();
        Gui = new GuiSection();
    }

    public object Clone()
    {
        Config clone = new Config();
        FieldInfo[] fields = typeof(Config).GetFields();
        foreach (FieldInfo field in fields)
        {
            object thisSection = field.GetValue(this);
            ConstructorInfo defaultConstructor = field.FieldType.GetConstructor(new Type[] { });
            if (defaultConstructor == null)
            {
                throw new NotImplementedException();
            }

            object otherSection = defaultConstructor.Invoke(new object[] { });

            FieldInfo[] sectionFields = field.FieldType.GetFields();
            foreach (FieldInfo sectionField in sectionFields)
            {
                sectionField.SetValue(otherSection, sectionField.GetValue(thisSection));
            }

            field.SetValue(clone, otherSection);
        }
        return clone;
    }

    public static void Save(Config cfg, bool prettyPrint = false)
    {
        string json = JsonUtility.ToJson(cfg, prettyPrint);
        File.WriteAllText(Path.Combine(UsedValues.DefaultFilePath, UsedValues.ConfigFileName), json);
    }

    public static Config Load() 
    {
        string json = File.ReadAllText(Path.Combine(UsedValues.DefaultFilePath, UsedValues.ConfigFileName));
        return JsonUtility.FromJson<Config>(json);
    }

    public static ProjectListData GetNewProjectListItem(string projName, string projectLocation)
    {
        ProjectListData projectListData = new ProjectListData();
        projectListData.projectName = projName;
        projectListData.projectLocationPath = projectLocation;
        return projectListData;
    }

    [Serializable]
    public class ProjectData
    {
        public string projectPath;
    }

    [Serializable]
    public class ProjectListData
    {
        public string projectName;
        public string projectLocationPath;
    }

    [Serializable]
    public class SystemSection
    {
        //public RagLog.MessageType LogMask;
        public CommandMode Mode;
        public string ProjectTitle;
    }

    [Serializable]
    public class TtsSection
    {
        public TtsMode Mode;
        public string Pass;
        public string Url;
        public string User;
        public VisemeMapping VisemeMapping;
    }

    [Serializable]
    public class DatabaseSection
    {
        public DatabaseModes DatabaseMode;
        public string LocalDatabaseName;
        public bool SaveLogEvents;
        public string RemoteDatabaseUrl;
        public string User;
        public string Pass;
        public int SessionTimeout;
    }

    [Serializable]
    public class NetworkSection
    {
        public string NetworkAddress;
    }

    [Serializable]
    public class ScriptSection
    {
        [AssetBundlePrefix("scripts")] public string ScriptBundle;
        public string StartScript;
        public bool UseRawScripts;
        public string TimeoutScript;
    }

    [Serializable]
    public class AgentSection
    {
        [AssetBundlePrefix("agents")] public string Character;

        [AssetBundlePrefix("environments")] public string Scenery;
        public string VoiceFile;
        public bool AutomateIdleBehavoir;
        public bool UseStaticCamera;
    }

    [Serializable]
    public class SpeechRecognitionSection
    {
        public bool EnableEmotionRecognition;
        public bool EnableSpeechRecognition;
        public int SentenceSensitivity;
        public bool UseKeywordSearch;
        public int WordSensitivity;
    }

    [Serializable]
    public class GuiSection
    {
        [AssetBundlePrefix("guis")] public string HorizontalLayout;

        public bool StatefulUi;

        [AssetBundlePrefix("guis")] public string VerticalLayout;
    }
}
