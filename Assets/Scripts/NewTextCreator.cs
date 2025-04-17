using System.Collections;
using UnityEngine;

public class NewTextCreator : MonoBehaviour
{
    public TMPro.TMP_Text viewText;
    [SerializeField] string transferText;
    public bool IsFinished { get; private set; }

    public void StartText(string dialogueText)
    {
        StopAllCoroutines(); 
        transferText = dialogueText;
        viewText.text = "";
        IsFinished = false;
        StartCoroutine(RollText());
    }

    IEnumerator RollText()
    {
        foreach (char c in transferText)
        {
            viewText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        IsFinished = true;
    }
}


