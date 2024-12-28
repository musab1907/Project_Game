using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyStateController : MonoBehaviour
{
    // Start is called before the first frame update

    public int myNumber;

    public int enemyCount;

    public static EnemyStateController instance;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ActiveControlFunction();
        //CharacterAnimationController();
        CrashMeFunction();
        FriendsBallActivated();
    }

    public void ActiveControlFunction(){
        if(gameObject.layer == 0){
            DOVirtual.DelayedCall(0.6f,EnableControlFunstion);
        }
    }

    public void EnableControlFunstion(){
        transform.gameObject.SetActive(false);
        //GameManager.instance.friends[gameObject.GetComponent<EnemyStateController>().myNumber].SetActive(true);
    }

    public void CrashMeFunction(){
        if(gameObject.layer == 0){
            //UIManager.instance.friendCount++;
            gameObject.layer = 1;
            //PlayerController.instance.enemyCounter--;//Burada enemyCounter azaltılıyor.Yani düşman sayı kontrolü
            EnemyNumberController.instance.leftEnemyCount--;//Burada kalan düşman sayısı azalıyor
            EnemyNumberController.instance.myFriendCount++;
            //UIManager.instance.transform.DORewind();
            //UIManager.instance.leftEnemyNumber.transform.DOPunchScale(new Vector3(1,1,1),0.2F);
        }
    }
    
    
    
    public void FriendsBallActivated(){
        //if(UIManager.instance.friendCount > 0){
          //UIManager.instance.redBalls[UIManager.instance.friendCount - 1].SetActive(false);
           //UIManager.instance.blueBalls[UIManager.instance.friendCount - 1].SetActive(true);
        //}
    }

    /*
    public void CharacterAnimationController(){
        //Yanımıza aldığımız karakterler eğer josticke basılı ise hareket edecek değilse etmeyecek
        if(Movement.instance.isPressedJoystick == true){
            GameManager.instance.friends[gameObject.GetComponent<EnemyStateController>().myNumber].GetComponent<Animator>().SetBool("walk",true);
        }
        else{
            GameManager.instance.friends[gameObject.GetComponent<EnemyStateController>().myNumber].GetComponent<Animator>().SetBool("walk",false);
        }
    }
    */

}
