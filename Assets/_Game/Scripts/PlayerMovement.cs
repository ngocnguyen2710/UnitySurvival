using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private FloatingJoystick floatingJoystick;
    private Character characterScript;

    // Update is called once per frame
    private void Start() {
        characterScript = GetComponent<Character>();
    }

    void FixedUpdate()
    {
        characterScript._rb.linearVelocity = new Vector3(floatingJoystick.Horizontal * _speed, characterScript._rb.linearVelocity.y, floatingJoystick.Vertical * _speed);

        Vector3 beginCast = transform.forward + transform.position;
        Ray ray = new Ray(beginCast, Vector3.down);
        Debug.DrawRay(beginCast, Vector3.down, Color.red, 1f);
        
        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0) {
            if (characterScript._rb.linearVelocity != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(characterScript._rb.linearVelocity);
            }
        } else {
        }
    }
}
