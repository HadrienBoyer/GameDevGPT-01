using UnityEngine;

namespace TappyTale
{
    public static class LayerMaskUtil
    {
        // Purpose: Provide common layer mask helpers without repeated inspector setup.
        public static LayerMask EverythingBut(params int[] excludedLayers)
        {
            int mask = ~0;
            for (int i = 0; i < excludedLayers.Length; i++)
                mask &= ~(1 << excludedLayers[i]);
            return mask;
        }
    }
}
