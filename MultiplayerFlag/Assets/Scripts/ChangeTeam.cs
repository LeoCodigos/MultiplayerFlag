using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class ChangeTeam : NetworkBehaviour
{
    [SerializeField]private Material red,blue;
    [SerializeField]private Button buttonBlue,buttonRed;
    // Start is called before the first frame update

    void ChangeRedTeam(){
        if(IsOwner==true){
            this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material=red;
            this.gameObject.layer=LayerMask.NameToLayer("Red");
        }
    }
    void ChangeBlueTeam(){
         if(IsOwner==true){
            this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material=blue;
            this.gameObject.layer=LayerMask.NameToLayer("Blue");
        }
    }

    override public void OnNetworkSpawn(){
        base.OnNetworkSpawn();

        buttonBlue= GameObject.Find("ButtonBlue").GetComponent<Button>();
        buttonRed= GameObject.Find("ButtonRed").GetComponent<Button>();
        buttonBlue.onClick.AddListener(()=>{
            ChangeBlueTeam();
        });
         buttonRed.onClick.AddListener(()=>{
            ChangeRedTeam();
        });
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
