using UnityEngine;

[CreateAssetMenu(fileName = "TakeDamageCollision", menuName = "Properties/TakeDamageCollision")]
public class TakeDamageCollision : EquipmentCollision
{
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        instance.health -= 1;
    }
}
