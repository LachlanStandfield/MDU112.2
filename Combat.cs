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
                    Console.WriteLine((i+1)+" "+Program.Team2[availableTurns[i] - Convert.ToInt32(Program.numberOfPlayers)].statBrief());
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
                       turns[specialEyes] = rng(0, Convert.ToInt32(Program.numberOfPlayers*2));
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
                        if (Program.Team2[turns[element]-Convert.ToInt32(Program.numberOfPlayers)].isDead == false)
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
            Console.WriteLine( Program.Team1[playerIndex].statBrief()+" - TURN -");
            Console.WriteLine();
            //Console.WriteLine("{1} {0} -TURN-",Program.Team1[playerIndex].jobNames[Program.Team1[playerIndex].Job],Program.Team1[playerIndex].characterName);
            Console.WriteLine("SELCET ACTION");
            Console.WriteLine();
            Console.WriteLine("1. ATTACK 2. HEAL 3. DEFEND 4. SCAN 5. HOLD 6. TURN ORDER");
            int input = checkNumberAnswer();
            if (input == 4)
            {
                scan(playerIndex);
            }
            

        }



        void scan(int playerIndex)
        {
            int selection;
            Console.WriteLine();
            Console.WriteLine("SELECT TARGET TO SCAN");
            Console.WriteLine();
            for (int i = 0; i < Program.Team1.Count; i++)
            {
                if (Program.Team1[i].isDead == false)
                {
                    Console.WriteLine((i + 1) + ". " + Program.Team1[i].statBrief());
                }
            }
            //duplicate code, must place teams in a nested array to fix this
            for (int i = 0; i < Program.Team1.Count; i++)
            {
                if (Program.Team2[i].isDead == false)
                {
                    Console.WriteLine((i + 1 + Program.Team1.Count) + ". " + Program.Team2[i].statBrief());
                }
            }
            selection = checkNumberAnswer()-1;
            Console.WriteLine("SCANNING...");
            if (selection < 0 || selection > Program.numberOfPlayers * 2-1)
            {
                Console.WriteLine("INVALID TARGET");
            }
            else
            {
            if (selection < Program.Team1.Count)
            {
                if (Program.Team1[selection].isDead == false)
                {
                    Program.Team1[selection].PrintStats();
                }
                else
                {
                    Console.WriteLine("INVALID TARGET");
                }
            }
            else
            {
                if (Program.Team2[selection - Program.Team2.Count].isDead == false)
                {
                    Program.Team2[selection - Program.Team2.Count].PrintStats();
                }
                else
                {
                    Console.WriteLine("INVALID TARGET");
                }
                
            }

            }
            Program.pressToContinue();
            Console.Clear();
            playerOptions(playerIndex);

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
        /// checks to see if team 1 is dead team is dead
        /// </summary>

       /// <summary>
       /// checks to see if team 1 is dead
       /// </summary>
       bool isEveryoneDead1()
       {
           int deaths = 0;
           bool yes = false;
           for (int i = 0; i < Program.numberOfPlayers; i++)
           {
               if (Program.Team1[i].isDead)
               {
                   deaths++;
               }
           }
           if (deaths == Program.Team1.Count)
           {
               yes = true;
           }
           return yes;
       }

       /// <summary>
       /// checks to see if team 2 is dead
       /// </summary>
       bool isEveryoneDead2()
       {
           int deaths = 0;
           bool yes = false;
           for (int i = 0; i < Program.numberOfPlayers; i++)
           {
               if (Program.Team2[i].isDead)
               {
                   deaths++;
               }
           }
           if (deaths == Program.Team2.Count)
           {
               yes = true;
           }
           return yes;
       }

        /// <summary>
        /// apply damage to target, base damage value is found in the character class, checks against defender's defensive stats
        /// </summary>
        public void dealdamage(bool player,int attacker,int target)
        {
            int damage;
            Console.WriteLine();
            if (player == true)
            {
                Console.WriteLine("{0} ATTACKS {1}...", Program.Team1[attacker].statBrief(), Program.Team2[target].statBrief());
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
                if (Program.Team2[target].dodge() == true){
                    Console.WriteLine("{0} DODGES IT!",Program.Team2[target].characterName);
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(500);
                    damage = 0;
                }
                    //checks if the target block the attack and changes damage to 0
                else if (Program.Team2[target].block() == true)
                {
                    Console.WriteLine("{0} BLOCKS IT!",Program.Team2[target].characterName);
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(500);
                    damage = 0;
                }
                else
                {
                    //check the attacker ins't a mage, if they aren't then apply armour
                    if (Program.Team1[attacker].Job != 3){
                        decimal armour = 1 - Convert.ToDecimal(Program.Team2[target].armour) / 100;
                        //if they're defending then the armour value is doubled
                        if (Program.Team2[target].defending == true)
                        {
                            armour -= Convert.ToDecimal(Program.Team2[target].armour) / 100;
                        }
                        damage = Convert.ToInt32(Convert.ToDecimal(damage)*armour);
                    }
                }
            }

                //duplicate code not sure how to to this atm. might have to place the two lists into an array.
            else
            {
                Console.WriteLine("{0} ATTACKS {1}", Program.Team2[attacker].statBrief(), Program.Team1[target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                damage = Program.Team2[attacker].characterDamage();
                if (Program.Team1[attacker].crit() == true)
                {
                    Console.WriteLine("A CRITICAL HIT");
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(500);
                    damage = damage * 5;
                }
                if (Program.Team1[target].dodge() == true)
                {
                    Console.WriteLine("{0} DODGES IT!", Program.Team1[target].characterName);
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(500);
                    damage = 0;
                }
                else if (Program.Team1[target].block() == true)
                {
                    Console.WriteLine("{0} BLOCKS IT!", Program.Team1[target].characterName);
                    Console.WriteLine();
                    System.Threading.Thread.Sleep(500);
                    damage = 0;
                }
                else
                {
                    if (Program.Team2[attacker].Job != 3)
                    {
                        decimal armour = 1 - Convert.ToDecimal(Program.Team1[target].armour) / 100;
                        //if they're defending then the armour value is doubled
                        if (Program.Team2[target].defending == true)
                        {
                            armour -= Convert.ToDecimal(Program.Team1[target].armour) / 100;
                        }
                        damage = Convert.ToInt32(Convert.ToDecimal(damage) * armour);
                    }
                }
            }


            if (player == true)
            {
                Program.Team2[target].takedamage(damage);
            }
            else
            {
                Program.Team1[target].takedamage(damage);
            }
            System.Threading.Thread.Sleep(500);
        }




        /// <summary>
        /// apply healing to target
        /// </summary>
        public void heal(bool player,int target, int healer)
        {
            if (player == true)
            {
                Console.WriteLine("{0} HEALS {1}", Program.Team1[healer].statBrief(), Program.Team1[target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                Program.Team1[target].receiveHeal(Program.Team1[healer].characterHeal());
            }
                //duplicate code will try to FIX THIS
            else
            {
                Console.WriteLine("{0} HEALS {1}", Program.Team2[healer].statBrief(), Program.Team2[target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                Program.Team2[target].receiveHeal(Program.Team2[healer].characterHeal());
            }
        }





    }

