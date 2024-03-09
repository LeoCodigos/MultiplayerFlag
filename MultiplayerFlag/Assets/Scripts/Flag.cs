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

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            ReturnFlag();
        }
    }
    void Start(){
        this.gameObject.layer=4;
    }
 
}
