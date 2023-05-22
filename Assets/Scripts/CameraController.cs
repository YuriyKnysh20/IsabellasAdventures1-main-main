using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void LateUpdate()
    {
        if (Vector2.Distance(new Vector3(player.transform.position.x, player.transform.position.y, -10), transform.position) > 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y -0.5f, -10), 0.05f);
        }
    }
}
