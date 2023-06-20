using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    private Vector2 vel; 
    public float a,b;
    public GameObject player;
    public bool bound;
    public Vector3 min, max;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref vel.x, a);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref vel.y, a);
            transform.position = new Vector3(posX, posY, transform.position.z);
            if (bound)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, min.x, max.x), Mathf.Clamp(transform.position.y, min.y, max.y), Mathf.Clamp(transform.position.z, min.z, max.z));
            }
        }

    }
}
