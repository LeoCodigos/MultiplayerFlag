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
        
    }
    void ChangeBlueTeam(){

    }

    void Awake(){
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
