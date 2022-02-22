using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    private string nameOfNpc;
    private Queue<string> sentences;
    public Text nameofNpcText;
    public Text sentenceText;

    public void Start()
    {
        sentences = new Queue<string>();
        gameObject.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        gameObject.SetActive(true);
        sentences.Clear();
        nameOfNpc = dialogue.nameOfNpc;
        nameofNpcText.text = nameOfNpc;
        foreach (var sentence in dialogue.dialogue)
        {
            sentences.Enqueue(sentence);
        }

        
        DisplayNextSentence();
    }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }

    IEnumerator WriteSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (var symbol in sentence)
        {
            sentenceText.text += symbol;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if (sentences.Count == 0)
        {
            EndDialogue();

            return;
        }
        var sentence = sentences.Dequeue();
        StartCoroutine(WriteSentence(sentence));
    }

}
