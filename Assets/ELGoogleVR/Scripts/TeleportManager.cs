using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public TeleportPosition[] teleportPositions;
    public Transform target;
    public FadeManager fadeManager;
    public TeleportPosition lastTeleportPosition;
    public bool freeTeleportation;
    public float FadeTime { get { return fadeManager.fadeTime; } }

    private int currentTelePos;

    private void Start()
    {
        currentTelePos = -1;
        ShowTeleportPositions(false);

        if(!freeTeleportation)
        {
            foreach (TeleportPosition telePos in teleportPositions)
            {
                telePos.Show(false);
            }
        }
    }

    public void TeleportTo(TeleportPosition telePos)
    {
        lastTeleportPosition = telePos;
        StartCoroutine(Teleporting(new Vector3(telePos.Position.x, target.transform.position.y, telePos.Position.z)));
        ShowTeleportPositions(true);
    }

    public void FocusAt(TeleportPosition focusPos)
    {
        StartCoroutine(Teleporting(new Vector3(focusPos.Position.x, target.transform.position.y, focusPos.Position.z)));
    }

    public void UnFocus()
    {
        StartCoroutine(Teleporting(new Vector3(lastTeleportPosition.Position.x, target.transform.position.y, lastTeleportPosition.Position.z)));
        lastTeleportPosition.Show(false);
    }

    public void ShowNextTeleportPosition()
    {
        currentTelePos++;

        for (int i = 0; i < teleportPositions.Length; i++)
        {
            teleportPositions[i].Show(false);
        }

        if (currentTelePos >= 0 && teleportPositions.Length > currentTelePos)
            teleportPositions[currentTelePos].Show(true);
    }

    public void ShowTeleportPositions(bool show)
    {
        if(freeTeleportation)
        {
            foreach (TeleportPosition telePos in teleportPositions)
            {
                telePos.Show(show);
            }

            if (lastTeleportPosition != null)
                lastTeleportPosition.Show(false);
        }
        else
        {
            if(currentTelePos >= 0 && teleportPositions.Length > currentTelePos)
                teleportPositions[currentTelePos].Show(show);

            if (lastTeleportPosition != null)
                lastTeleportPosition.Show(false);
        }
        
    }

    IEnumerator Teleporting(Vector3 position)
    {
        fadeManager.FadeToBlack();
        yield return new WaitForSeconds(fadeManager.fadeTime);

        target.position = position;

        fadeManager.FadeFromBlack();
    }
}
