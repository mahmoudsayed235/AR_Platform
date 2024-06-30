using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    public quizController quiz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()
    {
        print(this.gameObject.name);
        quiz.answerQuestion(this.gameObject.name);
    }
   
}
