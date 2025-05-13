using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    private Transform leftController;
    private Transform rightController;

    private Transform head;
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        leftController = GameObject.FindWithTag("LeftController").transform;
        rightController = GameObject.FindWithTag("RightController").transform;
        head = Camera.main.transform;;
    }
    void Update()
    {
       

        if (photonView.IsMine)
        {
            leftHand.position = leftController.position;
            leftHand.rotation = leftController.rotation;

            rightHand.position = rightController.position;
            rightHand.rotation = rightController.rotation;

            Debug.Log("Left controller : " + leftController.position);
            Debug.Log("Right controller : " + leftController.position);
        }
        

      
    }


}
