using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

public class Gun : MonoBehaviour
{
    #region Atributes

    [Header("Reference")]
    [SerializeField] private GunData gunData;
    [SerializeField]private NetworkObject bullet;
    private TMP_Text ammoUI;

    [Header("GunAtributes")]
    private Transform spawn;
    private int maxAmmo;
    private int magazineAmmo;
    private bool reloading;

    private float lastBullet=0;
    
    #endregion

    #region Gun Methods
    public bool CanShoot()=>!reloading && lastBullet>1.0f/(gunData.fireRate/60.0f);
    public void Fire(){
        
        if(magazineAmmo>0)
        {
            if(CanShoot())
            {
                magazineAmmo--;
                UpdateAmmoText();
                var instance=Instantiate(bullet,spawn.position,Quaternion.identity);
                var instanceNO=instance.GetComponent<NetworkObject>();
                instanceNO.Spawn();
                lastBullet=0;
                //bullet.Spawn();
            }
        }
    }

       public void UpdateAmmoText(){
        ammoUI.text=string.Format("{0}/{1}",magazineAmmo,maxAmmo);
    }

    public IEnumerator AutoFire(){
        while(true){
            if(gunData.isAutomatic) Fire();
            yield return null;
        }
    }

    public void Reload(){
        int emptyClip;
        if(magazineAmmo < gunData.magazineSize && maxAmmo>0) 
        {
            emptyClip = gunData.magazineSize - magazineAmmo;
            if(maxAmmo>=emptyClip){
                maxAmmo-=emptyClip;
                magazineAmmo+=emptyClip;
            }else
            {
                magazineAmmo+=maxAmmo;
                maxAmmo=0;
            }
            UpdateAmmoText();
        }
    }
    #endregion

    void Awake(){
        ammoUI=GameManager.instance.AmmoText;
    }

    void Start()
    {
        
        maxAmmo= gunData.magazineSize*gunData.clips;
        magazineAmmo=gunData.magazineSize;
        UpdateAmmoText();
        spawn=this.transform;
    }

    void Update(){
        lastBullet+=Time.deltaTime;
    }

}
