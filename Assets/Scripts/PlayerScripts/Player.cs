using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using JetBrains.Annotations;


public class Player : MonoBehaviour
{
    #region PrivateUnityClasses

    private Rigidbody2D rigB;
    
    private Animator anim;

    #endregion

    #region PrivateOwnClasses

    [SerializeField] private HealthBar healthBar;
    
    
    private LevelSystem levelSystem;
    

    #endregion

    #region PublicUnityClasses

    public GameObject arrow;
    
    public Vector2 move;
    
    public Transform groundCheck, shotPlace;
    

    #endregion
    
    
        
    public delegate void TakeDie();
    public event TakeDie OnDie;

    public delegate void TakeDamege();

   public event  TakeDamege OnDamage;

    public float speed, jump, hp, go;

    public bool isRight, isGrounded, CoolDown;
    public bool canHurt, canShoot, jumpBut;

    private void OnEnable()
    {
        OnDie += Die;
    }

    private void OnDisable()
    {
        OnDie -= Die;
    }

    void Start()
    {
        rigB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
       // life = GameObject.FindObjectOfType<Canvas>().transform.Find("HealthBar").GetChild(0).GetComponent<Image>();
        groundCheck = transform.GetChild(0);
        shotPlace = transform.GetChild(1);
        isRight = true;
        CoolDown = false;
        canHurt = true;
        canShoot = true;
        hp = 100;
        go = 0;
       // life = transform.GetChild(3).Find("LifeBar").GetComponent<Image>();
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
    }
    private void LevelSystem_OnLevelChanged(object sender, EventArgs e)
    {
        SetHealthBarSize(1f + levelSystem.GetLevelNumber() * .1f);
    }
    private void SetHealthBarSize(float healthBarSize)
    {
        transform.Find("Health").localScale = new Vector3(.7f * healthBarSize, 1, 1);
    }
    private void FixedUpdate()
       
    {
       
        GroundCheck();
        
        if(transform.position.y < -10)
        {
            Die();
        }
        if (canHurt == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
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
        //life.fillAmount = hp / 100;
        if (go == 0)
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
            if (isGrounded == true)
            {
                rigB.velocity = new Vector2(speed * go, rigB.velocity.y);
            }
            else
            {
                rigB.velocity = new Vector2(speed * 1.2f * go, rigB.velocity.y);

            }
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
            //anim.SetBool("Jump", true);
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
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Hurt" && canHurt == true)
        {
            //hp -= other.GetComponent<Damage>().damage;
            hp -= 20;
            healthBar.UpdateValue(hp);
            StartCoroutine(Hurt());
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Hurt" && canHurt)
        {
            //hp -= other.GetComponent<Damage>().damage;
            hp -= 20;
            healthBar.UpdateValue(hp);
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        if (hp > 0)
        {
            OnDamage.Invoke();
            canHurt = false;
            yield return new WaitForSeconds(0.1f);
            canHurt = true;
            Debug.LogWarning("Event @Damage");

        }
        else
        {
            OnDie?.Invoke();
        }
    }

    void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Debug.LogWarning("Event @Die");
    }

    
}
 

 
 
   

	
 
