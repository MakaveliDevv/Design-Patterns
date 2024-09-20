using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool inRange;

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.TryGetComponent<IInterActable>(out var interactable)) 
        {
            
        }
    }   
}
