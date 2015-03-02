using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonWarrior
{
    class PlayerLook
    {
        static void Main(string[] args)
        {
            if (playerlevel <= 2)
            {
                playermodel = "-.☺.-";
            }
            else if (playerlevel <= 5)
            {
                playermodel = " -.☺.= ";
            }
            else if (playerlevel <= 9)
            {
                playermodel = " =.☺.= ";
            }
            else if (playerlevel <= 12)
            {
                playermodel = " -.☺.== ";
            }
            else
            {
                playermodel = " ==.☺.== ";
            }
        }
    }
}
