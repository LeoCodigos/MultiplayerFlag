using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class CaptureController : NetworkBehaviour
{
    #region Atributos
    private NetworkVariable<int> redPoints=new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    private NetworkVariable<int> bluePoints= new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    [SerializeField]private GameObject blueBase,redBase;
    [SerializeField]private Flag blueFlag,redFlag;
    [SerializeField]private TMP_InputField redScore,blueScore;
    public static CaptureController instance;

    #endregion

    public void BlueFlagCapture(){
        //redPoints++;
        if(IsOwner==true){
             redPoints.Value++;
            RedScoreUpdate();
            blueFlag.ReturnFlag();
        }
       
    }
    public void RedFlagCapture(){
        //bluePoints++;
        if(IsOwner){
            bluePoints.Value++;
            BlueScoreUpdate();
            redFlag.ReturnFlag();
        }
        
    }

    void RedScoreUpdate(){
        redScore.text=redPoints.Value.ToString();
    }
     void BlueScoreUpdate(){
        blueScore.text=bluePoints.Value.ToString();
    }

    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();
        redPoints.OnValueChanged+=(int pastValue, int newValue)=>{
             Debug.Log(OwnerClientId + "\tNumero Aleatorio: " + newValue);
        };
    }

    void Start()
    {
        if(instance==null){
            instance=this;
        }else Destroy(gameObject);
    }

   
}
