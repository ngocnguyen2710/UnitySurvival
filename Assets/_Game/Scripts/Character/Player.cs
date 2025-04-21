using System;
using System.IO.Compression;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private FloatingJoystick floatingJoystick;

    private void Start() {
        OnInit();
    }

    void OnInit() {
        isWinning = false;
    }

    void Update() {
        UpdateState();
        // Attack();
    }

    void FixedUpdate() {
        MovementInput();
    }

    void MovementInput() {
        _rb.linearVelocity = new Vector3(floatingJoystick.Horizontal * _speed, _rb.linearVelocity.y, floatingJoystick.Vertical * _speed);
        
        if (floatingJoystick.Horizontal != 0 || floatingJoystick.Vertical != 0) {
            if (_rb.linearVelocity != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(_rb.linearVelocity);
            }
        } else {
        }
    }

    private void DrawCircle() {
        float radius = 10f;
        for (float radians = 0; radians <= Mathf.PI*2 ; radians ++) {
            float x = radius * Mathf.Cos(radians);
            float z = radius * Mathf.Sin(radians);
            float y = 1;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(x, y, z);
            cube.transform.SetParent(transform);
        }
    }
}
