using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class GameManager : NetworkBehaviour
{

    public static GameManager instance;

    [SerializeField]private TMP_InputField redScore,blueScore;
    [SerializeField]private NetworkVariable<int>redPoints= new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    [SerializeField]private NetworkVariable<int>bluePoints= new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);


    public NetworkVariable<int>GetRedPoints{
        get=>redPoints;
    }

    public void SetRedPoints(){
        
        redPoints.Value++; 
    }

    public NetworkVariable<int>GetBluePoints{
        get=>bluePoints;
    }
     public void SetBluePoints(){
        
        bluePoints.Value++;
    }

    void BluePointUpdate(){
        blueScore.text=bluePoints.Value.ToString();
    }
    void RedPointUpdate(){
        redScore.text=redPoints.Value.ToString();
    }

    public override void OnNetworkSpawn()
    {
        bluePoints.OnValueChanged+=(int pastValue,int newValue)=>{
            BluePointUpdate();
            Debug.Log("Passei");
        };
         redPoints.OnValueChanged+=(int pastValue,int newValue)=>{
            RedPointUpdate();
            Debug.Log("Passei");
        };
    }
    void Start()
    {
        if(instance==null)instance=this;
        else Destroy(gameObject);
    }

  

}
