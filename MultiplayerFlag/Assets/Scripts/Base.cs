using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Base : MonoBehaviour
{  

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Flag")&&other.gameObject.layer!=this.gameObject.layer){
            if(other.gameObject.layer==LayerMask.NameToLayer("Blue")){
                CaptureController.instance.BlueFlagCapture();
            }else CaptureController.instance.RedFlagCapture();
        }

    }
}
