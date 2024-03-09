using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{

    #region Atributos
    [SerializeField] private GameObject flag;

    #endregion
    
    void ReturnFlag()
    {
        flag.transform.parent=null;
        flag.transform.position= this.transform.position;
        flag.transform.rotation=Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            ReturnFlag();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
