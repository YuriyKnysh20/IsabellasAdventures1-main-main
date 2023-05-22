using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfDie : MonoBehaviour
{
    public AudioClip[] din;
    private int audioCount, audioId;
    public int Enemyhealth = 50;
    // Start is called before the first frame update
    private void PlaySlashAudio()
    {
        audioCount = din.Length;
        audioId = Random.Range(0, audioCount);
        gameObject.GetComponent<AudioSource>().PlayOneShot(din[audioId]);
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void TakeDamage(int amount)
    {
        Enemyhealth -= amount;
        if (Enemyhealth <= 0)
        {
            PlaySlashAudio();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //Instantiate(bloodSpot, transform.position, Quaternion.identity);
            PlaySlashAudio();
        }
    }
}
