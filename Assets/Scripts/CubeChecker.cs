using System;
using System.Collections.Generic;
using UnityEngine;

public class CubeChecker : MonoBehaviour
{
    public static event EventHandler<CubeCheckerEventArgs> OnCubeAdded;
    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = new Vector3(3.37f, 0.19f, 1.68f);
    
    public LayerMask detectionLayer;

    private int CubeBlockCounter = 0;
    private int maxCubeCount = 5;
    private bool canAddCube = true;

    private List<GameObject> cubes = new List<GameObject>();


    private void OnCollisionEnter(Collision collision)
    {
          Debug.Log("cube added to table : entered");
        if (CubeBlockCounter == maxCubeCount  && !canAddCube) {return;}
         Debug.Log("cube added to table : entered : 1");
        Collider[] hits = Physics.OverlapBox(transform.position + boxCenter, boxSize, Quaternion.identity, detectionLayer);
        Debug.Log("cube added to table : entered : 2 : length hit" + hits.Length);
        foreach (var hit in hits)
        {
            Debug.Log("cube added to table : entered : 3");
            if (hit.gameObject.tag == "CubeBlock" && !cubes.Contains(hit.gameObject))
            {
                AddingCube(hit.gameObject);   
                OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this) {canUpdateUi = true});
                Debug.Log("cube added to table : ");
            }
        }
        Debug.Log("cube added to table : entered : 4");
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CubeBlockCounter == 0) {return;}

        else
        {

            for (int i = 0; i <= cubes.Count; i++)
            {
                Debug.Log(" total cube length : 1" + cubes.Count);
                if (cubes[i] == collision.gameObject)
                {
                    Debug.Log(" total cube length : 2");
                    //Debug.Log("Remove object : " + cubes[i].name + "cubes count :" + cubes.Count);
                    cubes.Remove(collision.gameObject);
                    Debug.Log(" total cube length : 3");
                    CubeBlockCounter--;
                    Debug.Log(" total cube length : 4");
                    // Debug.Log("Remove object : " + cubes[i].name + "cubes count :" + cubes.Count);
                    OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this) {canUpdateUi = true});
                    Debug.Log(" total cube length : 5");

                }

            }
        }
       
    }


    private void AddingCube(GameObject cube)
    {
         cubes.Add(cube);
         CubeBlockCounter++;
         Debug.Log(" Total numbers of cubes :" + CubeBlockCounter );
         if (CubeBlockCounter == maxCubeCount)
        {
            canAddCube = false;
        }
      
    }


    
    public int GetCubeCount()
    {
        return CubeBlockCounter;
    }

    public int GetMaxCubeCount()
    {
        return maxCubeCount;
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

