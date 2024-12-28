using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyRandomMovement : MonoBehaviour
{
    // Start is called before the first frame update
    
    public NavMeshAgent agent;

    [Range(0,100)] public float speed;//Böyle yaprak hızı bir slider gibi ayarlabiliriz
    [Range(1,500)] public float radius;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if(agent != null && GameManager.instance.gameIsStarted == true){//Eğer oyunun başla butonuna basıldıysa ve agent boş değilse bu işlemler yapılacal
            //agent.speed = speed;
            agent.SetDestination(RandmNavMeshPosition());
            gameObject.GetComponent<Animator>().SetBool("fail",true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.gameIsStarted == true){
        UpdatePosition();
        }
    }

    public Vector3 RandmNavMeshPosition(){
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * radius;
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition,out NavMeshHit hit,radius,1)){
            finalPosition = hit.position;
        }
        return finalPosition;
    }

    public void UpdatePosition(){
        if(agent != null && agent.remainingDistance <= agent.stoppingDistance){
            agent.SetDestination(RandmNavMeshPosition());
            gameObject.GetComponent<Animator>().SetBool("fail",true);
            agent.speed = 6;
        }
    }
}
