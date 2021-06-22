using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviourPunCallbacks
{
    const int MAX_PLAYERS = 2;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");

        RoomOptions _roomOptions = new RoomOptions();
        _roomOptions.IsVisible = false;
        _roomOptions.MaxPlayers = MAX_PLAYERS;


        PhotonNetwork.JoinOrCreateRoom("My room", _roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"OnJoinedRoom:  player = {PhotonNetwork.LocalPlayer.ActorNumber}");
    }

    public override void OnPlayerEnteredRoom(Player _newPlayer)
    {
        Debug.Log($"OnPlayerEnteredRoom:  newPlayer = {_newPlayer.ActorNumber}");
    }
}
