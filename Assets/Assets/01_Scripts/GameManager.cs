using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;           // Texto de la UI para mostrar el puntaje
    public Text timerText;           // Texto de la UI para mostrar el cronómetro

    private int score;               // Puntaje del jugador
    private float timeElapsed;       // Tiempo transcurrido

    private void Start()
    {
        score = 0;                   // Inicializar el puntaje en 0
        timeElapsed = 0f;            // Inicializar el tiempo transcurrido en 0
        UpdateScoreText();           // Actualizar el texto del puntaje al inicio
        UpdateTimerText();           // Actualizar el texto del cronómetro al inicio
    }

    private void Update()
    {
        // Incrementar el tiempo cada frame para crear el cronómetro
        timeElapsed += Time.deltaTime;
        UpdateTimerText(); // Actualizar el texto del cronómetro en la UI
    }

    // Método para aumentar el puntaje al hacer clic en un objeto
    public void IncreaseScore()
    {
        score++;                       // Aumentar el puntaje en 1
        UpdateScoreText();             // Actualizar el texto del puntaje en la UI
    }

    // Método para actualizar el texto del puntaje en la UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Actualizar el texto para mostrar el puntaje actual
    }

    // Método para actualizar el texto del cronómetro en la UI
    private void UpdateTimerText()
    {
        // Convertir el tiempo transcurrido en minutos y segundos
        int minutes = Mathf.FloorToInt(timeElapsed / 60);  // Obtener minutos
        int seconds = Mathf.FloorToInt(timeElapsed % 60);  // Obtener segundos
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); // Mostrar tiempo en formato mm:ss
    }
}
