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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.1f)) {
            if (hit.collider.gameObject.CompareTag("Bot") || hit.collider.gameObject.CompareTag("Player")) {
                Debug.Log("Hit " + hit.collider.gameObject.name);
                
                Destroy(gameObject);
            }
        }
    }
}