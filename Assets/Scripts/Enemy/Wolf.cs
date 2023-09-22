using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : MonoBehaviour
{
    public float hp, speed;
    public GameObject pl;
    Transform front, down;
    public bool isRight, tow, dow, canHurt;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
        front = transform.GetChild(0);
        down = transform.GetChild(1);
        tow = false;
        canHurt = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canHurt == false)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        tow = Physics2D.OverlapCircle(front.position, 0.2f, LayerMask.GetMask("Ground"));
        dow = Physics2D.OverlapCircle(down.position, 0.2f, LayerMask.GetMask("Ground"));
        if (tow == true || dow == false)
        {
            isRight = !isRight;
        }
        if (Physics2D.OverlapCircle(front.position, 0.1f, LayerMask.GetMask("Character")))
        {
            isRight = !isRight;
        }
        if (Vector2.Distance(pl.transform.position, transform.position) < 20)
        {
            if (isRight == true)
            {
                //GetComponent<SpriteRenderer>().flipX = false;
                transform.localScale = new Vector2(1, 1);
                transform.Translate(Vector2.right * speed / 100);
            }
            else
            {
                //GetComponent<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector2(-1, 1);
                transform.Translate(-Vector2.right * speed / 100);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attack" && canHurt == true)
        {
            //hp -= other.GetComponent<Damage>().damage;
            hp -= 1;
            StartCoroutine(Hurt());
        }
    }

    IEnumerator Hurt()
    {
        if (hp > 0)
        {
            canHurt = false;
            yield return new WaitForSeconds(0.1f);
            canHurt = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
