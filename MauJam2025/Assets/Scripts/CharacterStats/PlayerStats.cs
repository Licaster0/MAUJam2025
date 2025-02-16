using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    private Player player;
    protected override void Start()
    {
        base.Start();

        player = GetComponent<Player>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    protected override void Die()
    {
        base.Die();

        player.Die();
    }

    protected override void DecreaseHealthBy(int _damage)
    {
        base.DecreaseHealthBy(_damage);

        if (isDead)
            return;

        if (_damage > GetMaxHealthValue() * .3f)
        {
            player.SetupKnockbackPower(new Vector2(2, 3));
            player.fx.ScreenShake(player.fx.shakeHighDamage);

            int randomSound = Random.Range(32, 33);
        }

        // ItemData_Equipment currentArmor = Inventory.instance.GetEquipment(EquipmentType.Armor);

        // if (currentArmor != null)
        //     currentArmor.Effect(player.transform);
    }

}
