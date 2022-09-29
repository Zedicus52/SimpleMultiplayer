using TMPro;
using UnityEngine;

public class PlayersListController : MonoBehaviour ,IListController
{
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject prefab;

    private void OnEnable()
    {
        NetworkManager.PlayerJoined += AddElement;
        NetworkManager.PlayerLeave += RemoveElement;
    } 
    private void OnDisable()
    { 
        NetworkManager.PlayerJoined -= AddElement;
        NetworkManager.PlayerLeave -= RemoveElement;
    } 
    
    public void AddElement(object element)
    {
        if (element is PhotonPlayer player)
        {
            var roomInfo = Instantiate(prefab, parent, false);
            if (roomInfo.TryGetComponent(out TMP_Text text))
            {
                text.text = player.NickName;
            }
            else
            {
                Destroy(roomInfo.gameObject);
            }
        }
    }

    public void RemoveElement(object element)
    {
        if (element is PhotonPlayer player)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                if (parent.GetChild(i).TryGetComponent(out TMP_Text text))
                {
                    if (text.text.Equals(player.NickName))
                    {
                        Destroy(parent.GetChild(0).gameObject);
                        return;
                    }
                }
            }
        }
    }
}
