using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJoystickMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public Joystick joystick;
    Vector3 moveVector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //moveVector = new Vector3(joystick.Horizontal,0,joystick.Vertical);
        if(GetComponent<CapsuleCollider>().isTrigger == true){
        if(joystick.Horizontal != 0 || joystick.Vertical != 0){
            //transform.rotation = Quaternion.LookRotation(moveVector);
            GetComponent<Animator>().SetBool("fail",true);
        }
        else{
            GetComponent<Animator>().SetBool("fail",false);
        }
        Debug.Log("Joystick doğru çalışıyor");
       }
    }
}
