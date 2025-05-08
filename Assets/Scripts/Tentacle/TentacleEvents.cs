using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TentacleEvents : MonoBehaviour
{
   
    public GameObject charNati;
    public GameObject charRits;
    public GameObject SpeakText;

    private Vector2 charNatiStartPos;
    private Vector2 charRitsStartPos;
    private int eventPos = 0;
    private int bgIndex = 0;

    [SerializeField] public float amplitude = 10f;     // ��鸲 ũ�� 
    [SerializeField] public float frequency = 5f;     // ��鸲 �ӵ�
    [SerializeField] string textToSpeak;
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject nextButton;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeIn;
    [SerializeField] GameObject fadeOut;
    [SerializeField] GameObject tentalce1;
    [SerializeField] GameObject tentalce2;
    [SerializeField] RawImage bgRawImage;
    [SerializeField] List<Texture> backgroundTextures;

    void Start()
    {
        StartCoroutine(EventStarter());

        //if (charKomi != null)
        //{
        //    charKomiStartPos = charKomi.GetComponent<RectTransform>().anchoredPosition;
        //    charVelaStartPos = charVela.GetComponent<RectTransform>().anchoredPosition;
        //}

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        //if (charKomi != null && charKomi.activeInHierarchy)
        //{
        //    float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        //    Vector2 newPos = new Vector2(charKomiStartPos.x, charKomiStartPos.y + offset);
        //    charKomi.GetComponent<RectTransform>().anchoredPosition = newPos;
        //}

        //if (charVela != null && charVela.activeInHierarchy)
        //{
        //    float offset = Mathf.Sin(Time.time * frequency) * amplitude;
        //    Vector2 newPos = new Vector2(charVelaStartPos.x, charVelaStartPos.y - offset);
        //    charVela.GetComponent<RectTransform>().anchoredPosition = newPos;
        //}

    }

    IEnumerator WaitForNext()
    {
         yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(1);

        

        while (bgIndex < backgroundTextures.Count)
        {
            bgRawImage.texture = backgroundTextures[bgIndex];
            charRits.SetActive(true);
            // �����̽��� ��ٸ�
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            bgIndex++;
            charRits.SetActive(false);
        }

        eventPos = 999;
        nextButton.SetActive(true);
        NextEvent();
    }


    IEnumerator EventLast()
    {
        yield return new WaitForSeconds(1);
        fadeIn.SetActive(false);
        charNati.SetActive(true);
        textBox.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "��Ƽ", Text = "�׷� ������ ������ ��å���� �帮�� �־����~" } },
            { 1, new DialogueLine { Speaker = "����", Text = "��, �̰� ��å�� �ƴ��ݾ�!" } },
            { 2, new DialogueLine { Speaker = "��Ƽ", Text = "��Ƽ: �ÿ��� �ٱ��ٶ��� ���ϱ� ��å �ƴұ��?" } },
        };

        NewTextCreator newTextCreator = SpeakText.GetComponent<NewTextCreator>();

        foreach (KeyValuePair<int, DialogueLine> dialogue in dialogues)
        {
            string charName = dialogue.Value.Speaker;
            string dialogueText = dialogue.Value.Text;

            newTextCreator.StartText(dialogueText);
            yield return new WaitUntil(() => newTextCreator.IsFinished);

            nextButton.SetActive(true);
            yield return StartCoroutine(WaitForNext());
            nextButton.SetActive(false);
        }

        textBox.SetActive(false);
        //charKomi.SetActive(false);

        yield return null; // �� ������ ��ٸ���
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));


        nextButton.SetActive(false);
        SpeakText.SetActive(false);
        tentalce1.SetActive(false);
        tentalce2.SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene(1);
    }

    public void NextEvent()
    {
        switch (eventPos)
        {
            case 999:
                StartCoroutine(EventLast());
                break;
        }
    }
}
