using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private Character character;

    void Start()
    {
        character = GetComponentInParent<Character>();
    }

    void Update()
    {
        if (character != null)
        {
            float size = character.attackRadius; 
            transform.localScale = new Vector3(size, size, 1);
        }
    }
}