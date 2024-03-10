using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
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

    public void PickUpFlag(){
        this.transform.parent=null;
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Player") && other.gameObject.layer == this.gameObject.layer){
            ReturnFlag();
            ocupado = false;
        }else if(other.gameObject.CompareTag("Player") && other.gameObject.layer != this.gameObject.layer){
            if(ocupado == false){
                ocupado = true;
                this.transform.parent = other.GetComponent<Player>().GetHand();         
                this.transform.position = other.GetComponent<Player>().GetHand().position - new Vector3(0.1f, 0.8f, 0);
                this.transform.rotation = Quaternion.identity;//other.GetComponent<Player>().GetHand().rotation ;
                Debug.Log("Peguei");
            }            
        }
    } 
}
