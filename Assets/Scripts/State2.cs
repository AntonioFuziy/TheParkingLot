using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State2 : MonoBehaviour
{
    public bool isDone;
    public GameObject luzesPostes;
    public GameObject luzesCarros;
    private GameObject playerDialog;
    private GameManager gameManager;
    private bool doItOnce;
    private GameObject allBlocks;
    private int bloco;
    private GameObject lightBlock1;
    private GameObject lightBlock2;
    private GameObject lightBlock3;
    private GameObject lightBlock4;
    private AudioSource audioSource;
    public AudioClip lightsOutSound;
    private GameObject music;

    void OnEnable() {
        
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");
        playerDialog.GetComponent<playerTalkingScript>().SetDialog(" WHO IS THERE? THIS IS NOT FUNNY ",10f);

        luzesPostes.SetActive(false);

        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = lightsOutSound;
        audioSource.Play();
        
        bloco = gameManager.blocoAtual;
        allBlocks = GameObject.Find("CarLightsEstado1");
        lightBlock1 = allBlocks.transform.GetChild(0).gameObject;
        lightBlock2 = allBlocks.transform.GetChild(1).gameObject;
        lightBlock3 = allBlocks.transform.GetChild(2).gameObject;
        lightBlock4 = allBlocks.transform.GetChild(3).gameObject;

    }

    void Start(){
        music = GameObject.Find("Music");
        music.GetComponent<MusicScript>().audioSource.Stop();
        music.GetComponent<MusicScript>().setAudioClip(2);
        music.GetComponent<MusicScript>().audioSource.Play();
    }

    void Update()
    {
        if(!doItOnce){
            if(gameManager.blocoAtual == 1){

                lightBlock3.GetComponent<blinkLights>().callBlink = true;
                lightBlock3.GetComponent<AudioSource>().Play();
            }
            else if(gameManager.blocoAtual  == 2){

                lightBlock4.GetComponent<blinkLights>().callBlink = true;
                lightBlock4.GetComponent<AudioSource>().Play();
            }
            else if(gameManager.blocoAtual  == 3){

                lightBlock1.GetComponent<blinkLights>().callBlink = true;
                lightBlock1.GetComponent<AudioSource>().Play();
            }
            else if(gameManager.blocoAtual  == 4){
                lightBlock2.GetComponent<blinkLights>().callBlink = true;
                lightBlock2.GetComponent<AudioSource>().Play();
            }
        }


        if(gameManager.blocoAtual != bloco && !doItOnce){

            if(gameManager.blocoAtual == 1){
                gameManager.InstantiateMonster( new Vector3(25f,26f,-10f) , 15f , 0.5f);
            }
            else if(gameManager.blocoAtual == 2){
                gameManager.InstantiateMonster( new Vector3(25f,26f,20f) , 15f , 0.5f);
            }
            else if(gameManager.blocoAtual == 3){
                gameManager.InstantiateMonster( new Vector3(-25f,26f,12.5f) , 15f , 0.5f);
            }
            else if(gameManager.blocoAtual == 4){
                gameManager.InstantiateMonster( new Vector3(-25f,26f,-15f) , 15f , 0.5f);
            }

            gameManager.goalText.text = "RUN";
            playerDialog.GetComponent<playerTalkingScript>().SetDialog("  ", 1f);
            luzesCarros.SetActive(true);
            doItOnce = true;
            isDone = true;
        }
    }
}
