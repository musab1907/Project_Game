using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update

    public Joystick joystick;
    Vector3 moveVector;

    public static Movement instance;

    public bool isPressedJoystick = false;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        //try
        //try
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = new Vector3(joystick.Horizontal,0,joystick.Vertical);

        if(joystick.Horizontal != 0 || joystick.Vertical != 0){
            transform.rotation = Quaternion.LookRotation(moveVector);
            GameManager.instance.player.GetComponent<Animator>().SetBool("play",true);
            isPressedJoystick = true;
            PlayerController.instance.BossSplattonEffect();
        }
        else{
            GameManager.instance.player.GetComponent<Animator>().SetBool("play",false);
            isPressedJoystick = false;
        }
    }
}
