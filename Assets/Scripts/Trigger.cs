using UnityEngine;

public class Trigger : MonoBehaviour
{
    Rigidbody2D rb;
    float Speed = 200f;
    Vector3 Bounds = new Vector2(6f, 10f);
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        rb.AddForce(rb.transform.up * Speed * Time.fixedDeltaTime);

        if (transform.position.x >= Bounds.x || transform.position.y >= Bounds.x ||
           transform.position.x <= -Bounds.x || transform.position.y <= -Bounds.x)
        {
            Disable();
        }

        
        

    }

    void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerMovement.instance.Die();
        }
    }
}
