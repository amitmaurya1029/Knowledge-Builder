using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeChecker : MonoBehaviour
{
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
            }
        }
        
    }


    private void AddingCube(GameObject cube)
    {
         cubes.Add(cube);
         CubeBlockCount++;
         Debug.Log(" Total numbers of cubes :" + CubeBlockCount );
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize);
    }
}
