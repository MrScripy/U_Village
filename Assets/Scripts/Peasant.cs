using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class Peasant : Character
    {
        private int wheatPerPeasant;
        public int WheatPerPeasant
        {
            get => wheatPerPeasant;
            set
            {
                if (value < 2)
                    wheatPerPeasant = 2;
                else
                    wheatPerPeasant = value;
            }

        }

        public int ProduceWheat(int wheat)
        {
            return wheat += CharacterAmount * wheatPerPeasant;
        }


    }
}
