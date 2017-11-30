using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals {

    public static Config Config { get; private set; }

    public static void SetConfig(Config config)
    {
        Config = config;
    }
}
