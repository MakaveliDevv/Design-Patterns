using UnityEngine;

public interface ICommand
{
    void Execute();
    void Undo();
}

public interface IAxisCommand 
{
    float GetAxisValue();
}

public class AxisCommand : IAxisCommand
{
    private readonly string Value = "";
    public AxisCommand(string value)
    {
        Value = value;
    }
    public float GetAxisValue() => Input.GetAxisRaw(Value);
}

public class KeyCommand 
{
    public KeyCode key;
    public ICommand command;
}
