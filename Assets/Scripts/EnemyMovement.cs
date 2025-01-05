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
       
    }
    void Update()
    {
        EnemyLayerControlSystem();
        
        TrianglePositionController();
    }

    public void EnemyAnimationController(){
        if(GameManager.instance.gameIsStarted == true){
            GetComponent<Animator>().SetBool("enemyPlay",true);
        }
    }

    public void EnemyLayerControlSystem(){
        if(transform.GetComponent<EnemyMovement>().myHealth == 0){            
          
            characterIsFriend = true;
        }
    }

    public void LookAtPlayerFunction(){
        if(gameObject.layer == 0){
           
            LookPlayer();
            GetComponent<EnemyRandomMovement>().enabled = false;
           
        }
    }

    public void LookPlayer(){
           transform.LookAt(GameManager.instance.player.transform);

          
           
           transform.position = Vector3.MoveTowards(new Vector3(transform.position.x,transform.position.y,GameManager.instance.player.transform.localPosition.z - 2),GameManager.instance.player.transform.position,5 * Time.deltaTime);
           
    }

    public void CharacterMovement(){
        if(gameObject.layer != 0){
            
        }
    }

    public void TrianglePositionController(){
        if(PlayerController.instance.playerMyHealth + 1 >= gameObject.transform.GetComponent<EnemyMovement>().myHealth){
            GameManager.instance.greenTriangle.transform.position = new Vector3(transform.localPosition.x,transform.localScale.y + 10,transform.localPosition.z);
        }
    }


}
