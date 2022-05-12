using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalMonsterScript : MonoBehaviour
{
    private GameObject player;
    public float moveSpeed;
    public float stopTimer;
    private AudioSource adSource;
    public AudioClip[] adClips;
    void Start()
    {
        moveSpeed = 0f;
        player = GameObject.Find("Player");
        adSource = this.GetComponent<AudioSource>();
        StartCoroutine(playRandomAudio());
        stopTimer = 8f;
    }
    void Update()
    {
        var step =  moveSpeed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        transform.position = new Vector3 (transform.position.x,17.5f,transform.position.z);

        stopTimer -= Time.deltaTime;

        if (Vector3.Distance(transform.position, player.transform.position) > 7f){
            transform.LookAt(player.transform);
        }

        if(stopTimer <= 0){
            
            moveSpeed = 10f;
            if (Vector3.Distance(transform.position, player.transform.position) < 5f)
            {
                player.GetComponent<playerMovement>().isDead = true;
            }
        }
    }

    IEnumerator playRandomAudio()
    {
        yield return null;

        while(!player.GetComponent<playerMovement>().isDead)
        {
            adSource.clip = adClips[Random.Range(0,adClips.Length)];

            adSource.Play();

            while (adSource.isPlaying)
            {
                yield return null;
            }
        }
    }
}
