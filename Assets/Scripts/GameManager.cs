using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private State1 state1Script;
    private GameObject player;
    public int blocoAtual;
    public int estadoAtual;
    public GameObject Monster;
    private GameObject tempMonster;
    public Text goalText;

    private GameObject music;

    void OnAwake(){
        this.GetComponent<State1>().enabled = true;
        this.GetComponent<State2>().enabled = false;
        this.GetComponent<State3>().enabled = false;
    }
    void Start()
    {
        state1Script = this.GetComponent<State1>();
        player = GameObject.Find("Player");
        blocoAtual = 1;
        estadoAtual = 1;
    }

    void Update()
    {
        if(player.GetComponent<playerMovement>().isDead){
            if(estadoAtual > 5){
                Initiate.Fade("Credits",Color.black, 1f);
            }
            else{
                Initiate.Fade("restartScene",Color.black, 1f);
            }
        }

        if(player.transform.position.x < 50 && player.transform.position.x > 5 && player.transform.position.z > -50 && player.transform.position.z < -5){
            blocoAtual = 1;
        }
        else if(player.transform.position.x < 50 && player.transform.position.x > 5 && player.transform.position.z < 50 && player.transform.position.z > 5){
            blocoAtual = 2;
        }
        else if(player.transform.position.x > -50 && player.transform.position.x < -5 && player.transform.position.z < 50 && player.transform.position.z > 5){
            blocoAtual = 3;
        }
        else if(player.transform.position.x > -50 && player.transform.position.x < -5 && player.transform.position.z > -50 && player.transform.position.z < -5){
            blocoAtual = 4;
        }

        if(estadoAtual == 1){
            this.GetComponent<State1>().enabled = true;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State3>().enabled = false;
            this.GetComponent<State4>().enabled = false;
            this.GetComponent<State5>().enabled = false;
            //setar false os outros estados
            if(this.GetComponent<State1>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual == 2){
            this.GetComponent<State3>().enabled = false;
            this.GetComponent<State2>().enabled = true;
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State4>().enabled = false;
            this.GetComponent<State5>().enabled = false;
            //setar false os outros estados
            if(this.GetComponent<State2>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual == 3){
            this.GetComponent<State3>().enabled = true;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State4>().enabled = false;
            this.GetComponent<State5>().enabled = false;
            //setar false os outros estados
            if(this.GetComponent<State3>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual == 4){
            this.GetComponent<State4>().enabled = true;
            this.GetComponent<State3>().enabled = false;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State5>().enabled = false;
            //setar false os outros estadoss
            if(this.GetComponent<State4>().isDone){
                estadoAtual += 1;
            }
        }
        else if (estadoAtual >= 5){
            this.GetComponent<State4>().enabled = false;
            this.GetComponent<State3>().enabled = false;
            this.GetComponent<State2>().enabled = false;
            this.GetComponent<State1>().enabled = false;
            this.GetComponent<State5>().enabled = true;
            //setar false os outros estados
            if(this.GetComponent<State5>().isDone && estadoAtual < 8){
                estadoAtual += 1;
            }
        }
    }

    public void InstantiateMonster(Vector3 position , float timeToDie, float timeStop){
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Monster");   
        foreach (GameObject monster in taggedObjects) {
            Destroy(monster);
        }
        tempMonster = Instantiate(Monster, position, Quaternion.identity);
        tempMonster.GetComponent<MonsterScript>().timeToDestroy = timeToDie;
        tempMonster.GetComponent<MonsterScript>().stopTimer = timeStop;
    }
}
