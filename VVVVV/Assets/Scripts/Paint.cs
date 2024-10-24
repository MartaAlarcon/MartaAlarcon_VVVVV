using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paint : MonoBehaviour
{
    public SpriteRenderer paint;
    private Rigidbody2D Rigidbody2D;
    [SerializeField] private float speed = 0.1f;
    // Start is called before the first frame update
    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        SetPaint();
    }
    public void SetPaint()
    {
        Rigidbody2D.velocity = new Vector2(0, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Player"))
        {
            Spawner.spawner.Push(this.gameObject);
        }
        
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
