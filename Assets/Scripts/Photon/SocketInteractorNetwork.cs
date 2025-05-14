using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class SocketInteractorNetwork : XRSocketInteractor
{
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();    
    }

    void Update()
    {
        
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        photonView.RequestOwnership();   
    }
}
