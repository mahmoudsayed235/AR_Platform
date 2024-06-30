using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Events;
using UnityEngine.Networking;

public class QuestionsManager : MonoBehaviour
{
    public Question.Type type;

    public bool shuffleQuestions;
    public bool shuffleChoices;

    public int lessonId;
    public int allowedMistakes;
    public float defaultAudioWait;
    public float defaultFeedbackWait;

    public GameObject eventSystem;
    public AudioSource tutorAudioSource;

    public GameObject questionsCanvas;
    public GameObject flipper;
    public GameObject startPanel;
    public GameObject questionsPanel;
    public GameObject finishPanel;
    public GameObject rewardPanel;
    
    public Image startMessageImage;
    public Image startActionImage;
    public Button startActionButton;

    public Image finishMessageImage;
    public Image rewardImage;

    public GameObject question;
    public Image questionImage;
    
    public Button trueButton;
    public Button falseButton;

    public Button[] choiceButtons;
    public Image[] choiceImages;

    public AudioClip[] correctAnswerFeedbacks;
    public AudioClip[] wrongAnswerFeedbacks;

    public int[] rewardValues;

    // 3 expected sprites [0 - normal] [1 - correct] [2 - wrong]
    public Sprite[] trueSprites;
    public Sprite[] falseSprites;
    public Sprite[] choiceBackgroundSprites;

    public TextSpriteAudioBlock[] startMessageTSAblocks;
    public TextSpriteAudioBlock[] startActionTSAblocks;
    public TextSpriteAudioBlock[] finishMessageGoodTSAblocks;
    public TextSpriteAudioBlock[] finishMessageBadTSAblocks;
    public TextSpriteAudioBlock[] reward1TSAblocks;
    public TextSpriteAudioBlock[] reward2TSAblocks;
    public TextSpriteAudioBlock[] reward3TSAblocks;
   
    public QuestionsGroup[] questionsGroups;

    public UnityEvent onFinishAcions;

    private int language;
    private int currentQuestionIndex;
    private int mistakes;
    private int attempt;

    private const int normal = 0;
    private const int correct = 1;
    private const int wrong = 2;

    private Question currentQuestion;
    private List<int> shuffledQuestionsList = new List<int>();
    private List<int> shuffledChoicesList = new List<int>();

    private Vector3 ltrScale = new Vector3(-1.0f, 1.0f, 1.0f);
    private Vector3 rtlScale = new Vector3(1.0f, 1.0f, 1.0f);
    
    private void Start()
    {
        Show(false);
    }

    private void Update()
    {
        //if(Input.GetKeyUp(KeyCode.S))
        //{
        //    Show(true);
        //}

        //if (Input.GetKeyUp(KeyCode.H))
        //{
        //    Show(false);
        //}
    }

    private void ResetAlignment(Vector3 alignmentScale)
    {
        flipper.transform.localScale = alignmentScale;
        
        startMessageImage.rectTransform.localScale = alignmentScale;
        startActionButton.transform.parent.localScale = alignmentScale;

        finishMessageImage.rectTransform.localScale = alignmentScale;
        rewardImage.rectTransform.localScale = alignmentScale;
        
        questionImage.rectTransform.localScale = alignmentScale;
        for (int i = 0; i < choiceImages.Length; i++)
        {
            choiceImages[i].transform.localScale = alignmentScale;
        }

        trueButton.transform.parent.localScale = alignmentScale;
        falseButton.transform.parent.localScale = alignmentScale;
    }

    private void Reward()
    {
        if (type == Question.Type.Sprite)
        {
            switch(attempt)
            {
                case 1:
                    rewardImage.sprite = reward1TSAblocks[language].sprite;
                    break;
                case 2:
                    rewardImage.sprite = reward2TSAblocks[language].sprite;
                    break;
                case 3:
                    rewardImage.sprite = reward3TSAblocks[language].sprite;
                    break;
            }
        }
        else if (type == Question.Type.Text)
        {
        }

        rewardPanel.SetActive(true);
    }

    private void NextQuestion()
    {
        eventSystem.SetActive(false);
        rewardPanel.SetActive(false);
        attempt = 0;
        currentQuestionIndex++;

        question.SetActive(false);
        
        trueButton.gameObject.SetActive(false);
        falseButton.gameObject.SetActive(false);

        for(int i = 0; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }


        if(currentQuestionIndex >= questionsGroups[language].questions.Count)
        {
            //Debug.LogFormat("No More Questions");

            ShowFinish(mistakes <= allowedMistakes);

            return;
        }

        currentQuestion = questionsGroups[language].questions[shuffledQuestionsList[currentQuestionIndex]];

        StartCoroutine(ShowingQuestion());
    }

    private void ShowFinish(bool didGreat)
    {
        StartCoroutine(ShowingFinish(didGreat));
    }

    private float PlayAudioClip(AudioClip audioClip)
    {
        if(audioClip != null)
        {
            tutorAudioSource.Stop();
            tutorAudioSource.clip = audioClip;
            tutorAudioSource.Play();
            return audioClip.length;
        }

        return defaultAudioWait;
    }

    private float PlayFeedback(bool isCorrect)
    {
        if(!isCorrect)
            mistakes++;

        if (isCorrect && correctAnswerFeedbacks[language] != null)
        {
            return PlayAudioClip(correctAnswerFeedbacks[language]);
        }
        else if (!isCorrect && wrongAnswerFeedbacks[language] != null)
        {
            return PlayAudioClip(wrongAnswerFeedbacks[language]);
        }

        return defaultFeedbackWait;
    }

    private void PrintList(string name, List<int> list)
    {
        StringBuilder sb = new StringBuilder();

        if(list.Count > 0)
        {
            sb.AppendFormat("{0} of {1} elements -> {2}", name, list.Count, list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                sb.AppendFormat(" | {0}", list[i]);
            }
        }

        //Debug.Log(sb.ToString());
    }

    public void Show(bool show)
    {
        if(!show)
        {
            questionsCanvas.SetActive(false);
            startMessageImage.gameObject.SetActive(false);
            startActionButton.gameObject.SetActive(false);
            finishMessageImage.gameObject.SetActive(false);
            return;
        }

        StopAllCoroutines();
        tutorAudioSource.Stop();

        mistakes = 0;
        currentQuestionIndex = -1;
        language = PlayerPrefs.GetInt("ELEgyptLanguage", 0);

        if (language >= questionsGroups.Length)
        {
            //Debug.LogFormat("Unavailable Questions Group");
            return;
        }

        ResetAlignment(questionsGroups[language].direction == QuestionsGroup.LanguageDirection.LTR ?
            ltrScale : rtlScale);

        shuffledQuestionsList.Clear();
        for(int i = 0; i < questionsGroups[language].questions.Count; i++)
        {
            shuffledQuestionsList.Add(i);
        }

        if (shuffleQuestions)
            shuffledQuestionsList.Shuffle();

        StartCoroutine(ShowingStart());
    }
    
    public void StartGroup()
    {
        startPanel.SetActive(false);
        questionsPanel.SetActive(true);
        NextQuestion();
    }
    
    public void Correctify(bool isTrue)
    {
        attempt++;

        StartCoroutine(Correctifying(isTrue));
    }

    public void ValidateChoice(int choiceIndex)
    {
        attempt++;

        StartCoroutine(ValidatingChoice(choiceIndex));
    }

    IEnumerator ShowingStart()
    {
        questionsCanvas.SetActive(true);
        startPanel.SetActive(true);
        questionsPanel.SetActive(false);
        finishPanel.SetActive(false);
        rewardPanel.SetActive(false);

        if (type == Question.Type.Sprite)
        {
            startMessageImage.sprite = startMessageTSAblocks[language].sprite;
            startMessageImage.gameObject.SetActive(true);

            yield return new WaitForSeconds(PlayAudioClip(startMessageTSAblocks[language].audio));

            startActionImage.sprite = startActionTSAblocks[language].sprite;
            startActionButton.gameObject.SetActive(true);
        }
        else if (type == Question.Type.Text)
        {
        }

        eventSystem.SetActive(true);
    }

    IEnumerator ShowingFinish(bool didGreat)
    {
        questionsPanel.SetActive(false);
        finishPanel.SetActive(true);

        if (type == Question.Type.Sprite)
        {
            if (didGreat)
            {
                finishMessageImage.sprite = finishMessageGoodTSAblocks[language].sprite;
                finishMessageImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(PlayAudioClip(finishMessageGoodTSAblocks[language].audio));
            }
            else
            {
                finishMessageImage.sprite = finishMessageBadTSAblocks[language].sprite;
                finishMessageImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(PlayAudioClip(finishMessageBadTSAblocks[language].audio));
            }
        }
        else if (type == Question.Type.Text)
        {

        }

        if (onFinishAcions != null)
        {
            onFinishAcions.Invoke();
        }
    }

    IEnumerator ShowingQuestion()
    {
        if (currentQuestion.type == Question.Type.Sprite)
        {
            questionImage.sprite = currentQuestion.questionSprite;
            question.SetActive(true);

            yield return new WaitForSeconds(PlayAudioClip(currentQuestion.audio));

            if (currentQuestion.kind == Question.Kind.MultipleChoice)
            {
                shuffledChoicesList.Clear();
                for (int i = 0; i < currentQuestion.choices.Count; i++)
                {
                    shuffledChoicesList.Add(i);
                }

                //PrintList(string.Format("{0}", currentQuestion.id), shuffledChoicesList);

                if (shuffleChoices)
                    shuffledChoicesList.Shuffle();

                //PrintList(string.Format("{0} Shuffled", currentQuestion.id), shuffledChoicesList);

                for (int i = 0; i < currentQuestion.choices.Count; i++)
                {
                    choiceImages[i].sprite = currentQuestion.choices[shuffledChoicesList[i]].sprite;
                    choiceButtons[i].interactable = choiceButtons[i].image.raycastTarget = true;
                    choiceButtons[i].image.sprite = choiceBackgroundSprites[normal];
                    choiceButtons[i].gameObject.SetActive(true);

                    yield return new WaitForSeconds(PlayAudioClip(currentQuestion.choices[shuffledChoicesList[i]].audio));
                }
            }
            else if (currentQuestion.kind == Question.Kind.TrueFalse)
            {
                trueButton.image.sprite = trueSprites[normal];
                trueButton.interactable = trueButton.image.raycastTarget = true;
                trueButton.gameObject.SetActive(true);
                yield return new WaitForSeconds(defaultAudioWait);

                falseButton.image.sprite = falseSprites[normal];
                falseButton.interactable = falseButton.image.raycastTarget = true;
                falseButton.gameObject.SetActive(true);
            }
        }

        eventSystem.SetActive(true);
    }

    IEnumerator Correctifying(bool isTrue)
    {
        trueButton.interactable = trueButton.image.raycastTarget = false;
        falseButton.interactable = falseButton.image.raycastTarget = false;

        if (isTrue)
        {
            if (currentQuestion.isCorrect)
            {
                trueButton.image.sprite = trueSprites[correct];

                Reward();

                Ranking(currentQuestion.id, true, attempt, rewardValues[attempt - 1]);

                yield return new WaitForSeconds(PlayFeedback(true));
                
            }
            else
            {
                trueButton.image.sprite = trueSprites[wrong];
                falseButton.image.sprite = falseSprites[correct];

                Ranking(currentQuestion.id, false, attempt + 1, 0);

                yield return new WaitForSeconds(PlayFeedback(false));
            }
        }

        else
        {
            if (!currentQuestion.isCorrect)
            {
                falseButton.image.sprite = falseSprites[correct];

                Reward();

                Ranking(currentQuestion.id, true, attempt, rewardValues[attempt - 1]);

                yield return new WaitForSeconds(PlayFeedback(true));
            }
            else
            {
                falseButton.image.sprite = falseSprites[wrong];
                trueButton.image.sprite = trueSprites[correct];

                Ranking(currentQuestion.id, false, attempt + 1, 0);

                yield return new WaitForSeconds(PlayFeedback(false));
            }
        }
        
        NextQuestion();
    }

    IEnumerator ValidatingChoice(int choiceIndex)
    {
        //Debug.LogFormat("Choice: {0} | Correct: {1}", currentQuestion.choices[shuffledChoicesList[choiceIndex - 1]].id, currentQuestion.choices[shuffledChoicesList[choiceIndex - 1]].isCorrect);

        if (currentQuestion.choices[shuffledChoicesList[choiceIndex - 1]].isCorrect)
        {
            choiceButtons[choiceIndex - 1].image.sprite = choiceBackgroundSprites[correct];
            choiceButtons[choiceIndex - 1].interactable = choiceButtons[choiceIndex - 1].image.raycastTarget = false;

            Reward();

            Ranking(currentQuestion.id, true, attempt, rewardValues[attempt - 1]);

            // disable all choices to disallow choosing another one 
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                choiceButtons[i].interactable = choiceButtons[i].image.raycastTarget = false;
            }

            yield return new WaitForSeconds(PlayFeedback(true));

            NextQuestion();
        }
        else
        {
            // is it the last try
            if (attempt == currentQuestion.choices.Count - 1)
            {
                choiceButtons[choiceIndex - 1].image.sprite = choiceBackgroundSprites[wrong];
                choiceButtons[choiceIndex - 1].interactable = choiceButtons[choiceIndex - 1].image.raycastTarget= false;

                // show the correct choice
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    if (choiceImages[i].IsActive() && choiceButtons[i].interactable)
                    {
                        choiceButtons[i].image.sprite = choiceBackgroundSprites[correct];
                        choiceButtons[i].interactable = choiceButtons[i].image.raycastTarget = false;
                        break;
                    }
                }

                // disable all choices to disallow choosing another one 
                for (int i = 0; i < choiceButtons.Length; i++)
                {
                    choiceButtons[i].interactable = choiceButtons[i].image.raycastTarget = false;
                }

                Ranking(currentQuestion.id, false, attempt + 1, 0);

                yield return new WaitForSeconds(PlayFeedback(false));

                NextQuestion();
            }
            else
            {
                choiceButtons[choiceIndex - 1].image.sprite = choiceBackgroundSprites[wrong];
                choiceButtons[choiceIndex - 1].interactable = choiceButtons[choiceIndex - 1].image.raycastTarget = false;

                yield return new WaitForSeconds(PlayFeedback(false));
            }
        }
    }

    // web service
    Hashtable ht = new Hashtable();
    public void Ranking(int questionId, bool correctAnswer, int attempt, int reward)
    {
        //Debug.LogFormat("Lesson ID: {0} | Question ID: {1} | Correct Answer: {2} | Attempt: {3} | Reward: {4}", lessonId, questionId, correctAnswer, attempt, reward);

        ht.Clear();
        ht.Add("answer", correctAnswer ? 1 : 0);
        ht.Add("part_id", lessonId);
        ht.Add("question_number", questionId);
        ht.Add("degree", reward);
        ht.Add("attempt", attempt);
        POST("http://enjoylearningvr.com/dashboard/Enjoylearning/api/userAnswer", ht);
    }

    public void POST(string url, Hashtable data)
    {
        StartCoroutine(Post(url, data));
    }

    IEnumerator Post(string url, Hashtable data)
    {
        List<IMultipartFormSection> form = new List<IMultipartFormSection>();
        foreach (DictionaryEntry item in data)
        {
            // print(item.Key.ToString());
            // print(item.Value.ToString());

            form.Add(new MultipartFormDataSection(item.Key.ToString(), item.Value.ToString()));
        }

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        www.SetRequestHeader("Authorization", "Bearer " + PlayerPrefs.GetString("ELEgypttoken"));
        //	www.SetRequestHeader ("Authorization","Bearer "+"eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsImp0aSI6ImViYWY1NDBmOGFkNTE4N2QwMjU0M2Y4NTU4ZGQxZWUyMjg5NTM0YmZjZjFhMGUxMTgzM2FlZjEyODgzZTUxOTJjM2QyODQxMGY2NTE2ZmE3In0.eyJhdWQiOiIxIiwianRpIjoiZWJhZjU0MGY4YWQ1MTg3ZDAyNTQzZjg1NThkZDFlZTIyODk1MzRiZmNmMWEwZTExODMzYWVmMTI4ODNlNTE5MmMzZDI4NDEwZjY1MTZmYTciLCJpYXQiOjE1MTMxNzI5OTYsIm5iZiI6MTUxMzE3Mjk5NiwiZXhwIjoxNTQ0NzA4OTk2LCJzdWIiOiI5Iiwic2NvcGVzIjpbXX0.jW25jiDm1WUpibWGvgumXaQz14Es_ki3cS500ntDIvCIR2mHs_KoBx4oMxLfMoYdiqLxsZAUUXMgOgMayU7FcrpIPH3hHEyFpfUsBoVh1SJ0-FoQR_6_-1JK1A4EROMje5pngM_C5PlNJTRST7ZqWoKqw7otqxnep9Z7iewmJTBBfklOQDaxOn7RMObjdlDxzJ4MrVgMdUP3gFbLkgFYcAJbT_L9U--KTdNTnGbx4jyUt7IV2x_Kc23InoJ2eYZvumyFnFlNi_uQCHQO2Bb2EoXFeFZ2vJ2Zq6bg1U7pfVhX5Dt8f8oQb9dy-S6ZFmFC_g3ik4APEI8AYgkSEjr4AxeLbyEz3owuZFRB9xJ3_dNwSAXsZ6E63YvT27NFUY3a157r4S8nHrjW5Ij5KIGR6wu9bTglkN0_HIvJf9kjMG_nOLcFiHJJBV-GU0DjUzO8npbBz_uEaeJr53NEdiF2Yg9-bb4XgJ6i6gaJ7E1zp1BY62xqHpvdkaZR01XlBaiN9dvw6bJROzXgua8LUtwW3uH5QFiBhSFHw5UhtRuJ9IGVnSq1pEVdFnp4QbeKnK6fdmrL49JbYw1k1GvB3QKh3BYGAo0vW3zRjoO-6PgBG2_mNrSRqEt9LpW5DNRBau9JaS1TeCjdp5Es9SjOFgTYgMezTX6yrmlk_jBpypfkntQ");
        www.SetRequestHeader("Accept", "application/json");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            //Debug.Log(www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
        }
    }
}
