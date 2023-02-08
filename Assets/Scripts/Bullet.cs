using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int shotSpeed;
    private Rigidbody2D rigidbody;

    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody.position;

        position.y += shotSpeed * Time.deltaTime;

        rigidbody.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MeteorBullet"))
        {
            Destroy(collision.gameObject);
            GameObject explosionInstance = Instantiate(explosionPrefab, new Vector3(rigidbody.position.x, rigidbody.position.y, 0.0f), Quaternion.identity);
            explosionInstance.transform.localScale = new Vector3(5.0f, 5.0f, 1.0f);
            Destroy(explosionInstance, 1);
            Destroy(gameObject);
        }

    }
}
