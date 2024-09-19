using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class WeaponSwitchCommand : ICommand 
{
    public Weapon currentWeapon;

    private readonly List<Weapon> weaponInventory;
    private readonly List<Weapon> activeWeaponInv;
    private int currentWeaponIndex;

    public WeaponSwitchCommand(List<Weapon> weaponInventory, List<Weapon> activeWeaponInv) 
    {
        this.weaponInventory = weaponInventory;
        this.activeWeaponInv = activeWeaponInv;
    }

    public void Execute()
    {
        SwitchWeaponMethod();
        Debug.Log("Weapon Switched");
    }

    public void Undo() 
    {
        // If button is pressed again Undo the shooting

        Debug.Log("Undo Weapon Switch");
    }

    public void SwitchWeaponMethod() 
    {
        // Deactivate all weapons
        foreach (Weapon weapon in weaponInventory)
        {
            weapon.isActive = false;
            Debug.Log($"{weapon.Name} is deactivated");
            activeWeaponInv.Clear();
        }

        for (int i = 0; i < weaponInventory.Count; i++)
        {
            Weapon weapon = weaponInventory[i];

            if(weapon.isActive) 
            {
                currentWeaponIndex = i;
                currentWeapon = weapon;
                Debug.Log($"{currentWeapon}");

                break;
            }
        }

        // Should check if the currentWeapon is the same as the next weapon
        // If so, return
        // if(currentWeaponIndex == activeWeapon) return; 
       
        if (currentWeapon == null) return;
        
        int nextWeaponIndex = (currentWeaponIndex + 1) % weaponInventory.Count; // Get the next weapon index
        Weapon nextWeapon = weaponInventory[nextWeaponIndex]; // Store the next weapon
        currentWeaponIndex = nextWeaponIndex; // Set the current weapon index to the next weapon index
        Debug.Log($"{nextWeapon.Name} switched with {currentWeapon} with the number {currentWeaponIndex} as index");
        
        activeWeaponInv.Add(nextWeapon); 
        if(activeWeaponInv.Contains(nextWeapon)) nextWeapon.isActive = true; 
    }
}

public class ShootCommand : ICommand
{
    private readonly List<Weapon> weaponInventory;
    private Weapon activeWeapon;

    public ShootCommand(List<Weapon> weaponInventory) 
    {
        this.weaponInventory = weaponInventory;
    }

    public void Execute()
    {
        ShootMethod();
    }

    public void Undo() 
    {
        StopShooting();
        Debug.Log("Undo Shoot");
    }

    public void ShootMethod() 
    {
        // Find the active weapon, should only be one
        activeWeapon = weaponInventory.Find(w => w.isActive);
        activeWeapon.nextTimeToFire = 0f;


        if (activeWeapon != null) 
        {            
            // Shoot only the active weapon if enough time has passed
            if (Time.time >= activeWeapon.nextTimeToFire)
            {
                activeWeapon.isShooting = true;
                activeWeapon.nextTimeToFire = Time.time + 1f / activeWeapon.fireRate;
                Debug.Log($"Shooting with the {activeWeapon.Name}");
            }
        }
        else 
        {
            Debug.Log("No active weapon found!");
            return;
        }
    }

    public void StopShooting()
    {
        if (activeWeapon != null)
        {
            activeWeapon.isShooting = false;
            Debug.Log($"Shooting stopped with the {activeWeapon.Name}");
        } 
    }
}

public class AddWeaponCommand : ICommand
{
    private readonly ConcreteComponent concreteComponent;
    private readonly List<Weapon> weapons = new();
    private readonly Weapon weapon;

    public AddWeaponCommand(ConcreteComponent concreteComponent, List<Weapon> weapons, Weapon weapon)
    {
        this.concreteComponent = concreteComponent;
        this.weapons = weapons;
        this.weapon = weapon;
    }
    
    public void Execute()
    {
        concreteComponent.AddWeapon(weapons, weapon);
        Debug.Log("AddWeapon Command Execute");
        
    }

    public void Undo()
    {
        concreteComponent.RemoveWeapon(weapons, weapon);
        Debug.Log("AddWeapon Command Undo");
    }
}

public class RemoveWeaponCommand : ICommand
{
    public void Execute()
    {
        
    }

    public void Undo()
    {
        
    }
}

public class KeyCommand 
{
    public KeyCode key;
    public ICommand command;
}
