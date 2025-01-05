using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMIBoss : MonoBehaviour
{
   

    public bool AMIBossValue;

    public GameObject canvas;

    public static AMIBoss instance;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        CanvasController();
    }

    public void CanvasController(){
        if(PlayerController.instance.gameIsFinished == true){
            canvas.SetActive(false);
        }
    }
}
