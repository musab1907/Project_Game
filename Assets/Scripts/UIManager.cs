using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    public GameObject startPanel;
    public GameObject gamePanel;
    public GameObject joystickPanel;
    public GameObject winPanel;
    public GameObject failPanel;

    public GameObject moneyText;

    public GameObject leftEnemyNumber;

    public GameObject myFriendText;



    void Awake() {
        instance = this;
    }
    void Start()
    {
        StartPanelFunction();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.GetComponent<Text>().text = "" + PlayerController.instance.moneyValue;
        if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == false){
            myFriendText.GetComponent<Text>().text = "  " + EnemyNumberController.instance.myFriendCount;
            leftEnemyNumber.GetComponent<Text>().text = "  / " + EnemyNumberController.instance.textLeftEnemyCount;
        }
        else if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true){
            myFriendText.GetComponent<Text>().text = "" + BossMovement.instance.myFriendNumber + " ";
            //leftEnemyNumber.GetComponent<Text>().text = "  / " + BossMovement.instance.textMyFriendNumber;
        }

       
        //UIBallControlSystem();
    }

    public void StartPanelFunction(){
        //EnemyNumberController.instance.UIBallControlSystem();
        startPanel.SetActive(true);
        gamePanel.SetActive(false);
        joystickPanel.SetActive(false);
        winPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    public void GamePanelFunction(){
        //UIBallControlSystem();//
        //GameManager.instance.levelOne.SetActive(true);
        //EnemyMovement.instance.EnemyAnimationController();
        EnemyNumberController.instance.UIBallControlSystem();
        GameManager.instance.gameIsStarted = true;
        PlayerController.instance.BossLevelControlSystem();
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        joystickPanel.SetActive(true);
        winPanel.SetActive(false);
        failPanel.SetActive(false);
         if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true){
            BossMovement.instance.tutorialText.SetActive(false);
        }
    }

    public void JoystickPanelFunction(){
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
        joystickPanel.SetActive(true);
        winPanel.SetActive(false);
        failPanel.SetActive(false);
    }

    public void WinPanelFunction(){
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        joystickPanel.SetActive(false);
        winPanel.SetActive(true);
        failPanel.SetActive(false);
        GameManager.instance.gameIsStarted = false;
        GameManager.instance.getOnlyTextMoneyValue.GetComponent<Text>().text = "" + PlayerController.instance.levelMoneyValue;
        GameManager.instance.finishCollectText.GetComponent<Text>().text = "" + PlayerController.instance.moneyValue;
        //AMIBoss.instance.canvas.SetActive(false);
    }

    public void FailPanelFunction(){
        startPanel.SetActive(false);
        gamePanel.SetActive(false);
        joystickPanel.SetActive(false);
        winPanel.SetActive(false);
        failPanel.SetActive(true);
    }

    public void OnClickStartButton(){
        GamePanelFunction();
        Debug.Log("Tıklandı");
    }
}
