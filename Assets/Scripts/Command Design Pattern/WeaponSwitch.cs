using System.Collections.Generic;
using UnityEngine;
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
