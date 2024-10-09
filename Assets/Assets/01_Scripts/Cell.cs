using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour
{
    private SpriteRenderer cellSpriteRenderer;   // Referencia al SpriteRenderer para cambiar el color
    private CellSpawner cellSpawnerRef;         // Referencia al CellSpawner para notificar cuando la célula es destruida
    private GameManager gameManagerRef;         // Referencia al GameManager para actualizar la puntuación
    public AudioClip destructionSoundClip;      // Clip de audio para reproducir al destruir
    public float soundVolume = 0.5f;            // Volumen del sonido de destrucción
    public GameObject deathEffectPrefab;        // Prefab de las partículas de muerte

    private void Start()
    {
        cellSpriteRenderer = GetComponent<SpriteRenderer>();  // Obtener referencia al SpriteRenderer
        gameManagerRef = FindObjectOfType<GameManager>();     // Encontrar el GameManager en la escena
        Adapt();                                              // Adaptar el color al inicio
    }

    // Método para cambiar color y posición de la célula al inicio
    public void Adapt()
    {
        // Cambiar color aleatoriamente
        Color randomColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        cellSpriteRenderer.color = randomColor;
    }

    // Asignar el tamaño de la célula
    public void SetSize(float newSize)
    {
        transform.localScale = Vector3.one * newSize;
    }

    // Asignar el spawner para notificar cuando sea destruida
    public void SetSpawner(CellSpawner spawnerReference)
    {
        this.cellSpawnerRef = spawnerReference;
    }

    // Detectar clic para destruir la célula
    private void OnMouseDown()
    {
        PlaySound();         // Reproducir sonido
        PlayDeathParticles(); // Reproducir las partículas de muerte

        // Notificar al spawner y al GameManager antes de destruir el objeto
        if (cellSpawnerRef != null)
        {
            cellSpawnerRef.OnCellDestroyed(); // Notificar al spawner que una célula fue destruida
        }

        if (gameManagerRef != null)
        {
            gameManagerRef.IncreaseScore(); // Aumentar el puntaje en el GameManager
        }

        Destroy(gameObject); // Destruir la célula de inmediato
    }

    // Método para reproducir el sonido en un GameObject temporal
    private void PlaySound()
    {
        // Crear un GameObject vacío temporal para el sonido
        GameObject tempSoundSource = new GameObject("CellSound");
        AudioSource audioSource = tempSoundSource.AddComponent<AudioSource>();

        // Configurar el clip de audio y el volumen
        audioSource.clip = destructionSoundClip;
        audioSource.volume = soundVolume;

        // Reproducir el sonido y destruir el objeto temporal después de la duración del clip
        audioSource.Play();
        Destroy(tempSoundSource, destructionSoundClip.length);
    }

    // Método para reproducir las partículas de muerte
    private void PlayDeathParticles()
    {
        // Instanciar el prefab de partículas en la posición actual
        if (deathEffectPrefab != null)
        {
            GameObject particles = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

            // Destruir las partículas después de 2 segundos para evitar objetos innecesarios
            Destroy(particles, 2f);
        }
    }
}
