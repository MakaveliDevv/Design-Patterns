using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ScriptableObject> weaponInventory = new();
    private InputHandler inputHandler; // Input Class
    private WeaponSwitchCommand weaponSwitchCommand; // Switching weapon class
    private ICommand command; 

    private void Awake() 
    {
        inputHandler = new();
        weaponSwitchCommand = new(weaponInventory)
        {
            currentWeapon = (Weapon)weaponInventory[0]
        };

        inputHandler.BindInputToCommand(KeyCode.Q, weaponSwitchCommand);
        inputHandler.BindInputToCommand(KeyCode.Space, new JumpCommand());
        inputHandler.BindInputToCommand(KeyCode.P, new ShootCommand(weaponInventory));
    }

    private void Update() 
    {
        command = inputHandler.HandleInput();
        command?.Execute();
        
    }
}
