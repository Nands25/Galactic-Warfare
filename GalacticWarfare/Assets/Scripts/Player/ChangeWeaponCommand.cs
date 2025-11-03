public class ChangeWeaponCommand : ICommand
{
    private PlayerController player;
    private WeaponType previous;
    private WeaponType next;

    public ChangeWeaponCommand(PlayerController player, WeaponType prev, WeaponType next)
    {
        this.player = player;
        this.previous = prev;
        this.next = next;
    }

    public void Execute()
    {
        player.ChangeWeapon(next);
    }

    public void Undo()
    {
        player.ChangeWeapon(previous);
    }
}