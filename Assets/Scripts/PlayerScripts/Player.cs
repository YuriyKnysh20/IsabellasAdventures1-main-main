using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rigB;
    Animator anim;
    public GameObject arrow;
    public Image life;
    public Vector2 move;
    public Transform groundCheck, shotPlace;
    public float speed, jump, hp, go;
    public bool isRight, isGrounded, coolDown, canHurt, canShoot, jumpBut;

    void Start()
    {
        rigB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = transform.GetChild(0);
        shotPlace = transform.GetChild(1);
        isRight = true;
        coolDown = false;
        canHurt = true;
        canShoot = true;
        hp = 100;
        go = 0;
        //life = transform.GetChild(3).Find("LifeBar").GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        //coinMeter.text = coin.ToString();
        //Health();
        Go();
        Jump();
    }

    public void MoveLeft()
    {
        go = -1;
        transform.localScale = new Vector2(-1, 1);
    }

    public void MoveRight()
    {
        go = 1;
        transform.localScale = new Vector2(1, 1);
    }

    public void MoveStop()
    {
        go = 0;
    }

    private void Go()
    {
        if(go == 0)
        {
            anim.SetBool("Go", false);
        }
        else
        {
            anim.SetBool("Go", true);
        }
#if UNITY_EDITOR
        go = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Horizontal") > 0)
            transform.localScale = new Vector2(1, 1);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector2(-1, 1);

#endif

        if (canShoot)
        {
            rigB.velocity = new Vector2(speed * go, rigB.velocity.y);
        }
        else
        {
            rigB.velocity = new Vector2(0, 0);
        }
    }
    public bool j;
    public int jit = 0;

    public void JButDown()
    {
        jumpBut = true;
    }
    public void JButUp()
    {
        jumpBut = false;
    }

    void Jump()
    {
        anim.SetBool("isGrounded", isGrounded);
        if (jumpBut == true)
        {
            if (isGrounded)
            {
                anim.SetTrigger("StartJump");
                j = true;
            }
        }
        else
        {
            j = false;
        }

        if (j == true)
        {
            anim.SetBool("Jump", true);
            if (jit++ < 60 && rigB.velocity.y < 50)
            {
                //rigB.AddForce(Vector2.up * jump / jit);
                rigB.AddForce(transform.up * jump / jit, ForceMode2D.Impulse);
            }
        }
        else
        {
            jit = 0;
        }
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, LayerMask.GetMask("Ground"));
        //isWalled = Physics2D.OverlapCapsule(wallCheck.position, new Vector2(1f, 2f), CapsuleDirection2D.Vertical, 0, LayerMask.GetMask("Ground"));
    }

    public void ShootBut()
    {
        if (canShoot == true && isGrounded == true)
        {
            anim.SetBool("Go", false);
            go = 0;
            canShoot = false;
            anim.SetTrigger("StartShoot");
            StartCoroutine(ShootCoolDown());
        }
    }

    public void Shoot()
    {
        //arrow.GetComponent<TriggerDamage>().parent = gameObject;
        if (transform.localScale.x == 1)
        {
            Instantiate(arrow, shotPlace.position, Quaternion.Euler(0, 0, 0));
        }
        else
        {
            Instantiate(arrow, shotPlace.position, Quaternion.Euler(0, 180, 0));
        }
    }

    IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        canShoot = true;
    }

    /* [SerializeField] private Rigidbody2D rigidboby;
     [SerializeField] private Animator animator;
     [SerializeField] private GroundDetection groundDetection;
     [SerializeField] private GameObject arrow;
     [SerializeField] private Image hpBar;
     [SerializeField] private Text coinMeter;
     public float speed = 2.5F, jump, health;
     public int coin;
     float go;
     bool canShoot, canHurt;

     public float Speed
     {
         get { return speed; }
         set
         {
             if (value > 4)
                 speed = value;
         }
     }

     private void Start()
     {
         rigidboby = GetComponent<Rigidbody2D>();
         animator = GetComponent<Animator>();
         groundDetection = transform.GetChild(0).GetComponent<GroundDetection>();
         hpBar = FindObjectOfType<Canvas>().transform.Find("HealthBar").GetChild(0).GetComponent<Image>();
         coinMeter = FindObjectOfType<Canvas>().transform.Find("HealthBar").GetChild(1).GetComponent<Text>();
         health = 100;
         speed = 4;
         canShoot = true;
         canHurt = true;
         coin = 0;
     }

     int ti;
     private void FixedUpdate()
     {
         coinMeter.text = coin.ToString();
         Health();
         Go();
         Jump();
     }

     public void MoveLeft()
     {
         go = -1;
         transform.localScale = new Vector2(-1, 1);
     }

     public void MoveRight()
     {
         go = 1;
         transform.localScale = new Vector2(1, 1);
     }

     public void MoveStop()
     {
         go = 0;
     }

     private void Go()
     {
         animator.SetBool("isGrounded", groundDetection.isGrounded);
         animator.SetFloat("Speed", Mathf.Pow(go,2));
 #if UNITY_EDITOR
         go = Input.GetAxis("Horizontal");
         if(Input.GetAxis("Horizontal") > 0)
             transform.localScale = new Vector2(1, 1);
         if (Input.GetAxis("Horizontal") < 0)
             transform.localScale = new Vector2(-1, 1);

 #endif
         if (canShoot)
         {
             rigidboby.velocity = new Vector2(speed * go, rigidboby.velocity.y);
         }
         else
         {
             rigidboby.velocity = new Vector2(0, 0);
         }
     }

     bool jumpBut;
     public void JumpButDown()
     {
         jumpBut = true;
         if (groundDetection.isGrounded == true)
         {
             animator.SetTrigger("StartJump");
         }
     }

     public void JumpButUp()
     {
         jumpBut = false;
     }

     public bool j;
     public int jit = 0;
     private void Jump()
     {
         animator.SetBool("isGrounded", groundDetection.isGrounded);

         if (jumpBut == true)
         {

             if (groundDetection.isGrounded == true)
             {
                 if (jit++ < 60 && rigidboby.velocity.y < 50)
                 {
                     //rigB.AddForce(Vector2.up * jump / jit);
                     rigidboby.AddForce(transform.up * jump / jit, ForceMode2D.Impulse);
                 }
             }
         }
         else
         {
             jit = 0;
         }
     }

     public void ShootBut()
     {
         if (canShoot == true && groundDetection.isGrounded == true)
         {
             animator.SetFloat("Speed", 0);
             go = 0;
             canShoot = false;
             animator.SetTrigger("StartShoot");
             StartCoroutine(ShootCoolDown());
         }
     }

     public void Shoot()
     {
         arrow.GetComponent<TriggerDamage>().parent = gameObject;
         if (transform.localScale.x == 1)
         {
             Instantiate(arrow, transform.GetChild(1).position, Quaternion.Euler(0, 0, 0));
         }
         else
         {
             Instantiate(arrow, transform.GetChild(1).position, Quaternion.Euler(0, 180, 0));
         }
     }

     IEnumerator ShootCoolDown()
     {
         yield return new WaitForSeconds(0.5f);
         canShoot = true;
     }

     public void Health()
     {
         hpBar.fillAmount = health / 100;
         if (transform.position.y < -30)
         {
             health = 0;
         }
         if (health <= 0)
         {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         }
         if (canHurt == false)
         {
             if (ti < 5)
             {
                 ti++;
             }
             else
             {
                 if (GetComponent<SpriteRenderer>().color == new Color(1, 1, 1, 1))
                 {
                     GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                 }
                 else
                 {
                     GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                 }
                 ti = 0;
             }
         }
         else
         {
             GetComponent<SpriteRenderer>().color = Color.white;
         }
     }

     public void OnTriggerStay2D(Collider2D other)
     {
         if (other.gameObject.tag == "Enemy" && canHurt == true)
         {
             animator.SetTrigger("TakeHit");
             StartCoroutine(Hurt());
             rigidboby.AddForce(transform.up * 5, ForceMode2D.Impulse);
             health -= 10;
             //soundSource.PlayOneShot(AidKitSound);
             //Destroy(col.gameObject);
             //SetHealth(10);
         }
     }
     public void OnTriggerEnter2D(Collider2D other)
     {
         if (other.gameObject.tag == "Tablet")
         {
             if (health <= 75)
             {
                 health += 25;
             }
             else
             {
                 health = 100;
             }
             Destroy(other.gameObject);
         }
         if (other.gameObject.tag == "Coin")
         {
             coin++;
             Destroy(other.gameObject);
         }
     }

     IEnumerator Hurt()
     {
         canHurt = false;
         yield return new WaitForSeconds(2f);
         canHurt = true;
     }*/
}
 

 
 
   

	
 
