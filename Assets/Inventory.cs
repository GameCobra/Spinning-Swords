using UnityEngine;


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
