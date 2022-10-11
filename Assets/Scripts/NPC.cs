using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Game
{
    // sprite
    // tool
    // job
    // resource
    // home
    // speed

    public Sprite sprite;
    public string tool;
    public string job;

    private void Start()
    {
        tool = "axe";
        job = "lumberjack";

        //MoveToResource();
    }

    public void MoveToResource()
    {
        print(game.manager.resources.Count);
        print(game.manager.FindClosestResource(transform.position));
        transform.position = game.manager.FindClosestResource(transform.position).transform.position;
    }
}
