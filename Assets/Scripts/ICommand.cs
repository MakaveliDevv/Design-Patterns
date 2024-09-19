using System.Collections.Generic;
using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public class JumpCommand : ICommand 
{
    public void Execute() 
    {
        Debug.Log("Jump");
    }

    public void Undo() 
    {
        Debug.Log("Undo from jump class");
    }
}

public class WeaponSwitchCommand : ICommand 
{
    private readonly List<Weapon> weaponInventory;
    private readonly List<Weapon> activeWeaponInv;
    public Weapon currentWeapon;
    public bool isWeaponSwitched;
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
        Debug.Log("Undo from WeaponSwitch class");
    }

    public void SwitchWeaponMethod() 
    {
        // Deactivate all weapons
        foreach (Weapon weapon in weaponInventory)
        {
            weapon.isActive = false;
            activeWeaponInv.Clear();
        }

        for (int i = 0; i < weaponInventory.Count; i++)
        {
            Weapon weapon = weaponInventory[i];

            if(weapon.isActive) 
            {
                currentWeaponIndex = i;
                currentWeapon = weapon;
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

        int nextWeaponIndex = (currentWeaponIndex + 1) % weaponInventory.Count;
        Weapon nextWeapon = weaponInventory[nextWeaponIndex];
        currentWeaponIndex = nextWeaponIndex;
        activeWeaponInv.Add(nextWeapon);

        
        if(activeWeaponInv.Contains(nextWeapon)) 
        {
            isWeaponSwitched = true;
            nextWeapon.isActive = true;
        }
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

        Debug.Log("Undo from Shoot class");
    }

    public void ShootMethod() 
    {
        // Find the active weapon, should only be one
        activeWeapon = weaponInventory.Find(w => w.isActive);
        activeWeapon.nextTimeToFire = 0f;

        if (activeWeapon == null)
        {
            Debug.LogError("No active weapon found at initialization!");
            return;
        }

        // Shoot only the active weapon if enough time has passed
        if (Time.time >= activeWeapon.nextTimeToFire)
        {
            activeWeapon.isShooting = true;
            activeWeapon.nextTimeToFire = Time.time + 1f / activeWeapon.fireRate;
            Debug.Log($"Shooting with {activeWeapon.name}");
        }
    }

    public void StopShooting()
    {
        if (activeWeapon != null)
        {
            // activeWeapon.nextTimeToFire = 0f;
            activeWeapon.isShooting = false;
            Debug.Log($"Stopped shooting with {activeWeapon.name}");
        }
    }
}

public class KeyCommand 
{
    public KeyCode key;
    public ICommand command;
}
