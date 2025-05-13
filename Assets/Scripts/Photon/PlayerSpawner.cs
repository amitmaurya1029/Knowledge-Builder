using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{

    private GameObject spawnPlayer; 
    
    void Start()
    {
        // Debug.Log("")
    }

    public override void OnJoinedRoom()
    {
        int offsetX = Random.Range(1,6);
        int offsetZ = Random.Range(1,4);
        GameObject spawnPlayer = PhotonNetwork.Instantiate("Player", new Vector3(offsetX, 0, offsetZ), Quaternion.identity);

        Debug.Log("Player get spawned : ");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.Destroy(spawnPlayer);

    }

    
}
