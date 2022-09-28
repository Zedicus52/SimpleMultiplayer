using Photon;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class NetworkManager : PunBehaviour
{
    public static NetworkManager Instance { get; private set; }

    private void Awake()
    {
        PhotonNetwork.playerName = "Player " + Random.Range(1000, 9999);
        Debug.Log($"{PhotonNetwork.playerName}, created!");
        //Logger.Instance.Log($"{PhotonNetwork.playerName}, created!");
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1");
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void CreateNewRoom(string room)
    {
        PhotonNetwork.CreateRoom(room);
    }

    public void JoinToRoom(string room)
    {
        PhotonNetwork.JoinRoom(room);
    }

    public void JoinToRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void LeaveFromRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log(PhotonNetwork.playerName + " connected to master");
        //Logger.Instance.Log(PhotonNetwork.playerName + " connected to master");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"Player joined!");
    }
    
    
    public override void OnLeftRoom()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log($"Player {newPlayer.NickName} entered to room!");

        if (PhotonNetwork.room.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevelAsync(1);
        }
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        if (PhotonNetwork.room.PlayerCount < 2)
            PhotonNetwork.LeaveRoom();
        Debug.Log($"Player {otherPlayer.NickName} from to room!");
    }
}
