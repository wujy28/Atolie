using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{

    public string speaker;

    [TextArea(3, 10)]
    public string[] sentences;

    public Dialogue(string speaker, string[] sentences)
    {
        this.speaker = speaker;
        this.sentences = sentences;
    }

}
