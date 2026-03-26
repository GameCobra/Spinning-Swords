using UnityEngine;

[CreateAssetMenu(fileName = "ApplyForceWeaponCollision", menuName = "Properties/ApplyForceWeaponCollision")]
public class ApplyForceWeaponCollision : EquipmentCollision
{
    public float force;
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        collision.otherCollider.gameObject.transform.parent.parent.gameObject.GetComponent<Rigidbody2D>().AddForce(collision.GetContact(0).normal * force, ForceMode2D.Impulse);

    }
}
