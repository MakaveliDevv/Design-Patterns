using System.Collections.Generic;
using UnityEngine;
public class InputHandler
{
    public List<KeyCommand> keyCommands = new();
    public bool keyPressed;

    public ICommand HandleInput() 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            if(Input.GetKeyDown(keyCommand.key)) 
            {
                keyCommand.command.Execute();
                keyPressed = true;
                Debug.Log("HandleInput() executed!");	
                return keyCommand.command;
            }

            if(Input.GetKeyUp(keyCommand.key)) 
            {
                keyPressed = false;
            }
        }

        return null;
    }

    public ICommand HandleContinuousInput() 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            if (Input.GetKey(keyCommand.key)) keyCommand.command.Execute();
            if(Input.GetKeyUp(keyCommand.key)) keyCommand.command.Undo(); 
        }

        return null;
    }

    public void BindInputToCommand(KeyCode keyCode, ICommand command) 
    {
        keyCommands.Add(new KeyCommand()
        {
            key = keyCode,
            command = command
        });
    }

    public void UnBindInput(KeyCode keyCode) 
    {
        var items = keyCommands.FindAll(x => x.key == keyCode);
        items.ForEach(x => keyCommands.Remove(x));

        Debug.Log($"{keyCode} unbind");
    }

    public ICommand HandleMovement(
        Transform transform, float moveSpeed, 
        IAxisCommand horizontalAxis, IAxisCommand verticalAxis, Player player
    ) 
    {
        if(horizontalAxis == null || verticalAxis == null) return null; 
    
        float x = horizontalAxis.GetAxisValue();
        float y = verticalAxis.GetAxisValue();

        Vector2 direction = moveSpeed * Time.deltaTime * new Vector2(x, y);
        transform.Translate(direction);

        player.playerState = Player.PlayerState.Moving;

        return null;
    }
}
