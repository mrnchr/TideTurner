using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.Obstacles
{
    public interface IBarrelFactory
    {
        Barrel Create(Transform spawn);
    }
}