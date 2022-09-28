using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Button createRoom;
    [SerializeField] private TMP_InputField roomNameInput;
    [SerializeField] private Button joinRoom;
    [SerializeField] private Button joinRandomRoom;

    private void OnEnable()
    {
        createRoom.onClick.AddListener(() => NetworkManager.Instance.CreateNewRoom(roomNameInput.text));
        joinRoom.onClick.AddListener(() => NetworkManager.Instance.JoinToRoom(roomNameInput.text));
        joinRandomRoom.onClick.AddListener(() => NetworkManager.Instance.JoinToRandomRoom());
    }

    private void OnDisable()
    {
        createRoom.onClick.RemoveListener(() => NetworkManager.Instance.CreateNewRoom(roomNameInput.text));
        joinRoom.onClick.RemoveListener(() => NetworkManager.Instance.JoinToRoom(roomNameInput.text));
        joinRandomRoom.onClick.RemoveListener(() => NetworkManager.Instance.JoinToRandomRoom());
    }
}
