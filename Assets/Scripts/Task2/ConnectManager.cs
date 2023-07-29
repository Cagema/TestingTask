using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectManager : MonoBehaviourPunCallbacks
{
    [SerializeField] string _roomName = "test";
    public void StartGame()
    {
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = 10;
        PhotonNetwork.JoinOrCreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Room");
    }
}
