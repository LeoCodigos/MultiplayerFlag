using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Gun", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    #region Atributes
    [Header("Info")]
    public new string name;

    [Header("Shooting")]
    [SerializeField] public int damage;
    [SerializeField] public float fireRate;
    [SerializeField] public bool isAutomatic;



    [Header("Reloading")]
    [SerializeField] public int magazineSize;
    [SerializeField] public int maxAmmo;
    [SerializeField] public int clips;
    [SerializeField] public float reloadTime;
    [SerializeField] public bool reloading;

    #endregion
}
