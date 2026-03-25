using UnityEngine;

[CreateAssetMenu(fileName = "ReverseSpin", menuName = "Properties/ReverseSpin")]
public class ReverseSpinCollision : EquipmentCollision
{
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        instance.weaponSpinSpeed *= -1;
    }
}