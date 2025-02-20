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
            if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance) {
                StopBot(); // Gọi hàm dừng bot
            }
        }
    }
    void StopBot()
    {
        agent.isStopped = true; // Dừng NavMeshAgent
        agent.velocity = Vector3.zero; // Đảm bảo bot dừng hẳn

        Debug.Log("Bot đã đến mục tiêu và dừng lại!");
    }
}