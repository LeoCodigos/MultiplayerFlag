using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;

public class NetworkManagerUI : MonoBehaviour
{
    #region Atributos

    [SerializeField] private Button buttonServer,buttonHost,buttonClient;
    [SerializeField] private TMP_InputField ipField;

    public static NetworkManagerUI instance;

    #endregion
 
    void Start()
    {
        instance = this;
        buttonHost.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartHost();
        });

        buttonClient.onClick.AddListener(()=>{
            TryConnect();
        });

        buttonServer.onClick.AddListener(()=>{
            NetworkManager.Singleton.StartServer();
        });
    }

    void TryConnect(){
        string ipAdress = ipField.text;
        if(ipAdress == null || ipAdress.Length == 0){
            ipAdress = "127.0.0.1";
        }
        UnityTransport transport = (UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
        transport.ConnectionData.Address = ipAdress;
        NetworkManager.Singleton.StartClient();        
    }

}
