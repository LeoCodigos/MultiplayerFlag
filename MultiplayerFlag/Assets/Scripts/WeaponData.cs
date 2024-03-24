using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Melee", menuName ="Weapon/Melee")]
public class WeaponData : ScriptableObject
{
    #region Atributes

    [Header("Info")]
    public new string name;

    [Header("Properties")]
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;

    

    #endregion
}
