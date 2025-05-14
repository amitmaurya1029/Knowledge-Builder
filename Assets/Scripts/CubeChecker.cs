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
        Collider[] hits = Physics.OverlapBox(transform.position + boxCenter, boxSize, Quaternion.identity, detectionLayer);
        foreach (var hit in hits)
        {
            if (hit.gameObject.tag == "CubeBlock" && !cubes.Contains(hit.gameObject))
            {
                AddingCube(hit.gameObject);   
                OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this) {canUpdateUi = true, Cubes = this.cubes,
                     maxCubeCount = this.maxCubeCount});
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (CubeBlockCounter == 0) {return;}

        else
        {
             Debug.Log(" total cube length BEFORE : " + cubes.Count);
            for (int i = 0; i < cubes.Count; i++)
            {
                Debug.Log(" total cube length : " + cubes.Count);
                if (cubes[i] == collision.gameObject)
                {
                    cubes.Remove(collision.gameObject);
                    CubeBlockCounter--;
                    OnCubeAdded?.Invoke(this, new CubeCheckerEventArgs(this) {canUpdateUi = true, Cubes = this.cubes,
                     maxCubeCount = this.maxCubeCount});
                     return;

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
            FindObjectOfType<LevelManager>().NextLevel();
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
    public int maxCubeCount;
    public CubeChecker cubeChecker {get;}
    public bool canUpdateUi = true;
    public List<GameObject> Cubes = new List<GameObject>();
    public CubeCheckerEventArgs(CubeChecker cubeChecker)
    {
        this.cubeChecker = cubeChecker;
    } 

}

