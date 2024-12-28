using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public Material enemyMaterial;

    public Material friendMaterial;

    public static GameManager instance;

    public ParticleSystem gunFire;

    public ParticleSystem upScalePartical;

    //public ParticleSystem confettiPartical;

    public List<ParticleSystem> friendGunFire = new List<ParticleSystem>();

    //public ParticleSystem splatonParticle;

    //public ParticleSystem finalDoorPartical;

    public bool gameIsStarted = false;

    //public GameObject levelOne;

    public List<GameObject> friends = new List<GameObject>();

    public int whichLevel = 0;

    public Level[] levels;

    public GameStatus status = GameStatus.empty;

    GameObject LevelArea;

    public GameObject FollowMainCamera;

    public GameObject FollowBossCamera;

    //public GameObject cashCollect;

    public GameObject finishCollectText;

    public GameObject getOnlyTextMoneyValue;

    public GameObject greenTriangle;

    //public GameObject myFriendText;

    //public GameObject LookSkyCamera;
    private void Awake() {
        instance = this;
    }
    void Start()
    {
        gunFire.Stop();
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Dost sayısı " + UIManager.instance.friendCount);
        switch (status)
        {
            case GameStatus.empty:

                whichLevel = PlayerPrefs.GetInt("whichLevel");
                PlayerController.instance.moneyValue = PlayerPrefs.GetInt("moneyValue");
                if (PlayerPrefs.GetInt("randomLevel") > 0)
                {
                    whichLevel = Random.Range(0, levels.Length);
                }
                LevelArea = Instantiate(levels[whichLevel].LevelObject, Vector3.zero, Quaternion.identity);
                status = GameStatus.initialize;
                break;
            case GameStatus.initialize:
                break;
            case GameStatus.start:
                break;
            case GameStatus.stay:
                break;
            case GameStatus.restart:
                break;
            case GameStatus.next:
                break;
            default:
                break;
        }
    }

    public void Next(){
        whichLevel++;
        //whichLevelNumber++;

        if(whichLevel >= levels.Length)
        {
            whichLevel--;
            PlayerPrefs.SetInt("randomLevel", 1);
        }

        //PlayerPrefs.SetInt("playerChoise",playerChoise);
        PlayerPrefs.SetInt("whichLevel", whichLevel);
        //PlayerPrefs.SetInt("whichLevelNumber",whichLevelNumber);
        PlayerPrefs.SetInt("moneyValue",PlayerController.instance.moneyValue);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        status = GameStatus.empty;
    }

        public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
