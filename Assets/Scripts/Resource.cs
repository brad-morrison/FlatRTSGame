using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : Game
{
    public Sprite sprite;
    public string name;
    public int health;
    public string dropCount;
    public Vector3 productSpawnPos;
    public GameObject product;

    private void Start()
    {
        health = 100;
        product = game.products.wood;
        productSpawnPos = transform.position;
        productSpawnPos.y += 2.0f;
        AddToResourceList();
    }

    public void AddToResourceList()
    {
        game.manager.resources.Add(this.gameObject);
    }

    public void RemoveHealth(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }

    public GameObject GetProduct()
    {
        return product;
    }

    public void DestroyResource()
    {
        game.manager.resources.Remove(this.gameObject);
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
}
