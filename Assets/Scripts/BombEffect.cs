using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEffect : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wallCrash;
    
    public ParticleSystem bombPartical;

    public static BombEffect instance;

    void Awake(){
        instance = this;
    }

    void Start()
    {
        bombPartical.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<GameManager>().player.tag == "bomb"){
            //Debug.Log("Bombaya çarptım");

        }
    }

}
