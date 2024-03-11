using UnityEngine;

public class Base : MonoBehaviour
{  

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Flag")&&other.gameObject.layer!=this.gameObject.layer){
            if(other.gameObject.layer==LayerMask.NameToLayer("Blue")){
                 GameManager.instance.SetRedPoints();
            }else GameManager.instance.SetBluePoints();
        }

    }
}
