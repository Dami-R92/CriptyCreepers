using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [SerializeField]
    int healt = 1;

    [SerializeField]
    float speed = 1;

    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        GameObject[] spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int randomSpawnPoint = Random.Range(0,spawnPoint.Length);
        transform.position = spawnPoint[randomSpawnPoint].transform.position;
    }

    private void Update()
    {
        EnemyIA();
        if (healt <=0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        healt--;
    }

    void EnemyIA()
    {
        Vector2 direction = player.position - transform.position;
        transform.position += (Vector3) direction * Time.deltaTime * speed;
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}
