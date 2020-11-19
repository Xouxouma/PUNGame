using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAttack : MonoBehaviourPun
{
    public Transform spellSpawn;
    //public GameObject[] spells;
    public ProjectileBehaviour currentSpell;

    float spellVelocity = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
            return;


        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("Cast spell Air : ");
            //photonView.RPC("RPC_LoadSpell", RpcTarget.All, 0);
            LoadSpell((int)Elems.Air);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Cast spell Water : ");
            //photonView.RPC("RPC_LoadSpell", RpcTarget.All, 1);
            LoadSpell((int)Elems.Water);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Cast spell Fire : ");
            //photonView.RPC("RPC_LoadSpell", RpcTarget.All, 2);
            LoadSpell((int)Elems.Fire);
        }

        if (Input.GetButton("Fire1") && currentSpell != null)
        {
            //photonView.RPC("RPC_ThrowSpell", RpcTarget.All);
            ThrowSpell();
        }

    }

    //[PunRPC]
    //void RPC_LoadSpell(int _elemIndex)
    //{
    //    if (currentSpell != null)
    //        return;

    //    string spellString = ElemsHandler.ElemIndexToString(_elemIndex);
    //    Debug.Log("LoadSpell : " + spellString);

    //    currentSpell = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Spells", spellString + "ball"), spellSpawn.transform.position, spellSpawn.transform.rotation).GetComponent<ProjectileBehaviour>();
    //    currentSpell.transform.SetParent(spellSpawn);
    //}


    //[PunRPC]
    //void RPC_ThrowSpell()
    //{
    //    currentSpell.Throw(spellVelocity, transform.forward);
    //    currentSpell.transform.parent = null;
    //    currentSpell = null;
    //}

    void LoadSpell(int _elemIndex)
    {
        if (currentSpell != null)
            return;

        string spellString = ElemsHandler.ElemIndexToString(_elemIndex);
        Debug.Log("LoadSpell : " + spellString);

        currentSpell = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Spells", spellString + "ball"), spellSpawn.transform.position, spellSpawn.transform.rotation).GetComponent<ProjectileBehaviour>();
        currentSpell.transform.SetParent(spellSpawn);
    }

    void ThrowSpell()
    {
        currentSpell.Throw(spellVelocity, transform.forward);
        currentSpell.transform.parent = null;
        currentSpell = null;
    }
}
