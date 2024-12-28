using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broke : MonoBehaviour
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
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "friend"){
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }

}
