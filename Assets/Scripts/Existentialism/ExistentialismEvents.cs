using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExistentialismEvents : MonoBehaviour
{
    public GameObject fadeScreenIn;
    public GameObject charKomi;
    public GameObject charVela;
    public GameObject charSherum;
    public GameObject SpeakText;

    public GameObject pvText;
    public GameObject summaryText;
    public GameObject velaText;
    public GameObject sherumText;

    public GameObject islandImg;
    public GameObject neverLandImg;

    private Vector2 charKomiStartPos;
    private Vector2 charVelaStartPos;
    private Vector2 charSherumStartPos;

    //[SerializeField] AudioSource audioSource; //Next 버튼 클릭음에 사용
    //[SerializeField] AudioClip[] buttonClickClips;
    [SerializeField] public float amplitude = 10f;     // 흔들림 크기 
    [SerializeField] public float frequency = 5f;     // 흔들림 속도
    [SerializeField] string textToSpeak;
    [SerializeField] int autoDelay;
    [SerializeField] bool isAutoMode;
    [SerializeField] GameObject textBox;
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject charName;
    [SerializeField] GameObject fadeOut;

    void Start()
    {
        StartCoroutine(EventStarter());

        if (charKomi != null)
        {
            charKomiStartPos = charKomi.GetComponent<RectTransform>().anchoredPosition;
            charVelaStartPos = charVela.GetComponent<RectTransform>().anchoredPosition;
            charSherumStartPos = charSherum.GetComponent<RectTransform>().anchoredPosition;
        }

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

        if (charKomi != null && charKomi.activeInHierarchy)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            Vector2 newPos = new Vector2(charKomiStartPos.x, charKomiStartPos.y + offset);
            charKomi.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
        
        if (charVela != null && charVela.activeInHierarchy)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            Vector2 newPos = new Vector2(charVelaStartPos.x, charVelaStartPos.y - offset);
            charVela.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
        
        if (charSherum != null && charSherum.activeInHierarchy)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            Vector2 newPos = new Vector2(charSherumStartPos.x,charSherumStartPos.y - offset);
            charSherum.GetComponent<RectTransform>().anchoredPosition = newPos;
        }
    }

    //void PlayRandomButtonClickSound()
    //{
    //    if (audioSource != null && buttonClickClips != null && buttonClickClips.Length > 0)
    //    {
    //        int index = Random.Range(0, buttonClickClips.Length);
    //        audioSource.PlayOneShot(buttonClickClips[index]);
    //    }
    //}

    IEnumerator WaitForNext()
    {
        if (isAutoMode)
        {
            float timer = 0f;
            while (timer < autoDelay)
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
        //PlayRandomButtonClickSound();
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(1);
        fadeScreenIn.SetActive(false);
        charKomi.SetActive(true);
        textBox.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "코미", Text = "이번에 벨라 출시와 함께\n 테마극장 『홀로 선 존재는 어디에도 없어』가 공개되었습니다." } },
            { 1, new DialogueLine { Speaker = "코미", Text = "지금까지 벨라가 등장한 스토리에서는 '존재의 유령'이라는 별명에 걸맞게,\n존재에 대한 고찰이 반복적으로 등장해왔습니다." } },
            { 2, new DialogueLine { Speaker = "코미", Text = "이번 테마극장 역시 예외는 아니었는데요." } },
            { 3, new DialogueLine { Speaker = "코미", Text = "이번 영상에서는 이와 관련된 철학,\n 바로 샤르트르의 무신론적 실존주의를 이야기해보고자 합니다." } },
            { 4, new DialogueLine { Speaker = "코미", Text = "샤르트르의 철학을 설명하기에 앞서,\n 먼저 테마극장에서 벨라가 남긴 존재에 관한 독백을 보시겠습니다." } }
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
        charKomi.SetActive(false);
        pvText.SetActive(true);

        yield return null; // 한 프레임 기다리기
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        eventPos = 2;
        nextButton.SetActive(true);
        NextEvent();
    }

    IEnumerator EventTwo()
    {
        pvText.SetActive(false); 
        textBox.SetActive(true);
        charKomi.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "코미", Text = "이 독백에서 눈여겨볼 부분은 세 가지입니다." } },
            { 1, new DialogueLine { Speaker = "코미", Text = "1.누굴 위해 생겨났는지, 무엇을 할지 모르겠다.\n2.세상에 던져졌다.\n3.어떤 상황이 던져질 때마다 즉흥적으로 목적을 만든다." } },
            { 2, new DialogueLine { Speaker = "코미", Text = "이 세 문장이 바로 샤르트르의 실존주의 핵심을 담고 있습니다." } },
            { 3, new DialogueLine { Speaker = "코미", Text = "하나씩 살펴보겠습니다." } },
            { 4, new DialogueLine { Speaker = "코미", Text = "1.\"누굴 위해 생겨났는지, 무엇을 할지 모르겠다.\"" } },
            { 5, new DialogueLine { Speaker = "코미", Text = "혹시 \"실존이 본질에 앞선다\"는 말을 들어보셨나요?\n이 말은 샤르트르의 무신론적 실존주의를 대표하는 문장입니다." } },
            { 6, new DialogueLine { Speaker = "코미", Text = "본질이란, 어떤 존재가 ‘무엇을 하기 위해’ 존재하는지를 의미합니다.\n반면 실존은, 어떤 목적도 없이 단지 '존재하고 있다'는 것을 말합니다." } },
            { 7, new DialogueLine { Speaker = "코미", Text = "예를 들어," } },
            { 8, new DialogueLine { Speaker = "코미", Text = "전화기는 통화를 위해,\n계산기는 계산을 위해 존재하죠.\n이 둘은 '본질이 실존에 앞서는' 존재들입니다." } },
            { 9, new DialogueLine { Speaker = "코미", Text = "그렇다면 인간은 어떨까요?" } },
            { 10, new DialogueLine { Speaker = "코미", Text = "인간은 정해진 목적이나 용도를 갖고 태어나는 존재가 아닙니다." } },
            { 11, new DialogueLine { Speaker = "코미", Text = "그저 '존재'부터 시작하며, 이후 어떻게 살지를 스스로 결정해야 하죠." } },
            { 12, new DialogueLine { Speaker = "코미", Text = "그래서 “실존이 본질에 앞선다”는 말이 나온 것입니다." } },
            { 13, new DialogueLine { Speaker = "코미", Text = "물론 예외도 있어요." } },
            { 14, new DialogueLine { Speaker = "코미", Text = "영화 <아일랜드>의 복제인간처럼\n 특정 목적(장기 이식)을 위해 태어난 존재라든지," } },
            { 15, new DialogueLine { Speaker = "코미", Text = "<약속의 네버랜드>처럼 식용으로 길러진 인간들은\n '본질이 실존에 앞선' 존재들이죠." } },
            { 16, new DialogueLine { Speaker = "코미", Text = "그러나 실존적인 존재인 벨라는 그렇지 않기에 다음과 같이 말합니다:" } }
        };

        NewTextCreator newTextCreator = SpeakText.GetComponent<NewTextCreator>();

        foreach (KeyValuePair<int, DialogueLine> dialogue in dialogues)
        {
            string charName = dialogue.Value.Speaker;
            string dialogueText = dialogue.Value.Text;

            if (dialogue.Key == 14)
            {
                islandImg.SetActive(true);
            }
            else if (dialogue.Key == 15)
            {
                neverLandImg.SetActive(true);
            }
            else
            {
                islandImg.SetActive(false);
                neverLandImg.SetActive(false);
            }


            newTextCreator.StartText(dialogue.Value.Text);
            yield return new WaitUntil(() => newTextCreator.IsFinished);

            nextButton.SetActive(true);
            yield return StartCoroutine(WaitForNext());
            nextButton.SetActive(false);
        }

        charKomi.SetActive(false);
        textBox.SetActive(false);
        velaText.SetActive(true);

        yield return null; // 한 프레임 기다리기
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        eventPos = 3;
        nextButton.SetActive(true);
        NextEvent();
    }

    IEnumerator EventThree()
    {
        velaText.SetActive(false);
        charKomi.SetActive(true);
        textBox.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "코미", Text = "2.\"세상에 던져졌다.\"" } },
            { 1, new DialogueLine { Speaker = "코미", Text = "벨라는 독백에서 ‘던져졌다’는 표현을 두 번이나 사용합니다." } },
            { 2, new DialogueLine { Speaker = "코미", Text = "이는 매우 중요한 개념입니다." } },
            { 3, new DialogueLine { Speaker = "코미", Text = "왜 ‘던져졌다’고 표현했을까요?" } },
            { 4, new DialogueLine { Speaker = "코미", Text = "우리 각자는 원해서 태어난 것도 아니고," } },
            { 5, new DialogueLine { Speaker = "코미", Text = "숭고한 목적을 갖고 세상에 나온 것도 아닙니다." } },
            { 6, new DialogueLine { Speaker = "코미", Text = "그냥 어느 날 눈을 떠보니 이 세계에 존재하게 된 것이죠." } },
            { 7, new DialogueLine { Speaker = "코미", Text = "샤르트르는 인간을 이렇게 표현합니다:" } },
            { 8, new DialogueLine { Speaker = "코미", Text = "“원하지도 않았고, 준비도 되지 않은 상태에서 갑자기 존재하게 된 존재.”" } },
            { 9, new DialogueLine { Speaker = "코미", Text = "이처럼 갑작스럽고 의도되지 않은 탄생," } },
            { 10, new DialogueLine { Speaker = "코미", Text = "그리고 이로 인한 혼란과 불안이" } },
            { 11, new DialogueLine { Speaker = "코미", Text = "바로 ‘세상에 던져졌다’는 표현의 의미입니다." } },
            { 12, new DialogueLine { Speaker = "코미", Text = "그래서 벨라는 말합니다:" } },
            { 13, new DialogueLine { Speaker = "코미", Text = "\"내 존재가 의미 있었으면 좋겠어.\"" } },
            { 14, new DialogueLine { Speaker = "코미", Text = "이 말 속에서 우리는 벨라의 자기 존재에 대한 불안과 질문을 읽을 수 있습니다." } },
            { 15, new DialogueLine { Speaker = "코미", Text = "3.\"어떤 상황이 던져질 때마다 즉흥적으로 목적을 만든다.\"" } },
            { 16, new DialogueLine { Speaker = "코미", Text = "이 말은 얼핏 보면 어려워 보이지만," } },
            { 17, new DialogueLine { Speaker = "코미", Text = "간단히 말해 “상황에 맞춰 즉석에서 결정을 내린다”,\n 즉 ‘선택’을 한다는 뜻입니다." } },
            { 18, new DialogueLine { Speaker = "코미", Text = "그 선택에는 반드시 책임이 따른다고 말합니다." } },
            { 19, new DialogueLine { Speaker = "코미", Text = "야식을 먹으면 살이 찌고," } },
            { 20, new DialogueLine { Speaker = "코미", Text = "밤새면 다음 날 힘든 것처럼," } },
            { 21, new DialogueLine { Speaker = "코미", Text = "우리의 사소한 선택에도 책임이 따릅니다." } },
            { 22, new DialogueLine { Speaker = "코미", Text = "즉, 인간은\n존재 → 선택 → 책임\n이라는 구조 속에서 살아갑니다." } },
            { 23, new DialogueLine { Speaker = "코미", Text = "이제 다시 정리하면," } }
        };

        NewTextCreator newTextCreator = SpeakText.GetComponent<NewTextCreator>();

        foreach (KeyValuePair<int, DialogueLine> dialogue in dialogues)
        {
            string charName = dialogue.Value.Speaker;
            string dialogueText = dialogue.Value.Text;

            if(dialogue.Key == 1)
                charVela.SetActive(true);

            newTextCreator.StartText(dialogue.Value.Text);
            yield return new WaitUntil(() => newTextCreator.IsFinished);

            nextButton.SetActive(true);
            yield return StartCoroutine(WaitForNext());
            nextButton.SetActive(false);
        }

        textBox.SetActive(false);
        summaryText.SetActive(true);
        charKomi.SetActive(false);
        charVela.SetActive(false);

        yield return null; // 한 프레임 기다리기
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        eventPos = 4;
        nextButton.SetActive(true);
        NextEvent();
    }

    IEnumerator EventFour()
    {
        summaryText.SetActive(false);
        charKomi.SetActive(true);
        charVela.SetActive(true);
        textBox.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "코미", Text = "벨라는 바로 이 실존주의적 인간처럼 보입니다." } },
            { 1, new DialogueLine { Speaker = "코미", Text = "스스로 존재의 의미를 찾아가려 애쓰고 있으니까요." } },
            { 2, new DialogueLine { Speaker = "코미", Text = "하지만..." } },
            { 3, new DialogueLine { Speaker = "코미", Text = "테마극장 『홀로 선 존재는 어디에도 없어』의 후반부에서," } },
            { 4, new DialogueLine { Speaker = "코미", Text = "놀랍게도 벨라의 창조자가 등장합니다." } },
            { 5, new DialogueLine { Speaker = "코미", Text = "바로 우리의 서기관 마녀, 벨라도나 셰럼이죠." } },
            { 6, new DialogueLine { Speaker = "코미", Text = "셰럼과 벨라가 함께 읊는 대사를 다시 볼까요?" } }
        };

        NewTextCreator newTextCreator = SpeakText.GetComponent<NewTextCreator>();

        foreach (KeyValuePair<int, DialogueLine> dialogue in dialogues)
        {
            string charName = dialogue.Value.Speaker;
            string dialogueText = dialogue.Value.Text;

            if (dialogue.Key == 5)
            {
                charKomi.SetActive(false);
                charSherum.SetActive(true);
            }

            newTextCreator.StartText(dialogue.Value.Text); ;
            yield return new WaitUntil(() => newTextCreator.IsFinished);

            nextButton.SetActive(true);
            yield return StartCoroutine(WaitForNext());
            nextButton.SetActive(false);
        }

        textBox.SetActive(false);
        charKomi.SetActive(false);
        charVela.SetActive(false);
        charSherum.SetActive(false);
        sherumText.SetActive(true);

        yield return null; // 한 프레임 기다리기
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        eventPos = 5;
        nextButton.SetActive(true);
        NextEvent();
    }

    IEnumerator EventFive()
    {
        sherumText.SetActive(false);
        charKomi.SetActive(true);
        charVela.SetActive(true);
        charSherum.SetActive(true);
        textBox.SetActive(true);

        Dictionary<int, DialogueLine> dialogues = new Dictionary<int, DialogueLine>()
        {
            { 0, new DialogueLine { Speaker = "코미", Text = "이 대사에서 알 수 있는 건," } },
            { 1, new DialogueLine { Speaker = "코미", Text = "셰럼은 벨라를 다음 두 가지 목적을 위해 창조했다는 것입니다," } },
            { 2, new DialogueLine { Speaker = "코미", Text = "1. 세상의 시작, 세계수의 진실에 접근하는 존재가 되는 것," } },
            { 3, new DialogueLine { Speaker = "코미", Text = "2. 모든 존재를 하나로 묶는 역할," } },
            { 4, new DialogueLine { Speaker = "코미", Text = "그런데 벨라는 이 사실을 전혀 몰랐습니다," } },
            { 5, new DialogueLine { Speaker = "코미", Text = "오히려 자신의 존재 이유를 알기 위해 세계수와 대화하고,\n다른 존재들의 의식을 통합하려 했죠," } },
            { 6, new DialogueLine { Speaker = "코미", Text = "그 결과, 그녀는 자신도 모르게\n'셰럼의 목적(본질)'을 굉장히 충실히 이행하고 있었던 겁니다," } },
            { 7, new DialogueLine { Speaker = "코미", Text = "결국 벨라는 자신을 실존주의적 존재라고 믿었지만,\n그 행동은 오히려 본질에 따라 움직인 셈입니다," } },
            { 8, new DialogueLine { Speaker = "코미", Text = "'실존이 본질에 앞선다'는 그녀의 말과는 달리," } },
            { 9, new DialogueLine { Speaker = "코미", Text = "그녀의 삶은 역설적으로 '본질이 실존에 앞선다'는 삶이었죠," } },
            { 10, new DialogueLine { Speaker = "코미", Text = "이 모순은 매우 흥미롭습니다," } },
            { 11, new DialogueLine { Speaker = "코미", Text = "스토리 작가 폴빠님이 의도했는지는 모르겠지만요, ㅎㅎ" } },
            { 12, new DialogueLine { Speaker = "코미", Text = "어쩌면 우리도 벨라처럼," } },
            { 13, new DialogueLine { Speaker = "코미", Text = "우리는 모르지만 누군가가 만들어둔 ‘본질’에 따라 살아가고 있는 건 아닐까요?" } }
        };

        NewTextCreator newTextCreator = SpeakText.GetComponent<NewTextCreator>();

        foreach (KeyValuePair<int, DialogueLine> dialogue in dialogues)
        {
            string charName = dialogue.Value.Speaker;
            string dialogueText = dialogue.Value.Text;

            newTextCreator.StartText(dialogue.Value.Text);
            yield return new WaitUntil(() => newTextCreator.IsFinished);

            nextButton.SetActive(true);
            yield return StartCoroutine(WaitForNext());
            nextButton.SetActive(false);
        }

        textBox.SetActive(false);
        eventPos = 999;
        nextButton.SetActive(true);
        NextEvent();
    }

    IEnumerator EventLast()
    {
        nextButton.SetActive(false);
        SpeakText.SetActive(false);
        fadeOut.SetActive(true);
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene(1);
    }

    public void NextEvent()
    {
   
        switch (eventPos)
        {
            case 2:
                StartCoroutine(EventTwo());
                break;
            case 3:
                StartCoroutine(EventThree());
                break;
            case 4:
                StartCoroutine(EventFour());
                break;
            case 5:
                StartCoroutine(EventFive());
                break;
            case 999:
                StartCoroutine(EventLast());
                break;
        }
    }
}
