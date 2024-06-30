using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragQuestionManager : MonoBehaviour
{
    public delegate void DragQuestionSolved(int questionID, int correctlyDraggedChoices, bool timeStamper);
    public static event DragQuestionSolved OnDragQuestionSolved;

    public int id;
    public bool validateAtOnce;
    public bool timeStamper;
    public int numberOfChoices;
    public DraggerTarget[] draggerTargets;

    public float delay;
    public UnityEvent onQuestionValidatedActions;

    private int validatedChoices;
    private int correctChoices;

    private void OnEnable()
    {
        DraggerTarget.OnObjectDragged += CheckDraggedObjects;
        DraggerTarget.OnObjectDragged += CheckDraggedObject;
    }

    private void OnDisable()
    {
        DraggerTarget.OnObjectDragged -= CheckDraggedObjects;
        DraggerTarget.OnObjectDragged -= CheckDraggedObject;
    }

    private void CheckDraggedObject(DraggerTarget choiceTarget)
    {
        if (validateAtOnce)
        {
            return;
        }

        ValidateChoice(choiceTarget);
    }

    private void CheckDraggedObjects(DraggerTarget choiceTarget)
    {
        if(!validateAtOnce)
        {
            return;
        }

        int draggedChoices = 0;

        for (int i = 0; i < draggerTargets.Length; i++)
        {
            if(draggerTargets[i].ObjectsInsideCount() > 0)
            {
                draggedChoices++;
            }
        }

        if(draggedChoices == numberOfChoices)
        {
            //Debug.LogFormat("Question: {0} --> All Objects are Dragged", id);
            ValidateQuestion();
        }
    }

    private void ValidateQuestion()
    {
        bool[] correctChoice = new bool[draggerTargets.Length];
        ValidChoicesKeys validChoicesKeys = null;

        for(int i = 0; i < draggerTargets.Length; i++)
        {
            validChoicesKeys = draggerTargets[i].GetComponent<ValidChoicesKeys>();

            if(validChoicesKeys != null)
            {
                if(draggerTargets[i].ObjectsInsideCount() > 0)
                {
                    correctChoice[i] = validChoicesKeys.IsValidChoice(draggerTargets[i].LastDraggedObjectKey());

                    draggerTargets[i].GetComponent<ChoiceFeedbackActions>().Feedback(correctChoice[i]);

                    if (correctChoice[i])
                        correctChoices++;

                    //Debug.LogFormat("Question: {0} --> Target: {1} contains {2}Valid choice", id, draggerTargets[i].name, correctChoice[i] ? "a " : "no ");
                }
            }
        }

        Invoke("ValidationFinished", delay);
    }

    private void ValidateChoice(DraggerTarget choiceTarget)
    {
        ValidChoicesKeys validChoicesKeys = choiceTarget.GetComponent<ValidChoicesKeys>();
        bool validChoice = validChoicesKeys.IsValidChoice(choiceTarget.LastDraggedObjectKey());
        choiceTarget.GetComponent<ChoiceFeedbackActions>().Feedback(validChoice);
        correctChoices += validChoice ? 1 : 0;
        validatedChoices++;

        if(validatedChoices == numberOfChoices)
        {
            Invoke("ValidationFinished", delay);
        }
    }

    private void ValidationFinished()
    {
        if(OnDragQuestionSolved != null)
        {
            OnDragQuestionSolved(id, correctChoices, timeStamper);
        }

        if (onQuestionValidatedActions != null)
        {
            onQuestionValidatedActions.Invoke();
        }
    }
}
