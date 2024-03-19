using UnityEngine;
using Unity.Netcode.Components;

namespace Unity.Multiplayer.Samples.Utilities.OwnerAutority
{
    [DisallowMultipleComponent]
    public class OwnerNetworkAnimator : NetworkAnimator
    {
          protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
