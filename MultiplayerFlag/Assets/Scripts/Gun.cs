using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    #region Atributes

    [Header("Reference")]
    [SerializeField] private GunData gunData;
    private NetworkVariable<int> ammunition= new NetworkVariable<int>(10,NetworkVariableReadPermission.Owner,NetworkVariableWritePermission.Owner);
    [SerializeField]private NetworkObject bullet;

    private Transform spawn;

    public PlayerInputActions input;
    
    #endregion


    public void Fire(){
        
        
        var instance=Instantiate(bullet,spawn.position,Quaternion.identity);
        var instanceNO=instance.GetComponent<NetworkObject>();
        instanceNO.Spawn();
        //bullet.Spawn();
    }

    public void Reload(){


    }

    void Start()
    {
        spawn=this.transform;
    }

}
