using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.Events;
using UnityEngine.Networking;
using TMPro;
using System.IO;

public class QuestionSystem : MonoBehaviour
{
    public delegate void QuestionSolved(int questionID, int choiceID, int attempt, bool timeStamper);
    public static event QuestionSolved OnQuestionSolved;

    public enum LayoutDirection { RTL, LTR }

	public LayoutDirection layoutDirection;
    public bool timeStamper;
    public GameObject eventSystem;

    public int allowedMistakes;
	public float defaultAudioWait;
	public float defaultFeedbackWait;

	public AudioSource feedbackAudioSource;
	public AudioSource tutorAudioSource;
	public AudioIntervalPlayer audioIntervalPlayer;

	public GameObject flipper;
	public GameObject startPanel;
	public GameObject questionsPanel;
	public GameObject finishPanel;
	public GameObject rewardPanel;

	public ArabicTextMeshProUGUI startMessageText;
	public Image startActionImage;
	public Button startActionButton;

	public ArabicTextMeshProUGUI finishMessageText;
	public ArabicTextMeshProUGUI pointsText;

	public GameObject question;
	public ArabicTextMeshProUGUI questionText;

	public Button trueButton;
	public Button falseButton;

	public Button[] choiceButtons;
	public ArabicTextMeshProUGUI[] choiceText;

	public AudioClip[] correctAnswerFeedbacks;
	public AudioClip[] wrongAnswerFeedbacks;

	// 3 expected sprites [0 - normal] [1 - correct] [2 - wrong]
	public Sprite[] trueSprites;
	public Sprite[] falseSprites;
	public Sprite[] choiceBackgroundSprites;

	
	public TextSpriteAudioBlock startActionTSAblock;

    public TextAudioBlock startMessageTAblock;
    public TextAudioBlock finishMessageGoodTAblock;
	public TextAudioBlock finishMessageBadTAblock;

	public UnityEvent onFinishAcions;

	//[SerializeField]
	private QuestionsData questionsData;

	private int simulationID;
	private int currentQuestionIndex;
	private int mistakes;
	private int attempt;

	private const int normal = 0;
	private const int correct = 1;
	private const int wrong = 2;

	private TextQuestion currentQuestion;
	private List<int> shuffledQuestionsList = new List<int>();
	private List<int> shuffledChoicesList = new List<int>();

	private Vector3 ltrScale = new Vector3(-1.0f, 1.0f, 1.0f);
	private Vector3 rtlScale = new Vector3(1.0f, 1.0f, 1.0f);

    private bool availableQuestionsData = false;

	private void Awake()
	{
		simulationID = PlayerPrefs.GetInt (PlayerPrefsKeys.SimulationID, -1);
        
		// initialize questions
		questionsData = JsonUtility.FromJson<QuestionsData> (PlayerPrefs.GetString(PlayerPrefsKeys.Questions));
		//Debug.LogFormat("Questions JSON : {0}", JsonUtility.ToJson (questionsData));

        if(questionsData == null)
        {
            //Debug.LogFormat("Questions System: {0}", "Bad Questions Data");
            flipper.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
            return;
        }

        availableQuestionsData = true;
        
		// intialize audio
		StartCoroutine(LoadingAudioClip());
    }

	private void Update()
	{
		//if(Input.GetKeyUp(KeyCode.S))
		//{
		//	Show(true);
		//}

		//if (Input.GetKeyUp(KeyCode.H))
		//{
		//	Show(false);
		//}

		//if (Input.GetKeyUp(KeyCode.X))
		//{
		//	//Debug.Log ("Saving questions to json");
		//	PlayerPrefs.SetString(PlayerPrefsKeys.Questions, JsonUtility.ToJson (questionsData));
		//	PlayerPrefs.Save ();
		//}
	}

	private void OnEnable()
	{
		startPanel.SetActive (false);
		questionsPanel.SetActive (false);
		finishPanel.SetActive (false);
		rewardPanel.SetActive (false);

        ResetAlignment(layoutDirection);
    }

	private void ResetAlignment(LayoutDirection layoutDirection)
	{
		Vector3 alignmentScale = layoutDirection == LayoutDirection.LTR ? ltrScale : rtlScale;
		bool fix = layoutDirection == LayoutDirection.LTR ? false : true;
		TextAlignmentOptions textAlignmentOptions = fix ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;

		flipper.transform.localScale = alignmentScale;
        
		startActionButton.transform.parent.localScale = alignmentScale;

        if (startMessageText.TextMeshProUGUI == null)
        {
            startMessageText.TextMeshProUGUI = startMessageText.GetComponent<TextMeshProUGUI>();
        }

        startMessageText.TextMeshProUGUI.rectTransform.localScale = alignmentScale;
        startMessageText.fix = fix;
        startMessageText.TextMeshProUGUI.isRightToLeftText = fix;
        
        if (finishMessageText.TextMeshProUGUI == null)
        {
            finishMessageText.TextMeshProUGUI = finishMessageText.GetComponent<TextMeshProUGUI>();
        }

        finishMessageText.TextMeshProUGUI.rectTransform.localScale = alignmentScale;
        finishMessageText.fix = fix;
        finishMessageText.TextMeshProUGUI.isRightToLeftText = fix;


        if (pointsText.TextMeshProUGUI == null)
        {
            pointsText.TextMeshProUGUI = pointsText.GetComponent<TextMeshProUGUI>();
        }

        pointsText.TextMeshProUGUI.rectTransform.localScale = alignmentScale;
        pointsText.fix = fix;
        pointsText.TextMeshProUGUI.isRightToLeftText = fix;

		if (questionText.TextMeshProUGUI == null) 
		{
			questionText.TextMeshProUGUI = questionText.GetComponent<TextMeshProUGUI> ();
		}

		questionText.TextMeshProUGUI.rectTransform.localScale = alignmentScale;
		questionText.fix = fix;
		questionText.TextMeshProUGUI.isRightToLeftText = fix;

		for (int i = 0; i < choiceText.Length; i++)
		{
			if (choiceText [i].TextMeshProUGUI == null) 
			{
				choiceText [i].TextMeshProUGUI = choiceText [i].GetComponent<TextMeshProUGUI> ();
			}

			choiceText[i].transform.localScale = alignmentScale;
			choiceText [i].fix = fix;

			choiceText [i].TextMeshProUGUI.isRightToLeftText = fix;
			choiceText [i].TextMeshProUGUI.alignment = textAlignmentOptions;
		}

		trueButton.transform.parent.localScale = alignmentScale;
		falseButton.transform.parent.localScale = alignmentScale;
	}

	private void Reward(bool showReward)
	{
        if(showReward)
        {
            pointsText.Text = string.Format("{0}", (currentQuestion.points / (currentQuestion.choices.Count - 1)) * (currentQuestion.choices.Count - attempt));
        }
        
        if(OnQuestionSolved != null)
        {
            OnQuestionSolved(currentQuestion.id, currentQuestion.GetCorrectChoiceID(), attempt, timeStamper);
        }

		rewardPanel.SetActive(showReward);
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


		if(currentQuestionIndex >= questionsData.questions.Count)
		{
			ShowFinish(mistakes <= allowedMistakes);

			return;
		}

		currentQuestion = questionsData.questions[shuffledQuestionsList[currentQuestionIndex]];

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
			feedbackAudioSource.Stop();
			feedbackAudioSource.clip = audioClip;
			feedbackAudioSource.Play();
			return audioClip.length;
		}

		return defaultAudioWait;
	}

	private float PlayFeedback(bool isCorrect)
	{
		if(!isCorrect)
			mistakes++;

		if (isCorrect && correctAnswerFeedbacks != null)
		{
			return PlayAudioClip(correctAnswerFeedbacks[Random.Range(0, correctAnswerFeedbacks.Length)]);
		}
		else if (!isCorrect && wrongAnswerFeedbacks != null)
		{
			return PlayAudioClip(wrongAnswerFeedbacks[Random.Range(0, wrongAnswerFeedbacks.Length)]);
		}

		return defaultFeedbackWait;
	}

	public void Show(bool show)
	{
		if(!show)
		{
			startPanel.SetActive (false);
			questionsPanel.SetActive (false);
			finishPanel.SetActive (false);
			rewardPanel.SetActive (false);
			return;
		}

        if(!availableQuestionsData)
        {
            if(onFinishAcions != null)
            {
                onFinishAcions.Invoke();
            }

            return;
        }

		StopAllCoroutines();
		tutorAudioSource.Stop();

		mistakes = 0;
		currentQuestionIndex = -1;
        
		shuffledQuestionsList.Clear();
		for(int i = 0; i < questionsData.questions.Count; i++)
		{
			shuffledQuestionsList.Add(i);
		}

		if (questionsData.shuffle)
			shuffledQuestionsList.Shuffle();

		StartCoroutine(ShowingStart());
	}

	public void StartQuestions()
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
		startPanel.SetActive(true);
		questionsPanel.SetActive(false);
		finishPanel.SetActive(false);
		rewardPanel.SetActive(false);

        startMessageText.Text = startMessageTAblock.text;
        startMessageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(PlayAudioClip(startMessageTAblock.audio));

        startActionImage.sprite = startActionTSAblock.sprite;
        startActionButton.gameObject.SetActive(true);

        eventSystem.SetActive(true);
	}

	IEnumerator ShowingFinish(bool didGreat)
	{
		questionsPanel.SetActive(false);
		finishPanel.SetActive(true);

        if (didGreat)
        {
            finishMessageText.Text = finishMessageGoodTAblock.text;
            finishMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(PlayAudioClip(finishMessageGoodTAblock.audio));
        }
        else
        {
            finishMessageText.Text = finishMessageBadTAblock.text;
            finishMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(PlayAudioClip(finishMessageBadTAblock.audio));
        }

        if (onFinishAcions != null)
		{
			onFinishAcions.Invoke();
		}
	}

	IEnumerator ShowingQuestion()
	{
		questionText.Text = currentQuestion.text;
		question.SetActive(true);

		audioIntervalPlayer.PlayInterval (currentQuestion.startTime, currentQuestion.endTime);
		yield return new WaitForSeconds(currentQuestion.endTime - currentQuestion.startTime);

		if (currentQuestion.type == TextQuestion.Type.MultipleChoice)
		{
			shuffledChoicesList.Clear();
			for (int i = 0; i < currentQuestion.choices.Count; i++)
			{
				shuffledChoicesList.Add(i);
			}

			if (currentQuestion.shuffle)
				shuffledChoicesList.Shuffle();

			for (int i = 0; i < currentQuestion.choices.Count; i++)
			{
				choiceText[i].Text = currentQuestion.choices[shuffledChoicesList[i]].text;
				choiceButtons[i].interactable = choiceButtons[i].image.raycastTarget = true;
				choiceButtons[i].image.sprite = choiceBackgroundSprites[normal];
				choiceButtons[i].gameObject.SetActive(true);

				audioIntervalPlayer.PlayInterval (currentQuestion.choices [shuffledChoicesList [i]].startTime, 
					currentQuestion.choices [shuffledChoicesList [i]].endTime);

				yield return new WaitForSeconds(currentQuestion.choices[shuffledChoicesList[i]].endTime - currentQuestion.choices[shuffledChoicesList[i]].startTime);
			}
		}
		else if (currentQuestion.type == TextQuestion.Type.TrueFalse)
		{
			trueButton.image.sprite = trueSprites[normal];
			trueButton.interactable = trueButton.image.raycastTarget = true;
			trueButton.gameObject.SetActive(true);
			yield return new WaitForSeconds(defaultAudioWait);

			falseButton.image.sprite = falseSprites[normal];
			falseButton.interactable = falseButton.image.raycastTarget = true;
			falseButton.gameObject.SetActive(true);
		}
		eventSystem.SetActive(true);
	}

	IEnumerator Correctifying(bool isTrue)
	{
		trueButton.interactable = trueButton.image.raycastTarget = false;
		falseButton.interactable = falseButton.image.raycastTarget = false;

		if (isTrue)
		{
			if (currentQuestion.IsCorrect())
			{
				trueButton.image.sprite = trueSprites[correct];

				Reward(true);

				yield return new WaitForSeconds(PlayFeedback(true));

			}
			else
			{
				trueButton.image.sprite = trueSprites[wrong];
				falseButton.image.sprite = falseSprites[correct];

				attempt++;
				Reward(false);

				yield return new WaitForSeconds(PlayFeedback(false));
			}
		}

		else
		{
			if (!currentQuestion.IsCorrect())
			{
				falseButton.image.sprite = falseSprites[correct];

				Reward(true);
                
				yield return new WaitForSeconds(PlayFeedback(true));
			}
			else
			{
				falseButton.image.sprite = falseSprites[wrong];
				trueButton.image.sprite = trueSprites[correct];

				attempt++;
				Reward(false);

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

			Reward(true);

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
					if (choiceText[i].TextMeshProUGUI.IsActive() && choiceButtons[i].interactable)
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

				attempt++;
				Reward(false);
				
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

	IEnumerator LoadingAudioClip()
	{
#if UNITY_ANDROID
        string audioPath = string.Format ("file:///{0}/{1}.mp3", Application.persistentDataPath, simulationID);
#elif UNITY_IOS || UNITY_IPHONE
        string audioPath = string.Format ("file://{0}/{1}.mp3", Application.persistentDataPath, simulationID);
#endif
        //Debug.LogFormat (audioPath);

        WWW www = new WWW(audioPath);

        yield return www;

        AudioClip clip = www.GetAudioClip();

        while (clip.loadState == AudioDataLoadState.Loading)
		{
            //Debug.Log ("Still Loading Audio File .......");
			yield return www;
		}

		//Debug.Log ("Done Loading Audio File!");
		tutorAudioSource.clip = clip;
	}

	// web service
	Hashtable ht = new Hashtable();
	public void Ranking(int questionId, bool correctAnswer, int attempt, int reward)
	{
		//Debug.LogFormat("Lesson ID: {0} | Question ID: {1} | Correct Answer: {2} | Attempt: {3} | Reward: {4}", lessonId, questionId, correctAnswer, attempt, reward);

		ht.Clear();
		ht.Add("answer", correctAnswer ? 1 : 0);
		ht.Add("part_id", simulationID);
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
