using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelOneInfoDragQuestionManager : MonoBehaviour
{
    public delegate void BeginSwitch();
    public static event BeginSwitch OnBeginSwitch;

    public delegate void EndSwitch();
    public static event EndSwitch OnEndSwitch;

    public ValidChoicesKeys targetValidChoicesKeys;
    public SpriteRenderer feedbackSpriteRenderer;
    public TransformReseter[] choiceTransformReseters;
    public GameObject[] choiceModels;
    public int[] choiceModelKeys;
    public bool shuffle;

    [Range(1.0f, 30.0f)]
    public float switchDelay = 1.0f;

    private int currentChoice = -1;
    private ValidChoicesKeys validChoicesKeys;
    private GameObject currentModel;

    private List<int> shuffledModelsList = new List<int>();

    private void OnEnable()
    {
        DraggerTarget.OnObjectDragged += Switch;
    }

    private void OnDisable()
    {
        DraggerTarget.OnObjectDragged -= Switch;
    }

    private void Awake()
    {
        shuffledModelsList = new List<int>();

        for (int i = 0; i < choiceModels.Length; i++)
        {
            shuffledModelsList.Add(i);
        }

        if (shuffle)
            shuffledModelsList.Shuffle();
    }

    public void Switch(DraggerTarget choiceTarget)
    {
        currentChoice++;

        if(currentChoice < choiceModels.Length)
        {
            StartCoroutine(SwitchingModel());
        }
    }

    IEnumerator SwitchingModel()
    {
        if(OnBeginSwitch != null)
        {
            OnBeginSwitch();
        }

        if (currentModel != null)
        {
            yield return new WaitForSeconds(switchDelay);
            currentModel.SetActive(false);
        }

        currentModel = choiceModels[shuffledModelsList[currentChoice]];
        targetValidChoicesKeys.keys[0] = choiceModelKeys[shuffledModelsList[currentChoice]];

        foreach(TransformReseter tf in choiceTransformReseters)
        {
            if(tf.transform.parent != tf.parent)
            {
                tf.ResetTransform();
            }
        }

        feedbackSpriteRenderer.sprite = null;
        currentModel.SetActive(true);

        if (OnEndSwitch != null)
        {
            OnEndSwitch();
        }
    }
}