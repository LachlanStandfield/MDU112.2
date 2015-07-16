using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Combat
    {
        /// <summary>
        /// Information printed between each turn,
        /// Shows Turn Order >>Horizontally>> with character 
        /// Names and hp as well as whether they're being defended by another character
        /// </summary>
        /// shown as     |DEF| JOHN D 150/150            |DEF| PLAYER2 200/400
        ///              >>     MAX C 30/100  >>            PLAYER1 40/200   >>           |DEF|PLAYER2 200/400   >>  |DEF| JOHN D 150/150          
        void battleInfo()
        {

        }


        /// <summary>
        /// determines player order according to a random number 
        /// which is influenced by the dex stat of each character
        /// </summary>

        public void turnOrder()
        {

        }

        /// <summary>
        /// main combat logic, team and player specifies who's fighting at 
        /// the time, if they're ai then the ai target choice and move choice plays
        /// </summary>
        public void combatphase(int team, int player)
        {

        }












        /// <summary>
        /// player chooses their target and their move
        /// if the target is an ally they'll be promted to heal or defend
        /// if it's an enemy then they can attack
        /// if one team member is defending another then choosing 
        /// the member the're defending forces them to attack the defender
        /// </summary>
        void playerCombat()
        {

        }


        /// <summary>
        /// Ai chooses their target
        /// if one team member is defending another then choosing 
        /// the member the're defending forces them to attack the defender
        /// if they're a mage then they'll have a 75% chance to heal allies with 20% health or less.
        /// if they're a paladin then they'll have a 75% to block for a rogue or mage.
        /// if they're a warrior then they have a 50% to block for a rogue or mage.
        /// if they're a rogue then they'll attack regardless, rogues are pricks.
        /// </summary>
        void AIcombat()
        {

        }
        /// <summary>
        /// apply damage to target, base damage value is found in the character class, checks against defender's defensive stats
        /// </summary>
        void dealdamage(int attackerDamage, int defenderBlock, int defenderArmour, int defenderDodge)
        {

        }
        /// <summary>
        /// apply healing to target
        /// </summary>
        void heal()
        {

        }









    }

