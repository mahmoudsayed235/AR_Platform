using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace EnjoyLearning.VR.SDK
{
    public class CardboardButtonSimulator : MonoBehaviour
    {
        public VRModeManager vrModeManager;

        public MeshRenderer gvrMeshRenderer;

        public Image innerLoadingImage;
        public Image outerLoadingImage;

        [Range(0.5f, 30.0f)]
        public float buttonPressWait = 0.5f;

        private float startTime;
        private float elpasedTime;

        [SerializeField] private bool simulating;

        [SerializeField] private GameObject target;

        private void OnEnable()
        {
            CardboardButtonTarget.OnTargetSelected += OnTargetSelected;
            CardboardButtonTarget.OnTargetDeSelected += OnTargetDeSelected;
        }

        private void OnDisable()
        {
            CardboardButtonTarget.OnTargetSelected -= OnTargetSelected;
            CardboardButtonTarget.OnTargetDeSelected -= OnTargetDeSelected;
        }

        private void Update()
        {
            if (simulating)
            {
                // calculating elpased time since start simulating button click
                elpasedTime = Time.time - startTime;

                // if it is the time to click
                if (elpasedTime > buttonPressWait)
                {
                    // send click event to target
                    ExecuteEvents.Execute<IPointerClickHandler>(target, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);

                    //Debug.LogFormat("CardboardButtonSimulator -> Button pressed after: {0} sec", elpasedTime);

                    // no need to update images
                    return;
                }

                innerLoadingImage.fillAmount = elpasedTime / buttonPressWait;
                outerLoadingImage.fillAmount = elpasedTime / buttonPressWait;
            }
        }

        private void OnTargetSelected(GameObject gameObject)
        {
            // set the target
            target = gameObject;

            // make sure that if the target is ui, it is also interactable
            var uiTarget = target.GetComponent<Selectable>();
            if (uiTarget != null && !uiTarget.interactable)
            {
                //Debug.LogFormat("Target: {0} is not interactable", target.name);
                return;
            }

            // hide gvr rectile pointet in vr mode
            if (vrModeManager.vrMode)
            {
                //gvrMeshRenderer.enabled = false;
            }

            // start simulating
            simulating = true;

            // set the start time of simulating
            startTime = Time.time;
        }

        private void OnTargetDeSelected()
        {
            // reset loader canvas
            innerLoadingImage.fillAmount = 0.0f;
            outerLoadingImage.fillAmount = 0.0f;

            // stop simulating
            simulating = false;

            // reset target
            target = null;

            // show gvr rectile pointet in vr mode
            if (vrModeManager.vrMode && gvrMeshRenderer != null)
            {
                //gvrMeshRenderer.enabled = true;
            }
        }
    }
}