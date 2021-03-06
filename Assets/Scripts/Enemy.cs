using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] int healt = 1, scorePoint = 100;
    [SerializeField] float speed = 1;
    [SerializeField] AudioClip impactClip;


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
            GameManager.Instance.Score +=scorePoint;
            Destroy(gameObject, 0.1f);
        }
    }

    public void TakeDamage()
    {
        healt--;
        AudioSource.PlayClipAtPoint(impactClip, transform.position);
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
