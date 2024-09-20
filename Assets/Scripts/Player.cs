using UnityEngine;

public class Player : MonoBehaviour
{
    public Interactable interactable;
    private GameManager gameManager;
    private InputHandler inputHandler;
    private AddWeaponCommand addWeaponCommand;
    private AddWeaponCommand lastWeaponCommand;
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

        if (inRange && interactable != null && command is AddWeaponCommand)        
        {
            // Execute the command if it's a weapon-related command
            addWeaponCommand.Execute();
            lastWeaponCommand = addWeaponCommand;

            Destroy(interactable.gameObject);

            // Unbind the input once executed
            inputHandler.UnBindInput(KeyCode.E);
            addWeaponCommand = null;

            inputHandler.BindInputToCommand(KeyCode.R, lastWeaponCommand);

        }

        // Check if the command corresponds to the R key command
        if (command == inputHandler.keyCommands.Find(k => k.key == KeyCode.R)?.command && lastWeaponCommand != null)
        {
            Debug.Log("R key detected");
            lastWeaponCommand.Undo();
            Debug.Log("R key command executed for undo.");

            inputHandler.UnBindInput(KeyCode.R);
            lastWeaponCommand = null;


            Debug.Log("Unbind R key.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.TryGetComponent<Interactable>(out var interactable))
        {
            this.interactable = interactable;

            if(this.interactable.scriptableObject is Weapon weapon) 
            {
                interactable.InRange();

                // Bind the command with the correct weapon once upon collision.
                addWeaponCommand = new(new ConcreteComponent(), gameManager.weaponInventory, weapon);
                inputHandler.BindInputToCommand(KeyCode.E, addWeaponCommand);  // Bind it when in range
                Debug.Log($"Ready to add {weapon.Name} into the inventory"); 
            }
        } 
    }

    private void OnTriggerExit2D(Collider2D collider) 
    { 
        if (collider.TryGetComponent<Interactable>(out var interactable) && this.interactable == interactable)
        {
            inRange = false;
            this.interactable = null;
            
            // Reset the command binding when leaving the radius
            if (addWeaponCommand != null)
            {
                inputHandler.UnBindInput(KeyCode.E);
                addWeaponCommand = null;
            }
        }
    }
}
