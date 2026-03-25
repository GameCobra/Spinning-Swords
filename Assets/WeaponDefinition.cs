using UnityEngine;
using Unity.Mathematics;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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
    public List<EquipmentCollision> onWeaponBlock;
    public List<EquipmentCollision> onWeaponHitEnemy;
    public List<EquipmentCollision> enemyOnWeaponBlock;
    public List<EquipmentCollision> enemyOnWeaponHitEnemy;
    public void TriggerOnWeaponBlock(ref EquipmentInstance userInstance, Collision2D collision)
    {
        List<EquipmentCollision> trigger = onWeaponBlock;
        if (trigger != null)
        {
            for (int i = 0; i < trigger.Count; i++)
                trigger[i].execute(ref userInstance, collision);
        }
    }

    public void TriggerOnWeaponHitEnemy(ref EquipmentInstance userInstance, Collision2D collision)
    {
        List<EquipmentCollision> trigger = onWeaponHitEnemy;
        if (trigger != null)
        {
            for (int i = 0; i < trigger.Count; i++)
                trigger[i].execute(ref userInstance, collision);
        }
    }

    public void EnemyTriggerOnWeaponBlock(ref EquipmentInstance enemyInstance, Collision2D collision)
    {
        List<EquipmentCollision> trigger = enemyOnWeaponBlock;
        if (trigger != null)
        {
            for (int i = 0; i < trigger.Count; i++)
                trigger[i].execute(ref enemyInstance, collision);
        }
    }

    public void EnemyTriggerOnWeaponHitEnemy(ref EquipmentInstance enemyInstance, Collision2D collision)
    {
        List<EquipmentCollision> trigger = enemyOnWeaponHitEnemy;
        if (trigger != null)
        {
            for (int i = 0; i < trigger.Count; i++)
                trigger[i].execute(ref enemyInstance, collision);
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

    public void RandomizeAtributes()
    {
        health = Random.Range(definition.health[0], definition.health[1]);
        weaponScale = Random.Range(definition.weaponScale[0], definition.weaponScale[1]);
        weaponSpinSpeed = Random.Range(definition.weaponSpinSpeed[0], definition.weaponSpinSpeed[1]);
    }
}


public abstract class EquipmentCollision : ScriptableObject
{
    public abstract void execute(ref EquipmentInstance instance, Collision2D collision);
}
