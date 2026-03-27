using UnityEngine;

[CreateAssetMenu(fileName = "ApplyForceUsingOthersForceValueCollision", menuName = "Properties/ApplyForceUsingOthersForceValueCollision")]
public class ApplyForceUsingOthersForceValueCollision : EquipmentCollision
{
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        //Types of colision (executer / other)
        // Sword -> Sword - Swords block
        // Sword -> Ball - Hit other enemy
        // Ball -> Sword - hit by sword

        GameObject otherBallObject;
        GameObject thisBallObject;

        if (collision.gameObject.tag == "Weapon")
        {
            otherBallObject = collision.gameObject.transform.parent.parent.gameObject;
        }
        else //(collision.gameObject.tag == "Ball")
        {
            otherBallObject = collision.gameObject;
        }

        if (collision.otherCollider.gameObject.tag == "Weapon")
        {
            thisBallObject = collision.otherCollider.gameObject.transform.parent.parent.gameObject;
        }
        else //(collision.otherCollider.gameObject.tag == "Ball")
        {
            thisBallObject = collision.otherCollider.gameObject;
        }


        

        float baseCollisionForce = otherBallObject.GetComponent<EntityController>().equipmentInstance.knockback;
        float knockbackResistance = instance.knockbackResistance;

        float forceScaler = baseCollisionForce * (1 - knockbackResistance);

        thisBallObject.GetComponent<Rigidbody2D>().AddForce(collision.GetContact(0).normal * forceScaler, ForceMode2D.Impulse);

    }
}
