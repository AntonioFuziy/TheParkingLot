using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State1 : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject allBlocks;
    private GameObject lightBlock1;
    private GameObject lightBlock2;
    private GameObject lightBlock3;
    private GameObject lightBlock4;
    public bool isDone;
    private int wrongCarsCount;
    private int targetBlock;
    private GameObject playerDialog;
    public GameObject luzesPostes;
    private GameObject music;
    private bool doItOnce;
    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        allBlocks = GameObject.Find("CarLightsEstado1");
        lightBlock1 = allBlocks.transform.GetChild(0).gameObject;
        lightBlock2 = allBlocks.transform.GetChild(1).gameObject;
        lightBlock3 = allBlocks.transform.GetChild(2).gameObject;
        lightBlock4 = allBlocks.transform.GetChild(3).gameObject;
        isDone = false;
        wrongCarsCount = 0;
        targetBlock = 0;
        playerDialog = GameObject.Find("playerDialog");
        playerDialog.GetComponent<playerTalkingScript>().SetDialog(" where did I park my car? ",10f);
        gameManager.goalText.text = "Find your car \n(Press K to use car keys) ";

        
    }


    void Update()
    {
        if(!doItOnce){
            music = GameObject.Find("Music");
            music.GetComponent<MusicScript>().setAudioClip(1);
            music.GetComponent<MusicScript>().audioSource.Play();
            doItOnce = true;
        }


        if(targetBlock == gameManager.blocoAtual){
            wrongCarsCount += 1;
            targetBlock = 0;
        }

        if(wrongCarsCount >= 3){
            isDone = true;
        }

        if(Input.GetKeyUp(KeyCode.K)){

            if(wrongCarsCount == 1){
                playerDialog.GetComponent<playerTalkingScript>().SetDialog(" What was that? I could swear it was there ",10f);
                gameManager.goalText.text = "Find your car";
            }
            else if(wrongCarsCount == 2){
                playerDialog.GetComponent<playerTalkingScript>().SetDialog(" Someone must be messing with me ",8f);
            }

            if(targetBlock == 0){
                if(gameManager.blocoAtual == 1){
                    targetBlock = 3;
                }
                else if(gameManager.blocoAtual == 2){
                    targetBlock = 4;
                }
                else if(gameManager.blocoAtual == 3){
                    targetBlock = 1;
                }
                else if(gameManager.blocoAtual == 4){
                    targetBlock = 2;
                }
            }

            if(targetBlock == 3){

                lightBlock3.GetComponent<blinkLights>().callBlink = true;
                lightBlock3.GetComponent<AudioSource>().Play();
            }
            else if(targetBlock == 4){

                lightBlock4.GetComponent<blinkLights>().callBlink = true;
                lightBlock4.GetComponent<AudioSource>().Play();
            }
            else if(targetBlock == 1){

                lightBlock1.GetComponent<blinkLights>().callBlink = true;
                lightBlock1.GetComponent<AudioSource>().Play();
            }
            else if(targetBlock == 2){
                lightBlock2.GetComponent<blinkLights>().callBlink = true;
                lightBlock2.GetComponent<AudioSource>().Play();
            }
        }
    }
}
