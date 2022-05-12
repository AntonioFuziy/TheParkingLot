using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State3 : MonoBehaviour
{
    public bool isDone;
    public GameObject luzesPostes;
    public GameObject luzesCarros;
    public GameObject painelEletrico;
    private GameObject playerDialog;
    private GameManager gameManager;
    private bool doItOnce;
    private bool doItOnce2;
    private bool monsterIsGone;
    public GameObject staticCar;
    private GameObject carObj;
    private GameObject music;

    void Start()
    {
        gameManager = this.GetComponent<GameManager>();
        playerDialog = GameObject.Find("playerDialog");

        music = GameObject.Find("Music");
        music.GetComponent<MusicScript>().audioSource.Stop();
        music.GetComponent<MusicScript>().setAudioClip(3);
        music.GetComponent<MusicScript>().audioSource.Play();
    }

    void OnEnable() 
    {

    }

    void Update()
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Death");   

        if(taggedObjects.Length == 0 && !doItOnce2){
            luzesCarros.SetActive(false);
            monsterIsGone = true;
            playerDialog.GetComponent<playerTalkingScript>().SetDialog(" I need to get the hell out of here ", 20f);
            gameManager.goalText.text = "Get the hell out of there";
            doItOnce2 = true;
        }

        if(monsterIsGone){
            if(painelEletrico.GetComponent<EletricSwitch>().isSet && !doItOnce){
                luzesPostes.SetActive(true);
                carObj =Instantiate(staticCar,new Vector3(44f,25.5f,28f),Quaternion.identity);
                //carObj.transform.eulerAngles = new Vector3(0,270,0) ;
                doItOnce = true;
                isDone = true;
            }
        }
    }
}
