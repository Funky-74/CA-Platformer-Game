using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public int speed = 5;
    private Vector2 move;
    private Rigidbody2D rb;
    private int direction = 1;
    public int keycardsCollected;

    private Animator animator;
    public float jumpHeight = 3;
    public int jumpCountBase = 2;
    private int jumpCount;
    private bool isJumping = false;
    private int score = 0;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private UIManager uiManager;
    private int lives = 3;
    public float bulletDamage = 0.4f;
    private Vector2 lastCheckpoint;
    //for invincibility
    private float invincibilityTime = 1.5f;
    private float invincibilityCount = 0f;
    public bool isInvin = false;
    private float fireRate = 0.25f;
    private float timeOfLastShot;
    private AudioSource audio;
    private bool isPlaying = false;
    [SerializeField] private AudioClip collectClip;
    [SerializeField] private AudioClip footstepClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip keycardFailureClip;


    private PowerUp powerUp;
    private float powerUpTime = 12f;
    private float powerUpCount = 0f;
    private bool isPower = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        jumpCount = jumpCountBase;
        lastCheckpoint = rb.transform.position;


    }

    private void Fire()
    {  
            GameObject go = Instantiate(projectilePrefab, rb.transform.position, Quaternion.identity);
            Projectile pro = go.GetComponent<Projectile>();
            pro.Launch(new Vector2(-direction, 0), 600);
        
    }

    // Update is called once per frame
    void Update()
    {
        move = InputSystem.actions["Move"].ReadValue<Vector2>();

        if (InputSystem.actions["Jump"].IsPressed() && jumpCount > 0)
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
            InputSystem.actions["Jump"].Reset();
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(new Vector2(0, Mathf.Sqrt(-2 * Physics2D.gravity.y * jumpHeight)), ForceMode2D.Impulse);
            jumpCount -= 1;
        }

        if (InputSystem.actions["Attack"].IsPressed())
        {
            if (Time.time - timeOfLastShot >= fireRate)
            {
                InputSystem.actions["Attack"].Reset();
                Fire();
                audio.PlayOneShot(shootClip, 2f);
                timeOfLastShot = Time.time;
            }
        }

        if (move.x != 0 && jumpCount == 0 && !isPlaying)
        {
            audio.Play();
            isPlaying = true;
        }
        if (isPlaying && (jumpCount > 0 || move.x == 0))
        {
            audio.Pause();
            isPlaying = false;
        }


        if (isInvin)
        {
            invincibilityCount += Time.deltaTime;
            if (invincibilityCount > invincibilityTime)
            {
                isInvin = false;
                invincibilityCount = 0;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
        }

        if (move.x != 0)
        {
            direction = move.x < 0 ? 1 : -1;
            animator.SetInteger("Direction", direction);
        }
        animator.SetFloat("Move", move.x);


        if (isPower) 
        {
            powerUpCount += Time.deltaTime;

            if (powerUpCount > powerUpTime)
            {
                isPower = false;
                powerUpCount = 0f;

                speed = 5;
                bulletDamage = 0.4f;
                this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
            }
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rb.transform.position;
        position.x += (move.x * speed) * Time.fixedDeltaTime;
        rb.transform.position = position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = jumpCountBase;
            isJumping = false;
            animator.SetBool("IsJumping", false);
        }
        else if (collision.gameObject.tag == "Hazard" && !isInvin)
        {
            PlayerHit();
        }
        else if (collision.gameObject.tag == "EnemyProjectile" && !isInvin)
        {
            rb.linearVelocity = Vector2.zero;
            Destroy(collision.gameObject);
            PlayerHit();
        }
        else if (collision.gameObject.tag == "EnemyProjectile" && isInvin)
        {
            rb.linearVelocity = Vector2.zero;
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "Door")
        {
            if (keycardsCollected == 6) 
            {
                SceneManager.LoadScene("WinScene");
               
            }
            else
            {
                audio.PlayOneShot(keycardFailureClip, 2f);
            }
        }

    }

    public void PlayerHit()
    {
        lives--;
        uiManager.setLives(lives);
        rb.transform.position = lastCheckpoint;
        if (lives == 0)
        {
            SceneManager.LoadScene("GameOverScene");

        }
        isInvin = true;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
    }

    public void ActivatePowerUp()
    {
        speed = 10;
        bulletDamage = 1;
        isPower = true;
        powerUpCount = 0f;

        GetComponent<SpriteRenderer>().color = new Color(0.53f, 0.81f, 0.92f, 1f);
    }

    public void AddKeycard()
    {
        if (isPlaying)
        {
            audio.Pause();
        }

        audio.PlayOneShot(collectClip);
        if (isPlaying)
        {
            audio.Play();
        }
        keycardsCollected++;
        
        uiManager.setScore(keycardsCollected);
    }

    public void PlayFootStep()
    {
        //if(audio != null && footstepClip != null) //make simpler
        //{
            audio.PlayOneShot(footstepClip, 1.5f);
        //}
    }
}