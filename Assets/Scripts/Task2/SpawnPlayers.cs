using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject Player;

    private void Start()
    {
        Vector3 randomPos = Random.insideUnitCircle * 3f;
        randomPos = new(randomPos.x, 0, randomPos.y);

        PhotonNetwork.Instantiate(Player.name, randomPos, Quaternion.identity);
    }
}
