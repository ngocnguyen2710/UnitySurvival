using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour {
    public Rigidbody _rb;
    public float _speed = 10f;
    private Animator anim;
    [SerializeField] private float attackCooldown = 0f;
    [SerializeField] private Weapon weapon;
    public float attackRadius = 10f;
    private string currentAnimName;
    protected bool isWinning;
    protected bool isAttacking = false;
    protected bool isMoving = false;
    protected bool isIdle = true;
    private static int botDownCount = 0;

    private void Start() {
        anim = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    public void ChangeAnim(string animName) {
        if(anim == null) return;
        if (currentAnimName == animName) return;
        if (animName == "attack") {
            anim.SetBool("isAttacking", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isIdle", false);
            isAttacking = true;
            isIdle = false;
            isMoving = false;
        } else if (animName == "walk") {
            anim.SetBool("isMoving", true);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttacking", false);
            isMoving = true;
            isIdle = false;
            isAttacking = false;
        } else if (animName == "idle") {
            anim.SetBool("isIdle", true);
            anim.SetBool("isMoving", false);
            anim.SetBool("isAttacking", false);
            isIdle = true;
            isMoving = false;
            isAttacking = false;
        }
    }

    protected void Attack(Character enemy) {
        if(anim != null) {
            ChangeAnim("attack");
        }
        //turn face to enemy
        Vector3 direction = enemy.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
        if (attackCooldown < 0.1f) {
            attackCooldown = 2f;
            if (weapon != null) {
                // if (weapon is Gun) {
                //     Debug.Log("Using Gun to shoot");
                // } else if (weapon is Hammer) {
                //     Debug.Log("Using Hammer to shoot");
                // }
                weapon.Shoot();
            }
        }
    }

    protected void UpdateState() {
        if(IsCharacterStay()) {
            if (ClosestPlayer() != null) {
                Attack(ClosestPlayer());
            } else {
                if(anim != null) {
                    ChangeAnim("idle");
                }
            }
        } else {
            if(anim != null) {
                ChangeAnim("walk");
            }
            // reset attack cooldown on moving
            // attackCooldown = 2f;
        }

        if (attackCooldown > 0f) {
            attackCooldown -= Time.deltaTime;
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    bool IsCharacterStay() {
        if (this is Bot) {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();
            return agent.velocity.magnitude < 0.1f;
        }
        return _rb.linearVelocity.magnitude < 0.1f;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet")) {
            Debug.Log(this.name + " get shot");
            if (this.CompareTag("Bot")) {
                Destroy(gameObject);
                botDownCount++;
                Debug.Log(botDownCount);
                if (botDownCount >= GameManager.instance.numberOfBots) {
                    WinGame();
                }
            } else {
                GameOver();
            }
        } 
    }

    protected void GameOver() {
        GameManager.instance.GameOver();
        botDownCount = 0;
        isWinning = false;
        Debug.Log("Game Over");
    }

    protected void WinGame() {
        GameManager.instance.WinGame();
        botDownCount = 0;
        isWinning = true;
        Debug.Log("You win!");
    }
}