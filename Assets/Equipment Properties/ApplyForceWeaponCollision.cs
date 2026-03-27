using UnityEngine;

[CreateAssetMenu(fileName = "ApplyForceWeaponCollision", menuName = "Properties/ApplyForceWeaponCollision")]
public class ApplyForceWeaponCollision : EquipmentCollision
{
    public float force;
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        GameObject otherBallObject = collision.gameObject.transform.parent.parent.gameObject;
        GameObject thisBallObject = collision.otherCollider.gameObject.transform.parent.parent.gameObject;
        float collisionForce = otherBallObject.GetComponent<EntityController>().equipmentInstance.knockback;
        thisBallObject.GetComponent<Rigidbody2D>().AddForce(collision.GetContact(0).normal * force, ForceMode2D.Impulse);

    }
}
