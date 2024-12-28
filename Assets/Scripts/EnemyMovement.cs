using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public int teamCount = 0;

    public Transform enemeyTransform;
    public float followSpeed = 5f;

    public Rigidbody rb;

    public Vector3 movement;

    public bool characterIsFriend = false;

    public static EnemyMovement instance;

    public int myHealth;

    void Awake(){
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame

    private void FixedUpdate() {
        LookAtPlayerFunction();
        EnemyAnimationController();
        //CharacterMovement();
    }
    void Update()
    {
        EnemyLayerControlSystem();
        //LookAtPlayerFunction();
        TrianglePositionController();
    }

    public void EnemyAnimationController(){
        if(GameManager.instance.gameIsStarted == true){
            GetComponent<Animator>().SetBool("enemyPlay",true);
        }
    }

    public void EnemyLayerControlSystem(){
        if(transform.GetComponent<EnemyMovement>().myHealth == 0){            
            //transform.position = new Vector3(GameManager.instance.player.transform.position.x,GameManager.instance.player.transform.position.y,GameManager.instance.player.transform.position.z - 2);
            //transform.DOScale(1.4f,1);
            //GetComponent<Animator>().SetBool("fail",false);
            //transform.LookAt(GameManager.instance.player.transform,Vector3.zero);

            //transform.LookAt(GameManager.instance.player.transform);
            
            characterIsFriend = true;
        }
    }

    public void LookAtPlayerFunction(){
        if(gameObject.layer == 0){
            //DOVirtual.DelayedCall(0.3f,LookPlayer);
            LookPlayer();
            GetComponent<EnemyRandomMovement>().enabled = false;
            //gameObject.GetComponent<NavMeshAgent>().speed = 15;
        }
    }

    public void LookPlayer(){
           transform.LookAt(GameManager.instance.player.transform);

           //transform.position = GameManager.instance.player.transform.localPosition;
           
           transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,GameManager.instance.player.transform.localPosition.z - 2),GameManager.instance.player.transform.position,5 * Time.deltaTime);
           //**Mathf.Clamp(transform.position.z,GameManager.instance.player.transform.position.z - 2, GameManager.instance.player.transform.position.z -5);

           //GetComponent<CapsuleCollider>().enabled = true;

           //GetComponent<NavMeshAgent>().SetDestination(GameManager.instance.player.transform.position);

           //GetComponent<CapsuleCollider>().enabled = true;

           //GetComponent<CapsuleCollider>().isTrigger = true;//Burası tekrardan eklenmeli

           //GetComponent<Animator>().SetBool("fail",true);
    }

    public void CharacterMovement(){
        if(gameObject.layer != 0){
            //transform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
        }
    }

    public void TrianglePositionController(){
        if(PlayerController.instance.playerMyHealth + 1 >= gameObject.transform.GetComponent<EnemyMovement>().myHealth){
            GameManager.instance.greenTriangle.transform.position = new Vector3(transform.localPosition.x,transform.localScale.y + 10,transform.localPosition.z);
        }
    }


}
