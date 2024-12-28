using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TapticPlugin;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public int moneyValue = 0;

    public int levelMoneyValue = 0;

    public int playerMyHealth = 1;

    public bool gameIsFinished = false;
    public GameObject Boss;
    //public int enemyCounter;

    //public GameObject finalDoor;

    //public GameObject finalDoorOpen;

    //public GameObject key;

    //public GameObject finalParticalSystem;

    //public int randomNumber = Random.Range(30,100);

    public static PlayerController instance;

    void Awake() {
        instance = this;
    }
    void Start()
    {
        //BossLevelControlSystem();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyFinalState();
        HealthSystem();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "money"){
            Destroy(other.gameObject);
            int randomMoneyValue = Random.RandomRange(30,100);
            levelMoneyValue += randomMoneyValue;
            moneyValue += randomMoneyValue;
        }

        //FinishController
        //FinishController

        if(other.gameObject.tag == "final"){
            SliderBarSystem.instance.barLevel.SetActive(false);
            //transform.DOMoveZ(transform.localPosition.z + 5,1f);
            //transform.DORotate(new Vector3(0,0,0), 0.3f, RotateMode.LocalAxisAdd);
        }

        if(other.gameObject.tag == "finish"){
            UIManager.instance.WinPanelFunction();
            gameIsFinished = true;
            //GameManager.instance.cashCollect.SetActive(true);
            //GameManager.instance.cashCollect.GetComponent<Animator>().SetBool("collect",true);
            //Destroy(gameObject);
            GameManager.instance.player.GetComponent<Animator>().SetBool("victory",true);
            GameManager.instance.player.GetComponent<Movement>().enabled = false;
            EnemyNumberController.instance.finalParticalSystem.GetComponent<ParticleSystem>().Play();
            GameManager.instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.GetChild(0).GetChild(3).gameObject.SetActive(false);//Raycasti kapatıyoruz
            /*
            for(int i = 0; i < 7; i++){
                GameManager.instance.friends[i].GetComponent<Animator>().SetBool("victoryFriend",true);//Tüm friendlerin sevinmesi için yapılan animasyon
            }
            */
        }

    

        if(other.gameObject.tag == "key"){
            EnemyNumberController.instance.finalDoorOpen.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            EnemyNumberController.instance.finalDoorOpen.transform.GetChild(1).GetComponent<Animator>().enabled = true;
            other.gameObject.transform.DOScale(0,0);
            TapticPlugin.TapticManager.Impact(ImpactFeedback.Heavy);
            EnemyNumberController.instance.finalDoor.SetActive(true);
        }

        if(other.gameObject.tag == "bomb"){
            Debug.Log("Bombaya çarptım");
            BombEffect.instance.bombPartical.Play();
            Destroy(other.gameObject);
            for(int i = 0; i < BombEffect.instance.wallCrash.transform.childCount - 1; i++){
                Debug.Log("Bombaya çarptım");
            int randomValueX = Random.RandomRange(0,3);
            int randomValueY = Random.RandomRange(0,3);
            int randomValueZ = Random.RandomRange(0,3);
            //ombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            //BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(new Vector3(1,1,1),ForceMode.Impulse);
            BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = false;
            BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.up * 2.5f,ForceMode.Impulse);
            BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.forward * 2.5f,ForceMode.Impulse); 
            BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.back * 0.5f,ForceMode.Impulse); 
            BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<Rigidbody>().AddForce(Vector3.right * 0.5f,ForceMode.Impulse); 

            //BombEffect.instance.wallCrash.transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
        }

        }
    }

    public void EnemyFinalState(){
          if(EnemyNumberController.instance.leftEnemyCount == 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
            //GameManager.instance.finalDoorPartical.Play();//Finishteki partical aktif olacak
            GameManager.instance.player.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
            Debug.Log("Biter");
            //GameObject.Find("Key").gameObject.SetActive(true);
            EnemyNumberController.instance.key.SetActive(true);
            EnemyNumberController.instance.finalParticalSystem.SetActive(true);
            GameManager.instance.greenTriangle.SetActive(false);
            //finalDoorOpen.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            //finalDoorOpen.transform.GetChild(1).GetComponent<Animator>().enabled = true;
        }

        else if(EnemyNumberController.instance.leftEnemyCount == 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<BossMovement>().bossMyHeath <= 0){
            //GameManager.instance.finalDoorPartical.Play();//Finishteki partical aktif olacak
            GameManager.instance.player.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            Debug.Log("Biter");
            //GameObject.Find("Key").gameObject.SetActive(true);
            EnemyNumberController.instance.key.SetActive(true);
            EnemyNumberController.instance.finalParticalSystem.SetActive(true);
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().acceleration = 0;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().speed = 0;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().angularSpeed = 0;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().autoBraking = false;
            GameManager.instance.greenTriangle.SetActive(false);
            //finalDoorOpen.transform.GetChild(0).GetComponent<Animator>().enabled = true;
            //finalDoorOpen.transform.GetChild(1).GetComponent<Animator>().enabled = true;
        }
    }

    public void BossLevelControlSystem(){
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true){
            Debug.Log("Burada Boss var");
            GameManager.instance.FollowMainCamera.SetActive(false);;
            GameManager.instance.FollowBossCamera.SetActive(true);
            //GameManager.instance.LookSkyCamera.SetActive(false);
        }
        else if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
            Debug.Log("Burada boss yok");
        }
        
    }
    
    public void BossSplattonEffect(){
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true && Movement.instance.isPressedJoystick == true){
            //GameManager.instance.splatonParticle.Play();
        }
    }

    public void HealthSystem(){
        if(SliderBarSystem.instance.currentBarValue == 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).transform.gameObject.active == false){
            Debug.Log("COLOR IS RED");
            GameManager.instance.player.GetComponent<Movement>().enabled = false;
            GameManager.instance.player.GetComponent<JoystickPlayerExample>().enabled = false;
            GameManager.instance.player.GetComponent<Animator>().SetBool("fail",true);
            GameManager.instance.player.GetComponent<Animator>().SetBool("play",false);
            GameManager.instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.enemyMaterial;
            UIManager.instance.FailPanelFunction();
            for(int i = 2; i < transform.childCount; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
            GameObject.Find("Enemys").SetActive(false);
            SliderBarSystem.instance.currentBarValue = 1;
        }
        else if(SliderBarSystem.instance.currentBarValue == 0 && GameObject.Find("EnemyBoss").transform.GetChild(0).transform.gameObject.active == true || transform.localScale.x < 0){
            Debug.Log("COLOR IS RED");
            GameManager.instance.player.transform.GetChild(0).transform.GetChild(5).GetComponent<FieldOfView>().enabled = false;
            GameManager.instance.player.GetComponent<Movement>().enabled = false;
            GameManager.instance.player.GetComponent<JoystickPlayerExample>().enabled = false;
            GameManager.instance.player.GetComponent<Animator>().SetBool("fail",true);
            GameManager.instance.player.GetComponent<Animator>().SetBool("play",false);
            GameManager.instance.player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material = GameManager.instance.enemyMaterial;
            UIManager.instance.FailPanelFunction();
            for(int i = 2; i < transform.childCount; i++){
                transform.GetChild(i).gameObject.SetActive(false);
            }
            //GameObject.Find("Enemys").SetActive(false);
            SliderBarSystem.instance.currentBarValue = 1;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<Animator>().SetBool("win",true);
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().speed = 0;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().acceleration = 0;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<EnemyRandomMovement>().enabled = false;
            GameObject.Find("EnemyBoss").transform.GetChild(0).transform.GetChild(2).gameObject.GetComponent<EnemyFieldOfView>().enabled = false;
            gameObject.layer = 0;
            GameObject.Find("EnemyBoss").gameObject.layer = 0;
            //GameObject.Find("EnemyBoss").transform.GetChild(0).GetComponent<NavMeshAgent>().enabled = false;
        }
    }
  

}
