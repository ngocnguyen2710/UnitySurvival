using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {
    [SerializeField] public Rigidbody _rb;
    [SerializeField] private Animator anim;
    [SerializeField] private float attackCooldown = 0f;
    [SerializeField] private Weapon weapon;
    public float attackRadius = 10f;
    private string currentAnimName;
    protected bool isWinning;

    public void ChangeAnim(string animName) {
        if(currentAnimName != animName) {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    protected void Attack(Character enemy) {
        ChangeAnim("attack");
        //turn face to enemy
        Vector3 direction = enemy.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
        if (attackCooldown < 0.1f) {
            attackCooldown = 2f;
            if (weapon != null) {
                if (weapon is Gun) {
                    Debug.Log("Using Gun to shoot");
                } else if (weapon is Hammer) {
                    Debug.Log("Using Hammer to shoot");
                }
                weapon.Shoot(); // Use the Shoot method from the Weapon class
            }
        } else {
            attackCooldown -= Time.deltaTime;
        }
    }

    protected void UpdateState() {
        if (_rb.linearVelocity.magnitude < 0.1f) {
            if (ClosestPlayer() != null) {
                Attack(ClosestPlayer());
            } else {
                ChangeAnim("idle");
            }
        } else {
            ChangeAnim("walk");
            // reset attack cooldown on moving
            // attackCooldown = 2f;
        }
    }

    protected Character ClosestPlayer() {
        float distanceToClosestPlayer = Mathf.Infinity;
        Character closestPlayer = null;
        Character[] allPlayers = FindObjectsByType<Character>(FindObjectsSortMode.None);

        foreach (Character currentPlayer in allPlayers) {
            if (currentPlayer == this) continue; //to avoid detecting itself
            float distanceToPlayer = Vector3.Distance(currentPlayer.transform.position, this.transform.position);
            if (distanceToPlayer < attackRadius && distanceToPlayer < distanceToClosestPlayer) {
                distanceToClosestPlayer = distanceToPlayer;
                closestPlayer = currentPlayer;
            }
        }

        return closestPlayer;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green; // Màu hình cầu
        Gizmos.DrawWireSphere(transform.position, attackRadius); // Vẽ hình cầu
    }
}