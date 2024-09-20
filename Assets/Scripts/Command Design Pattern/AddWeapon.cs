using System.Collections.Generic;
using UnityEngine;
public class AddWeaponCommand : ICommand
{
    private readonly ConcreteComponent component;
    private readonly List<Weapon> weapons = new();
    private readonly Weapon weapon;

    public AddWeaponCommand(ConcreteComponent component, List<Weapon> weapons, Weapon weapon)
    {
        this.component = component;
        this.weapons = weapons;
        this.weapon = weapon;
    }
    
    public void Execute()
    {
        component.AddWeapon(weapons, weapon);
        Debug.Log("AddWeapon Command Execute");
        
    }

    public void Undo()
    {
        component.RemoveWeapon(weapons, weapon);
        Debug.Log("AddWeapon Command Undo");
    }
}