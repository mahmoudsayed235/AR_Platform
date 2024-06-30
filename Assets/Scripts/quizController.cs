using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizController : MonoBehaviour
{
    public bool AR;
    public bool arabicLang = false;
    public bool englishLang = false;
    public int componentsNumbers = 0;
    public string[] arabicComponentsName;
    public string[] englishComponentsName;
    Dictionary<int, bool> choosed;
    Dictionary<int, string> arabic;
    Dictionary<int, string> english;
    Dictionary<int, string> englishKey;
    List<int> index;
    public Text questionName;
    public GameObject quizObject;
    public GameObject presentationObject;
    public GameObject[] parts;
    // Start is called before the first frame update
    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.orientation = ScreenOrientation.LandscapeLeft;

    }
    void Start()
    {
        index = new List<int>();

        for (int i = 0; i < componentsNumbers; i++)
        {
            index.Add(i);
        }
        choosed = new Dictionary<int, bool>();
        arabic = new Dictionary<int, string>();
        englishKey = new Dictionary<int, string>();
        english = new Dictionary<int, string>();
        for (int i = 0; i < componentsNumbers; i++)
        {
            choosed[i] = false;
        }

        for (int i = 0; i < componentsNumbers; i++)
        {
            arabic[i] = arabicComponentsName[i];
        }
        for (int i = 0; i < componentsNumbers; i++)
        {
            englishKey[i] = englishComponentsName[i];
        }
        for (int i = 0; i < componentsNumbers; i++)
        {
            english[i] = parts[i].name;
        }
    }
    bool quizStarted = false;
    // Update is called once per frame
    void Update()
    {
        
            if (!quizStarted)
            {
                if (quizObject.activeSelf)
                {
                    createQuestion();
                    quizStarted = true;

                }
        }
    }
    bool AnimatorIsPlaying(string stateName)
    {
        return  presentationObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
    void changeState()
    {

        presentationObject.SetActive(false);
        quizObject.SetActive(true);

    }
    public GameObject layers;
    string currentAnswer = "";
    void createQuestion()
    {
        int random = Random.Range(0, index.Count - 1);
        print("length "+ index.Count);
        if (index.Count!=0) {
            if (choosed.ContainsKey(index[random])) {
                if (arabicLang)
                {
                    questionName.text = arabic[index[random]];
                    currentAnswer = english[index[random]];
                }
                else if(englishLang)
                {
                    questionName.text = englishKey[index[random]];
                    currentAnswer = english[index[random]];
                }
            }
            else
            {
                createQuestion();
            }
        } else
        {
            layers.SetActive(false);
            print("finish quiz");
        }
    }
    public void answerQuestion(string name)
    {

        print(name + "    "+ currentAnswer);
        if (name == currentAnswer)
        {

            int key = KeyByValue(english,name);
            print(key);
            print(index.Count);
            for (int i = 0; i < index.Count; i++)
            {
                if (key == index[i])
                {
                    choosed[index[i]] = true;
                    index.Remove(key);
                    break;
                }
            }
            bool flag = false;
            for(int i = 0; i < parts.Length; i++)
            {
                if (parts[i].name == name)
                {
                    print(parts[i].name);
                    //play animation
                    parts[i].GetComponent<Animator>().enabled = true;

                    break;
                }
            }
            
            createQuestion();
        }else
        {
            showWrongAnswer();
            Invoke("hideWrongAnswer", 2f);

        }

    }
    public GameObject wrong;
    void showWrongAnswer()
    {
        wrong.SetActive(true);
    }
    void hideWrongAnswer()
    {
        wrong.SetActive(false);
    }
    public static int KeyByValue(Dictionary<int, string> dict, string val)
    {
        int key = -1;
        foreach (KeyValuePair<int, string> pair in dict)
        {
            if (pair.Value == val)
            {
                key = pair.Key;
                break;
            }
        }
        return key;
    }
}
