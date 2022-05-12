using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State4 : MonoBehaviour
{
    public bool isDone;
    private GameObject playerDialog;
    private GameManager gameManager;
    private bool monsterIsGone;
    private bool doItOnce2;
    private bool doItOnce;
    public GameObject elevatorButton;
    public GameObject killingCar;
    public GameObject exitGate;
    private GameObject music;
    
    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");

        music = GameObject.Find("Music");
        music.GetComponent<MusicScript>().audioSource.Stop();
        music.GetComponent<MusicScript>().setAudioClip(4);
        music.GetComponent<MusicScript>().audioSource.Play();
    }

    void Update()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Monster");   

        if(taggedObjects.Length == 0 && !doItOnce2){
            monsterIsGone = true;
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" The power must be back now, I should try to use use the elevator ", 25f);
            gameManager.goalText.text = "Get the hell out of there";
            doItOnce2 = true;
        }

        if(monsterIsGone && !doItOnce){
            if(elevatorButton.GetComponent<ElevatorButtons>().isSet){
                Instantiate(killingCar, new Vector3(-46f,25.5f,36f) , Quaternion.identity );
                exitGate.SetActive(false);
                isDone = true;
                doItOnce = true;
            }
        }
    }
}
