using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Combat

    {
        Random r = new Random();
        //ten possible turns, 33 is placed in the free spaces to prevent one of the of player IDs being used
        int[] turns = new int[20] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 };


        int turnOrderCount;
        int specialEyes = 0;
        ///<summary>
        ///random number generator
        ///</summary>

        int rng(int min, int max)
        {
            int number = r.Next(min, max);
            return number;
        }

        /// <summary>
        /// Information printed between each turn,
        /// Shows Turn Order >>Horizontally>> with character 
        /// Names and hp as well as whether they're being defended by another character
        /// </summary>
        /// shown as     |DEF| JOHN D 150/150            |DEF| PLAYER2 200/400
        ///              >>     MAX C 30/100  >>            PLAYER1 40/200   >>           |DEF|PLAYER2 200/400   >>  |DEF| JOHN D 150/150  


        public void testRemove()
        {
            turns[0] = 33;
            turns[1] = 33;

        }



        public void battleInfo()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("       BATTLE INFO");
            Console.WriteLine("---------------------------");
            Console.WriteLine("    v v TURN ORDER v v");
            for (int i = 0; i < (Program.numberOfPlayers*2); i++)
            {
                if (turns[i] != 33)
                {

                    if (turns[i] <= (Program.numberOfPlayers - 1))
                    {
                        Console.WriteLine();
                        Console.WriteLine(Program.Team1[turns[i]].statBrief());
                    }

                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine(Program.Team2[(turns[i] - Convert.ToInt32(Program.numberOfPlayers))].statBrief());

                    }
                }
            }

        }







        /// <summary>
        /// determines player order according to a random number 
        /// which is influenced by the dex stat of each character
        /// </summary>
        public void turnOrder()
        {
            //Console.WriteLine((Program.numberOfPlayers-1)*2+" turns");
           //reset the turn order
  
           while (specialEyes < (Program.numberOfPlayers*2)){

               bool valid = false;

               while (valid == false)
               {

                       turnOrderCount = specialEyes;
                       turns[specialEyes] = rng(0, Convert.ToInt32(Program.numberOfPlayers*2));
                       //if (turns[specialEyes] == 0)
                       //{
                       //    turns[specialEyes] = 1;
                      // }
                       //Console.WriteLine(turns[specialEyes]);
                       valid = checkAgainstOthers(turns[specialEyes]);

               }
               //Console.WriteLine("Turn "+specialEyes+" is "+turns[specialEyes]+". I've left the loop");
               specialEyes++;
           }
          // for (int counter = 0; counter < (Program.numberOfPlayers*2); counter++)
          // {
           //    Console.Write(" " + turns[counter]);
          // }

        }


        /// <summary>
        /// checks turn id value against other turns to make sure there are no duplicates
        /// </summary>

        bool checkAgainstOthers(int toCheck)
        {
            bool valid = true;
            bool firstTurn = false;
            int i = 0;

            if (turnOrderCount == 0)
            {
                //Console.WriteLine("This is turn 0");
                firstTurn = true;
                valid = true;
            }
            if (firstTurn == false)
            {
                while (i < turnOrderCount)
                {
                    if (turns[i] == toCheck)
                    {
                        //Console.WriteLine("This is a duplicate of Turn " + i);
                        //Console.ReadKey();
                        valid = false;
                    }
                    i++;
                }
            }
            return valid;
        }




        /// <summary>
        /// main combat sequence, team and player specifies who's fighting at 
        /// the time, if they're ai then the it itiliases thr ai commands otherwise the player chooses their move
        /// </summary>
        ///             
        ///choose action
        ///1.Attack  2.Defend  3.Heal  4.Hold  5.Scan
        ///
        ///Attack Who?
        ///for each character display name and health with number to select them based on their ID
        ///4.Jim Baddy 100/300       5.John Baddy 75/150           6.Harold Baddy 100/250
        ///
        /// Defend Who?
        /// Displays characters able to be defended, this includes themself
        /// 1. Self 200/200  2. John Hero 200/200 3. Jason Hero 153/200
        /// if you defend smomone else then the defend other method is called with Jim as the acitve defender
        /// 
        ///Scan who?
        ///for each character display name and health with number to select them based on their ID
        ///1. John Hero 200/200 2.Jim Hero 200/200 3. Jason Hero 153/200 4.Jim Baddy 100/300 5.John Baddy 75/150 6.Harold Baddy 100/250
        ///if you scan then you print their stats, including if they're being defended

        public void combatphase(int team, int player)
        {

        }



        /// <summary>
        /// player chooses their target and their move
        /// if the target is an ally they'll be promted to heal or defend
        /// if it's an enemy then they can attack
        /// if one team member is d*/*+*-*efending another then choosing 
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

