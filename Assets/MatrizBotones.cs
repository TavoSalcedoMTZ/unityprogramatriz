using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MatrizBotones : MonoBehaviour
{
    public int filas = 5; // Número de filas en la matriz
    public int columnas = 5; // Número de columnas en la matriz
    public Button[,] matrizBotones; // Matriz de botones
    public GameObject actor; // Actor que se moverá
    public float tiempoEspera = 0.5f; // Tiempo de espera entre movimientos

    void Start()
    {
        // Inicializar la matriz de botones
        matrizBotones = new Button[filas, columnas];

        // Buscar y organizar botones con CoordenadasBoton
        CoordenadasBoton[] botones = GetComponentsInChildren<CoordenadasBoton>();
        foreach (CoordenadasBoton boton in botones)
        {
            if (boton.TryGetComponent(out Button uiButton))
            {
                matrizBotones[boton.fila, boton.columna] = uiButton;

                // Opcional: Asignar un nombre descriptivo
                uiButton.name = $"Botón ({boton.fila}, {boton.columna})";
            }
        }
    }

    public void MoverActor(Vector2Int inicio, Vector2Int fin)
    {
        if (actor == null)
        {
            Debug.LogError("El actor no está asignado.");
            return;
        }

        StartCoroutine(MoverActorCoroutine(inicio, fin));
    }

    IEnumerator MoverActorCoroutine(Vector2Int inicio, Vector2Int fin)
    {
        Vector2Int posicionActual = inicio;

        while (posicionActual != fin)
        {
            // Calcular la siguiente posición en el camino
            Vector2Int siguiente = CalcularSiguienteNodo(posicionActual, fin);

            // Obtener la posición del siguiente botón
            RectTransform botonTransform = matrizBotones[siguiente.x, siguiente.y].GetComponent<RectTransform>();
            actor.transform.position = botonTransform.position;

            // Esperar hasta que el movimiento se complete
            yield return new WaitUntil(() => actor.transform.position == botonTransform.position);

            // Actualizar la posición actual
            posicionActual = siguiente;
        }

        Debug.Log("El actor ha llegado a la posición final.");
    }


    Vector2Int CalcularSiguienteNodo(Vector2Int actual, Vector2Int objetivo)
    {
        int deltaX = objetivo.x - actual.x;
        int deltaY = objetivo.y - actual.y;

        // Moverse en el eje X si es necesario
        if (deltaX != 0)
        {
            return new Vector2Int(actual.x + (deltaX > 0 ? 1 : -1), actual.y);
        }

        // Moverse en el eje Y si es necesario
        if (deltaY != 0)
        {
            return new Vector2Int(actual.x, actual.y + (deltaY > 0 ? 1 : -1));
        }

        return actual;
    }
}
