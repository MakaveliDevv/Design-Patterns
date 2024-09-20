using System.Collections.Generic;
using UnityEngine;
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