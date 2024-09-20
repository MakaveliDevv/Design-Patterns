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

public class HorizontalAxisCommand : IAxisCommand
{
    public float GetAxisValue() => Input.GetAxisRaw("Horizontal");
}

public class VerticalAxisCommand : IAxisCommand 
{
    public float GetAxisValue() => Input.GetAxisRaw("Vertical");
}

public class KeyCommand 
{
    public KeyCode key;
    public ICommand command;
}
