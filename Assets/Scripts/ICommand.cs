using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
}

public class JumpCommand : ICommand 
{
    public void Execute() 
    {
        Debug.Log("Jump");
    }
}

public class WeaponSwitchCommand : ICommand 
{
    private readonly List<ScriptableObject> weaponInventory;
    public Weapon currentWeapon;
    public bool isWeaponSwitched;
    private int currentWeaponIndex;

    public WeaponSwitchCommand(List<ScriptableObject> weaponInventory) 
    {
        this.weaponInventory = weaponInventory;
    }

    public void Execute()
    {
        SwitchWeaponMethod();
        Debug.Log("Weapon Switched");
    }

    public void SwitchWeaponMethod() 
    {
        // Weapon currentWeapon = null;
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            Weapon weapon = (Weapon)weaponInventory[i];

            if(weapon.isActive) 
            {
                // Get the index from that weapon
                currentWeaponIndex = i;
                currentWeapon = weapon;
                Debug.Log($"{currentWeapon}");
                break;
            }
        }

        // Should check if the currentWeapon is the same as the next weapon
        // If so, return
        // if(currentWeaponIndex == activeWeapon) return; 
       
        if (currentWeapon == null)
        {
            Debug.LogError("No active weapon found!");
            return;
        }

        // Get the next weapon in the list
        int nextWeaponIndex = (currentWeaponIndex + 1) % weaponInventory.Count;

        // Deactivate the current weapon
        currentWeapon.isActive = false;
        Debug.Log($"{currentWeapon.name} turned off");
        
        // Activate the next weapon
        Weapon nextWeapon = (Weapon)weaponInventory[nextWeaponIndex];
        nextWeapon.isActive = true;
        Debug.Log($"{nextWeapon.name}");

        // Store the current selected weapon index
        currentWeaponIndex = nextWeaponIndex;
        Debug.Log($"The {currentWeaponIndex} is now stored as the {nextWeaponIndex}");

        isWeaponSwitched = true;
    }
}

public class ShootCommand : ICommand
{
    private readonly List<ScriptableObject> weaponInventory;

    public ShootCommand(List<ScriptableObject> weaponInventory) 
    {
        this.weaponInventory = weaponInventory;
    }

    public void Execute()
    {
        ShootMethod();
    }

    public void ShootMethod() 
    {
        // Check if the weapon is active
        // Need a reference to the scriptobject in the weaponInventory
        for (int i = 0; i < weaponInventory.Count; i++)
        {
            Weapon weapon = (Weapon)weaponInventory[i];
            if(weapon.isActive) 
            {
                Debug.Log($"Shooting with {weapon}");
            }
        }
    }
}

public class KeyCommand 
{
    public KeyCode key;
    public ICommand command;
}
