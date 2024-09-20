using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class ConcreteComponent : IComponent
{
    private ConcreteDecorator concreteDecorator;
    public void CustomAwake() 
    {
        concreteDecorator = new();
    }

    public void AddWeapon(List<Weapon> weaponList, Weapon weapon)
    {
        // If in range, then
        // If certain condition is met
        // Then
        // Invoke method to add a weapon
        concreteDecorator.AddWeapon(weaponList, weapon);

        // Other stuff

    }

    public void RemoveWeapon(List<Weapon> weaponList, Weapon weapon)
    {    
        // If certain condition is met
        // Then
        // Invoke method to remove a weapon
        concreteDecorator.AddWeapon(weaponList, weapon);

        // Other stuff
    
    }
}
