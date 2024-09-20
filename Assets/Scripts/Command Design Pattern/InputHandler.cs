using System.Collections.Generic;
using UnityEngine;
public class InputHandler
{
    public bool isInputDone;
    private readonly List<KeyCommand> keyCommands = new();

    public ICommand HandleInput() 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            if(Input.GetKeyDown(keyCommand.key)) keyCommand.command.Execute();
            isInputDone = true;
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
        IAxisCommand horizontalAxis, IAxisCommand verticalAxis
    ) 
    {
        if(horizontalAxis == null || verticalAxis == null) return null; 
    
        float x = horizontalAxis.GetAxisValue();
        float y = verticalAxis.GetAxisValue();

        Vector2 direction = moveSpeed * Time.deltaTime * new Vector2(x, y);
        transform.Translate(direction);

        return null;
    }
}
