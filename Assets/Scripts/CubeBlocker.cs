using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlocker : MonoBehaviour
{

    private List<XRGrabInteractableOnNetwork> XrGrabIntrt = new List<XRGrabInteractableOnNetwork>();
    private bool isXrGrabCompCashed = false;
    private bool isXrGrabCompDisabled = false;
    
    void Start()
    {
        CubeChecker.OnCubeAdded += SetCubesToUnGrabable;
    }

    private void SetCubesToUnGrabable(object sender, CubeCheckerEventArgs e)
    {
        
        if (e.maxCubeCount == e.Cubes.Count)
        {
            if (!isXrGrabCompCashed)
            {
                GetRefreceOfCubeComp(e);
            }
            if (!isXrGrabCompDisabled)
            {
                CubesInteractableState(false);
            }
            
        }
        else
        {
            if (isXrGrabCompDisabled)
            {
                CubesInteractableState(true);
            }
        }
    }

    private void CubesInteractableState( bool state)
    {
        for (int i = 0; i < XrGrabIntrt.Count; i++)
        {
            XrGrabIntrt[i].enabled = state;

        }
        isXrGrabCompDisabled = !state;
        Debug.Log("All cubes gets ungrabable :  dissabled ");
    }


    private void GetRefreceOfCubeComp(CubeCheckerEventArgs e)
    {
        foreach(GameObject cube in e.Cubes)
        {
             XrGrabIntrt.Add(cube.GetComponent<XRGrabInteractableOnNetwork>());
        }
        isXrGrabCompCashed = true;

    }
}
