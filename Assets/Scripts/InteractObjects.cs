using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractObjects : MonoBehaviour
{
    private Player2dPlatform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player2dPlatform>();
    }

    // Update is called once per frame
    void Update()
    {
        Interactions();
    }

    void Interactions()
    {

    }
}
