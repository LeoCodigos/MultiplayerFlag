using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    #region Atributes
    private NetworkVariable<int> ammunition= new NetworkVariable<int>(10,NetworkVariableReadPermission.Owner,NetworkVariableWritePermission.Owner);
    [SerializeField]private NetworkObject bullet;

    private Transform spawn;

    public PlayerInputActions input;
    
    #endregion


    public void Fire(){
        
        //bullet.Spawn();
    }

    void Start()
    {
        spawn=this.transform;
    }

}
