using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public Transform target;
    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (target != null) {
            agent.SetDestination(target.position);
        }
    }


    protected void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            GameManager.instance.GameOver();
        }

        if (collision.gameObject.CompareTag("Finish") && isWinning == false) {
            GameManager.instance.WinGame();
        }
    }
}