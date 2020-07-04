using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(1, 5)]
    public string line;
    public int position;
    public Sprite sprite;
    public Sprite estelleImg;
    public Sprite serenaImg;
    public Sprite background;
}
