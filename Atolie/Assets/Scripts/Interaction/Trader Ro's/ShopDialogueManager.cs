using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopDialogueManager : MonoBehaviour
{
    public static ShopDialogueManager instance;

    // [SerializeField] private TraderRoShop traderRoShop;

    public Text nameText;
    public Text dialogueText;
    public Button continueButton;

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

    // Start is called before the first frame update
    void Start()
    {
        continueButton.interactable = true;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        continueButton.interactable = true;

        nameText.text = dialogue.speaker;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

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

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        TraderRoShop.instance.DialogueCompleted();
    }

    public void StartStubDialogue(Dialogue dialogue)
    {
        continueButton.interactable = false;

        nameText.text = dialogue.speaker;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        string onlySentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(onlySentence));
    }

    public void DisplayLastSentence(Dialogue dialogue)
    {
        continueButton.interactable = false;

        int lastSentenceIndex = dialogue.sentences.Length - 1;

        nameText.text = dialogue.speaker;
        dialogueText.text = dialogue.sentences[lastSentenceIndex];
    }

}
