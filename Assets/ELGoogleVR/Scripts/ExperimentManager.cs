using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperimentManager : MonoBehaviour
{
    public bool startOnEnable;
    public bool autoStartNextStep;

    [Range(0.0f, 60.0f)]
    public float startDelay;

    public ExperimentStep[] steps;

    public UnityEvent onEnableEvent;

    [Range(0.0f, 60.0f)]
    public float finishDelay;
    public UnityEvent onFinishEvent;

    private ExperimentStep currentExperimentStep;
    private int currentStepIndex;

    private void Awake()
    {
        if(steps.Length > 0)
        {
            currentStepIndex = 0;
            currentExperimentStep = steps[currentStepIndex];
        }
    }

    private void OnEnable()
    {
        if(startOnEnable)
        {
            Invoke("BeginStep", startDelay);
        }

        if(onEnableEvent != null)
        {
            onEnableEvent.Invoke();
        }
    }

    public void BeginStep()
    {
        if(currentExperimentStep != null)
        {
            //Debug.LogFormat("Experiment Manager -> Begin Step: {0}", currentExperimentStep.name);
            currentExperimentStep.Begin();
        }
    }

    public void FinishStep()
    {
        if (currentExperimentStep != null)
        {
            //Debug.LogFormat("Experiment Manager -> Finish Step: {0}", currentExperimentStep.name);
            currentExperimentStep.Finish();
        }

        if (autoStartNextStep)
        {
            Invoke("BeginStep", currentExperimentStep.finishDelay);
        }

        currentExperimentStep = null;
        currentStepIndex++;
        
        if (currentStepIndex < steps.Length)
        {
            currentExperimentStep = steps[currentStepIndex];
        }
        else
        {
            Invoke("ExperimentFinish", steps[steps.Length - 1].finishDelay);
        }
    }

    private void ExperimentFinish()
    {
        if (onFinishEvent != null)
        {
            //Debug.LogFormat("Experiment Manager -> {0}", "All Steps Finished");
            onFinishEvent.Invoke();
        }
    }
}
