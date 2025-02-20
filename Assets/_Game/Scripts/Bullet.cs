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
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Bot") || collision.gameObject.CompareTag("Player")) {
            Destroy(gameObject);
        }
    }
}