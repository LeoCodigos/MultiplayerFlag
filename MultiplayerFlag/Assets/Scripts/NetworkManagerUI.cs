using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkManagerUI : MonoBehaviour
{
    #region Atributos

    [SerializeField] private Button buttonServer,buttonHost,buttonClient;

    #endregion
 
    void Awake()
    {
        buttonHost.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
        });

        buttonClient.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartClient();
        });

        buttonServer.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartServer();
        });
    }
}
