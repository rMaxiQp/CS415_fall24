using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject ball;

    // Update is called once per frame
    void Update()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        float angle = Mathf.Atan2(mouse.y - pos.y, mouse.x - pos.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void SpawnBall()
    {
        Vector2 pos = transform.position;
        Instantiate(ball, pos, Quaternion.identity);
    }

}
