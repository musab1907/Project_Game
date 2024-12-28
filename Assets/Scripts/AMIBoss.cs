using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMIBoss : MonoBehaviour
{
    // Start is called before the first frame update

    public bool AMIBossValue;

    public GameObject canvas;

    public static AMIBoss instance;

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
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
