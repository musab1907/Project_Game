using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement:MonoBehaviour
{
    public JoystickController JoystickController;
    public CharacterController CharacterController;
    //public PlayerAnimator PlayerAnimator;
    Vector3 MoveVector;
    public float MoveSpeed = 5f; // Karakterin hareket hızı


    // Start is called before the first frame update
    void Start()
    {
        //character controller bileşenine erişim sağlamak için yazılan kod
        CharacterController=GetComponent<CharacterController>();
        JoystickController=GetComponent<JoystickController>();
       //PlayerAnimator=GetComponent<PlayerAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        // hareket vektörü belirleme
       MoveVector=JoystickController.GetMousePosition() * MoveSpeed * Time.deltaTime/Screen.width;    


        //HAREKETİ ATAMA
       MoveVector.z=MoveVector.y;
       MoveVector.y=0;
       CharacterController.Move(MoveVector);
       //PlayerAnimator.ManageAnimators(MoveVector);



    }
}
