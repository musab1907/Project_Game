using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossSliderBarSystem: MonoBehaviour
{
    
    // Start is called before the first frame update
    public Slider levelBar;

    public GameObject barLevel;
    private float maxBarValue = 100;
    public float currentBarValue;

    private WaitForSeconds nowTime = new WaitForSeconds(0.1f);//Her 0.1f de işlem yapmak için böyle bir WaitForSeconds değeri ekledik
    private Coroutine stopValue;
    public static BossSliderBarSystem instance;//Singleton yapısı için

    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
        currentBarValue = 100;
        levelBar.value = 100;
        levelBar.maxValue = maxBarValue;
        
    }

    private void Update() {
        MoneyBarControlSystem();
    }

     public void BossNegativeUseMoney(float amount){
        if(currentBarValue - amount >= 0){
            currentBarValue -= amount;
            levelBar.value = currentBarValue;

            if(stopValue != null){
                StopCoroutine(stopValue);//Her bastığında orası duracak
            }
        }
        else{
            Debug.Log("Not enough to money for this piece");
        }
    }

    public void MoneyBarControlSystem(){
    }

    public void PositionUpY(){
        transform.position -= new Vector3(0, 5, 0);
    }

    public void PlayerDead(){
        BossNegativeUseMoney(100);
    }

   
}
