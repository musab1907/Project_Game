using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SwerveInput : MonoBehaviour
{
    // Start is called before the first frame update
    
    private float lastMoveX;
    private  float moveX;

    public static float speed = 15;

    public float moveFactorX => moveX;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * 5 * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime); 
        if(Input.GetMouseButtonDown(0)){
            lastMoveX = Input.mousePosition.x;
        }

        else if(Input.GetMouseButton(0)){
            moveX = Input.mousePosition.x - lastMoveX;
            lastMoveX = Input.mousePosition.x;
        }

        else if(Input.GetMouseButtonUp(0)){
            moveX = 0;
        }

        

    }

}
