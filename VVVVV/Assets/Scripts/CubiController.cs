using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CubiController : MonoBehaviour
{

    private static CubiController player;  
    public ColorController colorController; 

    [Header("Movement")]
    public float speed;
    private Animator animator;
    private Rigidbody2D rigidbody2D;
    private bool isGrounded = false;

    [Header("Auto-Movement")]
    public Vector3 startPosition;   
    public Vector3 endPosition;
    public Vector3 targetPosition;  
    public float moveDuration = 2f; 
    private bool autoMoving = true; 


    [Header("Color")]
    private Color paintableColor;  
    private Color currentPlatformColor;  
    public SpriteRenderer paint;  
    private bool paintable = false; 
    private bool everythingPainted = false;
    private SpriteRenderer currentPaintableSprite;


    private bool back = false;
    private bool next = false;
    private bool up = false;

    private void Start()
    {
        transform.position = startPosition;
        StartCoroutine(MoveToScene()); // Iniciar el movimiento autom�tico en la escena actual

    }


    private void Awake()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
        }

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Movement()
    {
        if (autoMoving) return; // Evitar mover el personaje durante el movimiento autom�tico

        float inputMovement = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(inputMovement * speed, rigidbody2D.velocity.y);
        animator.SetFloat("MoveX", Mathf.Abs(inputMovement));

        if (inputMovement < 0)
        {
            if (rigidbody2D.gravityScale == -1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);

            }
            // Mover a la izquierda
        }
        else if (inputMovement > 0)
        {
            if (rigidbody2D.gravityScale == -1)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            // Mover a la derecha
        }
    }

    void ChangeGravity()
    {
        if (autoMoving) return;  // Evitar cambiar la gravedad durante el movimiento autom�tico

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
        {
            rigidbody2D.gravityScale *= -1;
            isGrounded = false;
            transform.Rotate(0f, 0f, 180f);
        }
    }

    void Update()
    {
        Movement();
        ChangeGravity();
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (currentPlatformColor != Color.white && !paintable)
            {
                ChangeCubiColor(currentPlatformColor);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (paintable && CompareColors(paint.color, paintableColor, 0.01f))
            {
                ChangePlatformColor(paint.color);
                paint.color = Color.white;  
                paintable = false;  
                everythingPainted = true;
                currentPaintableSprite.tag = "Platform";
            }
        }

    }

    public void ChangeCubiColor(Color color)
    {
        if (autoMoving) return;  // Evitar cambiar el color durante el movimiento autom�tico
        paint.color = color;  
    }

    public void ChangePlatformColor(Color color)
    {
        if (autoMoving) return;  // Evitar cambiar el color durante el movimiento autom�tico

        if (currentPaintableSprite != null)
        {
            currentPaintableSprite.color = color;  // Cambiar el color de la plataforma pintable
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Up"))
        {
            up = true;
            GameManager.gameManager.LoadNextScene();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (everythingPainted)
            {
                next = true;
                everythingPainted = false;
                GameManager.gameManager.LoadNextScene();
            }
        }
        if (collision.gameObject.CompareTag("Back"))
        {  
            back = true;
            GameManager.gameManager.LoadPreviousScene();            
        }
       

        if (collision.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector3(-7.93f, -2.55f);
            if (rigidbody2D.gravityScale == -1)
            {
                transform.rotation = Quaternion.identity;
                rigidbody2D.gravityScale *= -1;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            PlatformController platform = collision.gameObject.GetComponent<PlatformController>();
            isGrounded = true;

            if (platform != null)
            {
                currentPlatformColor = platform.GetPlatformColor();  // Recoge el color de la plataforma
            }
        }
        else if (collision.gameObject.CompareTag("Paintable"))
        {
            SpriteRenderer platformSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            isGrounded = true;

            if (platformSprite != null)
            {
                currentPaintableSprite = platformSprite;  
                paintable = true;  
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paintable"))
        {
            paintable = false;  
            currentPaintableSprite = null;  
            isGrounded = false;
        }
        if (collision.gameObject.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    bool CompareColors(Color a, Color b, float tolerance)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }

    IEnumerator MoveToScene()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        rigidbody2D.isKinematic = true;

        transform.localScale = new Vector3(1, 1, 1);

        animator.SetFloat("MoveX", 1); 

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        animator.SetFloat("MoveX", 0); 

        rigidbody2D.isKinematic = false;
        autoMoving = false;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    /*
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    */
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdatePaintableColor(); 
        if (next)
        {
            transform.position = new Vector3(startPosition.x, transform.position.y, transform.position.z); // Restablecer la posici�n del personaje
            next = false;
        }
        if (back)
        {
            transform.position = new Vector3(endPosition.x, transform.position.y, transform.position.z); // Restablecer la posici�n del personaje
            back = false;
        }
        if (up)
        {
            transform.position = new Vector3(transform.position.x, -5.2f, transform.position.z); // Restablecer la posici�n del personaje
            up = false;
        }
    }



    private void UpdatePaintableColor()
    {
        if (colorController == null)
        {
            colorController = FindObjectOfType<ColorController>();
        }
        if (colorController != null)
        {
            paintableColor = colorController.paintableColor;
        }
    }

}
