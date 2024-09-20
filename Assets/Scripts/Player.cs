using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState
    {
        Moving,
        Interacting,
        Shooting,
        Switching
    }

    public PlayerState playerState;

    private GameManager gameManager;
    private InputHandler inputHandler;
    private AddWeaponCommand addWeaponCommand;
    public Interactable interactable;
    private ICommand command;

    public bool inRange;
    public float moveSpeed = 5f;

    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        inputHandler = new();
    }

    void Update()
    {
        command = inputHandler.HandleInput();

        inputHandler.HandleMovement(
            transform, moveSpeed,
            new AxisCommand("Horizontal"), new AxisCommand("Vertical"), this);

        if (inRange && interactable != null)
        {
            // Execute the command if it's a weapon-related command
            if (command is AddWeaponCommand addWeaponCommand)
            {
                playerState = PlayerState.Interacting;
                addWeaponCommand.Execute();
            }
        }

        inputHandler.BindInputToCommand(KeyCode.R, addWeaponCommand); 
        
        // Specifically handle R key logic if necessary
        if (command != null && command != addWeaponCommand) 
        {
            // Ensure R key is not triggering weapon addition
            if (inputHandler.keyCommands.Find(k => k.key == KeyCode.R)?.command == command)
            {
                // addWeaponCommand.Undo();
                Debug.Log("R key command executed.");
            }
        }
    
        // // Check if the command is for R key (assuming you bind it in the OnTriggerEnter2D method)
        // if (command is not null && command == inputHandler.keyCommands.Find(k => k.key == KeyCode.R)?.command)
        // {
        //     addWeaponCommand.Undo();
        //     // Execute R key command logic here
        //     Debug.Log("R key command executed");
        //     // Add logic for the R key if needed
        // }


        // if(inputHandler.keyPressed) 
        // {
        //     Debug.Log("Key is pressed");
        // }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.TryGetComponent<Interactable>(out var interactable))
        {
            this.interactable = interactable;

            if(this.interactable.scriptableObject is Weapon weapon) 
            {
                // Bind the command with the correct weapon once upon collision.
                AddWeaponCommand newAddWeaponCommand = new(new ConcreteComponent(), gameManager.weaponInventory, weapon);
                addWeaponCommand = newAddWeaponCommand;

                inputHandler.BindInputToCommand(KeyCode.E, addWeaponCommand);  // Bind it when in range
                interactable.InRange();
                Debug.Log($"Ready to add {weapon.Name} into the inventory"); 
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collider) { inRange = false; interactable = null; }
}
