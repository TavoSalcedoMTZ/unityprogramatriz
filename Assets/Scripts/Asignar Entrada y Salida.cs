using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignarEntradaySalida : MonoBehaviour
{
    public bool SelectorActivado;
    public bool SelectorActivadorSalida;
    public bool s1, s2;

    public GameObject[] Botones; // Aquí están los botones que representan la matriz
    private CoordenadasBoton[] coordenadasBotones; // Array para guardar las coordenadas de cada botón

    private CoordenadasBoton entrada; // Coordenadas de la entrada
    private CoordenadasBoton salida;  // Coordenadas de la salida

    void Start()
    {
        SelectorActivado = false;
        SelectorActivadorSalida = false;
        s1 = false;
        s2 = false;

        // Inicializamos el array de coordenadas de los botones
        coordenadasBotones = new CoordenadasBoton[Botones.Length];

        for (int i = 0; i < Botones.Length; i++)
        {
            coordenadasBotones[i] = Botones[i].GetComponent<CoordenadasBoton>();
            if (coordenadasBotones[i] != null)
            {
                coordenadasBotones[i].fila = i / 5; // Ejemplo, fila depende del índice
                coordenadasBotones[i].columna = i % 5; // Ejemplo, columna depende del índice
            }
            else
            {
                Debug.LogError("El botón no tiene el componente CoordenadasBoton.");
            }
        }
    }

    public void SelectorActivate()
    {
        SelectorActivado = true;
        SelectorActivadorSalida = false;
    }

    public void SelectorActivateSalida()
    {
        SelectorActivado = false;
        SelectorActivadorSalida = true;
    }

    public void EstablecerEntrada(Button boton)
    {
        if (SelectorActivado && !s1)
        {
            Image imageComponent = boton.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = Color.green;
            }
            else
            {
                Debug.LogError("El botón no tiene un componente Image.");
            }

            entrada = boton.GetComponent<CoordenadasBoton>();
            if (entrada == null)
            {
                Debug.LogError("El botón no tiene un componente CoordenadasBoton.");
                return;
            }

            s1 = true;

            // Intentar el recorrido si la salida ya está establecida
            IntentarRecorrido();
        }
    }

    public void EstablecerSalida(Button boton)
    {
        if (SelectorActivadorSalida && !s2)
        {
            Image imageComponent = boton.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.color = Color.red;
            }
            else
            {
                Debug.LogError("El botón no tiene un componente Image.");
            }

            salida = boton.GetComponent<CoordenadasBoton>();
            if (salida == null)
            {
                Debug.LogError("El botón no tiene un componente CoordenadasBoton.");
                return;
            }

            s2 = true;

            // Intentar el recorrido si la entrada ya está establecida
            IntentarRecorrido();
        }
    }

    private void IntentarRecorrido()
    {
        if (entrada != null && salida != null)
        {
            RecorrerMatriz(entrada, salida);
        }
    }

    private void RecorrerMatriz(CoordenadasBoton inicio, CoordenadasBoton destino)
    {
        Queue<CoordenadasBoton> cola = new Queue<CoordenadasBoton>();
        Dictionary<CoordenadasBoton, CoordenadasBoton> padre = new Dictionary<CoordenadasBoton, CoordenadasBoton>();

        cola.Enqueue(inicio);
        padre[inicio] = null;

        while (cola.Count > 0)
        {
            CoordenadasBoton actual = cola.Dequeue();

            if (actual == destino)
            {
                TrazarCamino(padre, destino);
                return;
            }

            foreach (CoordenadasBoton vecino in ObtenerVecinos(actual))
            {
                if (!padre.ContainsKey(vecino))
                {
                    cola.Enqueue(vecino);
                    padre[vecino] = actual;
                }
            }
        }

        Debug.Log("No se encontró un camino entre la entrada y la salida.");
    }

    private List<CoordenadasBoton> ObtenerVecinos(CoordenadasBoton actual)
    {
        List<CoordenadasBoton> vecinos = new List<CoordenadasBoton>();
        int[][] direcciones = new int[][]
        {
            new int[] { -1, 0 }, 
            new int[] { 1, 0 },  
            new int[] { 0, -1 }, // Izquierda
            new int[] { 0, 1 }   // Derecha
        };

        foreach (int[] direccion in direcciones)
        {
            int nuevaFila = actual.fila + direccion[0];
            int nuevaColumna = actual.columna + direccion[1];

            CoordenadasBoton vecino = System.Array.Find(coordenadasBotones, b => b.fila == nuevaFila && b.columna == nuevaColumna);
            if (vecino != null)
            {
                vecinos.Add(vecino);
            }
        }

        return vecinos;
    }

    private void TrazarCamino(Dictionary<CoordenadasBoton, CoordenadasBoton> padre, CoordenadasBoton destino)
    {
        CoordenadasBoton actual = destino;

        while (actual != null)
        {
            GameObject boton = System.Array.Find(Botones, b => b.GetComponent<CoordenadasBoton>() == actual);
            if (boton != null)
            {
                Image imageComponent = boton.GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.color = Color.yellow;
                }
            }

            actual = padre[actual];
        }
    }
}
