using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 spawnPosition;

    private float attackRadius;
    public void SetAttackRadius(float radius) {
        attackRadius = radius;
    }

    void Start() {
        spawnPosition = transform.position;
    }

    void Update() {
        if (Vector3.Distance(transform.position, spawnPosition) > attackRadius) {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Bot") || other.CompareTag("Player")) {
            Debug.Log("Hit " + other.gameObject.name);
            Destroy(gameObject);
        }
    }
}