using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignarEntradaySalida : MonoBehaviour
{
    public bool SelectorActivado; 
    public bool SelectorActivadorSalida; 
    public bool s1, s2;

    public GameObject[] Botones;  // Aquí están los botones que representan la matriz
    private CoordenadasBoton[] coordenadasBotones;  // Array para guardar las coordenadas de cada botón

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
            // Aseguramos que cada botón tenga un componente de CoordenadasBoton
            coordenadasBotones[i] = Botones[i].GetComponent<CoordenadasBoton>();

            // Asignamos las coordenadas (fila, columna) manualmente o de forma automática
            if (coordenadasBotones[i] != null)
            {
                // Aquí puedes asignar las coordenadas de cada botón. Puedes hacerlo manualmente o automáticamente.
                coordenadasBotones[i].fila = i / 5; // Ejemplo, fila depende del índice
                coordenadasBotones[i].columna = i % 5; // Ejemplo, columna depende del índice
            }
            else
            {
                Debug.LogError("El botón no tiene el componente CoordenadasBoton.");
            }
        }
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
            s1 = true;
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
            s2 = true;
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

    public void EstablecerNumero()
    {
       
    }


    public void ObtenerCoordenadas(Button boton)
    {
        CoordenadasBoton coordBoton = boton.GetComponent<CoordenadasBoton>();
        if (coordBoton != null)
        {
            Debug.Log($"Coordenadas del botón: Fila = {coordBoton.fila}, Columna = {coordBoton.columna}");
        }
        else
        {
            Debug.LogError("El botón no tiene un componente CoordenadasBoton.");
        }   
    }
}
