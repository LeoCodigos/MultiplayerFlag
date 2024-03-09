using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaptureController : MonoBehaviour
{
    #region Atributos
    private int redPoints,bluePoints;
    [SerializeField]private GameObject blueBase,redBase;
    [SerializeField]private Flag blueFlag,redFlag;
    [SerializeField]private TMP_InputField redScore,blueScore;
    public static CaptureController instance;

    #endregion

    void BlueFlagCapture(){
        redPoints++;
        blueFlag.ReturnFlag();
    }
    void RedFlagCapture(){
        bluePoints++;
        redFlag.ReturnFlag();
    }

    void Start()
    {
        if(instance==null){
            instance=this;
        }else Destroy(gameObject);
    }

   
}
