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

    // ダメージ判定
    public override void ApllayDamage(Damage damage, HittingObject.WeaponOwner owner)
    {
        if (owner == HittingObject.WeaponOwner.Player) return;
        if (this.gameObject.layer == 6) return;
        Animate(Player.Damage);
        HpDown(damage._damagereceived());
    }

    // 現在のHPをリターンする
    public int GetPlayerHp()
    {
        return status.GetHp();
    }

    /// <summary>
    /// 死亡フラグ
    /// </summary>
    public void PlayerIsDead()
    {
        Animate(Player.Dead);
        // 無敵レイヤーに移動
        this.gameObject.layer = 6;
    }
}
