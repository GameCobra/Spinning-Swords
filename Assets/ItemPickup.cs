using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Inventory inventory;

    void Start()
    {
        inventory.PopulateInventoryRandomly();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            //collision.gameObject.GetComponent<EntityController>().inventory = inventory;
            //Destroy(gameObject);
        }
    }
}
