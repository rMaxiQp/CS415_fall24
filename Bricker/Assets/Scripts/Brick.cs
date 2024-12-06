using UnityEngine;

public class Brick : MonoBehaviour
{
    public int Health { get; private set; }
    public Sprite[] states;

    public SpriteRenderer Renderer { get; private set; }

    public bool unbreakable;

    public int points = 100;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        if (!unbreakable)
        {
            Health = states.Length;
            Renderer.sprite = states[Health - 1];
        }
    }

    private void TakeDamage()
    {
        if (!unbreakable)
        {
            return;
        }
        Health--;

        if (Health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            Renderer.sprite = states[Health - 1];
        }

        GameManager.Instance.OnBrickHit(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            TakeDamage();
        }
    }

}
