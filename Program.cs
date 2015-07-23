using System;
using System.Collections.Generic;
    class Program
    {


        Random r = new Random();

        public static List<Character> Team1 = new List<Character>();
        public static List<Character> Team2 = new List<Character>();
        public static List<Character>[] Teams = new List<Character>[2] { Team1, Team2 };

        //lists for Turn order
        List<int> turn1 = new List<int>();
        List<int> availableTurns = new List<int>();


        string team1Name;
        string team2Name;
        int assignID = 1;
        public static int numberOfPlayers;

        int turnCount = 1;

        //COMBAT INFO VARIABLES
        int comabtlogTurnNumber;
        List<string> combatLog = new List<string>();

        //LISTS USED IN AI BEHAVIOR
        List<int> aiValues = new List<int>();
        List<int> aiIDValues = new List<int>();



        ///<summary>
        ///RANDOM NUMBER GENERATOR
        ///</summary>
        int rng(int min, int max)
        {
            int number = r.Next(min, max);
            return number;
        }

        ///<summary>
        ///PRESS ANY KEY TO CONTINUE
        ///</summary>
        static public void pressToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("[PRESS ANY KEY TO CONTINUE]");
            Console.WriteLine();
            Console.ReadKey();
        }

        /// <summary>
        /// CHECK FOR NUMBER BASED ANSWER
        /// </summary>
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

        /// <summary>
        /// CHECK FOR Y/N ANSWER
        /// </summary>
        bool yesOrNo(string input)
        {
            bool valid = false;
            if (input == "y" || input == "Y") 
            {
                valid = true;
            }
            else if (input == "n" || input == "N")
            {
                valid = false;
            }
            else
            {
                Console.WriteLine("Y/N?");
                valid = yesOrNo(Console.ReadLine());

            }
            return valid;
        }





        /// <summary>
        /// CHOOSE NUMBER OF PLAYERS FOR EACH TEAM
        /// </summary>
        void choosePlayerNumber()
        {

            Console.WriteLine("HOW MANY PLAYERS PER TEAM? (3-10)");
            numberOfPlayers = checkNumberAnswer();
            if (numberOfPlayers < 3 || numberOfPlayers > 10)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID NUMBER (3-10)");
                choosePlayerNumber();
                return;
            }
            Console.WriteLine();
            Console.WriteLine("{0} PLAYERS PER TEAM", numberOfPlayers);
            Console.WriteLine();
        }

        /// <summary>
        /// CREATE PLAYER CHARACTERS
        /// </summary>
        void createPlayerCharacter()
        {

            int counter = 0;
            bool valid = false;

            //Asks the player to name the team
            while (valid == false)
            {
                Console.WriteLine("TEAM NAME:");
                team1Name = ("TEAM " + Console.ReadLine());
                Console.WriteLine("SO YOUR TEAM IS CALLED {0}? Y/N", team1Name);
                valid = yesOrNo(Console.ReadLine());
            }
            valid = false;


            //Creates the new character, then assigns the team name to them
            while (counter <= (numberOfPlayers - 1))
            {
                Team1.Add(new Character());
                valid = false;
                Team1[counter].teamName = "[" + team1Name + "]";
                Team1[counter].characterID = assignID;
                Team1[counter].TeamID = 0;
                assignID++;

                //asks for their name
                while (valid == false)
                {
                    Console.WriteLine("ENTER THE NAME FOR {1}, FIGHTER {0}:", Team1[counter].characterID, team1Name);
                    Team1[counter].characterName = Console.ReadLine();
                    Console.WriteLine(Team1[counter].characterName + ", REGISTERED. PROCEED? Y/N");

                    valid = yesOrNo(Console.ReadLine());

                }
                valid = false;

                //Asks the player to choose the character's job
                while (valid == false)
                {
                    Console.WriteLine("CHOOSE " + Team1[counter].characterName + "'S JOB");
                    Console.WriteLine("0. {0}    1. {1}   2. {2}   3. {3}", Team1[0].jobNames[0], Team1[0].jobNames[1], Team1[0].jobNames[2], Team1[0].jobNames[3]);
                    Team1[counter].Job = checkNumberAnswer();
                    if (Team1[counter].Job > 3)
                    {
                        Team1[counter].Job = 0;
                    }
                    Console.WriteLine(Team1[counter].jobNames[Team1[counter].Job] + ", REGISTERED. PROCEED? Y/N");
                    valid = yesOrNo(Console.ReadLine());
                }
                valid = false;


                //generates the character stats then askss if they're ok
                while (valid == false)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.Write("GENERATING FIGHTER");
                    for (int i = 0; i < 4; i++)
                    {
                        System.Threading.Thread.Sleep(250);
                        Console.Write(".");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    Team1[counter].level = 0;
                    Team1[counter].XP += 100;
                    Team1[counter].Levelup();
                    Team1[counter].PrintStats();
                    Console.WriteLine();
                    Console.WriteLine("ARE THESE STATS SATISFACTORY?");
                    valid = yesOrNo(Console.ReadLine());
                    Console.Clear();

                    if (valid == false)
                    {
                        Team1[counter].clearStats();
                    }
                }
                counter++;

            }
            Console.Clear();
            for (int count = 1; count < numberOfPlayers; count++)
            {
                Team1[count].PrintStats();
            }


        }

        /// <summary>
        /// CREATE ENEMY CHARACTERS
        /// </summary>
        public void createEnemy()
        {
            string[] randomName = new string[16] { "SLAB", "SLATE", "SMASH", "JOHN", "HUNK", "FLINT", "CHUNKY", "CHAD", "BIG", "BOB", "BLAST","DIRK","BRICK","BEAT","STOMP","DIO" };
            string[] randomTitle = new string[16] { "BULKHEAD", "McLARGEHUGE", "SQUATTHRUST", "SLAMCHEST", "HARDPEC", "DEADLIFT", "CHESTHAIR", "McRUNFAST", "CHUNKMAN", "BEEFKNOB","JOHNSON","BRANDO","STEAKFIST","LARGEMEAT","PUNCHBEEF","CENA" };
            string[] randomTeamName = new string[12] { "KILLER", "SANDY", "SALTY", "SEXUAL", "ANGRY", "SILENT", "MEN", "KILLERS", "RASCALS", "GENTLEMEN", "FREAKS", "BOYS" };

            team2Name = "[THE " + randomTeamName[rng(0, randomTeamName.Length / 2)] + " " + randomTeamName[rng(randomTeamName.Length / 2, randomTeamName.Length)] + "]";
            for (int count = 0; count < numberOfPlayers; count++)
            {
                //create random character
                Teams[1].Add(new Character() { characterName = "[" + (randomName[rng(0, (randomName.Length))] + " " + randomTitle[rng(0, (randomTitle.Length))] + "]"), Job = rng(0, 4), TeamID = 1 });
                Teams[1][count].teamName = team2Name;
                Teams[1][count].characterID = assignID;
                assignID++;
                Teams[1][count].XP = rng(1, 100) + (Team1[count].level * 100);
                Teams[1][count].Levelup();

                //announce them
                Teams[1][count].PrintStats();
            }
        }






        /// <summary>
        /// PRINTS TURN ORDER
        /// </summary>
        public void turnOrderPrint()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("        TURN ORDER");
            Console.WriteLine("---------------------------");
            Console.WriteLine();


            for (int i = 0; i < availableTurns.Count; i++)
            {
                if (availableTurns[i] <  numberOfPlayers)
                {
                    Console.WriteLine((i + 1) + " " +  Team1[availableTurns[i]].statBrief());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine((i + 1) + " " +  Team2[availableTurns[i]-numberOfPlayers].statBrief());
                    Console.WriteLine();
                }
            }


             pressToContinue();
            Console.Clear();

        }

        /// <summary>
        /// CREATES TURN ORDER BASED ON A RANDOM NUMBER AND THE CHARACTER'S DEXTERITY
        /// </summary>
        void createTurnOrder()
        {
            availableTurns.Clear();
            //assign the 
            for (int j = 0; j < Teams.Length; j++)
            {
                for (int i = 0; i < Teams[j].Count; i++)
                {
                    //create a random based on the character's dex and a random number between 1 and 1000
                    turn1.Add(rng(1, 1000) + rng(0,Teams[j][i].dex));
                    //assign the index number in the available turns list
                    if (j == 0)
                    {
                        availableTurns.Add(i);
                        Console.WriteLine(turn1[i]);
                    }
                    else
                    {
                        availableTurns.Add(i + numberOfPlayers);
                        Console.WriteLine(turn1[i]);
                    }

                    Console.WriteLine("{0} {1}", Teams[j][i].characterName, turn1[i]);
                }
            
            }

            var sortedList = availableTurns.OrderBy(item => int.Parse(item));

            //sort the list to put the highest value first, followed by the lower values                                        
            for (int k = 0; k < availableTurns.Count; k++)
            {
                Console.WriteLine(k);
                //if k isn't the first number in the array then 
                if (k > 0)
                {
                    // if k isn't at the end of the list then compare the one after it
                    if (k < turn1.Count-1)
                    {
                        
                        if (turn1[k + 1] > turn1[k])
                        {
                            int temp = turn1[k];
                            turn1[k] = turn1[k + 1];
                            turn1[k + 1] = temp;

                            temp = availableTurns[k];
                            availableTurns[k] = availableTurns[k + 1];
                            availableTurns[k + 1] = temp;
                        }
                        // if k is after the first number in the array then compare it to the previous
                        if (k >= 1)
                        {
                            if (turn1[k - 1] > turn1[k])
                            {
                                int temp = turn1[k-1];
                                turn1[k-1] = turn1[k];
                                turn1[k] = temp;

                                temp = availableTurns[k-1];
                                availableTurns[k-1] = availableTurns[k];
                                availableTurns[k] = temp;
                            }
                        }

                    }
                 // if k = 0 then compare it the the number after
                }
                else if (k == 0)
                {
                    if (turn1[k + 1] > turn1[k])
                    {
                        int temp = turn1[k];
                        turn1[k] = turn1[k + 1];
                        turn1[k + 1] = temp;

                        temp = availableTurns[k];
                        availableTurns[k] = availableTurns[k + 1];
                        availableTurns[k + 1] = temp;

                    }
                }
                    //if k is the last value in the list then compare it to the one before it
                else if (k == turn1.Count - 1)
                {
                    if (turn1[k - 1] > turn1[k])
                    {
                        int temp = turn1[k - 1];
                        turn1[k - 1] = turn1[k];
                        turn1[k] = temp;

                        temp = availableTurns[k - 1];
                        availableTurns[k - 1] = availableTurns[k];
                        availableTurns[k] = temp;
                    }
                }

            }

            for (int h = 0; h < availableTurns.Count; h++)
            {
                Console.WriteLine(turn1[h]);
            }
            Console.WriteLine();
            for (int h = 0; h < availableTurns.Count; h++)
            {
                //check to see if anyone's dead, if they are then they are removed from the list
                if (availableTurns[h] < numberOfPlayers)
                {
                    if (Team1[availableTurns[h]].isDead)
                    {
                        availableTurns.Remove(h);
                    }
                }
                else
                {
                    if (Team2[availableTurns[h]-numberOfPlayers].isDead)
                    {
                        availableTurns.Remove(h);
                    }
                }
                
            }


            Console.WriteLine();
            //print them out to test
            for (int p = 0; p < availableTurns.Count; p++)
            {
                if (p < numberOfPlayers)
                {
                    Console.WriteLine("{0}", Teams[0][p].characterName);
                }
                else
                {
                    Console.WriteLine("{0}", Teams[1][p-numberOfPlayers].characterName);
                }
            }

            turnOrderPrint();
        }





     
        /// <summary>
        /// MENU FOR PLAYER TO CHOOSE THEIR ACTION
        /// </summary>
        public void playerOptions(int playerIndex)
        {
            Console.Clear();
            if ( Teams[0][playerIndex].defending)
            {
                Console.WriteLine("{0} STOPPED DEFENDING",  Teams[0][playerIndex].characterName);
            }
            //turn off defending
             Teams[0][playerIndex].defendingOther = false;
             Teams[0][playerIndex].defending = false;
             Teams[0][ Teams[0][playerIndex].defendedID].defended = false;
            bool holding = false;
            int actionInput;
            bool actionTaken = false;
            Console.WriteLine( Team1[playerIndex].statBrief() + " - TURN -");
            Console.WriteLine();
            Console.WriteLine("SELCET ACTION");
            Console.WriteLine();
            Console.WriteLine("1. ATTACK  2. HEAL  3. DEFEND  4. SCAN 5. HOLD  6. TURN ORDER");
            int input = checkNumberAnswer();
            if (input < 1 || input > 6)
            {
                Console.WriteLine("INVALID COMMAND");
                 pressToContinue();
                Console.Clear();
                playerOptions(playerIndex);
                return;
            }
            //attack
            if (input == 1)
            {
                Console.WriteLine();
                Console.WriteLine("SELECT TARGET TO ATTACK");
                Console.WriteLine();
                for (int i = 0; i <  Teams[1].Count; i++)
                {

                    if ( Teams[1][i].isDead == false)
                    {
                        Console.WriteLine();
                        Console.Write((i + 1) + ". " +  Teams[1][i].statBrief());
                        if ( Teams[1][i].defended == true)
                        {
                            Console.Write(" << DEFENDED BY " +  Teams[1][ Teams[1][i].defender].statBrief());
                        }
                        Console.WriteLine();
                    }

                }
                actionInput = checkNumberAnswer();
                if (checkInvalidTarget(actionInput, 1, playerIndex))
                {
                    playerOptions(playerIndex);
                    return;
                }
                attack(playerIndex, actionInput - 1, 0, 1);
                actionTaken = true;

            }
            //heal
            if (input == 2)
            {
                Console.WriteLine();
                Console.WriteLine("SELECT ALLY TO HEAL");
                Console.WriteLine();
                listAllies(playerIndex);
                actionInput = checkNumberAnswer();
                if (checkInvalidTarget(actionInput, 0, playerIndex))
                {
                    playerOptions(playerIndex);
                    return;
                }
                heal(actionInput - 1, playerIndex, 0);
                actionTaken = true;

            }
            //defend
            if (input == 3)
            {
                Console.WriteLine();
                Console.WriteLine("SELECT ALLY TO DEFEND");
                Console.WriteLine();
                listAllies(playerIndex);
                actionInput = checkNumberAnswer();
                if (checkInvalidTarget(actionInput, 0, playerIndex))
                {
                    playerOptions(playerIndex);
                    return;
                }
                defend(actionInput - 1, playerIndex, 0);
                actionTaken = true;

            }
            //scan
            if (input == 4)
            {
                scan(playerIndex);
            }
            //hold
            if (input == 5)
            {
                hold(playerIndex, 0);
                holding = true;
                actionTaken = true;
            }
            //turnorder
            if (input == 6)
            {
                turnOrderPrint();
            }
            if (actionTaken == true)
            {
                if (holding)
                {
                     pressToContinue();
                    Console.Clear();
                    nextTurn(true);
                    return;
                }
                else
                {
                    pressToContinue();
                    Console.Clear();
                    turnCount++;
                    nextTurn(false);
                    return;
                }
            }
            else
            {
                playerOptions(playerIndex);
                Console.Clear();
                return;
            }
        }

        /// <summary>
        /// ATTACKING
        /// </summary>
        void attack(int attacker, int target, int attackerTeamNumber, int defenderTeamNumber)
        {
            int damage;
            Console.WriteLine();
            Console.WriteLine();
            //check if the target is being defended by another, then retarget the defender
            if ( Teams[defenderTeamNumber][target].defended)
            {
                Console.WriteLine("{0} ATTACKS {1}...",  Teams[attackerTeamNumber][attacker].statBrief(),  Teams[defenderTeamNumber][target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("BUT {0} INTERCEPTS THEIR ATTACK!",  Teams[defenderTeamNumber][ Teams[defenderTeamNumber][target].defender].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(1000);
                target =  Teams[defenderTeamNumber][target].defender;
            }
            //otherwise continue as normal
            Console.WriteLine("{0} ATTACKS {1}...",  Teams[attackerTeamNumber][attacker].statBrief(),  Teams[defenderTeamNumber][target].statBrief());
            Console.WriteLine();
            System.Threading.Thread.Sleep(500);
            //checks if the target dodges and changes damage to 0
            damage =  Team1[attacker].characterDamage();
            if ( Team1[attacker].crit() == true)
            {
                Console.WriteLine("A CRITICAL HIT!");
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                damage = damage * 3;
            }
            if ( Teams[defenderTeamNumber][target].dodge() == true)
            {
                Console.WriteLine("{0} DODGES IT!",  Teams[defenderTeamNumber][target].characterName);
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                damage = 0;
            }
            //checks if the target block the attack and changes damage to 0
            else if ( Teams[defenderTeamNumber][target].block() == true)
            {
                Console.WriteLine("{0} BLOCKS IT!",  Teams[defenderTeamNumber][target].characterName);
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                damage = 0;
            }
            else
            {
                //check the attacker ins't a mage, if they aren't then apply armour
                if ( Teams[attackerTeamNumber][attacker].Job != 3)
                {
                    decimal armour;
                    if ( Teams[defenderTeamNumber][target].defending == false)
                    {
                        armour = 1 - Convert.ToDecimal( Teams[defenderTeamNumber][target].armour) / 100;
                    }
                    //if they're defending then the armour value is doubled
                    else
                    {
                        armour = 1 - ((Convert.ToDecimal( Teams[defenderTeamNumber][target].armour) / 100) * 2);
                    }
                    damage = Convert.ToInt32(Convert.ToDecimal(damage) * armour);
                }
            }

            //the target receives the damage
             Teams[defenderTeamNumber][target].takedamage(damage);

            if (isEveryoneDead(defenderTeamNumber))
            {
                Console.WriteLine();
                Console.WriteLine("{0} HAVE BEEN COMPLETEY DEFEATED BY {1}",  Teams[defenderTeamNumber][0].teamName,  Teams[attackerTeamNumber][0].teamName);
            }
        }

        /// <summary>
        /// HEALING
        /// </summary>
        void heal(int target, int healer, int healerTeam)
        {

            if (healer == target)
            {
                Console.WriteLine("{0} HEALS [SELF]",  Teams[healerTeam][healer].statBrief());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} HEALS {1}",  Teams[healerTeam][healer].statBrief(),  Teams[healerTeam][target].statBrief());
                Console.WriteLine();
            }
            System.Threading.Thread.Sleep(500);
             Teams[healerTeam][target].receiveHeal( Teams[healerTeam][healer].characterHeal());


        }

        /// <summary>
        /// DEFENCE
        /// </summary>
        void defend(int defendTarget, int defender, int teamIndex)
        {
            //if they're already defended , then they're an invalid target, they can be defending themself though. You can't stack up defenders either.
            if ( Teams[teamIndex][defendTarget].defended == true ||  Teams[teamIndex][defendTarget].defendingOther == true ||  Teams[teamIndex][defender].defended == true)
            {
                Console.WriteLine("INVALID TARGET");
                 pressToContinue();
                playerOptions(defender);
                return;

            }
            else
            {
                if (defender == defendTarget)
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE",  Teams[teamIndex][defender].statBrief());
                     Teams[teamIndex][defender].defending = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE IN FRONT OF {1}",  Teams[teamIndex][defender].statBrief(),  Teams[teamIndex][defendTarget].statBrief());
                     Teams[teamIndex][defender].defending = true;
                     Teams[teamIndex][defender].defendedID = defendTarget;
                     Teams[teamIndex][defender].defendingOther = true;
                     Teams[teamIndex][defendTarget].defended = true;
                     Teams[teamIndex][defendTarget].defender = defender;
                }
                System.Threading.Thread.Sleep(500);
            }

        }

        /// <summary>
        /// SCAN
        /// </summary>
        void scan(int playerIndex)
        {
            int selection;
            Console.WriteLine();
            Console.WriteLine("SELECT TARGET TO SCAN");
            Console.WriteLine();

            //lists allied team
            for (int i = 0; i <  Teams[0].Count; i++)
            {
                if ( Teams[0][i].isDead == false)
                {
                    //Specifies that the target is themself
                    if (i == playerIndex)
                    {
                        Console.WriteLine((i + 1) + ". [SELF]");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine((i + 1) + ". " +  Teams[0][i].statBrief());
                        Console.WriteLine();
                    }
                }
            }

            //lists enemy team.
            for (int i = 0; i <  Teams[1].Count; i++)
            {
                if ( Teams[1][i].isDead == false)
                {
                    Console.WriteLine((i + 1 +  Team1.Count) + ". " +  Teams[1][i].statBrief());
                    Console.WriteLine();
                }
            }

            selection = checkNumberAnswer() - 1;
            Console.Clear();
            Console.WriteLine();
            Console.Write("SCANNING");
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(250);
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine();
            //check for invalid selection, this has one is different to the other three, as it lists both teams.
            if (selection < 0 || selection >  numberOfPlayers * 2 - 1)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                Console.WriteLine();
            }
            else
            {
                if (selection <  Team1.Count)
                {
                    if ( Teams[0][selection].isDead == false)
                    {
                         Teams[0][selection].PrintStats();
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("INVALID TARGET");
                        Console.WriteLine();
                    }
                }
                //if the selection isn't invalid then the character's stats are printed
                else
                {
                    if ( Teams[1][selection -  Teams[1].Count].isDead == false)
                    {
                         Teams[1][selection -  Teams[1].Count].PrintStats();
                    }
                    //unless they're dead. which doen't make much sense
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("INVALID TARGET");
                        Console.WriteLine();
                    }

                }

            }
             pressToContinue();
            Console.Clear();

        }

        /// <summary>
        /// HOLD
        /// </summary>
        void hold(int playerIndex, int teamNumber)
        {
            List<int> tempTurns = new List<int>();
            Console.WriteLine();
            if (playerIndex >  numberOfPlayers - 1)
            {
                Console.WriteLine("{0} WAITS",  Teams[teamNumber][playerIndex -  numberOfPlayers].statBrief());
            }
            else
            {
                Console.WriteLine("{0} WAITS",  Teams[teamNumber][playerIndex].statBrief());
            }

            for (int i = 0; i < availableTurns.Count; i++)
            {
                tempTurns.Add(availableTurns[i]);
            }
            availableTurns[availableTurns.Count - 1] = tempTurns[0];
            for (int i = 0; i < availableTurns.Count - 1; i++)
            {
                availableTurns[i] = tempTurns[i + 1];
            }
        }

        /// <summary>
        /// INVALID INPUT CHECK, USED FOR ATTACKING AND HEALING
        /// </summary>
        bool checkInvalidTarget(int actionInput, int targetTeam, int playerIndex)
        {
            bool invalidTarget = false;
            if (actionInput < 1 || actionInput >  Teams[targetTeam].Count)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                 pressToContinue();
                Console.Clear();
                invalidTarget = true;

            }
            else if ( Teams[targetTeam][actionInput - 1].isDead == true)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                 pressToContinue();
                Console.Clear();
                invalidTarget = true;
            }
            return invalidTarget;
        }

        /// <summary>
        /// LIST ALLIES, THIS IS USED FOR HEALING AND DEFENDING
        /// </summary>
        void listAllies(int playerIndex)
        {
            for (int i = 0; i <  Teams[0].Count; i++)
            {

                if ( Teams[0][i].isDead == false)
                {
                    if (i == playerIndex)
                    {
                        Console.WriteLine((i + 1) + ". [SELF]");
                    }
                    else
                    {
                        Console.WriteLine((i + 1) + ". " +  Teams[0][i].statBrief());
                    }
                    Console.WriteLine();
                }

            }
        }







        /// <summary>
        /// ADVANCE TURNS, CHECKS TO SEE IF PLAYER OR AI IS NEXT. ALSO CHECKS TO SEE IF BATTLE IS OVER
        /// </summary>
        public void nextTurn(bool hold)
        {
            //if there are turns that need to be played out
            if (availableTurns.Count > 0)
            {
                //if it's not the very first turn
                if (turnCount > 1)
                {
                    //if the previous turn didn't result in a hold then remove the previous turn from the stack
                    if (hold)
                    {

                    }
                    else
                    {
                        availableTurns.Remove(availableTurns[0]);
                    }
                }
                // if there are still more turns after the previous one is removed then
                if (availableTurns.Count > 0)
                {
                    //print the turn order
                    createTurnOrder();
                    //check to see whether it is the ai or the player going next
                    if (availableTurns[0] < numberOfPlayers)
                    {
                        playerOptions(availableTurns[0]);
                    }
                    else
                    {
                        AIcombat(availableTurns[0] - numberOfPlayers);
                    }
                }

            }
            // if there are no turns left then
            if (availableTurns.Count == 0)
            {
                //check to see if one team or the other is dead and if they are then do the according action
                if (isEveryoneDead(0))
                {
                    battle(false, true);
                }
                if (isEveryoneDead(1))
                {
                    battle(true, false);
                }
                //ortherwise contunue the battle
                battle(false, false);
            }
            // if the turns haven't run out, but someone is dead, then do the according action
            else if (isEveryoneDead(0))
            {
                battle(false, true);
            }
            else if (isEveryoneDead(1))
            {
                battle(true, false);
            }
        }




        /// <summary>
        /// AI CODE
        /// if they're a mage then they'll have a 30% chance to heal  the lowest health ally. They also have a 15% to pass their turn.
        /// if they're a tank then they'll have a 70% to defend for a rogue or mage.
        /// if they're a warrior then they have a 40% to defend for a rogue or mage.
        /// if they're a rogue then they'll attack regardless, they're pricks.
        /// default behavior is just to attack the lowest health target
        /// </summary>
        void AIcombat(int botID)
        {
            aiValues.Clear();
            aiIDValues.Clear();
            //turn off defending
            if ( Teams[1][botID].defending)
            {
                Console.WriteLine("{0} STOPPED DEFENDING",  Teams[1][botID].characterName);
            }
             Teams[1][botID].defendingOther = false;
             Teams[1][botID].defending = false;
             Teams[1][ Teams[1][botID].defendedID].defended = false;
            bool holding = false;
            //clear the value lists so they can be reassinged*
            bool actionTaken = false;
            //generate the behavior value
            int aiBehavior = rng(1, 100);
            //the value used to check which behavior the ai should perform
            int aiBehaviorCheck;


            //tank specific
            if ( Teams[1][botID].Job == 0)
            {
                aiBehaviorCheck = 70;
                if (aiBehavior < aiBehaviorCheck)
                {
                    if (blockForSquishy(botID))
                    {
                        actionTaken = true;
                    }

                }

            }
            //warior specific
            if ( Teams[1][botID].Job == 1)
            {
                aiBehaviorCheck = 40;
                if (aiBehavior < aiBehaviorCheck)
                {
                    if (blockForSquishy(botID))
                    {
                        actionTaken = true;
                    }
                }
            }

            //mage specific
            if ( Teams[1][botID].Job == 3)
            {
                aiBehaviorCheck = 30;
                //heal lowest health ally
                if (aiBehavior < aiBehaviorCheck)
                {
                    for (int i = 0; i <  numberOfPlayers; i++)
                    {
                        aiValues.Add( Teams[1][i].HP);
                    }
                    checkLowestValue();
                    heal(aiIDValues[rng(0,aiIDValues.Count-1)], botID, 1);
                    actionTaken = true;
                }

                //wait until the end
                aiBehaviorCheck = 15;
                if (aiBehavior < aiBehaviorCheck && actionTaken == false)
                {
                    //pass turn
                    hold(botID, 1);
                    holding = true;
                    actionTaken = true;
                }
            }
            if (actionTaken == false)
            {
                aiValues.Clear();
                for (int i = 0; i <  numberOfPlayers; i++)
                {
                    aiValues.Add(Teams[0][i].HP);
                }
                checkLowestValue();
                attack(botID, aiIDValues[rng(0, aiIDValues.Count - 1)], 1, 0);
            }
            pressToContinue();
            Console.Clear();
            turnCount++;
            nextTurn(holding);
            return;
        }

        /// <summary>
        /// USED TO FIND A ROGUE OR MAGE WHICH THE AI BLOCK FOR
        /// </summary>
        bool blockForSquishy(int botID)
        {
            aiValues.Clear();
            aiIDValues.Clear();
            bool ok = false;
            bool noBlock = false;
            for (int i = 0; i <  Teams[1].Count; i++)
            {
                //assigns the job value of ai teammates to the list
                aiValues.Add( Teams[1][i].Job);
            }
            //look for mages and rougues
            for (int j = 0; j < aiValues.Count; j++)
            {
                if (aiValues[j] == 2 || aiValues[j] == 3)
                {
                    //assign index from aivalues to aiIDvalues, this can then be used to find the correct id for the defend target
                    aiIDValues.Add(j);
                }
            }

            if (aiIDValues.Count > 0)
            {
                int k = 0;

                while (k < aiIDValues.Count && ok == false)
                {
                    if ( Teams[1][botID].defended == false)
                    {
                        if ( Teams[1][aiIDValues[k]].defended == false &&  Teams[1][aiIDValues[k]].isDead == false)
                        {
                            defend(aiIDValues[k], botID, 1);
                            ok = true;
                            noBlock = false;
                        }
                    }
                    else
                    {
                        noBlock = true;
                    }
                    k++;
                }
            }
            if (noBlock)
            {
                ok = false;
            }
            return ok;
        }

        /// <summary>
        /// USED TO FIND LOW HEALTH TARGETS FOR THE AI
        /// </summary>
        void checkLowestValue()
        {
            aiIDValues.Clear();
            //aiValues is assigned during the aiCombat method, this can represent the health of allies or enemies
            //assigns the values from aiValues 1 to aiValues2, this is then used to find the lowest value
            for (int i = 0; i < aiValues.Count; i++)
            {
                aiIDValues.Add(i);
            }

            if (aiValues.Count > 1)
            {


                for (int i = 0; i < aiValues.Count; i++)
                {
                    int temp;
                    //check to see if the current i is not the last item in the list
                    if (i + 1 < aiValues.Count)
                    {
                        // if the current value is greater than the next value, then those values are switched using a temporary variable
                        if (aiValues[i] > aiValues[i + 1])
                        {
                            temp = aiValues[i];
                            aiValues[i] = aiValues[i + 1];
                            aiValues[i + 1] = temp;

                            //change the id value to match the health value
                            temp = aiIDValues[i];
                            aiIDValues[i] = aiIDValues[i + 1];
                            aiIDValues[i + 1] = temp;

                            //if i is the second or higher item in the list
                            if (i - 1 >= 0)
                            {

                                //checks to see if if the current value is smaller than the previous
                                if (aiValues[i] < aiValues[i - 1])
                                {
                                    temp = aiValues[i - 1];
                                    //switch current value to the lower slot
                                    aiValues[i - 1] = aiValues[i];
                                    aiValues[i] = temp;
                                    //do the same with the id values
                                    temp = aiIDValues[i - 1];
                                    aiIDValues[i - 1] = aiIDValues[i];
                                    aiIDValues[i] = temp;

                                }
                            }
                        }

                    }
                    //if i is the last item in the list, then check it against the first and move the rest back accordingly
                    else
                    {
                        if (aiValues[i] < aiValues[0] && i >= 1)
                        {
                            temp = aiValues[0];
                            aiValues[0] = aiValues[i];
                            aiValues[i] = aiValues[i - 1];
                            aiValues[1] = temp;

                            int temp2 = aiIDValues[0];
                            aiIDValues[0] = aiIDValues[i];
                            aiIDValues[i] = aiIDValues[i - 1];
                            aiIDValues[1] = temp2;
                        }
                    }
                }
            }

            //remove 0 values as it means the target is dead
            for (int i = 0; i < aiValues.Count; i++)
            {
                if (aiValues[0] == 0)
                {
                    aiValues.Remove(aiValues[0]);
                    aiIDValues.Remove(aiIDValues[0]);
                }
            }

        }












        /// <summary>
        /// CHECKS IF A TEAM IS DEAD THEN RETURNS TRUE OR FALSE
        /// </summary>
        bool isEveryoneDead(int teamToCheck)
        {
            int deaths = 0;
            bool yes = false;
            for (int i = 0; i <  numberOfPlayers; i++)
            {
                if ( Teams[teamToCheck][i].isDead)
                {
                    deaths++;
                }
            }
            if (deaths ==  Teams[teamToCheck].Count)
            {
                yes = true;
            }
            return yes;
        }



        ///<summary>
        //MAIN LOGIC
        ///</summary>
        public void Run2()
        {


            //intro text
            Console.WriteLine("WELCOME CHALLENGERS TO CYBER AREA 20XX 2: TEAM BATTLE TOURNAMENT");
            Console.WriteLine();
            Console.WriteLine("IN THIS INTENSE COMBAT GAME YO WILL FACE OFF AGAINST THE FEARCEST");
            Console.WriteLine("TEAMS OF FIGHTERS DROM ACCROSS THE GALAXY");
            Console.WriteLine();
            Console.WriteLine("REMEMBER THIS IS LIFE OR DEATH, IF A TEAM MEMBER GOES DOWN");
            Console.WriteLine("THEN THEY'RE STAYING DOWN FOR GOOD");
            Console.WriteLine();
            choosePlayerNumber();
            Console.WriteLine("BEGGINNING FIGHTER CREATION");
            pressToContinue();
            createPlayerCharacter();
            createEnemy();

            Console.ReadKey();
        }
 
        /// <summary>
        /// A TEST GAME WHICH CREATES SOME GENERIC CHARACTERS
        /// </summary>
        public void Run()
        {
            Console.WriteLine("TEST GAME");
            pressToContinue();
            testMen();
            createEnemy();
            Console.Clear();
            battle(false, false);
            return;


        }

        /// <summary>
        /// CREATES GENERIC CHARACTERS FOR TEST GAME
        /// </summary>
        void testMen()
        {
            int testXP = 500;
            numberOfPlayers = 4;
            team1Name = "[TEAM TESTER]";
            Team1.Add(new Character() { characterID = 1, characterName = "[TEST TANK]", Job = 0, XP = testXP, teamName = team1Name, TeamID = 0 });
            Team1.Add(new Character() { characterID = 2, characterName = "[TEST WARRIOR]", Job = 1, XP = testXP, teamName = team1Name, TeamID = 0 });
            Team1.Add(new Character() { characterID = 3, characterName = "[TEST MAGE THE 3RD]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0 });
            Team1.Add(new Character() { characterID = 4, characterName = "[TEST MAGE]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0 });
            //Team1.Add(new Character() { characterID = 5, characterName = "[TEST MAGE THE 2ND]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0});
            Team1[0].Levelup();
            Team1[1].Levelup();
            Team1[2].Levelup();
            Team1[3].Levelup();
            //Team1[4].Levelup();
            Team1[0].PrintStats();
            Team1[1].PrintStats();
            Team1[2].PrintStats();
            Team1[3].PrintStats();
            //Team1[4].PrintStats();
        }


        /// <summary>
        /// METHOD CALLED BETWEEN FIGHTS, LEVELS UP CHARACTERS IF YOU DEFEAT THE ENEMY
        /// </summary>
        public void battle(bool aiDead, bool playerDead)
        {
            if (aiDead)
            {
                for (int i = 0; i < Teams[0].Count; i++)
                {
                    Teams[0][i].XP += 100 * Teams[1][0].level;
                    Teams[0][i].Levelup();

                }
                Team2.Clear();
                createEnemy();
            }
            if (playerDead)
            {
                //GOTTA FINSIH THIS
                Console.WriteLine(" GAME OVER LOL");
            }
            createTurnOrder();
            nextTurn(false);

        }




    }
