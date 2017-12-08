using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class Utils
{
    private static readonly string baseFileName = "userlog_";
    public static int user;
    public static StreamWriter sw;

    public static string logFileName = "";
    private static SHA256 _hasher = SHA256.Create();

    static Utils()
    {
        //Find an available logging name
#if (!UNITY_WEBPLAYER) && (!UNITY_IOS)
        logFileName = baseFileName + user + ".csv";
        while (File.Exists(logFileName))
        {
            user++;
            logFileName = baseFileName + user + ".csv";
        }
        File.WriteAllText(logFileName, "EventType,EventValue,VideoValence,VideoEngagement,AudioValence\n");
        sw = File.AppendText(logFileName);
        sw.WriteLine("LogCreated," + DateTime.Now.ToString("MM-dd-YYYY-hh:mm:ss"));
#endif
    }

    //Util to ignore case sensitivity in attribute names

    public static void Log(string eventType, string eventValue)
    {
        sw.WriteLine(eventType + "," + eventValue);
    }

    public static void Log(string eventType, string eventValue, string videoValence, string videoEngagement,
        string audioValence)
    {
        sw.WriteLine(eventType + "," + eventValue + "," + videoValence + "," + videoEngagement + "," + audioValence);
    }
    
    public static string CombinePath(params string[] pathParts)
    {
        string output = "";
        for (int i = 0; i < pathParts.Length; i++)
        {
            output = Path.Combine(output, pathParts[i]);
        }
        return output;
    }

    public static string GeneratePasswordHash(string username, string password)
    {
        string hashTarget = username + Consts.PasswordSalt + password;
        ToBase64Transform base64 = new ToBase64Transform();
        byte[] hashBytes = _hasher.ComputeHash(Encoding.Default.GetBytes(hashTarget));
        byte[] outputBytes = new byte[base64.OutputBlockSize];
        int inputOffset = 0;
        int outputOffset = 0;
        int inputBlockSize = base64.InputBlockSize;

        StringBuilder builder = new StringBuilder();
        while (hashBytes.Length - inputOffset > inputBlockSize)
        {
            base64.TransformBlock(hashBytes, inputOffset, Mathf.Min(hashBytes.Length - inputOffset, base64.InputBlockSize), outputBytes, outputOffset);
            inputOffset += base64.InputBlockSize;
            builder.Append(Encoding.Default.GetString(outputBytes));
        }
        outputBytes = base64.TransformFinalBlock(hashBytes, inputOffset, hashBytes.Length - inputOffset);
        builder.Append(Encoding.Default.GetString(outputBytes));
        return builder.ToString();
    }

    public static int ToPosixTimestamp(this DateTime time)
    {
        return (int)time.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
}