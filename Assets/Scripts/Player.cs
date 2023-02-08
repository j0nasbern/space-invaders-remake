using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rigidbody;

    private float gameTime, lastShotTime;
    public float shotCooldown;

    public GameObject bulletPrefab;
    public float bulletLifeTime;

    public GameObject explosionPrefab;

    public GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        lastShotTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            gameTime = Time.time;
            if (gameTime - lastShotTime >= shotCooldown)
            {
                ShootBullet();
            }
        }

        Vector2 position = rigidbody.position;

        position.x = Mathf.Clamp(position.x + Input.GetAxis("Horizontal") * speed * Time.deltaTime, 30, 610);

        rigidbody.position = position;
    }

    void ShootBullet()
    {
        GameObject bulletInstance = Instantiate(bulletPrefab, new Vector3(rigidbody.position.x, rigidbody.position.y + 10, 0.0f), Quaternion.identity);

        Destroy(bulletInstance, bulletLifeTime);

        lastShotTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MeteorBullet"))
        {
            Destroy(collision.gameObject);
            GameObject explosionInstance = Instantiate(explosionPrefab, new Vector3(rigidbody.position.x, rigidbody.position.y, 0.0f), Quaternion.identity);
            Destroy(explosionInstance, 1);
            gameController.GameOver(false);
            Destroy(gameObject);
        }

    }
}
