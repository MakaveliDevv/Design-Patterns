using System.Collections.Generic;

public class ConcreteComponent : IComponent
{
    private ConcreteDecorator concreteDecorator;

    public ConcreteComponent() 
    {
        CustomAwake();
    }
    
    public void CustomAwake() 
    {
        concreteDecorator = new();
    }

    public void AddWeapon(List<Weapon> weaponList, Weapon weapon)
    {
        // Invoke method to add a weapon
        concreteDecorator.AddWeapon(weaponList, weapon);

        // Other stuff for the weapon

    }

    public void RemoveWeapon(List<Weapon> weaponList, Weapon weapon)
    {    
        // Invoke method to remove a weapon
        concreteDecorator.RemoveWeapon(weaponList, weapon);

        // Other stuff
    
    }
}
