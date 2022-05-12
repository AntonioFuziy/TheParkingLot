using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButtons : Interactable
{
    public bool isSet = false;
    public GameObject gameManager;
    private GameObject playerDialog;

    public override void OnFocus(){
        print("looking at :" + gameObject.name);
    }

    public override void OnInteract(){
        print("interacted with :" + gameObject.name);
        if(gameManager.GetComponent<GameManager>().estadoAtual == 1){
            playerDialog = GameObject.Find("playerDialog");
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" I should really find my car ",10f);
        }
        else if(gameManager.GetComponent<GameManager>().estadoAtual == 3){
            playerDialog = GameObject.Find("playerDialog");
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" I need to turn on the power before using the elevator ",15f);
            gameManager.GetComponent<GameManager>().goalText.text = "Turn on the power";
        }
        else if(gameManager.GetComponent<GameManager>().estadoAtual == 4){
            playerDialog = GameObject.Find("playerDialog");
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" Hurry up stupid elevator ",10f);
            isSet = true;
        }

        else if(gameManager.GetComponent<GameManager>().estadoAtual >= 5){
            playerDialog = GameObject.Find("playerDialog");
            isSet = false;
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" The elevator seems to be broken ",10f);
        }
    }

    public override void OnLoseFocus(){
        print("stopped looking at :" + gameObject.name);
    }
}
