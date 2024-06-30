using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationDelayer : MonoBehaviour {

	Animator anim;

	void Awake ()
	{	
		anim = gameObject.GetComponent<Animator>();	
	}

	void OnEnable()
	{
		anim.enabled=false;
		Invoke("DelayAnimation", Random.Range(0.0f, anim.GetCurrentAnimatorClipInfo(0).Length));
	}
		
	public void DelayAnimation()
	{
		anim.enabled=true;
	}
}
