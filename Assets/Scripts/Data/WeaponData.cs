using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponData : ScriptableObject
{
    public int weaponID;
    public string weaponName;
    public weaponType weaponType;
    public float weaponDamage;
    public float weaponDefense;
}
public enum weaponType
{
    MechanicalWeapon,
    AlchemicalWeapon,
    BiologicalWeapon,
    StreamWeapon,
    CosmicWeapon,
    MythicWeapon,
}

