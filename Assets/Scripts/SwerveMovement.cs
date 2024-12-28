using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using DG.Tweening;

public class SwerveMovement : MonoBehaviour
{
    // Start is called before the first frame update
      private SwerveInput swerveInput;
      //GameManager gm;
      
        [SerializeField] private float swerveSpeed = 0.5f;
        [SerializeField] private float maxSwerveAmaount = 1f;   

        float swerveAmount;
        float moveX;
        
    


    private void Awake() {
        swerveInput = GetComponent<SwerveInput>();
    }
    // Update is called once per frame
    void Update()
    {
        swerveAmount = Time.deltaTime * swerveSpeed * swerveInput.moveFactorX;
        swerveAmount = Mathf.Clamp(swerveAmount,-1,1);

        //moveX = Mathf.Clamp(moveX,-5,5);
        //Debug.Log(swerveAmount)
        
        transform.Translate(swerveAmount,0,0);
       // transform.DOMoveX(swerveAmount,0.1f);
        //transform.position = new Vector3(swerveAmount,0,0);
        /*
        transform.DOMoveX(swerveAmount,0.2f);
        transform.DOMoveY(0,0);
        transform.DOMoveZ(0,0);
        */
        //transform.position = Vector3.Lerp(transform.position,new Vector3(swerveAmount,0,0),1f);
        
        transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x,-5,5),transform.localPosition.y,transform.localPosition.z);
        
        //transform.position = Vector3.Lerp(transform.position,new Vector3(Mathf.Clamp(transform.position.x,-5,5),transform.position.y,transform.position.z),2f);
        //transform.DOMoveX(Mathf.Clamp(moveX,-5,5),1);
        //transform.DOMoveX(Mathf.Clamp(transform.position.x,-5,5),1f);

        //transform.Translate(Mathf.Clamp(transform.position.x,-10,10),transform.position.y,transform.position.z);
         
    }

    private void LateUpdate() {
    }
}
