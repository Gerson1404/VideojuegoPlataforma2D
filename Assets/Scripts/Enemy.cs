using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float bounceForce = 10f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y < -0.5f)
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity =
                    new Vector2(collision.gameObject.GetComponent<Rigidbody2D>().velocity.x, bounceForce);

                Destroy(gameObject);
            }
        }
    }
}
