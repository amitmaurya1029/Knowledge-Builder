using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class NetworkPlayer : MonoBehaviour
{
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    void Start()
    {
        
    }
    void Update()
    {
        SetLocation(leftHand, XRNode.LeftHand);
        SetLocation(rightHand, XRNode.RightHand);
    }


    private void SetLocation(Transform obj ,XRNode node)
    {

        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceRotation, out Quaternion rotation);
        obj.position = position;
        obj.rotation = rotation;

        Debug.Log("current controller position : " + leftHand.position);

    }
}
