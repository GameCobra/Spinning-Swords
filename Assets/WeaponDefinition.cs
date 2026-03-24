using UnityEngine;
using Unity.Mathematics;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EquipmentDefinition", menuName = "Scriptable Objects/WeaponDefinition")]
public class EquipmentDefinition : ScriptableObject
{
    [Header("Stat Ranges")]
    public int2 health;
    public float2 weaponSpinSpeed;
    public float2 weaponScale;
    public float2 userScale;

    [Header("Visual Aspects")]
    public GameObject weaponObject;

    [Header("Code Execution")]
    public OnWeaponBlock onWeaponBlock;
    public void TriggerOnWeaponBlock(ref EquipmentInstance instance, Collision2D collision)
    {
        if (onWeaponBlock != null)
        {
            onWeaponBlock.execute(ref instance, collision);
        }
    }

}

public class EquipmentInstance
{
    public int health;
    public float weaponSpinSpeed;
    public float weaponScale;
    public float userScale;

    public EquipmentDefinition definition;
}


public abstract class OnWeaponBlock : ScriptableObject
{
    public abstract void execute(ref EquipmentInstance instance, Collision2D collision);
}
