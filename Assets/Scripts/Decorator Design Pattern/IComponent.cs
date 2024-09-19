using System.Collections.Generic;

public interface IComponent
{
    void AddWeapon(List<Weapon> weaponList, Weapon weapon);
    void RemoveWeapon(List<Weapon> weaponList, Weapon weapon);
}