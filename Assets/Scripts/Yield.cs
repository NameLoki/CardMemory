using System.Collections.Generic;
using UnityEngine;

namespace CardMemory
{
    public class Yield
    {
        private static readonly Dictionary<float, WaitForSeconds> timeInterval = new Dictionary<float, WaitForSeconds>(new FloatComparer());

        public static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

        public static WaitForSeconds WaitForSecond(float second)
        {
            WaitForSeconds waitForSeconds;
            if(!timeInterval.TryGetValue(second, out waitForSeconds))
            {
                timeInterval.Add(second, new WaitForSeconds(second));
            }

            return waitForSeconds;
        }

    }

    internal class FloatComparer : IEqualityComparer<float>
    {
        public bool Equals(float x, float y)
        {
            return x == y;
        }

        public int GetHashCode(float obj)
        {
            return obj.GetHashCode();
        }
    }
}
