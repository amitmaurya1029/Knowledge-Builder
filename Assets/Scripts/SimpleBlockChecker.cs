using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SimpleBlockChecker : MonoBehaviour
{
    [Header("XR Sockets to Check")]
    public UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor[] sockets;

    private bool isLocked = false;

    void Update()
    {
        if (isLocked) return;

        int filledCount = 0;

        foreach (var socket in sockets)
        {
            if (socket != null && socket.isSelectActive && socket.firstInteractableSelected != null)
            {
                filledCount++;
            }
        }

        if (filledCount == sockets.Length)
        {
            LockAllBlocks();
        }
    }

    void LockAllBlocks()
    {
        isLocked = true;

        foreach (var socket in sockets)
        {
            var interactable = socket.firstInteractableSelected as XRGrabInteractable;

            if (interactable != null)
            {
                interactable.enabled = false;

                var rb = interactable.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                    rb.useGravity = false;
                }
            }
        }
        FindObjectOfType<LevelManager>().NextLevel();
        SoundManager.Instance.PlayMusic();
    }
}
