using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nightmare
{
    public abstract class Weapon : MonoBehaviour
    {
        public int baseDamage;
        public float attackSpeed;
        public float range;
        public AudioSource attackSound;

        public abstract void Attack();
        public abstract void DisableEffects();
        public abstract void UpdateAttack();
        public abstract void IncreaseDamage(int damageIncrease);
    }

}
