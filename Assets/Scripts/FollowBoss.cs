using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class FollowBoss : MonoBehaviour
{
    // Start is called before the first frame update

    NavMeshAgent agent;

    public Transform bossPosition;
    public bool safeZoneActivated = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        bossPosition.position = GameObject.Find("EnemyBoss").transform.GetChild(0).transform.position;
        
        if(agent != null && GameManager.instance.gameIsStarted == true ){//Eğer oyunun başla butonuna basıldıysa ve agent boş değilse bu işlemler yapılacal
            //agent.speed = speed;
            //agent.SetDestination(GameObject.Find("EnemyBoss").transform.GetChild(0).transform.position);
            gameObject.GetComponent<Animator>().SetBool("fail",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameIsStarted == true){
            BossFollow();
            PlayerScaleController();
        }
        

    }

    public void BossFollow(){
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true && transform.GetComponent<FollowBoss>().safeZoneActivated== false){
            //transform.DOMove(GameObject.Find("EnemyBoss").transform.GetChild(0).transform.position,5);
            //transform.LookAt(GameObject.Find("EnemyBoss").transform);
            transform.GetComponent<Animator>().SetBool("enemyPlay",true);
            Debug.Log("Burada hareket var");
            agent.SetDestination(bossPosition.position);
            agent.speed = 10;
            agent.acceleration = 15;
            agent.angularSpeed = 120;
            //transform.GetComponent<EnemyRandomMovement>().enabled = false;
        }
        else if(transform.GetComponent<FollowBoss>().safeZoneActivated == true){
            transform.GetComponent<Animator>().SetBool("enemyPlay",false);
            transform.GetComponent<Rigidbody>().isKinematic = true;
            //agent.SetDestination(transform.position);
        }
    }

    private void OnTriggerEnter(Collider other) {
       if(other.gameObject.tag == "safeZone"){
           Destroy(gameObject);
           BossMovement.instance.myFriendNumber--;
           transform.GetComponent<FollowBoss>().safeZoneActivated = true;
           GameObject.Find("EnemyBoss").transform.GetChild(0).transform.localScale += new Vector3(0.1f,0.1f,0.1f);
       }
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "safeZone"){
            //DOVirtual.DelayedCall(0.5f,DestroyObjectFunction);
        }
    }

    public void DestroyObjectFunction(){
        Destroy(gameObject);
        BossMovement.instance.myFriendNumber--;
    }
    private void OnCollisionEnter(Collision other) {
         if(other.gameObject.tag == "boss"){
            //Destroy(gameObject);
        }
    }

    
    public void PlayerScaleController(){
        if(GameManager.instance.player.transform.localScale.x >= GameObject.Find("EnemyBoss").transform.GetChild(0).transform.localScale.x && BossMovement.instance.myFriendNumber == 0 ){
            BossMovement.instance.myFriendNumber = 0;
            for(int i = 0; i < GameObject.Find("Enemys").transform.childCount -1; i++){
                GameObject.Find("Enemys").transform.GetChild(i).gameObject.SetActive(false);
            }
            GameObject.Find("EnemyBoss").transform.GetChild(0).transform.GetComponent<EnemyRandomMovement>().enabled = true;
            GameObject.Find("EnemyBoss").transform.GetChild(0).transform.GetComponent<Animator>().SetBool("fight",true);
            //GameObject.Find("EnemyBoss").transform.GetChild(0).transform.GetComponent<NavMeshAgent>().SetDestination(GameManager.instance.player.transform.position);
            GameObject.Find("EnemyBoss").transform.GetChild(0).transform.LookAt(GameManager.instance.player.transform);
        }
    }
    

}
