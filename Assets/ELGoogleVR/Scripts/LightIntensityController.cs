using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityController : MonoBehaviour
{
	private bool EnableCounter;
	private float IntensityValue;

	void Update()
    {
		
		CounterTrigger (EnableCounter);
	}

	public void DecreaseIntensityValueTo(float IntensityNewValue)
    {
		IntensityValue = IntensityNewValue;
	}

	public void CounterTrigger (bool EnableCounter)
    {
		if (EnableCounter == true)
        {
			StartCoroutine (IntensityValueDecreaser(IntensityValue));
		}
	}

	IEnumerator IntensityValueDecreaser (float IntensityNewValue)
    {
		while (RenderSettings.ambientIntensity > IntensityNewValue) {
			
			if (RenderSettings.ambientIntensity - 0.05f  < IntensityValue)
            {
				Debug.Log ("BREAK");
				break;
			}
            else
            {
				Debug.Log(RenderSettings.ambientIntensity);
				RenderSettings.ambientIntensity -= 0.05f;
				//Debug.Log (RenderSettings.ambientIntensity);
				yield return new WaitForSeconds (0.08f);
			} 
		}
	}

	public void IntensitySetter (float setIntensityValue)
    {
		RenderSettings.ambientIntensity = setIntensityValue;
	}
}
