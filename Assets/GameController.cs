using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviourPunCallbacks
{
    const int MAX_PLAYERS = 2;
    const string PLAYER_YEL_PREPHAB_NAME = "PlayerTankFree_Yel";  
    const string PLAYER_BLUE_PREPHAB_NAME = "PlayerTankFree_Blue";

    [SerializeField]
    Transform firstPointTr;
    [SerializeField]
    Transform secondPointTr;

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

        Transform _targetTr = PhotonNetwork.LocalPlayer.ActorNumber == 1 ?
            firstPointTr : 
            secondPointTr;

        string _playerPrephabName = PhotonNetwork.LocalPlayer.ActorNumber == 1 ?
            PLAYER_YEL_PREPHAB_NAME :
            PLAYER_BLUE_PREPHAB_NAME;

        PhotonNetwork.Instantiate(_playerPrephabName, _targetTr.position, _targetTr.rotation);
    }

    public override void OnPlayerEnteredRoom(Player _newPlayer)
    {
        Debug.Log($"OnPlayerEnteredRoom:  newPlayer = {_newPlayer.ActorNumber}");
    }
}
