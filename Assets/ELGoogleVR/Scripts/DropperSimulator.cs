using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropperSimulator : MonoBehaviour
{
    public Transform dropper;

    public bool randomDrops;

    [Range(1, 60)]
    public int drops = 1;

    [Range(1, 60)]
    public int minDrops = 1;

    [Range(1, 60)]
    public int maxDrops = 1;

    [Range(0.1f, 10.0f)]
    public float minWait = 0.1f;

    [Range(0.1f, 10.0f)]
    public float maxWait = 0.1f;

    public float speed;
    public Color dropColor;

    public float scaleMultiplier;
    public GameObject dropPrefab;
    public Sprite dropSprite;

    public UnityEvent onSimulationFinishEvent;
    
    public void Simulate()
    {
        if(randomDrops)
        {
            StartCoroutine(Simulating(Random.Range(minDrops, maxDrops)));
        }
        else
        {
            StartCoroutine(Simulating(drops));
        }
    }

    IEnumerator Simulating(int drops)
    {
        Drop drop;

        for(int i = 0; i < drops; i++)
        {
            drop = Instantiate(dropPrefab).GetComponent<Drop>();
            
            drop.transform.localScale = drop.transform.localScale * scaleMultiplier;

            drop.spriteRenderer.sprite = dropSprite;
            drop.transform.parent = dropper;
            drop.transform.localPosition = Vector3.zero;
            
            drop.Color = dropColor;
            drop.Speed = speed;

            drop.gameObject.SetActive(true);
            //Debug.LogFormat("DropperSimulator -> Dropping: {0} ", drop.name);

            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }

        if(onSimulationFinishEvent != null)
        {
            onSimulationFinishEvent.Invoke();
        }
    }
}
