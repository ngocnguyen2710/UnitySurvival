using UnityEngine;

public class Hammer : Weapon
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float force = 10f;

    public override void Damage() {
        throw new System.NotImplementedException();
    }

    public override void Shoot() {
        if (bulletPrefab != null) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Bullet>().SetAttackRadius(attackRadius);
            Vector3 current = bullet.transform.rotation.eulerAngles;
            Quaternion target = Quaternion.Euler(current.x, current.y + 90, current.z);
            bullet.transform.rotation = Quaternion.Slerp(bullet.transform.rotation, target, Time.deltaTime * 100f);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
            Destroy(bullet, 2f);
        } else {
            Debug.LogWarning("Bullet prefab or shooting point is not assigned.");
        }
    }
}