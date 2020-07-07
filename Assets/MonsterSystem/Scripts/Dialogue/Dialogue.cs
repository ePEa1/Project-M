using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string emotion;
    [TextArea(1, 5)]
    public string script;
}
