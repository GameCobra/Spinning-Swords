using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;
using System.Linq;

public class EntityController : MonoBehaviour
{
    //public Inventory inventory;

    EquipmentInstance equipmentInstance = new EquipmentInstance();
    public EquipmentDefinition definition;

    //Stats
    //public StatsContainer baseStats;
    //public StatsContainer stats;

    [SerializeField] GameObject swordSparksPrefab;
    [SerializeField] GameObject dashParticalsPrefab;

    GameObject heldWeapon;
    GameObject arm;
    TextMeshPro healthTextObject;

    public float dashCharge = 0;
    float maxDashEnergy = 10;

    void Start()
    {
        equipmentInstance.definition = definition;
        equipmentInstance.RandomizeAtributes();



        //inventory.PopulateInventoryRandomly();

        //baseStats = new StatsContainer();
        //baseStats.SetEmpty();
        //baseStats.health = Random.Range(1, 8);


        // Disables colision with its weapon 
        arm = gameObject.transform.GetChild(0).gameObject;

        heldWeapon = GameObject.Instantiate(equipmentInstance.definition.weaponObject, arm.transform, false);

        //heldWeapon = arm.transform.GetChild(0).gameObject;
        Physics2D.IgnoreCollision(heldWeapon.GetComponent<Collider2D>(), gameObject.GetComponent < Collider2D>());

        // Connects to the health text
        healthTextObject = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();

        // Sets stats randomly
        //stats.health = Random.Range(1, 10);
        //stats.weaponSpinSpeed = Random.Range(2.5f, 10f);
        //stats.weaponScale = Random.Range(0.5f, 2.5f);
    }

    void FixedUpdate()
    {
        //stats = inventory.GetStatsAsObject();
        //stats.addStatsObject(baseStats);

        gameObject.transform.localScale = new Vector3(equipmentInstance.userScale, equipmentInstance.userScale, 1);

        if (equipmentInstance.health <= 0)
        {
            Destroy(gameObject);
        }

        arm.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), equipmentInstance.weaponSpinSpeed);
        arm.transform.localScale = new Vector3(equipmentInstance.weaponScale, equipmentInstance.weaponScale, 1);
        healthTextObject.text = "" + equipmentInstance.health;


        GameObject[] ballList = GameObject.FindGameObjectsWithTag("Ball");
        if (ballList.Length > 1)
        {
            GameObject closestBall = ballList.OrderBy(x => (x.transform.position - transform.position).sqrMagnitude)
                                             .Where(x => x.transform.position != gameObject.transform.position)
                                             .First();
            Vector3 directionToClosestBall = gameObject.transform.position - closestBall.transform.position;


            gameObject.GetComponent<Rigidbody2D>().AddForce((closestBall.transform.position - gameObject.transform.position).normalized * 0.5f);

            dashCharge += Time.deltaTime;
            if (dashCharge >= maxDashEnergy)
            {
                dashCharge = 0;
                gameObject.GetComponent<Rigidbody2D>().AddForce((closestBall.transform.position - gameObject.transform.position).normalized * 15f, ForceMode2D.Impulse);
                GameObject spawnedSparksObject = GameObject.Instantiate(dashParticalsPrefab, gameObject.transform.position, Quaternion.FromToRotation(Vector3.right, directionToClosestBall));
                Destroy(spawnedSparksObject, 1);
            }
        }


    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            collision.gameObject.transform.parent.parent.gameObject.GetComponent<EntityController>().equipmentInstance.definition.EnemyTriggerOnWeaponHitEnemy(ref equipmentInstance, collision);

        }
    }

    public void OnSwordCollision(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            equipmentInstance.definition.TriggerOnWeaponBlock(ref equipmentInstance, collision);
            collision.gameObject.transform.parent.parent.gameObject.GetComponent<EntityController>().equipmentInstance.definition.EnemyTriggerOnWeaponBlock(ref equipmentInstance, collision);
        }

        if (collision.gameObject.tag == "Ball")
        {
            equipmentInstance.definition.TriggerOnWeaponHitEnemy(ref equipmentInstance, collision);
        }

    }
    /*
    void FlipSwordSpin()
    {
        baseStats.weaponSpinSpeed *= -1;
    }*/
}
