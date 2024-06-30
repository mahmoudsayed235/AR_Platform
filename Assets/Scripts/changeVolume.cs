using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeVolume : MonoBehaviour
{
    // Start is called before the first frame update
    float m_MySliderValue;
    AudioSource m_MyAudioSource;

    void Start()
    {
        m_MySliderValue = 0.2f;
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.volume = m_MySliderValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
