using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviourPun
{
    public GameObject impactEffect;

    protected int damages = 10;
    //protected float velocity = 0.0f;
    protected bool isLaunched = false;

    public Photon.Pun.PhotonView invokerPhotonView = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Throw(float vel, Vector3 direction)
    {
        Debug.Log("Throw projectile at vel " + vel + " direction : " + direction);
        //transform.LookAt(direction);
        GetComponent<Rigidbody>().velocity = direction.normalized * vel;
        //GetComponent<Rigidbody>().AddForce(direction.normalized * vel, ForceMode.VelocityChange);
        //velocity = vel;
        //Destroy(gameObject, 5.0f);
        isLaunched = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (photonView.AmOwner)
        //{
        //    Debug.Log("projectile velocity = " + velocity);
        //    transform.position += transform.forward.normalized * velocity * Time.deltaTime;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            Debug.Log("Trigger projectile frm another player");
            return;
        }

        Debug.Log("My projectile is triggered");
        if (isLaunched)
        {
            Debug.Log("Projectile hits : " + other.transform.name + " | tag : " + other.transform.tag);
            if (other.CompareTag("Player"))
            {
                Debug.Log("Projectile hits a Player");
                if (other.gameObject.GetComponentInChildren<PlayerHealth>().TakeDamages(damages))
                    photonView.RPC("RPC_Explode", RpcTarget.All);
            }

            else if (other.tag == "Projectile")
            {
                Debug.Log("Projectile hits projectile");
                photonView.RPC("RPC_Explode", RpcTarget.All);
            }

            else// if (other.tag == "Terrain")
            {
                Debug.Log("Projectile hits Terrain");
                photonView.RPC("RPC_Explode", RpcTarget.All);
            }
        }
        else Debug.Log("Projectile not launched.");
    }

    //void Explode()
    //{
    //    if (impactEffect != null)
    //    {
    //        Debug.Log("Projectile explodes");
    //        //GameObject _impactEffect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Spells", spellString + "Explosion"), spellSpawn.transform.position, spellSpawn.transform.rotation).GetComponent<ProjectileBehaviour>();
    //        GameObject _impactEffect = Instantiate(impactEffect, transform.position, Quaternion.LookRotation(transform.forward));
    //        GameObject.Destroy(_impactEffect, _impactEffect.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
    //    }
    //    else Debug.Log("Projectile dissapear silently");
    //    Destroy(gameObject);
    //}

    [PunRPC]
    void RPC_Explode()
    {
        if (impactEffect != null)
        {
            Debug.Log("Projectile explodes");
            //GameObject _impactEffect = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Spells", spellString + "Explosion"), spellSpawn.transform.position, spellSpawn.transform.rotation).GetComponent<ProjectileBehaviour>();
            GameObject _impactEffect = Instantiate(impactEffect, transform.position, Quaternion.LookRotation(transform.forward));
            GameObject.Destroy(_impactEffect, _impactEffect.GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        }
        else Debug.Log("Projectile dissapear silently");
        Destroy(gameObject);
    }
}
