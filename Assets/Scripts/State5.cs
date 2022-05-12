using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State5 : MonoBehaviour
{
    private GameObject playerDialog;
    private GameManager gameManager;
    private GameObject music;
    public GameObject playerCar;
    private GameObject player;
    public GameObject lightL;
    public bool isDone;
    public GameObject cirleOfMonsters;
    private bool doItOnce;
    private bool doItOnce2;

    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");

        music = GameObject.Find("Music");
        music.GetComponent<MusicScript>().audioSource.Stop();
        music.GetComponent<MusicScript>().setAudioClip(5);
        music.GetComponent<MusicScript>().audioSource.Play();

        player = GameObject.Find("Player");
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position, playerCar.transform.position) < 30f && !doItOnce2){
            gameManager.estadoAtual += 1;
            gameManager.goalText.text = " ";
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" Is that what I think it is? ", 5f);
            doItOnce2 = true;
        }

        if(playerCar.transform.GetChild(0).GetComponent<playerCar>().isSet){
            playerCar.transform.GetChild(0).gameObject.SetActive(false);
            playerCar.transform.GetChild(1).gameObject.SetActive(true);

            player.GetComponent<playerMovement>().noMovement = true;


            if(!doItOnce){
                gameManager.goalText.text = "DIE";
                Instantiate(cirleOfMonsters, new Vector3 (0,13f,0) , Quaternion.identity);
                doItOnce = true;
            }
        }
    }
}
