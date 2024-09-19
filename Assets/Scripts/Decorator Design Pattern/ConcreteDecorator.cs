using System.Collections.Generic;
using UnityEngine;

public class ConcreteDecorator : Decorator
{
    public override void AddWeapon(List<Weapon> weaponList, Weapon weapon)
    {
        if(weaponList.Contains(weapon)) 
        {
            Debug.LogError($"{weapon} already exist in the invetory");
        }

        weaponList.Add(weapon);
        
        Debug.Log($"{weapon} is added to the inventory");
    }

    public override void RemoveWeapon(List<Weapon> weaponList, Weapon weapon)
    {
        if(!weaponList.Contains(weapon)) 
        {
            Debug.LogError("Weapon not found");
        }

        weaponList.Remove(weapon);
        Debug.Log($"{weapon} is removed from the inventory");
        throw new System.NotImplementedException();
    }
}
