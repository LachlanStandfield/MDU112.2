using System;
using System.Collections.Generic;
    class Program
    {

        //used in random number generator
        Random r = new Random();


        //used for character teams, Team1 is the player, Team2 is the ai
        public static List<Character> Team1 = new List<Character>();
        public static List<Character> Team2 = new List<Character>();
        public static List<Character>[] Teams = new List<Character>[2] { Team1, Team2 };

        //list used for turn order
         List<Turns> availableTurns = new List<Turns>();

        //used to define the team names
        string team1Name;
        string team2Name;
        
        //defines the number of players per team
        public static int numberOfPlayers;


        //used for printing the combat log
        public static int comabtlogTurnNumber;
        public static List<string> combatLog = new List<string>();

        //LISTS USED IN AI BEHAVIOR
        List<int> aiValues = new List<int>();
        List<int> aiIDValues = new List<int>();
        List<Health> aiHealthValues = new List<Health>();

        //GENERIC METHODS

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






        ///<summary>
        //MAIN LOGIC
        ///</summary>
        public void Run()
        {


            //intro text
            Console.WriteLine("WELCOME CHALLENGERS TO CYBER AREA 20XX 2: CLAN WARS");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine("IN THIS INTENSE COMBAT GAME YOU WILL FACE OFF AGAINST THE FEARCEST");
            Console.WriteLine();
            Console.WriteLine("TEAMS OF FIGHTERS FROM ACCROSS THE GALAXY");
            System.Threading.Thread.Sleep(1200);
            Console.WriteLine();
            Console.WriteLine("REMEMBER THIS IS LIFE OR DEATH, IF A TEAM MEMBER GOES DOWN...");
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("THEN THEY'RE STAYING DOWN");
            Console.WriteLine();
            System.Threading.Thread.Sleep(2000);
            choosePlayerNumber();
            Console.WriteLine("BEGGINNING FIGHTER CREATION");
            pressToContinue();
            Console.Clear();
            createPlayerCharacter();
            Console.Clear();
            Console.WriteLine();
            Console.Write("FINDING OPPONENTS");
            for (int i = 0; i < 4; i++)
            {
                System.Threading.Thread.Sleep(250);
                Console.Write(".");
            }
            Console.WriteLine();
            Console.WriteLine();
            createEnemy();
            pressToContinue();
            Console.Clear();
            Console.WriteLine("LET THE GAMES BEGIN!!!");
            pressToContinue();
            Console.Clear();
            battle(false, false);
            return;
        }






        /// <summary>
        /// PLAYER CHOOSES NUMBER OF PLAYERS FOR EACH TEAM 3-10
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
                //checks for yes or no via method
                valid = yesOrNo(Console.ReadLine());
            }
            valid = false;


            //Creates the new character, then assigns the team name to them
            while (counter <= (numberOfPlayers - 1))
            {
                Team1.Add(new Character());
                valid = false;
                Team1[counter].teamName = "[" + team1Name + "]";
                Team1[counter].characterID = counter +1;
                Team1[counter].TeamID = 0;

                //asks for their name
                while (valid == false)
                {
                    Console.WriteLine("ENTER THE NAME FOR {1}, FIGHTER {0}:", Team1[counter].characterID, team1Name);
                    Team1[counter].characterName = Console.ReadLine();
                    Console.WriteLine(Team1[counter].characterName + ", REGISTERED. PROCEED? Y/N");

                    valid = yesOrNo(Console.ReadLine());
                    Console.Clear();
                }
                valid = false;

                //Asks the player to choose the character's job
                while (valid == false)
                {
                    Console.WriteLine("CHOOSE " + Team1[counter].characterName + "'S JOB");
                    Console.WriteLine();
                    Console.WriteLine("0. {0} - A DAMAGE SOAKER, GOOD AT DEFENDING THEIR ALLIES", Team1[0].jobNames[0]);
                    Console.WriteLine();
                    Console.WriteLine("1. {0} - A BRAWLER, CAN DEFEND THEIR ALLIES IN A PINCH BUT PREFER TO RUSH THE BACKLINE",Team1[0].jobNames[1]);
                    Console.WriteLine();
                    Console.WriteLine("2. {0} - AN ASSASSIN, HIGH CRITICAL CHANCE SPELLS DEATH FOR THEIR ENEMIES",Team1[0].jobNames[2]);
                    Console.WriteLine();
                    Console.WriteLine("3. {0} - A STRATEGIST, HEALS ALLIES AND IGNORES ENEMY ARMOUR",Team1[0].jobNames[3]);
                    Console.WriteLine();
                    Team1[counter].Job = checkNumberAnswer();
                    if (Team1[counter].Job > 3)
                    {
                        Team1[counter].Job = 0;
                    }
                    Console.WriteLine(Team1[counter].jobNames[Team1[counter].Job] + ", REGISTERED. PROCEED? Y/N");
                    valid = yesOrNo(Console.ReadLine());
                    Console.Clear();
                }
                valid = false;


                //generates the character stats then askss if they're ok
                while (valid == false)
                {
                    Console.Clear();
                    Console.WriteLine();
                    //pauses for a second while generating, this was an aesthetic choice
                    Console.Write("GENERATING FIGHTER");
                    for (int i = 0; i < 4; i++)
                    {
                        System.Threading.Thread.Sleep(250);
                        Console.Write(".");
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    //sets the characters level to 0 then awards them with 100 xp
                    Team1[counter].level = 0;
                    Team1[counter].XP += 100;
                    //levels the character iup, giving them their stats
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
            //prints stats
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
            //random name arrays 
            string[] randomName = new string[16] { "SLAB", "SLATE", "SMASH", "JOHN", "HUNK", "FLINT", "CHUNKY", "CHAD", "BIG", "BOB", "BLAST","DIRK","BRICK","BEAT","STOMP","DIO" };
            string[] randomTitle = new string[16] { "BULKHEAD", "McLARGEHUGE", "SQUATTHRUST", "SLAMCHEST", "HARDPEC", "DEADLIFT", "CHESTHAIR", "McRUNFAST", "CHUNKMAN", "BEEFKNOB","JOHNSON","BRANDO","STEAKFIST","LARGEMEAT","PUNCHBEEF","CENA" };
            string[] randomTeamName = new string[12] { "KILLER", "SANDY", "SALTY", "SEXUAL", "ANGRY", "SILENT", "MEN", "KILLERS", "RASCALS", "GENTLEMEN", "FREAKS", "BOYS" };
            //generates random name from first two arrays, and tema naem from the third one
            team2Name = "[THE " + randomTeamName[rng(0, randomTeamName.Length / 2)] + " " + randomTeamName[rng(randomTeamName.Length / 2, randomTeamName.Length)] + "]";
            for (int count = 0; count < numberOfPlayers; count++)
            {
                //create random character
                Teams[1].Add(new Character() { characterName = "[" + (randomName[rng(0, (randomName.Length))] + " " + randomTitle[rng(0, (randomTitle.Length))] + "]"), Job = rng(0, 4), TeamID = 1 });
                Teams[1][count].teamName = team2Name;
                Teams[1][count].characterID = count+1+numberOfPlayers;
                //generates levels based on player levels
                Teams[1][count].XP = rng(1,20)+ +Team1[0].XP+(Team1[0].level * 100);
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
            //updates to see who's dead
            updateTurnOrder();

            //depending on wether they are an ai or a player character the index for printing has to be altered
            for (int i = 0; i < availableTurns.Count; i++)
            {
                if (availableTurns[i].ID <  numberOfPlayers)
                {
                    Console.WriteLine((i + 1) + " " +  Team1[availableTurns[i].ID].statBrief());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine((i + 1) + " " +  Team2[availableTurns[i].ID-numberOfPlayers].statBrief());
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
            for (int j = 0; j < Teams.Length; j++)
            {
                //generates a number which is a random of 0-500 + a random of 0 to the characters dextertiy and assigns it to their priority
                for (int i = 0; i < Teams[j].Count; i++)
                {
                    if (j == 0)
                    {
                        availableTurns.Add(new Turns() {ID = i, priority = (rng(1,500)+rng(0,Teams[j][i].dex))});
                    }
                    else
                    {
                        availableTurns.Add(new Turns() { ID = i+numberOfPlayers, priority = (rng(1, 500) + rng(0, Teams[j][i].dex)) });
                    }
                }
            }
            //sorts the order by turn priority
            availableTurns.Sort((a, b) => a.priority.CompareTo(b.priority));  
        }

        /// <summary>
        /// updates turn order by removing dead guys
        /// </summary>
        void updateTurnOrder()
        {
            for (int i = 0; i < availableTurns.Count; i++)
            {
                if (availableTurns[i].ID < numberOfPlayers)
                {
                    if (Teams[0][availableTurns[i].ID].isDead)
                    {
                        availableTurns.RemoveAt(i);
                    }
                }
                else
                {
                    if (Teams[1][availableTurns[i].ID - numberOfPlayers].isDead)
                    {
                        availableTurns.RemoveAt(i);
                    }
                }
                }
                
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
            //this boolean tells whether the player chose to hold their turn
            bool holding = false;
            int actionInput;
            bool actionTaken = false;
            Console.WriteLine( Team1[playerIndex].statBrief() + " - TURN -");
            Console.WriteLine();
            Console.WriteLine("SELCET ACTION");
            Console.WriteLine();
            Console.WriteLine("1. ATTACK  2. HEAL  3. DEFEND  4. SCAN 5. HOLD  6. TURN ORDER");
            int input = checkNumberAnswer();
            // if they don't make a valid input then the'yr told it a bad number
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
                        //prints a list of available targets to attack as well as whether they're being defended
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
                //prints a list of available allies to heal
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
                //prints alist of available allies to defend
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
            {// prints stat information for a target
                scan(playerIndex);
            }
            //hold
            if (input == 5)
            {// holds until the last turn
                hold(playerIndex, 0);
                holding = true;
                actionTaken = true;
            }
            //turnorder
            if (input == 6)
            {//prints the turn order
                turnOrderPrint();
            }
            if (actionTaken == true)
            {
                //checks wether the player chose to hold then passes this information on o the next turn method
                if (holding)
                {
                     pressToContinue();
                    Console.Clear();
                    nextTurn(true,false);
                    return;
                }
                else
                {
                    pressToContinue();
                    Console.Clear();
                    nextTurn(false,false);
                    return;
                }
            }
            else
            {
                //if the player didn't make a valid selection then they're presented with their options again
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
            
            damage =  Team1[attacker].characterDamage();
            if ( Team1[attacker].crit() == true)
            {
                //checks to see if thr attack is critical to deal bonus damage
                Console.WriteLine("A CRITICAL HIT!");
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                damage = damage * 3;
            }
            if ( Teams[defenderTeamNumber][target].dodge() == true)
            {
                //checks if the target dodges and changes damage to 0
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
            //add to combat log
            combatLog.Add(Teams[attackerTeamNumber][attacker].characterName+ " ATTACKED "+ Teams[defenderTeamNumber][target].characterName+ " FOR "+damage+".");
            if (isEveryoneDead(defenderTeamNumber))
            {
                Console.WriteLine();
                Console.WriteLine("{0} HAVE BEEN COMPLETEY DEFEATED BY {1}",  Teams[defenderTeamNumber][0].teamName,  Teams[attackerTeamNumber][0].teamName);
                combatLog.Add(Teams[defenderTeamNumber][0].teamName + " WERE WIPED OUT.");
            }
        }

        /// <summary>
        /// HEALING
        /// </summary>
        void heal(int target, int healer, int healerTeam)
        {
            //print list to be healed
            if (healer == target)
            {
                Console.WriteLine("{0} HEALS [SELF]",  Teams[healerTeam][healer].statBrief());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} HEALS {1}",  Teams[healerTeam][healer].statBrief(),  Teams[healerTeam][target].statBrief());
                Console.WriteLine();
            }// heal the target
            System.Threading.Thread.Sleep(500);
            Teams[healerTeam][target].receiveHeal( Teams[healerTeam][healer].characterHeal());
            //add to combat log
            combatLog.Add(Teams[healerTeam][healer].characterName+" HEALED "+Teams[healerTeam][target].characterName+" FOR "+Teams[healerTeam][healer].characterHeal()+".");

        }

        /// <summary>
        /// DEFENCE
        /// </summary>
        void defend(int defendTarget, int defender, int teamIndex)
        {
            //if the target is already defended , then they're an invalid target. targets can be defending themself though. You can't stack up defenders either.
            if ( Teams[teamIndex][defendTarget].defended == true ||  Teams[teamIndex][defendTarget].defendingOther == true ||  Teams[teamIndex][defender].defended == true)
            {
                Console.WriteLine("INVALID TARGET");
                 pressToContinue();
                playerOptions(defender);
                return;

            }
            else
            {// if they only defend themself then they enter a defensive stance and set defending to true
                if (defender == defendTarget)
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE",  Teams[teamIndex][defender].statBrief());
                     Teams[teamIndex][defender].defending = true;
                    //add to comabt log
                     combatLog.Add(Teams[teamIndex][defender].characterName+" DEFENDED.");
                }
                else
                {
                    // if they defend an ally then the defenders id as well as the defended targets id have to be recorded
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE IN FRONT OF {1}",  Teams[teamIndex][defender].statBrief(),  Teams[teamIndex][defendTarget].statBrief());
                     Teams[teamIndex][defender].defending = true;
                     Teams[teamIndex][defender].defendedID = defendTarget;
                     Teams[teamIndex][defender].defendingOther = true;
                     Teams[teamIndex][defendTarget].defended = true;
                     Teams[teamIndex][defendTarget].defender = defender;
                    //add to combat log
                     combatLog.Add(Teams[teamIndex][defender].characterName + " DEFENDED " + Teams[teamIndex][defendTarget].characterName+".");
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
            // moves each part of the list upward one space
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
                tempTurns.Add(availableTurns[i].ID);
            }
            availableTurns[availableTurns.Count - 1].ID = tempTurns[0];
            for (int i = 0; i < availableTurns.Count - 1; i++)
            {
                availableTurns[i].ID = tempTurns[i + 1];
            }
            //add to combat log
            combatLog.Add(Teams[teamNumber][playerIndex].characterName + " WAITED.");
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
                {//if they're behavior rolls is under 70 then they attempt to defend a target, if they can't defend a squishy then they just attack
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
                {//similar behavior to the tank but less chance to block
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
                    aiHealthValues.Clear();
                    for (int i = 0; i <  numberOfPlayers; i++)
                    {
                        if (Teams[1][i].HP > 0)
                        {
                            aiHealthValues.Add(new Health() { ID = i, HP = Teams[1][i].HP });
                        }
                    }
                    aiHealthValues.Sort((a, b) => a.HP.CompareTo(b.HP));
                    heal(aiHealthValues[0].ID, botID, 1);
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
            {//default action is to attack, the health values of eahc opponent are placed into a list then the lowest is found and this dictates the ai's target.
                aiHealthValues.Clear();
                for (int i = 0; i <  numberOfPlayers; i++)
                {
                    if (Teams[0][i].HP > 0)
                    {
                        aiHealthValues.Add(new Health(){ID = i, HP = Teams[0][i].HP});
                    }
                }
                aiHealthValues.Sort((a, b) => a.HP.CompareTo(b.HP));
                attack(botID,aiHealthValues[0].ID, 1, 0);
            }
            pressToContinue();
            Console.Clear();
            nextTurn(holding,false);
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
                    if (Teams[1][aiIDValues[k]].defended == false && Teams[1][aiIDValues[k]].isDead == false)
                    {
                        defend(aiIDValues[k], botID, 1);
                        ok = true;
                        noBlock = false;
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
        /// METHOD CALLED BETWEEN FIGHTS, LEVELS UP CHARACTERS IF YOU DEFEAT THE ENEMY
        /// </summary>
        public void battle(bool aiDead, bool playerDead)
        {
            //checks to see if the ai is dead
            if (aiDead)
            {
                Console.Clear();
                Console.WriteLine("{0} HAS EMERGED VICTORIOUS. BUT HOW WILL YOUR REMAINING MEMBERS FAIR AGAINST THEIR NEXT OPPONENT?",team1Name);
                for (int i = 0; i < Teams[0].Count; i++)
                {
                    //gives xp to characters regardless of whether they died for the sake of enemy generation
                    Teams[0][i].XP += rng(200, 500);
                    if (Teams[0][i].isDead == false)
                    {
                        //randomly awards 2-5 levels
                        
                        Teams[0][i].Levelup();
                    }
                }
                    //prints the stats for each player character
                    for (int j = 0; j < Teams[0].Count;j++ )
                    {
                        if (Teams[0][j].isDead == false)
                        {
                            Console.WriteLine();
                            Console.WriteLine("   --LEVELLING UP--");
                            Teams[0][j].PrintStats();
                            pressToContinue();
                        }
                    }
                // prints the new opponennts stats
            Console.Clear();
            Console.WriteLine("INTRODUCING YOUR NEXT OPPONENTS...");
                Team2.Clear();
                createEnemy();
            }
            if (playerDead)
            {// if the player dies then they're they are given the choice to restart
                Console.WriteLine(" -YOU HAVE BEEN DEFEATED-");
                Console.WriteLine();
                Console.WriteLine(" PRINT COMBAT LOG? Y/N");
                if (yesOrNo(Console.ReadLine()))
                {
                    for (int i = 0; i < combatLog.Count; i++)
                    {
                        Console.WriteLine();
                        Console.WriteLine(combatLog[i]);
                    }
                }
                Console.WriteLine(" PLAY AGAIN? Y/N");
                if (yesOrNo(Console.ReadLine()))
                {
                    Console.Clear();
                    Run();
                    return;
                }
                else
                {
                    Application.Exit();
                }
            }
            createTurnOrder();
            combatLog.Add("NEW ROUND");
            nextTurn(false, true);

        }

        /// <summary>
        /// CHECKS IF A TEAM IS DEAD THEN RETURNS TRUE OR FALSE
        /// </summary>
        bool isEveryoneDead(int teamToCheck)
        {
            int deaths = 0;
            bool yes = false;
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if (Teams[teamToCheck][i].isDead)
                {
                    deaths++;
                }
            }
            if (deaths == Teams[teamToCheck].Count)
            {
                yes = true;
            }
            return yes;
        }

        /// <summary>
        /// ADVANCE TURNS, CHECKS TO SEE IF PLAYER OR AI IS NEXT. ALSO CHECKS TO SEE IF BATTLE IS OVER
        /// </summary>
        public void nextTurn(bool hold, bool firstTurn)
        {

            if (isEveryoneDead(0))
            {
                availableTurns.Clear();
                battle(false, true);
                return;
            }
            if (isEveryoneDead(1))
            {
                availableTurns.Clear();
                battle(true, false);
                return;
            }
            //if there are turns that need to be played out
            if (availableTurns.Count > 0)
            {
                //if it's not the very first turn
                if (firstTurn == false)
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
                    turnOrderPrint();
                    //check to see whether it is the ai or the player going next
                    if (availableTurns[0].ID < numberOfPlayers)
                    {
                        playerOptions(availableTurns[0].ID);
                    }
                    else
                    {
                        AIcombat(availableTurns[0].ID - numberOfPlayers);
                    }
                }

            }
            // if there are no turns left then
            if (availableTurns.Count == 0)
            {
                //check to see if one team or the other is dead and if they are then do the according action
                if (isEveryoneDead(0))
                {
                    availableTurns.Clear();
                    battle(false, true);
                    return;
                }
                if (isEveryoneDead(1))
                {
                    availableTurns.Clear();
                    battle(true, false);
                    return;
                }
                //ortherwise contunue the battle
                availableTurns.Clear();
                battle(false, false);
            }
            // if the turns haven't run out, but someone is dead, then do the according action

        }






        /// <summary>
        /// A TEST GAME WHICH CREATES SOME GENERIC CHARACTERS, CHANGE RUN2 TO RUN TO TEST IT OUT
        /// </summary>
        public void Run2()
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
        /// THE CHARACTERS FOR TEST GAME
        /// </summary>
        void testMen()
        {
            int testXP = 500;
            numberOfPlayers = 3;
            team1Name = "[TEAM TESTER]";
            Team1.Add(new Character() { characterID = 1, characterName = "[TEST TANK]", Job = 0, XP = testXP, teamName = team1Name, TeamID = 0 });
            Team1.Add(new Character() { characterID = 2, characterName = "[TEST WARRIOR]", Job = 1, XP = testXP, teamName = team1Name, TeamID = 0 });
            Team1.Add(new Character() { characterID = 3, characterName = "[TEST MAGE THE 3RD]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0 });
            //Team1.Add(new Character() { characterID = 4, characterName = "[TEST MAGE]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0 });
            //Team1.Add(new Character() { characterID = 5, characterName = "[TEST MAGE THE 2ND]", Job = 3, XP = testXP, teamName = team1Name, TeamID = 0});
            Team1[0].Levelup();
            Team1[1].Levelup();
            Team1[2].Levelup();
            //Team1[3].Levelup();
            //Team1[4].Levelup();
            Team1[0].PrintStats();
            Team1[1].PrintStats();
            Team1[2].PrintStats();
            // Team1[3].PrintStats();
            //Team1[4].PrintStats();
        }

    }
