using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviourPun
{
    public int maxHp = 100;
    public int hp;

    public Healthbar healthbar;

    float respawnTime = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    void RPC_TakeDamages(int damages)
    {
        hp -= damages;
        healthbar.SetHealth(hp);

        if (photonView.IsMine && hp <= 0)
        {
            hp = 0;
            Die();
        }


    }

    public bool TakeDamages(int damages)
    {
        photonView.RPC("RPC_TakeDamages", RpcTarget.All, damages);
        return true;
    }

    void Die()
    {
        Debug.Log("Player dead");
        //StartCoroutine(RespawnCoroutine());
        photonView.RPC("RPC_Respawn", RpcTarget.All);
        hp = maxHp;
    }

    [PunRPC]
    void RPC_Respawn()
    {
        StartCoroutine(RespawnCoroutine());
    }

    public IEnumerator RespawnCoroutine()
    {
        Debug.Log("RespawnCoroutine before desactivate");
        // Desactivate player while he's dead
        if (photonView.IsMine)
        {
            gameObject.transform.Rotate(new Vector3(1f, 0f, 0f), 90f);
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<PlayerMove>().enabled = false;
        }

        Debug.Log("RespawnCoroutine before yield");
        // Wait for respawnTime
        yield return new WaitForSeconds(respawnTime);
        Debug.Log("RespawnCoroutine after yield");

        // Replace the player if it's him who is dead
        if (photonView.IsMine)
        {
            Debug.Log("Respawn my player");
            // Respawn
            Transform spawn = PhotonPlayer.GetRandomSpawn();
            gameObject.transform.position = spawn.position;
            gameObject.transform.rotation = spawn.rotation;
            // Activate back the player
            GetComponent<PlayerAttack>().enabled = true;
            GetComponent<PlayerMove>().enabled = true;
        }

        // Sethealth back
        healthbar.SetHealth(maxHp);
    }


}
