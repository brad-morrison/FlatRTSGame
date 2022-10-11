using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game game;

    public GM manager { get { return GameObject.FindObjectOfType<GM>(); } }
    public Products products { get { return GameObject.FindObjectOfType<Products>(); } }

    private void Awake()
    {
        game = this;

        // Make the game run as fast as possible
        Application.targetFrameRate = 300;
    }
}
