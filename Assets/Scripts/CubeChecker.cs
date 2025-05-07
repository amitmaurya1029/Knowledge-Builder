using System;
using System.Collections.Generic;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEngine;

public class CubeChecker : MonoBehaviour
{
    public static event EventHandler<CubeCheckerEventArgs> OnCubeAdded;
    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = new Vector3(2.34f, 0.08f, 1f);
    public LayerMask detectionLayer;

    private int CubeBlockCount = 0;
    
    
    private List<GameObject> cubes = new List<GameObject>();


    private void OnCollisionEnter(Collision collision)
    {
        Collider[] hits = Physics.OverlapBox(transform.position + boxCenter, boxSize, Quaternion.identity, detectionLayer);

        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "CubeBlock" && !cubes.Contains(hit.gameObject))
            {
                AddingCube(hit.gameObject);   
                OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this));
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        foreach (GameObject cube in cubes)
        {
            if (cube == collision.gameObject)
            {
                Debug.Log("Remove object : " + cube.name + "cubes count :" + cubes.Count);
                cubes.Remove(collision.gameObject);
                Debug.Log("Remove object : " + cube.name + "cubes count :" + cubes.Count);
                OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this));

            }
        }
    }


    private void AddingCube(GameObject cube)
    {
         cubes.Add(cube);
         CubeBlockCount++;
         Debug.Log(" Total numbers of cubes :" + CubeBlockCount );
    }
    
    public int GetCubeCount()
    {
        return CubeBlockCount;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize);
    }

     
}

public class CubeCheckerEventArgs : EventArgs
{
    public CubeChecker cubeChecker {get;}
    public bool canUpdateUi = true;
    public CubeCheckerEventArgs(CubeChecker cubeChecker)
    {
        this.cubeChecker = cubeChecker;
    } 

}

