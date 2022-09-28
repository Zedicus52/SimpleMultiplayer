using Photon;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : PunBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Material otherPlayerMaterial;

    private void Start()
    {
        CreatePlayer();
    }

    private void CreatePlayer()
    {
        Vector3 pos = new Vector3(Random.Range(-5f, 5f), 2, Random.Range(-5f, 5f));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity, 0);
    }
    
    
}
