using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    abstract class Character
    {
        private int characterAmount;
        private int characterCost;
        public int CharacterAmount
        {
            get => characterAmount;
            set => characterAmount = value;
        }

        public int CharacterCost
        {
            get => characterCost;
            set
            {
                if (value < 1)
                    characterCost = 1;
                else
                    characterCost = value;
            }
        }

        //public void AddCharacter(int wheat)
        //{
        //    characterAmount += 1;
        //    wheat -= characterCost;
        //}
    }
}
