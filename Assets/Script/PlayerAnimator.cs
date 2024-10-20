using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
public Animator PlayerAC; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void ManageAnimators(Vector3 move)
    {
        //KARAKTERİN BİR ŞİDDETİ VARSA KOŞSUN YOKSA NEFES ALMA ANİMASYONU VERİLSİN.
        if(move.magnitude>0)
        {
            PlayRunAnimation();

            PlayerAC.transform.forward = move.normalized;
        }
        else
        {
            PlayIdleAnimation();
        }
       
    }
    public void PlayRunAnimation()
    {
        PlayerAC.Play("RUN");
    }

    public void PlayIdleAnimation()
    {
        PlayerAC.Play("IDLE");
    } 

    
}
