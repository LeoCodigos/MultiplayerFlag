using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class CaptureController : MonoBehaviour
{
    #region Atributos
    private NetworkVariable<int> redPoints=new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    private NetworkVariable<int> bluePoints= new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Server);
    [SerializeField]private GameObject blueBase,redBase;
    [SerializeField]private Flag blueFlag,redFlag;
    [SerializeField]private TMP_InputField redScore,blueScore;
    public static CaptureController instance;

    #endregion

    public void BlueFlagCapture(){
        //redPoints++;
      
            redPoints.Value++;
            RedScoreUpdate();
            blueFlag.ReturnFlag_ServerRpc();
       
    }
    public void RedFlagCapture(){
        //bluePoints++;
            bluePoints.Value++;
            BlueScoreUpdate();
            redFlag.ReturnFlag_ServerRpc();
        
    }

    void RedScoreUpdate(){
        redScore.text=redPoints.Value.ToString();
    }
     void BlueScoreUpdate(){
        blueScore.text=bluePoints.Value.ToString();
    }

    void Start()
    {
        if(instance==null){
            instance=this;
        }
    }

   
}
