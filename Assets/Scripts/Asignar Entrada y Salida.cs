using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignarEntradaySalida : MonoBehaviour
{
    public bool SelectorActivado; 
    public bool SelectorActivadorSalida; 
    public bool s1, s2;

    public GameObject[] Botones;  // Aqu� est�n los botones que representan la matriz
    private CoordenadasBoton[] coordenadasBotones;  // Array para guardar las coordenadas de cada bot�n

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
            // Aseguramos que cada bot�n tenga un componente de CoordenadasBoton
            coordenadasBotones[i] = Botones[i].GetComponent<CoordenadasBoton>();

            // Asignamos las coordenadas (fila, columna) manualmente o de forma autom�tica
            if (coordenadasBotones[i] != null)
            {
                // Aqu� puedes asignar las coordenadas de cada bot�n. Puedes hacerlo manualmente o autom�ticamente.
                coordenadasBotones[i].fila = i / 5; // Ejemplo, fila depende del �ndice
                coordenadasBotones[i].columna = i % 5; // Ejemplo, columna depende del �ndice
            }
            else
            {
                Debug.LogError("El bot�n no tiene el componente CoordenadasBoton.");
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
                Debug.LogError("El bot�n no tiene un componente Image.");
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
                Debug.LogError("El bot�n no tiene un componente Image.");
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
            Debug.Log($"Coordenadas del bot�n: Fila = {coordBoton.fila}, Columna = {coordBoton.columna}");
        }
        else
        {
            Debug.LogError("El bot�n no tiene un componente CoordenadasBoton.");
        }   
    }
}
