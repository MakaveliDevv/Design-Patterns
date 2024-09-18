using System.Collections.Generic;
using UnityEngine;
public class InputHandler
{
    private readonly List<KeyCommand> keyCommands = new();

    public ICommand HandleInput() 
    {
        foreach (KeyCommand keyCommand in keyCommands)
        {
            if(Input.GetKeyDown(keyCommand.key)) 
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
