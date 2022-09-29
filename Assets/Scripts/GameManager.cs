using Photon;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : PunBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start() => CreatePlayer();

    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-5f, 5f));
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity, 0);
    }
    
    
}
