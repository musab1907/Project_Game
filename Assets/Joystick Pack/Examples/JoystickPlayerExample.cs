using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayerExample : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private VariableJoystick variableJoystick;
    [SerializeField] private Animator anim = null;

    private float newAngleX;

    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
        transform.localPosition += direction * speed * Time.deltaTime;

        newAngleX = Mathf.Atan2(variableJoystick.Horizontal, variableJoystick.Vertical) * Mathf.Rad2Deg;

        if (variableJoystick.Horizontal != 0 || variableJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, newAngleX, 0));
            anim.SetBool("Walk", true);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
            anim.SetBool("Walk", false);
        }
    }
}