using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;
using System.Linq;

public class EntityController : MonoBehaviour
{
    public Inventory inventory;

    //Stats
    public StatsContainer stats;

    [SerializeField] GameObject swordSparksPrefab;
    [SerializeField] GameObject dashParticalsPrefab;

    GameObject heldWeapon;
    GameObject arm;
    TextMeshPro healthTextObject;

    public float dashCharge = 0;
    float maxDashEnergy = 10;

    void Start()
    {
        inventory.PopulateInventoryRandomly();
        stats = inventory.GetStatsAsObject();

        StatsContainer baseStats = new StatsContainer();
        baseStats.SetEmpty();
        baseStats.health = Random.Range(1, 8);
        stats.addStatsObject(baseStats);


        // Disables colision with its weapon 
        arm = gameObject.transform.GetChild(0).gameObject;
        heldWeapon = arm.transform.GetChild(0).gameObject;
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
        arm.transform.RotateAround(gameObject.transform.position, new Vector3(0, 0, 1), stats.weaponSpinSpeed);
        arm.transform.localScale = new Vector3(stats.weaponScale, stats.weaponScale, 1);
        healthTextObject.text = "" + stats.health;


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
            stats.health -= 1;
            gameObject.GetComponent<Rigidbody2D>().AddForce(collision.GetContact(0).normal * 3, ForceMode2D.Impulse);

        }
        if (stats.health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnSwordCollision(Collision2D collision)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(collision.GetContact(0).normal * 7, ForceMode2D.Impulse);
        GameObject spawnedSparksObject = GameObject.Instantiate(swordSparksPrefab, collision.GetContact(0).point, Quaternion.FromToRotation(collision.GetContact(0).point, collision.GetContact(0).normal));
        Destroy(spawnedSparksObject, 1);
        FlipSwordSpin();
    }

    void FlipSwordSpin()
    {
        stats.weaponSpinSpeed *= -1;
    }
}

[System.Serializable]
public class Inventory
{
    public Helmet head;
    public Weapon weapon;

    public void PopulateInventoryRandomly()
    {
        head = new Helmet();
        weapon = new Weapon();
        head.GenerateRandomPiece();
        weapon.GenerateRandomPiece();
    }
    public StatsContainer GetStatsAsObject()
    {
        StatsContainer stats = new StatsContainer();
        stats.SetEmpty();

        stats.addStatsObject(head.GetStatsAsObject());
        stats.addStatsObject(weapon.GetStatsAsObject());
        return stats;
    }
}


[System.Serializable]
public class StatsContainer
{
    public int health;
    public float speed;
    public float weaponScale;
    public float weaponSpinSpeed;

    public void SetEmpty()
    {
        health = 0;
        speed = 0;
        weaponScale = 0;
        weaponSpinSpeed = 0;
    }

    public void addStatsObject(StatsContainer stats)
    {
        health += stats.health;
        speed += stats.speed;
        weaponScale += stats.weaponScale;
        weaponSpinSpeed += stats.weaponSpinSpeed;
    }
}

public class Helmet : IGear
{
    public int bonusHealth { get; set; }
    public float bonusSpeed { get; set; }
    public void GenerateRandomPiece()
    {
        bonusHealth = Random.Range(0, 4);
        bonusSpeed = Random.Range(0.0f, 0.4f);
    }
    public StatsContainer GetStatsAsObject()
    {
        StatsContainer stats = new StatsContainer();
        stats.SetEmpty();

        stats.health = bonusHealth;
        stats.speed = bonusSpeed;
        return stats;
    }
}

public class Weapon : IGear
{
    public int bonusHealth { get; set; }
    public float bonusSpeed { get; set; }
    public float weaponScale { get; set; }
    public float weaponSpinSpeed { get; set; }
    public void GenerateRandomPiece()
    {
        bonusHealth = Random.Range(0, 4);
        bonusSpeed = Random.Range(0.0f, 0.4f);
        weaponScale = Random.Range(0.5f, 2.5f);
        weaponSpinSpeed = Random.Range(2.5f, 10f);
    }
    public StatsContainer GetStatsAsObject()
    {
        StatsContainer stats = new StatsContainer();
        stats.SetEmpty();

        stats.health = bonusHealth;
        stats.speed = bonusSpeed;
        stats.weaponScale = weaponScale;
        stats.weaponSpinSpeed = weaponSpinSpeed;
        return stats;
    }
}

public interface IGear
{
    int bonusHealth { get; set; }
    float bonusSpeed { get; set; }

    void GenerateRandomPiece();
    StatsContainer GetStatsAsObject();

}