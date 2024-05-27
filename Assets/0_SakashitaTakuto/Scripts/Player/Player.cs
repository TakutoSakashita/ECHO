using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    private void Update()
    {
        if(GetPlayerHp() <= 0)
        {
            PlayerIsDead();
        }
    }

    // �_���[�W����
    public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner owner)
    {
        if (owner == HittingObject.WeaponOwner.Player) return;
        if (this.gameObject.layer == 6) return;
        Animate(Player.Damage);
        HpDown(damage._damagereceived());
    }

    // ���݂�HP�����^�[������
    public int GetPlayerHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// ���S�t���O
    /// </summary>
    public void PlayerIsDead()
    {
        Animate(Player.Dead);
        // ���G���C���[�Ɉړ�
        this.gameObject.layer = 6;
    }
}
