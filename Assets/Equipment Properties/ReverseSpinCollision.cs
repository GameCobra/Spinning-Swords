using UnityEngine;

[CreateAssetMenu(fileName = "ReverseSpinCollision", menuName = "Properties/ReverseSpinCollision")]
public class ReverseSpinCollision : EquipmentCollision
{
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        instance.weaponSpinSpeed *= -1;
    }
}