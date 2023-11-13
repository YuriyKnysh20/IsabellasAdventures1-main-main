using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Dialogue  
{
    public string name;// name of npc with whom we talk
    [TextArea(4, 10)] // riadki v inspectore
    public string[] sentences;
}
