﻿using System.Linq;
using Gameplay.Handlers;
using Scriptables.Weapons;
using Utilities;

namespace Managers.Spawners
{
    public class WeaponContainerSpawner : BaseContainerSpawner<WeaponContainer, WeaponConfig>
    {
        public override void Enable()
        {
            base.Enable();
            
            PlayerSpawner.PlayerStats.WeaponSelector.Set(Items.GetRandom());
        }

        public override WeaponConfig GetItem()
        {
            var weapon = PlayerSpawner.PlayerStats.WeaponSelector.CurrentWeapon;
            
            return Items.Where(item => item != weapon).ToArray().GetRandom();
        }
    }
}