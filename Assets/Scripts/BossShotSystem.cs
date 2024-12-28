using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShotSystem : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public float range = 0.3f;

    //public ParticleSystem BossGunParticle;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shot();
    }

    void Shot(){
        RaycastHit hit;
        if(Physics.Raycast(new Vector3(player.transform.position.x,player.transform.position.y + 2, player.transform.position.z),player.transform.forward,out hit,10f)){
            if(GameObject.Find("EnemyBoss").transform.GetChild(0).gameObject.active == true && hit.transform.name == "Boss"){
                //BossMovement.instance.bossPartical.Play();
                SliderBarSystem.instance.NegativeUseMoney(0.2f);//Parantez içine eksili ifade yazarsan UIDA dolum yapmış olursun.
                BossMovement.instance.bossMyHeath--;
                BossMovement.instance.Boss.transform.LookAt(GameManager.instance.player.transform);
                }
            if(hit.transform.name == "Boss"){
            }
        }
    }

    

}
