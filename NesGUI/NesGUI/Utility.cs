using UnityEngine;

namespace NesGUI
{
    class Utility
    {
        public static int DetermineScrollDelta(Event e)
        {
            int returnInt;

            returnInt = (e.delta.y > 0) ? 1 : -1;
            returnInt = (e.shift) ? returnInt *= 100 : returnInt;
            returnInt = (e.control) ? returnInt *= 10 : returnInt;


            if (e.alt)
            {
                returnInt = (e.delta.y > 0) ? 1 : -1;

            } else if(returnInt == 1 || returnInt == -1)
            {
                returnInt = (e.delta.y > 0) ? 5 : -5;
            }


            return returnInt;
        }
    }
}
