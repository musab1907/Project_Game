using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public JoystickController joystickController;
    public CharacterController characterController;
    //public PlayerAnimator PlayerAnimator;
    Vector3 moveVector;
    public float moveSpeed = 5f; // Karakterin hareket hızı


    // Start is called before the first frame update
    void Start()
    {
        //character controller bileşenine erişim sağlamak için yazılan kod
        characterController = GetComponent<CharacterController>();
        joystickController = GetComponent<JoystickController>();
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
        moveVector = joystickController.GetMousePosition() * moveSpeed * Time.deltaTime / Screen.width;


        //HAREKETİ ATAMA
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
        //PlayerAnimator.ManageAnimators(MoveVector);



    }
}
