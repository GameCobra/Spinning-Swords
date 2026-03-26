using UnityEngine;

[CreateAssetMenu(fileName = "SpawnParticalsCollision", menuName = "Properties/SpawnParticalsCollision")]
public class SpawnParticalsCollision : EquipmentCollision
{
    public GameObject particalsToSpawn;
    public override void execute(ref EquipmentInstance instance, Collision2D collision)
    {
        GameObject spawnedSparksObject = GameObject.Instantiate(particalsToSpawn, collision.GetContact(0).point, Quaternion.FromToRotation(collision.GetContact(0).point, collision.GetContact(0).normal));
        Destroy(spawnedSparksObject, 1);
    }
}
