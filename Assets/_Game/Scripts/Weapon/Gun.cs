using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float force = 10f;

    public override void Shoot() {
        if (bulletPrefab != null) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetAttackRadius(attackRadius);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        } else {
            Debug.LogWarning("Bullet prefab or shooting point is not assigned.");
        }
    }

    public override void Damage() {
        throw new System.NotImplementedException();
    }
}