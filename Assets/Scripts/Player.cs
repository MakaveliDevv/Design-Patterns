using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;
    private InputHandler inputHandler;
    private AddWeaponCommand addWeaponCommand;
    private Interactable interactable;
    public bool inRange;
    public float moveSpeed = 5f;

    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        inputHandler = new();
        inputHandler.BindInputToCommand(KeyCode.E, addWeaponCommand);
    }

    void Update() 
    {
        inputHandler.HandleMovement(
            transform, moveSpeed, 
            new HorizontalAxisCommand(), new VerticalAxisCommand());
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if(collider.TryGetComponent<Interactable>(out var interactable))
        {
            this.interactable = interactable;
            this.interactable.InRange();

            if(this.interactable.scriptableObject is Weapon weapon) 
            {
                // If input then execute command
                inputHandler.HandleInput();

                if(inputHandler.isInputDone) 
                {
                    addWeaponCommand = new(new ConcreteComponent(), gameManager.weaponInventory, weapon);
                    addWeaponCommand.Execute();
                    Debug.Log($"Ready to add {weapon.Name} into the inventory");
                } 
            }
        } 
    }

    void OnTriggerStay2D(Collider2D collider2D) 
    {
        
    } 

  

    void OnTriggerExit2D(Collider2D collider) { inRange = false; }
}
