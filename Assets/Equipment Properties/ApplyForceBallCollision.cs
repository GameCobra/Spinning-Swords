using UnityEngine;

[CreateAssetMenu(fileName = "ApplyForceBallCollision", menuName = "Properties/ApplyForceBallCollision")]
public class ApplyForceBallCollision : EquipmentCollision
{
    public float force;
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        collision.otherCollider.attachedRigidbody.AddForce(collision.GetContact(0).normal * force, ForceMode2D.Impulse);

    }
}
