using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected float attackRadius;
    private Character character;

    void Start() {
        character = GetComponentInParent<Character>();
        attackRadius = character.attackRadius;
    }
    public abstract void Shoot();

    public abstract void Damage();
}