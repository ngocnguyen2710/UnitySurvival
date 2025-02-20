using System;
using System.IO.Compression;
using UnityEngine;

public class Player : Character
{
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
