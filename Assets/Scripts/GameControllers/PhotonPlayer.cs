using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviourPun
{
    public GameObject myAvatar;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    public static Transform GetRandomSpawn()
    {
        int spawnPicker = Random.Range(0, GameSetup.GS.spawnPoints.Length);
        Transform spawn = GameSetup.GS.spawnPoints[spawnPicker];
        return spawn;
    }
    public void Spawn()
    {
        if (photonView.IsMine)
        {
            Debug.Log("Spawn");
            Transform spawn = GetRandomSpawn();
            myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawn.position, spawn.rotation, 0);
        }
    }

}
