using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnParticleCollisionEvents : MonoBehaviour
{
    public bool decativateOnParticleCollision;
    public string[] interestedInParticleTags;
    public UnityEvent[] onParticleCollisionEvents;

    private Collider mCollider;

    private void Awake()
    {
        mCollider = gameObject.GetComponent<Collider>();
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.LogFormat("OnParticleCollision -> Particle: {0} | Collision With: {1}", other.name, this.name);
        
        for(int i = 0; i < interestedInParticleTags.Length; i++)
        {
            if(interestedInParticleTags[i] == other.tag)
            {
                mCollider.enabled = !decativateOnParticleCollision;

                if (onParticleCollisionEvents != null && onParticleCollisionEvents.Length > i && onParticleCollisionEvents[i] != null)
                {
                    onParticleCollisionEvents[i].Invoke();
                }
            }
        }
    }
}
