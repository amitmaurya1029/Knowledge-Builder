using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabInteractableOnNetwork : XRGrabInteractable
{

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();    
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonView.RequestOwnership();
    }

    
}
