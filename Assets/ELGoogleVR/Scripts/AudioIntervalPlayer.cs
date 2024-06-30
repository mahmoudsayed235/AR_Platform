using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntervalPlayer : MonoBehaviour
{
	public AudioSource audioSource;

	// for testing
	public float MaxInterval;

	private float currentChunckEnd = float.MaxValue;

	void Update()
	{
		//if (Input.GetKeyUp (KeyCode.P)) 
		//{
		//	float starTime = Random.Range (0, audioSource.clip.length);
		//	float endTime = Mathf.Min(starTime + MaxInterval, Random.Range (starTime, audioSource.clip.length));

		//	//Debug.LogFormat ("Playing Chunck - Start: {0} | End: {1}", starTime, endTime);

		//	PlayInterval (starTime, endTime);
		//}

		if (audioSource.time >= currentChunckEnd) 
		{
			//Debug.LogFormat ("Finished Chunck - Chunck End: {0} | Audio Time: {1}", currentChunckEnd, audioSource.time);
			audioSource.Pause ();
			currentChunckEnd = float.MaxValue;
		}
	}

	public void PlayInterval(float startTime, float endTime)
	{
		PlayChunck (startTime, endTime);
	}

	public void PlayInterval(AudioClip audioClip, float startTime, float endTime)
	{
		audioSource.clip = audioClip;
		PlayChunck (startTime, endTime);
	}

	private void PlayChunck(float startTime, float endTime)
	{
		audioSource.time = startTime;
		currentChunckEnd = endTime;
		audioSource.Play ();
	}
}
