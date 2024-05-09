using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;

[Serializable]
public class StateData
{
    public string playerName;
    public int volume = 100;
    public List<string> difficulty = new List<string>{"easy", "medium", "hard"};
}