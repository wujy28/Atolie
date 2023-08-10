using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        sentences = new Queue<string>();
    }

    //Function to start a dialogue
    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);
        
        nameText.text = dialogue.speaker;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //Function to display next sentence of dialogue
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    //Function to type out sentence in dailogue
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    //Function to end dialogue
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        InteractionExecutable.currentExecutableCompleted();
    }

}
