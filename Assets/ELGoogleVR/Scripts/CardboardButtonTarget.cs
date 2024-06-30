using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnjoyLearning.VR.SDK
{
    public class CardboardButtonTarget : MonoBehaviour
    {
        public delegate void TargetSelected(GameObject gameObject);
        public static event TargetSelected OnTargetSelected;

        public delegate void TargetDeSelected();
        public static event TargetDeSelected OnTargetDeSelected;

        private void Awake()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();

            if(trigger == null)
            {
                gameObject.AddComponent<EventTrigger>();
                trigger = GetComponent<EventTrigger>();
            }

            EventTrigger.Entry PointerEnterEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerEnter
            };
            PointerEnterEntry.callback.AddListener((eventData) => { Selected(); });
            trigger.triggers.Add(PointerEnterEntry);

            EventTrigger.Entry PointerExitEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerExit
            };
            PointerExitEntry.callback.AddListener((eventData) => { DeSelected(); });
            trigger.triggers.Add(PointerExitEntry);

            EventTrigger.Entry PointerClickEntry = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerClick
            };
            PointerClickEntry.callback.AddListener((eventData) => { DeSelected(); });
            trigger.triggers.Add(PointerClickEntry);
        }

        private void OnDisable()
        {
            if (OnTargetDeSelected != null)
            {
                OnTargetDeSelected();
            }
        }
        public void Selected()
        {
            if(enabled && OnTargetSelected != null)
            {
                OnTargetSelected(this.gameObject);
            }
        }

        public void DeSelected()
        {
            if (enabled && OnTargetDeSelected != null)
            {
                OnTargetDeSelected();
            }
        }
    }
}

