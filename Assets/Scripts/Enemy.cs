using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject MeteorBulletPrefab;
    public float MeteorBulletLifeTime;

    private float gameTime, lastShotTime;
    public float shotCooldown, random;

    private Rigidbody2D rigidbody;

    public GameObject explosionPrefab;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        random = Random.Range(3, 10);

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lastShotTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time;
        if (gameTime - lastShotTime >= random)
        {
            ShootMeteor();
            random = Random.Range(6, 10);
        }

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            float playerY = GameObject.FindGameObjectWithTag("Player").transform.position.y;
            if (gameObject.transform.position.y - 25 <= playerY + 15)
            {
                gameController.GameOver(false);
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Destroy(collision.gameObject);
            DestroyMeteor();
        }
        
    }

    void ShootMeteor()
    {
        GameObject bulletInstance = Instantiate(MeteorBulletPrefab, new Vector3(rigidbody.position.x, rigidbody.position.y, 0.0f), Quaternion.identity);

        Destroy(bulletInstance, MeteorBulletLifeTime);

        lastShotTime = Time.time;
    }

    public void DestroyMeteor()
    {
        GameObject explosionInstance = Instantiate(explosionPrefab, new Vector3(rigidbody.position.x, rigidbody.position.y, 0.0f), Quaternion.identity);
        Destroy(explosionInstance, 1);
        Destroy(gameObject);
    }
}
