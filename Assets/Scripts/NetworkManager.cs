using System;
using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NetworkManager : PunBehaviour
{
    public static event Action<string> ShowInfo;
    public static event Action<int> StartingGame;
    public static event Action<object> PlayerJoined;
    public static event Action<object> PlayerLeave;
    public static NetworkManager Instance { get; private set; }
    

    private void Awake()
    {
        PhotonNetwork.playerName = "Player " + Random.Range(1000, 9999);
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1");
        
        PlayerJoined?.Invoke(PhotonNetwork.player);


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }
    

    public void CreateNewRoom(string room) => PhotonNetwork.CreateRoom(room);

    public void JoinToRoom(string room) => PhotonNetwork.JoinRoom(room);

    public void JoinToRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveFromRoom()
    {
        if(PhotonNetwork.inRoom)
            PhotonNetwork.LeaveRoom();
    }

    public override void OnCreatedRoom() => ShowInfo?.Invoke("Room created!");

    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg) => 
        ShowInfo?.Invoke("Cannot create new room because name already use!");

    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg) =>
        ShowInfo?.Invoke("Cannot join to room because incorrect name or room does not exist!");

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg) =>  
        ShowInfo?.Invoke("Cannot join to random!");
    
    public override void OnConnectedToMaster() => 
        ShowInfo?.Invoke($"{PhotonNetwork.player.NickName} connected to master!");
    
    public override void OnJoinedRoom()
    {
        ShowInfo?.Invoke($"{PhotonNetwork.player.NickName} joined!");
        UpdatePlayersInRoom();
    }

    public override void OnLeftRoom() => SceneManager.LoadSceneAsync(0);

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        ShowInfo?.Invoke($"Player {newPlayer.NickName} entered to room!");
        PlayerJoined?.Invoke(newPlayer);
        StartingGame?.Invoke(PhotonNetwork.room.PlayerCount);
    }
    
    public void StartGame()
    {
        if(PhotonNetwork.player != null)
            if(PhotonNetwork.player.IsMasterClient)
                PhotonNetwork.LoadLevelAsync(1);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (PhotonNetwork.room.PlayerCount < 2)
            PhotonNetwork.LeaveRoom();
        PlayerLeave?.Invoke(otherPlayer);
        ShowInfo?.Invoke($"Player {otherPlayer.NickName} leave from room!");
    }

    private void UpdatePlayersInRoom()
    { 
        foreach (var player in PhotonNetwork.otherPlayers) 
        {
           PlayerJoined?.Invoke(player); 
        }
    }
 
       
    
}
