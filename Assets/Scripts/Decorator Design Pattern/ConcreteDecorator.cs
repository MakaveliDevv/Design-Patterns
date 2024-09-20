using System.Collections.Generic;
using UnityEngine;

public class ConcreteDecorator : Decorator
{
    public override void AddWeapon(List<Weapon> weaponList, Weapon weapon)
    {
        if(CheckInList(weaponList, weapon)) return;

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
    }

    public override bool CheckInList(List<Weapon> weaponList, Weapon weapon) 
    {
        if(weaponList.Contains(weapon)) 
        {
            Debug.LogError($"{weapon} already exist in the invetory");
            return true;
        }

        return false;
    }
}

