using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Button createRoom;
    [SerializeField] private Button joinRoom;
    [SerializeField] private Button joinRandomRoom;
    [SerializeField] private Button startGame;
    [SerializeField] private Button leaveRoom;
    [SerializeField] private TMP_Text textInfo;
    [SerializeField] private TMP_InputField roomNameInput;

    private void Awake() => startGame.interactable = false;

    private void OnEnable()
    {
        createRoom.onClick.AddListener(CreateRoom);
        leaveRoom.onClick.AddListener(() => NetworkManager.Instance.LeaveFromRoom());
        startGame.onClick.AddListener(() => NetworkManager.Instance.StartGame());
        joinRoom.onClick.AddListener(JoinToRoom);
        joinRandomRoom.onClick.AddListener(() => NetworkManager.Instance.JoinToRandomRoom());
        NetworkManager.ShowInfo += ShowRoomInfo;
        NetworkManager.StartingGame += StartGame;
    }

    private void JoinToRoom()
    {
        string roomName = GetRoomName();
        if(roomName.Equals(String.Empty))
            return;
            
        NetworkManager.Instance.JoinToRoom(roomName);
    }
    
    private void CreateRoom()
    {
        string roomName = GetRoomName();
        if(roomName.Equals(String.Empty))
            return;
            
        NetworkManager.Instance.CreateNewRoom(roomName);
    }

    private void OnDisable()
    {
        NetworkManager.ShowInfo -= ShowRoomInfo;
        NetworkManager.StartingGame -= StartGame;
        startGame.onClick.RemoveListener(() => NetworkManager.Instance.StartGame());
        createRoom.onClick.RemoveListener(CreateRoom);
        joinRoom.onClick.RemoveListener(JoinToRoom);
        joinRandomRoom.onClick.RemoveListener(() => NetworkManager.Instance.JoinToRandomRoom());
    }

    private string GetRoomName()
    {
        string roomName = roomNameInput.text.Trim();
        if (roomName.Equals(String.Empty))
        {
            ShowRoomInfo("Room name is empty");
            return String.Empty;
        }

        return roomName;
    }
    private async void ShowRoomInfo(string msg)
    {
        textInfo.text = msg;
        await Task.Delay(3000);
        textInfo.text = String.Empty;
    }

    private void StartGame(int playerCount) => startGame.interactable = playerCount >= 2 ? true : false;

}
