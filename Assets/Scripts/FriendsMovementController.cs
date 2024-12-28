using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendsMovementController : MonoBehaviour
{
    // Start is called before the first frame update

    public int friendMyNumber;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterAnimationController();
    }

    public void CharacterAnimationController(){
        //Yanımıza aldığımız karakterler eğer josticke basılı ise hareket edecek değilse etmeyecek
        if(Movement.instance.isPressedJoystick == true){
            GameManager.instance.friends[gameObject.GetComponent<FriendsMovementController>().friendMyNumber].GetComponent<Animator>().SetBool("walk",true);
        }
        else{
            GameManager.instance.friends[gameObject.GetComponent<FriendsMovementController>().friendMyNumber].GetComponent<Animator>().SetBool("walk",false);
        }
    }

}
