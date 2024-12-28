using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNumberController : MonoBehaviour
{
    // Start is called before the first frame update

    public int enemyCount;

    public int leftEnemyCount;

    public int textLeftEnemyCount;

    public int myFriendCount;

    public static EnemyNumberController instance;

    public GameObject finalParticalSystem;

    public GameObject key;

    public GameObject finalDoorOpen;
    public GameObject finalDoor;


    private void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //UIBallControlSystem();
    }

    //Kaç düşman varsa o kadarını aktif ediyor
    public void UIBallControlSystem(){
        //for(int i =enemyCount; i >= 0; i--){
            //UIManager.instance.redBalls[enemyCount].SetActive(true);
            //transform.GetChild(i).gameObject.SetActive(true);
            //if(enemyCount > 0){
                //enemyCount--;
            //}

    }
    //Kaç düşman varsa o kadarını aktif ediyor
}
