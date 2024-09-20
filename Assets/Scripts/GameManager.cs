using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Weapon> weaponInventory = new();
    public List<Weapon> activeWeapon = new();

    private InputHandler inputHandler;
    private WeaponSwitchCommand weaponSwitchCommand; 
    private ICommand command; 

    private void Awake() 
    {
        inputHandler = new();

        weaponSwitchCommand = new(weaponInventory, activeWeapon)
        {
            currentWeapon = weaponInventory[0]
        };

        inputHandler.BindInputToCommand(KeyCode.Q, weaponSwitchCommand);
        inputHandler.BindInputToCommand(KeyCode.Mouse0, new ShootCommand(weaponInventory));
    }

    private void Start() 
    {
        // Clear the active state for all weapons first
        foreach (Weapon weapon in weaponInventory)
        {
            weapon.isActive = false;
            weapon.isShooting = false;
            weapon.nextTimeToFire = 0f;
        }

        // Set the first weapon as active and add it to activeWeapon list
        weaponInventory[0].isActive = true;
        activeWeapon.Add(weaponInventory[0]); 
    }

    private void Update() 
    {   
        if(activeWeapon[0].isShooting) command = inputHandler.HandleContinuousInput();  
    
        command = inputHandler.HandleInput();
        command?.Execute();
        command?.Undo();
    }
}
