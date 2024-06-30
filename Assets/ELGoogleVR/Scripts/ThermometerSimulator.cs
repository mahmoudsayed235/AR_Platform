using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class ThermometerSimulator : MonoBehaviour
{
    public TextMeshPro[] thermometerTextMeshPros;
    public Text[] thermometerTexts;

    [Range(35.0f, 45.0f)]
    public float targetDegree = 35.0f;

    public bool randomTargetDegree;

    [Range(35.0f, 45.0f)]
    public float minTargetDegree = 35.0f;

    [Range(35.0f, 45.0f)]
    public float maxTargetDegree = 35.0f;

    [Range(1.0f, 10.0f)]
    public float period = 1.0f;

    public UnityEvent onSimulationFinishEvent;

    private readonly float minDegree = 35.0f;
    private readonly float flashingWait = 0.1f;

    private bool paused;

    private void OnEnable()
    {
        PlayPauseManager.OnPause += OnPause;
    }

    private void OnDisable()
    {
        PlayPauseManager.OnPause -= OnPause;
    }

    private void OnPause(bool pause)
    {
        paused = pause;
    }

    public void Simulate()
    {
        StartCoroutine(Simulating());
    }

    IEnumerator Simulating()
    {
        int steps = (int)(period / flashingWait);
        float step = (targetDegree - minDegree ) / steps;

        if(randomTargetDegree)
        {
            step = (Random.Range(minTargetDegree, maxTargetDegree) - minDegree) / steps;
        }

        for (int i = 0; i <= steps; i++)
        {
            for (int j = 0; j < thermometerTextMeshPros.Length; j++)
            {
                while (paused)
                {
                    yield return new WaitForEndOfFrame();
                }

                thermometerTextMeshPros[j].text = string.Format("{0:0.00} ºC", minDegree + step * i);
            }

            for (int j = 0; j < thermometerTexts.Length; j++)
            {
                while (paused)
                {
                    yield return new WaitForEndOfFrame();
                }

                thermometerTexts[j].text = string.Format("{0:0.00} ºC", minDegree + step * i);
            }

            yield return new WaitForSeconds(flashingWait);
        }


        if(onSimulationFinishEvent != null)
        {
            onSimulationFinishEvent.Invoke();
        }
    }
}
