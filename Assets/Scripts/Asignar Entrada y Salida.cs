using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsignarEntradaySalida : MonoBehaviour
{
    public bool SelectorActivado; 
    public bool SelectorActivadorSalida; 
    public bool s1, s2;

    void Start()
    {
        SelectorActivado = false;
        SelectorActivadorSalida = false;
        s1 = false;
        s2 = false;
    }

    public void EstablecerEntrada(Button boton)
    {
        if (SelectorActivado&& !s1)
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
}
