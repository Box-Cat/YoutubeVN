using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene01Events : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charKasumi;
    public GameObject charHaruka;
    public GameObject textBox;

    [SerializeField] AudioSource girlGasp;
    [SerializeField] AudioSource girlSigh;

    [SerializeField] string textToSpeak;
    [SerializeField] int currentTextLength;
    [SerializeField] int textLength;
    [SerializeField] GameObject mainTextObject;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeOut;
    [SerializeField] CinemachineVirtualCamera kasumiZoom;

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    void Update()
    {
        textLength = TextCreator.charCount;
        
        if (Input.GetKeyDown(KeyCode.Space) && nextButton.activeSelf)
        {
            NextButton(); 
        }
        // ESC 키를 누르면 게임 종료
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit(); // 게임 종료 (빌드된 게임에서만 작동)

            // 에디터에서도 ESC를 누르면 PlayMode 종료
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2);
        fadeScreenIn.SetActive(false);
        charKasumi.SetActive(true);
        yield return new WaitForSeconds(2);
        //대사 입력
        mainTextObject.SetActive(true);
        textToSpeak = "나는 너무 졸립다. 그래서 바로 자고 싶다. 하지만 공부를 해야 한다.";
        kasumiZoom.Priority = 20;
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        girlSigh.Play();
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);

        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        // event 1
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Haruka";
        textToSpeak = "오랫만에 10시 30분에 자고 싶다.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        girlGasp.Play();
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator EventTwo()
    {
        // event 2
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textToSpeak = "Oh, you startled me. I didn't expect you there.";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator EventThree()
    {
        // event 3
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        charName.GetComponent<TMPro.TMP_Text>().text = "Haruka";
        textToSpeak = "I'm sorry I didn't mean to...\n Let's go to the park and look for Akane";
        textBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator EventFour()
    {
        // event 4
        nextButton.SetActive(false);
        charHaruka.SetActive(true);
        textBox.SetActive(true);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        eventPos = 4;
        SceneManager.LoadScene(1);
    }
    public void NextButton()
    {
        kasumiZoom.Priority = 0;

        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }
        if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        if (eventPos == 3)
        {
            StartCoroutine(EventThree());
        }
        if (eventPos == 4)
        {
            StartCoroutine(EventFour());
        }
    }
}