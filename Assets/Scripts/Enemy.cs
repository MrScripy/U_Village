using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class Enemy : Character
    {
        //public Enemy(int enemiesAmount = 5)
        //{
        //    this.characterAmount = enemiesAmount;
        //}


        public int Attack(int warriorsAmounts)
        {
            if (CharacterAmount > 0)
                warriorsAmounts -= CharacterAmount;
            CharacterAmount += 1;
            return warriorsAmounts;
        }

    }
}
