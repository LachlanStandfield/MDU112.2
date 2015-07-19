using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Combat

    {
        //used to print the combat info
        int comabtlogTurnNumber;
        List<string> combatLog = new List<string>();


        Random r = new Random();
        /// <summary>
        /// twenty possible turns, 33 is placed in the free spaces to prevent one of the of player IDs being used
        /// </summary>
        int[] turns = new int[20] { 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33, 33 };
        List<int> availableTurns = new List<int>();


         int checkNumberAnswer()
        {
            int? number = null;
            int numcheck;
            while (number == null)
            {
                string Answer = Console.ReadLine();
                if (int.TryParse(Answer, out numcheck))
                {
                    number = Convert.ToInt32(Answer);
                }
                else
                {
                    Console.WriteLine("Please enter a number");
                }
            }
            return Convert.ToInt32(number);
        }

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
        /// prints turn order
        /// </summary>
        public void turnOrderPrint()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("        TURN ORDER");
            Console.WriteLine("---------------------------");
            Console.WriteLine();
            turnOrderFill();
            for (int i = 0; i < availableTurns.Count; i++ )
            {
                if (availableTurns[i] < Program.numberOfPlayers)
                {
                    Console.WriteLine((i+1)+" "+Program.Team1[availableTurns[i]].statBrief());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine((i+1)+" "+Program.Team2[availableTurns[i] - Program.numberOfPlayers].statBrief());
                    Console.WriteLine();
                }
            }

        }


        /// <summary>
        /// determines player order according to a random number 
        /// which is influenced by the dex stat of each character
        /// </summary>
        public void turnOrder()
        {
           while (specialEyes < (Program.numberOfPlayers*2)){

               bool valid = false;

               while (valid == false)
               {

                       turnOrderCount = specialEyes;
                       turns[specialEyes] = rng(0, Program.numberOfPlayers*2);
                       valid = checkAgainstOthers(turns[specialEyes]);

               }
               specialEyes++;
           }


        }


        /// <summary>
        /// checks turn indx value against other turns to make sure there are no duplicates
        /// </summary>

        bool checkAgainstOthers(int toCheck)
        {
            bool valid = true;
            bool firstTurn = false;
            int i = 0;

            if (turnOrderCount == 0)
            {
                firstTurn = true;
                valid = true;
            }
            if (firstTurn == false)
            {
                while (i < turnOrderCount)
                {
                    if (turns[i] == toCheck)
                    {
                        valid = false;
                    }
                    i++;
                }
            }
            return valid;
        }

        /// <summary>
        /// fill the turn order list
        /// </summary>
     public   void turnOrderFill()
        {
            availableTurns.Clear();
            for( int element=0; element <Program.numberOfPlayers*2; element ++)
            {
                if (turns[element] != 33)
                {
                    if (turns[element] < Program.numberOfPlayers)
                    {
                        if (Program.Team1[turns[element]].isDead == false)
                        {
                            availableTurns.Add(turns[element]);
                        }
                    }
                    else
                    {
                        if (Program.Teams[1][turns[element]-Convert.ToInt32(Program.numberOfPlayers)].isDead == false)
                        {
                            availableTurns.Add(turns[element]);
                        }
                    }
                }
            }
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
        /// Heal Who?
        /// displays allies with less than full health
        /// 1. Self 199/200   3. Jason Hero 153/200*
        ///Scan who?
        ///for each character display name and health with number to select them based on their ID
        ///1. John Hero 200/200 2.Jim Hero 200/200 3. Jason Hero 153/200 4.Jim Baddy 100/300 5.John Baddy 75/150 6.Harold Baddy 100/250
        ///if you scan then you print their stats, including if they're being defended

        public void playerOptions(int playerIndex)
        {
            int actionInput;
            bool actionTaken = false;
            Console.WriteLine( Program.Team1[playerIndex].statBrief()+" - TURN -");
            Console.WriteLine();
            //Console.WriteLine("{1} {0} -TURN-",Program.Team1[playerIndex].jobNames[Program.Team1[playerIndex].Job],Program.Team1[playerIndex].characterName);
            Console.WriteLine("SELCET ACTION");
            Console.WriteLine();
            Console.WriteLine("1. ATTACK  2. HEAL  3. DEFEND  4. SCAN 5. HOLD  6. TURN ORDER");
            int input = checkNumberAnswer();
            if (input < 1 || input > 6)
            {
                Console.WriteLine("INVALID COMMAND");
                Program.pressToContinue();
                Console.Clear();
                playerOptions(playerIndex);
                return;
            }
            //attack
            if (input == 1)
            {
                Console.WriteLine("SELECT TARGET TO ATTACK");
                Console.WriteLine();
                for (int i = 0; i < Program.Teams[1].Count; i++)
                {
            
                    if (Program.Teams[1][i].isDead == false)
                    {
                        Console.WriteLine();
                        Console.Write((i + 1) + ". " + Program.Teams[1][i].statBrief());
                        if (Program.Teams[1][i].defended == true)
                        {
                            Console.Write(" << DEFENDED BY " + Program.Teams[1][Program.Teams[1][i].defender].statBrief());
                        }
                        Console.WriteLine();
                    }

                }
                actionInput = checkNumberAnswer();
                dealdamage(playerIndex, actionInput - 1, 0, 1);
                actionTaken = true;

            }
            //heal
            if (input == 2)
            {
                Console.WriteLine("SELECT TARGET TO HEAL");
                Console.WriteLine();
                for (int i = 0; i < Program.Teams[0].Count; i++)
                {
            
                    if (Program.Teams[0][i].isDead == false)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.Teams[0][i].statBrief());
                        Console.WriteLine();
                    }

                }
                actionInput = checkNumberAnswer();
                if (actionInput < 1 || actionInput > Program.Teams[0].Count)
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID TARGET");
                    Program.pressToContinue();
                    Console.Clear();
                    playerOptions(playerIndex);
                    return;

                }
                if (Program.Teams[0][actionInput - 1].isDead == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("INVALID TARGET");
                    Program.pressToContinue();
                    Console.Clear();
                    playerOptions(playerIndex);
                    return;
                }
                heal(actionInput - 1, playerIndex, 0);
                actionTaken = true;

            }
            //defend
            if (input == 3)
            {

            }
            //scan
            if (input == 4)
            {
                scan(playerIndex);
            }
            //hold
            if (input == 5)
            {
                hold(playerIndex);
            }
            //turnorder
            if (input == 6)
            {
                turnOrderPrint();
            }
            if (actionTaken == true)
            {

            }
            else
            {
                playerOptions(playerIndex);
                return;
            }
            

        }

        void hold(int playerIndex)
        {

        }
        

        /// <summary>
        /// Scan Enemies and allies for detailed information
        /// </summary>
        /// <param name="playerIndex"></param>
        void scan(int playerIndex)
        {
            int selection;
            Console.WriteLine();
            Console.WriteLine("SELECT TARGET TO SCAN");
            Console.WriteLine();
                for (int i = 0; i < Program.Teams[0].Count; i++)
                {
                    if (Program.Teams[0][i].isDead == false)
                    {
                        Console.WriteLine((i + 1) + ". " + Program.Teams[0][i].statBrief());
                        Console.WriteLine();
                    }
                }
            //duplicate code, not sure how to fix this one.
            for (int i = 0; i < Program.Teams[1].Count; i++)
            {
                if (Program.Teams[1][i].isDead == false)
                {
                    Console.WriteLine((i + 1 + Program.Team1.Count) + ". " + Program.Teams[1][i].statBrief());
                    Console.WriteLine();
                }
            }
            selection = checkNumberAnswer() - 1;
            Console.WriteLine("SCANNING...");
            if (selection < 0 || selection > Program.numberOfPlayers * 2 - 1)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                Console.WriteLine();
            }
            else
            {
                if (selection < Program.Team1.Count)
                {
                    if (Program.Teams[0][selection].isDead == false)
                    {
                        Program.Teams[0][selection].PrintStats();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("INVALID TARGET");
                        Console.WriteLine();
                    }
                }
                else
                {
                    if (Program.Teams[1][selection - Program.Teams[1].Count].isDead == false)
                    {
                        Program.Teams[1][selection - Program.Teams[1].Count].PrintStats();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("INVALID TARGET");
                        Console.WriteLine();
                    }

                }

            }
            Program.pressToContinue();
            Console.Clear();
            playerOptions(playerIndex);

        }
        


        /// <summary>
        /// Ai chooses their target
        /// if one team member is defending another then choosing 
        /// the member the're defending forces them to attack the defender
        /// if they're a mage then they'll have a 75% chance to heal allies with 20% health or less.
        /// if they're a paladin then they'll have a 85% to defend for a rogue or mage.
        /// if they're a warrior then they have a 50% to defend for a rogue or mage.
        /// if they're a rogue then they'll attack regardless, rogues are pricks.
        /// </summary>
        void AIcombat()
        {

        }



       /// <summary>
       /// checks to see if a team  is dead
       /// </summary>
       bool isEveryoneDead(int teamToCheck)
       {
           int deaths = 0;
           bool yes = false;
           for (int i = 0; i < Program.numberOfPlayers; i++)
           {
               if (Program.Teams[teamToCheck][i].isDead)
               {
                   deaths++;
               }
           }
           if (deaths == Program.Teams[teamToCheck].Count)
           {
               yes = true;
           }
           return yes;
       }


        /// <summary>
        /// apply damage to target, base damage value is found in the character class, checks against defender's defensive stats
        /// </summary>
       public void dealdamage(int attacker, int target, int attackerTeamNumber, int defenderTeamNumber)
       {
           int damage;
           Console.WriteLine("{0} ATTACKS {1}...", Program.Teams[attackerTeamNumber][attacker].statBrief(), Program.Teams[defenderTeamNumber][target].statBrief());
           Console.WriteLine();
           System.Threading.Thread.Sleep(500);
           //checks if the target dodges and changes damage to 0
           damage = Program.Team1[attacker].characterDamage();
           if (Program.Team1[attacker].crit() == true)
           {
               Console.WriteLine("A CRITICAL HIT!");
               Console.WriteLine();
               System.Threading.Thread.Sleep(500);
               damage = damage * 5;
           }
           if (Program.Teams[defenderTeamNumber][target].dodge() == true)
           {
               Console.WriteLine("{0} DODGES IT!", Program.Teams[defenderTeamNumber][target].characterName);
               Console.WriteLine();
               System.Threading.Thread.Sleep(500);
               damage = 0;
           }
           //checks if the target block the attack and changes damage to 0
           else if (Program.Teams[defenderTeamNumber][target].block() == true)
           {
               Console.WriteLine("{0} BLOCKS IT!", Program.Teams[defenderTeamNumber][target].characterName);
               Console.WriteLine();
               System.Threading.Thread.Sleep(500);
               damage = 0;
           }
           else
           {
               //check the attacker ins't a mage, if they aren't then apply armour
               if (Program.Teams[attackerTeamNumber][attacker].Job != 3)
               {
                   decimal armour;
                   if (Program.Teams[defenderTeamNumber][target].defending == false)
                   {
                       armour = 1 - Convert.ToDecimal(Program.Teams[defenderTeamNumber][target].armour) / 100;
                   }
                   //if they're defending then the armour value is doubled
                   else
                   {
                       armour = Convert.ToDecimal(Program.Teams[defenderTeamNumber][target].armour);
                       //armour can't be higher than 90%
                       if (armour > 90)
                       {
                           armour = 90;
                       }
                       armour /= 100;
                   }
                   damage = Convert.ToInt32(Convert.ToDecimal(damage) * armour);
               }
           }


           Program.Teams[defenderTeamNumber][target].takedamage(damage);
           System.Threading.Thread.Sleep(500);
       }




        /// <summary>
        /// apply healing to target
        /// </summary>
        public void heal(int target, int healer,int healerTeam)
        {
                Console.WriteLine("{0} HEALS {1}", Program.Teams[healerTeam][healer].statBrief(), Program.Teams[healerTeam][target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                Program.Teams[healerTeam][target].receiveHeal(Program.Teams[healerTeam][healer].characterHeal());

        }





    }

