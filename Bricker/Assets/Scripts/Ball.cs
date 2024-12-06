using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    public float speed = 500f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // Call SetRandomTrajectory after 1 second
        Invoke(nameof(SetRandomTrajectory), 1f);
    }

    private void SetRandomTrajectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;

        Rigidbody.AddForce(force.normalized * speed);
    }
}
