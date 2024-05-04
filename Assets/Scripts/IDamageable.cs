using System;
using System.Collections;
using System.Collections.Generic;
using Nightmare;
using UnityEngine;
using UnityEngine.AI;
public interface IDamageable
{
    public void TakeDamage(int amount);
}