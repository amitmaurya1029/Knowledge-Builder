using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    
    void Start()
    {
        OnConnectedToServer();
    }

    private void OnConnectedToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 5;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        PhotonNetwork.JoinOrCreateRoom("RoomNum1",roomOptions, TypedLobby.Default);
        Debug.Log("we connected with server :");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("We joined the room");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("new player has enterd the room : " + newPlayer.NickName);
    }
}
