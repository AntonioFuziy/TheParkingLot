using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillingCarScript : MonoBehaviour
{
    private GameObject pointA;
    private GameObject pointB;
    private GameObject car;
    public string targetPoint;
    private float step;
    private float speed;
    private Vector3 alturaY;
    private GameObject playerDialog;
    public AudioClip crashSound;
    private bool doItOnce;

    void Start()
    {
        playerDialog = GameObject.Find("playerDialog");
        car = this.transform.GetChild(0).gameObject;
        pointA = this.transform.GetChild(1).gameObject;
        pointB = this.transform.GetChild(2).gameObject;
        car.transform.position = pointA.transform.position;
        targetPoint = "B";
        speed = 21f;
    }

    void Update()
    {
        
        if(targetPoint == "B"){
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" I think I hear something ", 10f);
            step =  speed * Time.deltaTime;
            car.transform.position = Vector3.MoveTowards(car.transform.position, pointB.transform.position, step);
            alturaY = new Vector3 (car.transform.position.x,25.5f,car.transform.position.z);
            car.transform.position = alturaY;
            if (Vector3.Distance(car.transform.position, pointB.transform.position) < 0.001f)
            {
                targetPoint = null;
            }
        }
        else{
            if(!doItOnce){
                car.GetComponent<AudioSource>().Stop();
                car.GetComponent<AudioSource>().loop = false;
                car.GetComponent<AudioSource>().clip = crashSound;
                car.GetComponent<AudioSource>().Play();
                doItOnce = true;
                playerDialog.GetComponent<playerTalkingScript>().SetDialog(" Holy sh*t, better find another way down ", 30f);
                car.layer = 0;
            }
            step = 0;
        }
        
    }
}
