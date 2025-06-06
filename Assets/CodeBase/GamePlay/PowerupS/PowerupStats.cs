using Space_lancer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space_lancer
{
    public class PowerupStats : Powerup
    {
        public enum EffectType
        {
            AddAmmo,
            AddEnergy,
            SpeedUp
        }

        [SerializeField] private EffectType _effectType;
        [SerializeField] private float _value;
        protected override void OnPickedUp(SpaceShip ship)
        {
            if (_effectType == EffectType.AddEnergy)
            {
                ship.AddEnergy((int)_value);
            }

            if (_effectType == EffectType.AddAmmo)
            {
                ship.AddAmmo((int)_value);
            }

            if (_effectType == EffectType.SpeedUp)
            {
                ship.SpeedUp((int)_value);
            }
        }
    }
}