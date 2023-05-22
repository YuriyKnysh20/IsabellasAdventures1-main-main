using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    public int lifeTime;

    private void Start()
    {
        if (transform.rotation.y == 0)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 25, ForceMode2D.Impulse);
        }
        else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 25, ForceMode2D.Impulse);

        }
        StartCoroutine(LifeTime());
    }
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }
    /*[SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float lifeTime;
    [SerializeField] private TriggerDamage triggerDamage;
    private Player player;
    public float Force
    {
        get { return force; }
        set { force = value; }
    }
    public void Destroy(GameObject gameObject)
    {
        //GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().ReturnArrowToPool(this);
    }
    public void SetImpulse(Vector2 direction, float force, int extraDamage, Player player)
    {
        this.player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject;
        triggerDamage.Damage += extraDamage;
        rb.AddForce(direction * force, ForceMode2D.Impulse);
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLife());
    }
    private IEnumerator StartLife()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
        yield break;
    }*/

}
	

