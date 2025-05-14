using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBlocker : MonoBehaviour
{
    void Start()
    {
        CubeChecker.OnCubeAdded += SetCubesToUnGrabable;
    }

    private void SetCubesToUnGrabable(object sender, CubeCheckerEventArgs e)
    {
        
        if (e.maxCubeCount == e.Cubes.Count)
        {
            foreach (GameObject cube in e.Cubes)
            {
                cube.GetComponent<XRGrabInteractableOnNetwork>().enabled = false;    
            }
            Debug.Log("All cubes gets ungrabable : " + e.Cubes.Count);
        }
    }



}
