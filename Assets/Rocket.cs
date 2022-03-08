using UnityEngine;

public class Rocket : MonoBehaviour
{
    private Rigidbody2D cachedRigidBody;
    private Vector2 velocity;

    private void Start()
    {
        cachedRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity = cachedRigidBody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        //cachedRigidBody.velocity = Vector2.Reflect(velocity, collision.contacts[0].normal);
    }
}
