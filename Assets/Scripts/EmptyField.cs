using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyField : MonoBehaviour
{
    public TowerSelectionMenu towerSelectionMenu; // Odwo�anie do menu wyboru wie�y

    private void OnMouseDown()
    {
        // Otw�rz menu wyboru wie�y
        towerSelectionMenu.Open(this);
    }
}
