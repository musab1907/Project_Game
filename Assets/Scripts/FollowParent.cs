using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowFuntion();
    }

    public void FollowFuntion(){
        if(transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().material == GameManager.instance.friendMaterial){
            transform.LookAt(GameManager.instance.player.transform);
            Debug.Log("Eveet");
        }
    }
}
