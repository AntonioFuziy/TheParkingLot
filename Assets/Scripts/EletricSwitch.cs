using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricSwitch : Interactable
{
    public bool isSet = false;
    public GameObject gameManager;

    public override void OnFocus(){
        print("looking at :" + gameObject.name);
    }

    public override void OnInteract(){
        print("interacted with :" + gameObject.name);
        if(gameManager.GetComponent<GameManager>().estadoAtual == 3){
            isSet = true;
        }
    }

    public override void OnLoseFocus(){
        print("stopped looking at :" + gameObject.name);
    }
}
