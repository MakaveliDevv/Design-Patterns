using System.Collections.Generic;

public abstract class Decorator 
{
    public abstract void AddWeapon(List<Weapon> wpnList, Weapon wpn);
    public abstract void RemoveWeapon(List<Weapon> wpnList, Weapon wpn);
}
