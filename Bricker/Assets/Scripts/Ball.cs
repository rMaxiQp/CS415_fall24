using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    public float speed;

    private Vector2 _velocity;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        Vector2 direction = (mouse - pos).normalized;

        _velocity = direction * speed;
        Rigidbody.linearVelocity = _velocity;
        gameObject.tag = "Ball";
    }

    private void Update()
    {
        if (Rigidbody.linearVelocity.magnitude < speed)
        {
            Rigidbody.linearVelocity = _velocity;
        }

        // If the ball falls below the screen, destroy it.
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    // OnCollisionEnter2D is called when this collider/rigidbody has begun touching another rigidbody/collider.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // When the ball collides with a brick/wall, it should bounce off.
        if (collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("Wall"))
        {
            _velocity = Vector2.Reflect(_velocity, collision.GetContact(0).normal);
            Rigidbody.linearVelocity = _velocity;
        }
    }

}
