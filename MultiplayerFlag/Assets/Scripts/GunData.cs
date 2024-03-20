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
    [SerializeField] private int damage;
    [SerializeField] private float fireRate;

    [Header("Reloading")]
    [SerializeField] private int magazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private bool reloading,firing;

    #endregion
}
