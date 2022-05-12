using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAllLights : MonoBehaviour
{
    private float startCooldownOn = 0.2f;
    private float cooldownOn;
    private float startCooldownOff = 0.1f;
    private float cooldownOff;
    private GameObject lightObj;
    private bool on;
    private AudioSource audioSource;
    


    void Start()
    {
        lightObj = this.gameObject.transform.GetChild(0).gameObject;
        lightObj.SetActive(false);
        on = false;
        cooldownOff = startCooldownOff;
        cooldownOn = startCooldownOn;
    }

    void OnEnable() {
        audioSource = this.GetComponent<AudioSource>();
        audioSource.Play();
    }

    void Update()
    {
        if(!on){
            cooldownOn -= Time.deltaTime;
            if(cooldownOn <= 0){
                cooldownOn = startCooldownOn;
                lightObj.SetActive(true);
                on = true;
            }
        }
        if(on){
            cooldownOff -= Time.deltaTime;
            if(cooldownOff <= 0){
                cooldownOff = startCooldownOff;
                lightObj.SetActive(false);
                on = false;
            }
        }
    }
}
