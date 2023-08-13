using System;
using UnityEngine;

namespace Assets.Game.Scripts.Builders
{
    public interface IBuildObject
    {
        GameObject GameObject { get; }

        event Action<Collider> TriggerEntered;
        event Action<Collider> TriggerExit;
    }
}
