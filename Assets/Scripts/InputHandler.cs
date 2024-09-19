using System.Collections.Generic;
using UnityEngine;
public class InputHandler
{
    private readonly List<KeyCommand> keyCommands = new();
    public delegate bool InputHandlerDelegate(KeyCode key);

    public ICommand HandleInput(InputHandlerDelegate inputMethod) 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            if(inputMethod(keyCommand.key)) 
            {
                keyCommand.command.Execute();
            }

            if(Input.GetKeyUp(keyCommand.key)) 
            {
                keyCommand.command.Undo();
            }
        }

        return null;
    }

    public ICommand HandleContinuousInput(InputHandlerDelegate inputMethod) 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            // Use the delegate for continuous actions
            if (inputMethod(keyCommand.key))
            {
                keyCommand.command.Execute();
            }
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
}
