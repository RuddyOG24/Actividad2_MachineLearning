using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;           // Texto de la UI para mostrar el puntaje
    public Text timerText;           // Texto de la UI para mostrar el cron�metro

    private int score;               // Puntaje del jugador
    private float timeElapsed;       // Tiempo transcurrido

    private void Start()
    {
        score = 0;                   // Inicializar el puntaje en 0
        timeElapsed = 0f;            // Inicializar el tiempo transcurrido en 0
        UpdateScoreText();           // Actualizar el texto del puntaje al inicio
        UpdateTimerText();           // Actualizar el texto del cron�metro al inicio
    }

    private void Update()
    {
        // Incrementar el tiempo cada frame para crear el cron�metro
        timeElapsed += Time.deltaTime;
        UpdateTimerText(); // Actualizar el texto del cron�metro en la UI
    }

    // M�todo para aumentar el puntaje al hacer clic en un objeto
    public void IncreaseScore()
    {
        score++;                       // Aumentar el puntaje en 1
        UpdateScoreText();             // Actualizar el texto del puntaje en la UI
    }

    // M�todo para actualizar el texto del puntaje en la UI
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Actualizar el texto para mostrar el puntaje actual
    }

    // M�todo para actualizar el texto del cron�metro en la UI
    private void UpdateTimerText()
    {
        // Convertir el tiempo transcurrido en minutos y segundos
        int minutes = Mathf.FloorToInt(timeElapsed / 60);  // Obtener minutos
        int seconds = Mathf.FloorToInt(timeElapsed % 60);  // Obtener segundos
        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds); // Mostrar tiempo en formato mm:ss
    }
}
