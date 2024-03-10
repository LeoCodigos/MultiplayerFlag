using UnityEngine;
using Unity.Netcode.Components;

namespace Unity.Multiplayer.Samples.Utilities.ClientAutority
{
    [DisallowMultipleComponent]
    public class ClientNetworkAnimator : NetworkAnimator
    {
          protected override bool OnIsServerAuthoritative()
        {
            return false;
        }
    }
}
