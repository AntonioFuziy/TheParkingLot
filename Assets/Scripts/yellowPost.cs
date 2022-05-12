using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowPost : Interactable
{

    private GameObject playerDialog;
    public override void OnFocus(){
        print("looking at :" + gameObject.name);
    }

    public override void OnInteract(){
        print("interacted with :" + gameObject.name);
        playerDialog = GameObject.Find("playerDialog");
        playerDialog.GetComponent<playerTalkingScript>().SetDialog(" I should try to get into my car, what could go wrong? ", 10f);
    }

    public override void OnLoseFocus(){
        print("stopped looking at :" + gameObject.name);
    }
}
