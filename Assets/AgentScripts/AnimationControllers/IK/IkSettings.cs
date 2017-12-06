using System;
using System.Collections.Generic;

[Serializable]
public class IkSettings
{
    public List<Arm> ArmSettings;
    public List<Body> BodySettings;
    public List<Head> HeadSettings;

    public Arm GetSetting(ArmAnimation.Type type)
    {
        for (int i = 0; i < ArmSettings.Count; i++)
        {
            if (ArmSettings[i].Animation == type)
            {
                return ArmSettings[i];
            }
        }
        return null;
    }

    public Head GetSetting(HeadAnimation.Type type)
    {
        for (int i = 0; i < HeadSettings.Count; i++)
        {
            if (HeadSettings[i].Animation == type)
            {
                return HeadSettings[i];
            }
        }
        return null;
    }

    public Body GetSetting(BodyAnimation.Type type)
    {
        for (int i = 0; i < BodySettings.Count; i++)
        {
            if (BodySettings[i].Animation == type)
            {
                return BodySettings[i];
            }
        }
        return null;
    }

    public abstract class IkSetting
    {
        public float BodyStrength;
        public float ElbowPositionStrength;
        public float HandOrientationStrength;
        public float HandPositionStrength;
        public float LookBodyStrength;
        public float LookEyeStrength;
        public float LookHeadStrength;
        public float LookStrength;
    }

    [Serializable]
    public class Arm : IkSetting
    {
        public ArmAnimation.Type Animation;
    }

    [Serializable]
    public class Head : IkSetting
    { 
        public HeadAnimation.Type Animation;
    }

    [Serializable]
    public class Body : IkSetting
    {
        public BodyAnimation.Type Animation;
    }
}