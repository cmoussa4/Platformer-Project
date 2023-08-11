using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class BadController : MonoBehaviour
{
    GameManager gm;
    private Rigidbody2D rb;
    [SerializeField] float speed = 100f;
    private Vector2 move;
    [SerializeField] public float jumpVelocity = 5f;
    [SerializeField] LayerMask jumpLayer;
    private bool facingRight = true;
    private float jumpBufferRemember = 0f;
    private float coyotetimeCounter = 0f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 12f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 3f;
    [SerializeField] TrailRenderer tr;
    private BoxCollider2D boxCollider;
    [SerializeField] Transform respawnLoc;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] TMP_Text textDeaths;
    [SerializeField] TMP_Text textCollectables;
    private int deaths;
    private int collectables;
    private float timer;
    [SerializeField] private TMP_Text timerText;
    

    // Start is called before the first frame update
    void Awake()
    {
        gm = GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        deaths = 0;
        collectables = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        Inputs();
        Jump();
        FlipFunction();
        Timer();
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movement();

    }

    void Movement()
    {
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);



    }
    void Jump()
    {


        if (coyotetimeCounter > 0f && jumpBufferRemember > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);

            
        }

    }
    void Inputs()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Jump") && GroundCheck())
        {
            rb.velocity += Vector2.up * jumpVelocity;
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            StartCoroutine(Dash());
        }


    }


    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    void FlipFunction()
    {
        
        if (move.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (move.x < 0 && facingRight)
        {
            Flip();
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.05f, jumpLayer);
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            deaths++;
            textDeaths.text = "Deaths: " + deaths.ToString();
            this.transform.position = respawnLoc.position;


        }

        if (collision.gameObject.CompareTag("Collectable"))
        {
            collectables++;
            textCollectables.text = "Collectables: " + collectables.ToString() + "/5";
        }

    }


    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(move.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;

    }

    void Timer()
    {
        timer += Time.deltaTime;
        timerText.text = "Seconds:" + ((int)timer).ToString();
    }

}
