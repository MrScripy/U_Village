using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class Warrior : Character
    {
        //public Warrior(int warriorsAmount = 5)
        //{
        //    this.characterAmount = warriorsAmount;
        //}
        //int characterAmount = 5;
        private int wheatToWarriors;
        public int WheatToWarriors { get => wheatToWarriors; set => wheatToWarriors = value; }

        public int Eat(int wheat)
        {
            return wheat -= CharacterAmount * wheatToWarriors;            
        }
        
    }
}
