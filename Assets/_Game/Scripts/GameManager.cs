using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI winScreenText;
    public static GameManager instance;
    private int highScore;
    private bool hasWon = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        if(instance == null) {
            instance = this;
        }
        replayButton.onClick.AddListener(Replay);
        OnInit();
        // DrawCircle();
    }

    private void OnInit() {
        winScreen.SetActive(false);
        hasWon = false;
    }

    public void WinGame() {
        hasWon = true;
        winScreen.SetActive(true);
        winScreenText.text = "You Win!";
        Debug.Log("You win!");
    }

    public void GameOver() {
        winScreen.SetActive(true);
        winScreenText.text = "Game Over!";
        Debug.Log("Game Over!");
    }

    public void Replay() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void DrawCircle() {
        float radius = 0.1f;
        for (float radians = Mathf.PI/4f; radians <= Mathf.PI*2 ; radians += Mathf.PI/2f) {
            float x = radius * Mathf.Cos(radians);
            float z = radius * Mathf.Sin(radians);
            float y = 1;
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = new Vector3(x, y, z);
            cube.transform.SetParent(transform);
        }
    }

    private void SaveHighScore() {
        string path = Application.dataPath + "/_Game/SaveFiles/highscore.txt";
        try {
            if (!Directory.Exists(Application.dataPath + "/_Game/SaveFiles")) {
                Directory.CreateDirectory(Application.dataPath + "/_Game/SaveFiles");
            }
            File.WriteAllText(path, highScore.ToString());
            Debug.Log("High score saved to " + path);
        } catch (Exception e) {
            Debug.LogError("Failed to save high score: " + e.Message);
        }
}
}
