using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flag : NetworkBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private bool ocupado = false;

    [Rpc(SendTo.Server)]
    public void ReturnFlag_ServerRpc()
    {
        ocupado = false;
        this.transform.parent = null;
        this.transform.position = spawnPoint.transform.position;
        this.transform.rotation = Quaternion.identity;
    }

    [Rpc(SendTo.Server)]
    public void Catch_ServerRpc(GameObject other){
        if(ocupado == false)
        {
            ocupado = true;
            //this.transform.parent = other.transform;
            this.NetworkObject.TrySetParent(other.gameObject,false);         
            this.transform.position = other.GetComponent<CharacterMovement>().GetBack().position - new Vector3(0.1f, 0.8f, 0);
            this.transform.rotation = Quaternion.identity;//other.GetComponent<Player>().GetHand().rotation ;
            Debug.Log("Peguei");
        }
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Player") && other.gameObject.layer == this.gameObject.layer)
        {
            ReturnFlag_ServerRpc();
            ocupado = false;
        }else if(other.gameObject.CompareTag("Player") && other.gameObject.layer != this.gameObject.layer)
        {
            Catch_ServerRpc(other.gameObject);
        }
        else if(other.gameObject.CompareTag("Base") && other.gameObject.layer != this.gameObject.layer)
        {
            ReturnFlag_ServerRpc();
        }
    } 
}
