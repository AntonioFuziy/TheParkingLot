using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCars : MonoBehaviour
{
    private GameObject player;
    private GameManager gameManager;
    public bool forwardDir;
    public Vector3 targePos;
    private float step;
    private float speed;
    private Vector3 alturaY;
    private bool move;
    private float randomNum;
    private GameObject lights;
    public AudioSource audioSource;
    private bool doItOnce;

    private bool doItOnce2;

    void Start(){
        audioSource = this.GetComponent<AudioSource>();
        speed = 8f;
        lights = this.transform.GetChild(0).gameObject;
        lights.SetActive(false);
        randomNum = Random.Range(2.5f,5.5f);
        if(forwardDir){
            targePos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z + randomNum);
        }
        else{
            targePos = new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z - randomNum);
        }
        player = GameObject.Find("Player");
        gameManager =  GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.estadoAtual == 4){
            if(Vector3.Distance(this.transform.position, player.transform.position) < 15f){
                move = true;
            }
        }
        if(move){   
            step =  speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targePos, step);
            alturaY = new Vector3 (this.transform.position.x,25.5f,this.transform.position.z);
            this.transform.position = alturaY;
            lights.SetActive(true);
            if(!doItOnce){
                audioSource.loop = false;
                audioSource.Play();
                doItOnce = true;
            }
            if (Vector3.Distance(this.transform.position, targePos) < 0.001f)
            {
                step = 0;
                lights.SetActive(false);
            }
        }
    }
}
