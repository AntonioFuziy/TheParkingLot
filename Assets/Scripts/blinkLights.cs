using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blinkLights : MonoBehaviour
{
    private float startCooldownOn = 0.1f;
    private float cooldownOn;
    private float startCooldownOff = 0.05f;
    private float cooldownOff;
    private int startNumCount = 2;
    private int numCount;
    private GameObject lightObj;
    private bool on;
    public bool callBlink;
    


    void Start()
    {
        lightObj = this.gameObject.transform.GetChild(0).gameObject;
        lightObj.SetActive(false);
        on = false;
        callBlink = false;
        cooldownOff = startCooldownOff;
        cooldownOn = startCooldownOn;
        numCount = startNumCount;
    }

    void Update()
    {
        if(callBlink){
            if(numCount > 0){
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
                        numCount -= 1;
                    }
                }
            }
            else{
                numCount = startNumCount;
                callBlink = false;
            }
        }
    }
}
