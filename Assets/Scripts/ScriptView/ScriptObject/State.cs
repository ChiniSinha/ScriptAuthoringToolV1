using System;
using System.Collections.Generic;

[Serializable]
public class State
{
    public List<List<Action>> ActionSets;
    public string Execute;
    public string StateName;
    public bool Timeout = true;
    public UI Ui;


}