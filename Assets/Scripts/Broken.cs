using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brokern : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "friend"){
            transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(1).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(2).GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(3).GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
