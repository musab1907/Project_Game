using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public static BossMovement instance;

    public bool bossIsMove = false;    

    public GameObject tutorialText;
    public GameObject Boss;
    public int bossMyHeath;

    public int myFriendNumber;

    public int textMyFriendNumber;

    public List<GameObject> myFriendList = new List<GameObject>();

    public GameObject safeZone;

    //public ParticleSystem bossPartical;
    public float speed = 2f;

    void Awake(){
        instance = this;
    }
    void Start()
    {
        transform.GetComponent<Animator>().SetBool("enemyPlay",true);
        //GameManager.instance.LookSkyCamera.SetActive(true);
        //GameManager.instance.FollowMainCamera.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HealthController();
        FollowPlayer();
        //bossPartical.Stop();
        MyFriendIsWhere();
        BossHealthController();
        //WinOrFailFunction();
    }

    void HealthController(){
        if(bossMyHeath <= 0){
            //gameObject.SetActive(false);
            //gameObject.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.friendMaterial;
            //gameObject.SetActive(false);
            gameObject.GetComponent<Animator>().SetBool("sad",true);
        }
    }

    public void FollowPlayer(){
        if(GameManager.instance.gameIsStarted == true){
            //transform.LookAt(GameManager.instance.player.transform.position);
            //transform.Translate(Vector3.forward * speed *Time.deltaTime);
            //transform.GetComponent<Animator>().SetBool("enemyPlay",true);
        }
    }

    public void MyFriendIsWhere(){
        if(myFriendNumber == 0 && GameManager.instance.player.layer == 8){
            //transform.GetComponent<Animator>().SetBool("enemyPlay",true);
            //transform.GetComponent<EnemyRandomMovement>().enabled = true;
            safeZone.SetActive(false);
            Debug.Log("Çalışıyorrrr");
            transform.GetComponent<NavMeshAgent>().SetDestination(GameManager.instance.player.transform.localPosition);
            transform.GetComponent<NavMeshAgent>().speed = 3;
            transform.GetComponent<Animator>().SetBool("fight",true);
            //transform.GetComponent<NavMeshAgent>().SetDestination(GameManager.instance.player.transform.position);,
            transform.LookAt(GameManager.instance.player.transform);
            gameObject.layer = 7;
            GameObject.Find("EnemyBoss").transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(true);
            //GameObject.Find("SafeZone").gameObject.SetActive(false);
        }
        else if(GameManager.instance.player.layer == 0){
            transform.GetComponent<NavMeshAgent>().SetDestination(transform.position);
        }
    }

    public void BossHealthController(){
        if(BossSliderBarSystem.instance.currentBarValue == 0){
            EnemyNumberController.instance.finalDoor.SetActive(true);
            EnemyNumberController.instance.key.SetActive(true);
            transform.gameObject.SetActive(false);
            //transform.GetComponent<Animator>().SetBool("fail",true);
        }
    }

        /*
       public void WinOrFailFunction(){
        if(myFriendNumber == 0 && GameManager.instance.player.transform.localScale.x < transform.localScale.x){
            Debug.Log("COLOR IS RED");
            GameManager.instance.player.GetComponent<Movement>().enabled = false;
            GameManager.instance.player.GetComponent<JoystickPlayerExample>().enabled = false;
            GameManager.instance.player.GetComponent<Animator>().SetBool("fail",true);
            GameManager.instance.player.GetComponent<Animator>().SetBool("play",false);
            GameManager.instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            GameManager.instance.player.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.enemyMaterial;
            UIManager.instance.FailPanelFunction();
            for(int i = 2; i < transform.childCount; i++){
                GameManager.instance.player.transform.GetChild(i).gameObject.SetActive(false);
            }
            GameObject.Find("Enemys").SetActive(false);
            SliderBarSystem.instance.currentBarValue = 1;
            myFriendNumber = 1;
            transform.GetComponent<NavMeshAgent>().speed = 0;
        }
    }
    */

}
