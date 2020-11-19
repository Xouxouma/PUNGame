using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarSetup : MonoBehaviourPun
{

    public int characterValue;
    public GameObject myCharacter;

    public GameObject othersCanvas;

    public GameObject cam;
    public GameObject myCanvas;
    public Healthbar myHealthbar;

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("RPC_AddCharacter", RpcTarget.AllBuffered, PlayerInfo.PI.mySelectedCharacter);
        }
    }

    [PunRPC]
    void RPC_AddCharacter(int whichCharacter)
    {
        characterValue = whichCharacter;
        myCharacter = Instantiate(PlayerInfo.PI.allCharacters[whichCharacter], transform.position, transform.rotation, transform);
        if (photonView.IsMine)
        {
            // Activate my personal components
            cam.SetActive(true);
            myCanvas.SetActive(true);
            // Change the healthbar
            GetComponent<PlayerHealth>().healthbar = myHealthbar;

            // Desactivate infos for others only
            othersCanvas.SetActive(false);
        }
        else
        {
            // Activate infos for others only
            cam.SetActive(false);
            othersCanvas.SetActive(true);

            // Desactivate personal components
            myCanvas.SetActive(false);
        }

        // Set healthbar UI to max because bug sometimes, idk why
        //GetComponent<PlayerHealth>().healthbar.SetHealth(myHealthbar.maximumHealth);
    }
}
