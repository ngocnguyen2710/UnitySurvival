using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    public Transform target;
    private NavMeshAgent agent;
    private Vector3 spawnPosition;
    private float circleSpeed = 0.5f; // Speed of circling
    private float angle = 0f; // Angle for circular movement

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        spawnPosition = transform.position; // Save the spawn position
    }

    void Update() {
        Character closestPlayer = ClosestPlayer();

        if (closestPlayer != null) {
            // Stop if a character is nearby
            agent.isStopped = true;
        } else {
            // Run in a circle around the spawn position
            agent.isStopped = false;
            CircleAroundSpawn();
        }

        UpdateState();
    }

    private void CircleAroundSpawn() {
        angle += circleSpeed * Time.deltaTime; // Increment the angle over time
        float x = Mathf.Cos(angle) * GameManager.instance.botCircleRadius;
        float z = Mathf.Sin(angle) * GameManager.instance.botCircleRadius;
        Vector3 circlePosition = new Vector3(spawnPosition.x + x, spawnPosition.y, spawnPosition.z + z);
        agent.SetDestination(circlePosition);
    }
}