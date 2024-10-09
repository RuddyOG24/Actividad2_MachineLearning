using UnityEngine;
using System.Collections.Generic;

public class CellSpawner : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab de la c�lula a instanciar
    public int initialCellCount = 10; // Cantidad de c�lulas a generar inicialmente (siempre 10)
    public float minSize = 0.5f;  // Tama�o m�nimo de las bolas
    public float maxSize = 1.5f;  // Tama�o m�ximo de las bolas
    public float sizeReductionPerRound = 0.2f; // Cu�nto se reducen las bolas por ronda
    public float changePositionInterval = 10f; // Tiempo en segundos para cambiar la posici�n de las bolas

    private List<GameObject> cells = new List<GameObject>(); // Lista para almacenar las c�lulas generadas
    private int currentCellsActive = 0; // Contador de las c�lulas activas
    private float positionChangeTimer; // Temporizador para cambiar la posici�n de las c�lulas

    private void Start()
    {
        positionChangeTimer = changePositionInterval; // Inicializar el temporizador de cambio de posici�n
        SpawnCells(); // Generar las c�lulas al inicio
    }

    private void Update()
    {
        // Contar el tiempo para cambiar de posici�n las bolas activas
        if (positionChangeTimer > 0)
        {
            positionChangeTimer -= Time.deltaTime;
        }
        else
        {
            ChangePositionsOfActiveCells(); // Cambiar la posici�n de las bolas no clickeadas
            positionChangeTimer = changePositionInterval; // Reiniciar el temporizador
        }
    }

    // M�todo para generar c�lulas
    public void SpawnCells()
    {
        for (int i = 0; i < initialCellCount; i++)
        {
            // Generar posici�n aleatoria dentro del �rea de spawn
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject newCell = Instantiate(cellPrefab, spawnPosition, Quaternion.identity);
            newCell.GetComponent<Cell>().SetSpawner(this); // Pasar referencia del spawner a la c�lula

            // Generar un tama�o aleatorio para la c�lula dentro de los l�mites especificados
            float randomSize = Random.Range(minSize, maxSize);
            newCell.GetComponent<Cell>().SetSize(randomSize); // Establecer el tama�o de la c�lula

            cells.Add(newCell);  // Guardar la c�lula en la lista
            currentCellsActive++; // Incrementar el contador de c�lulas activas
        }
    }

    // M�todo para notificar cuando una c�lula es eliminada
    public void OnCellDestroyed()
    {
        currentCellsActive--;

        // Si todas las c�lulas fueron eliminadas, iniciar una nueva ronda con bolas m�s peque�as
        if (currentCellsActive <= 0)
        {
            StartNextRound();
        }
    }

    // M�todo para iniciar una nueva ronda con exactamente 10 c�lulas
    public void StartNextRound()
    {
        // Eliminar todas las c�lulas existentes
        foreach (GameObject cell in cells)
        {
            if (cell != null)
            {
                Destroy(cell); // Eliminar la c�lula de la escena
            }
        }
        cells.Clear(); // Limpiar la lista de c�lulas
        currentCellsActive = 0; // Reiniciar el contador de c�lulas activas

        // Generar nuevas c�lulas
        SpawnCells();
    }

    // M�todo para cambiar la posici�n de las c�lulas activas que no han sido clickeadas
    public void ChangePositionsOfActiveCells()
    {
        foreach (GameObject cell in cells)
        {
            if (cell != null && cell.activeSelf) // Solo cambiar la posici�n de las c�lulas activas
            {
                Vector2 newPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
                cell.transform.position = newPosition; // Cambiar la posici�n de la c�lula
            }
        }
    }
}
