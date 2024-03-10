using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField]private GameObject spawnPoint;
    public void ReturnFlag()
    {
        this.transform.parent=null;
        this.transform.position= spawnPoint.transform.position;
        this.transform.rotation=Quaternion.identity;
    }

    public void PickUpFlag(){
        this.transform.parent=null;
    }

    private void OnTriggerEnter(Collider other){
        
        if(other.gameObject.CompareTag("Player")&& other.gameObject.layer==this.gameObject.layer){
            ReturnFlag();
        }
        else if(other.gameObject.CompareTag("Player")&& other.gameObject.layer!=this.gameObject.layer){
            //this.transform.parent=other.GetComponent<Player>().Hand;
            this.transform.parent=other.transform;
            //PickUpFlag();
            Debug.Log("Peguei");
        }

    }
 
}
