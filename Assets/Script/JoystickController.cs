using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;


public class JoystickController : MonoBehaviour

{

    public RectTransform joystickOutline;
    public RectTransform joystickButton;
    private bool CanControlJoystick;
    private Vector3 tapPosition;
    public float moveFactory = 10f;
    private Vector3 move;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void TappedOnJoystickZone()
    {
        Debug.Log("ekrana dokunuldu");
        //Ekrana bastığımız anda joystick'in ekranda belirmesi
        tapPosition = Input.mousePosition;
        joystickOutline.position = tapPosition;
        ShowJoystick();
    }

    public void ShowJoystick()
    {
        //joystick gözükmesi için
        joystickOutline.gameObject.SetActive(true);
        CanControlJoystick = true;
    }

    public void HideJoystick()
    {
        //joystick gözükmemesi için 
        joystickOutline.gameObject.SetActive(false);
        CanControlJoystick = false;
        move = Vector3.zero;
    }

    public void ControlJoystick()
    {
        //joystiğin ekranın herhangi bir yerinde belirmesi 
        Vector3 currentposition = Input.mousePosition;
        Vector3 direction = currentposition - tapPosition;
        joystickButton.position = direction;

        float moveMagnitude = direction.magnitude * moveFactory / Screen.width;
        moveMagnitude = Math.Min(moveMagnitude, joystickOutline.rect.width / 2);
        move = direction.normalized * moveMagnitude;

        Vector3 targetpos = tapPosition + move;
        joystickButton.position = targetpos;

        //joystick ile karakteri kontrol etmek
        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }

    }
    public Vector3 GetMousePosition()
    {
        return move;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanControlJoystick)
        {
            ControlJoystick();
        }

    }
}
