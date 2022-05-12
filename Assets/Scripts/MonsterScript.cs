using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed;
    public float timeToDestroy;
    public float stopTimer;
    private AudioSource adSource;
    void Start()
    {
        moveSpeed = 0f;
        player = GameObject.Find("Player");
        adSource = this.GetComponent<AudioSource>();
    }
    void Update()
    {
        var step =  moveSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.position = new Vector3 (transform.position.x,30.3f,transform.position.z);
        if (Vector3.Distance(transform.position, player.transform.position) > 7f){
            transform.LookAt(player.transform);
        }
        
        stopTimer -= Time.deltaTime;
        if(stopTimer <= 0){
            moveSpeed = 5.5f;
        }

        if (Vector3.Distance(transform.position, player.transform.position) < 5f && moveSpeed > 1f)
        {
            player.GetComponent<playerMovement>().isDead = true;
        }

        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0){
            Destroy(this.gameObject);
        }
    }
}
