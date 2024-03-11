using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Flag : NetworkBehaviour
{
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private bool ocupado = false;

    public void ReturnFlag()
    {
        ocupado = false;
        this.transform.parent=null;
        this.transform.position= spawnPoint.transform.position;
        this.transform.rotation=Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Player") && other.gameObject.layer == this.gameObject.layer){
            ReturnFlag();
            ocupado = false;
        }else if(other.gameObject.CompareTag("Player") && other.gameObject.layer != this.gameObject.layer){
            if(ocupado == false){
                ocupado = true;
                //this.transform.parent = other.transform;
                this.NetworkObject.TrySetParent(other.gameObject,false);         
                this.transform.position = other.GetComponent<Player>().GetHand().position - new Vector3(0.1f, 0.8f, 0);
                this.transform.rotation = Quaternion.identity;//other.GetComponent<Player>().GetHand().rotation ;
                Debug.Log("Peguei");
            }            
        }
        else if(other.gameObject.CompareTag("Base") && other.gameObject.layer != this.gameObject.layer){
            ReturnFlag();
        }
    } 
}
