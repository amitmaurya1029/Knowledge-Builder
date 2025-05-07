using UnityEngine;

public class BlockChecker : MonoBehaviour
{
    public Vector3 boxCenter = Vector3.zero;
    public Vector3 boxSize = new Vector3(2.34f, 0.08f, 1f);
    public LayerMask detectionLayer;

    void Update()
    {
        Collider[] hits = Physics.OverlapBox(transform.position + boxCenter, boxSize, Quaternion.identity, detectionLayer);

        foreach (var hit in hits)
        {
            Debug.Log("Detected object: " + hit.name);
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + boxCenter, boxSize);
    }
}
