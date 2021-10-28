using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    //! Este mismo scrip serviria para hacer acido o lava u otros tipos de liquidos.
    float originalSpeed;

    Player player;

    [SerializeField]
    float speedReductionRatio = 0.5f;

    void Start()
    {
        player = FindObjectOfType<Player>();
        originalSpeed = player.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed *= speedReductionRatio;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.speed = originalSpeed;
        }
    }
}
