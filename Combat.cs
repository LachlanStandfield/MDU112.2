﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberArea
{
    class Combat
    {
        Program program = new Program();

        int turnCount = 1;
        //used to print the combat info
        int comabtlogTurnNumber;
        List<string> combatLog = new List<string>();

        //lsit used for ai behavior
        List<int> aiValues = new List<int>();
        List<int> aiIDValues = new List<int>();

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
            turnOrderFill(false);


            for (int i = 0; i < availableTurns.Count; i++)
            {
                if (availableTurns[i] < Program.numberOfPlayers)
                {
                    Console.WriteLine((i + 1) + " " + Program.Team1[availableTurns[i]].statBrief());
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine((i + 1) + " " + Program.Team2[availableTurns[i] - Program.numberOfPlayers].statBrief());
                    Console.WriteLine();
                }
            }


            Program.pressToContinue();
            Console.Clear();

        }


        /// <summary>
        /// determines player order according to a random number CURRENTLY ONLY RNG
        /// which is influenced by the dex stat of each character
        /// </summary>
        public void turnOrder()
        {
            specialEyes = 0;
            while (specialEyes < (Program.numberOfPlayers * 2))
            {

                bool valid = false;

                while (valid == false)
                {

                    turnOrderCount = specialEyes;
                    turns[specialEyes] = rng(0, Program.numberOfPlayers * 2);
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
        public void turnOrderFill(bool phasestart)
        {
            if (phasestart)
            {
                availableTurns.Clear();
                for (int i = 0; i < Program.numberOfPlayers * 2; i++)
                {
                    if (turns[i] != 33)
                    {
                        if (turns[i] < Program.numberOfPlayers)
                        {
                            if (Program.Team1[turns[i]].isDead == false)
                            {
                                availableTurns.Add(turns[i]);
                            }
                        }
                        else
                        {
                            if (Program.Teams[1][turns[i] - Program.numberOfPlayers].isDead == false)
                            {
                                availableTurns.Add(turns[i]);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < availableTurns.Count; i++)
                {
                    if (availableTurns[i] < Program.numberOfPlayers)
                    {
                        if (Program.Teams[0][availableTurns[i]].isDead)
                        {
                            availableTurns.Remove(availableTurns[i]);
                        }
                    }
                    else
                    {
                        if (Program.Teams[1][availableTurns[i] - Program.numberOfPlayers].isDead)
                        {
                            availableTurns.Remove(availableTurns[i]);
                        }
                    }
                }
            }

        }


        /// <summary>
        /// Player chooses their command. AI doesn't use this menu
        /// MENU
        /// </summary>
        public void playerOptions(int playerIndex)
        {
            Console.Clear();
            if (Program.Teams[0][playerIndex].defending)
            {
                Console.WriteLine("{0} STOPPED DEFENDING", Program.Teams[0][playerIndex].characterName);
            }
            //turn off defending
            Program.Teams[0][playerIndex].defendingOther = false;
            Program.Teams[0][playerIndex].defending = false;
            Program.Teams[0][Program.Teams[0][playerIndex].defendedID].defended = false;
            bool holding = false;
            int actionInput;
            bool actionTaken = false;
            Console.WriteLine(Program.Team1[playerIndex].statBrief() + " - TURN -");
            Console.WriteLine();
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
                Console.WriteLine();
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
                    Program.pressToContinue();
                    Console.Clear();
                    nextTurn(true);
                    return;
                }
                else
                {
                    Program.pressToContinue();
                    Console.Clear();
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
        /// apply damage to target, base damage value is found in the character class, checks against defender's defensive stats
        /// </summary>
        void attack(int attacker, int target, int attackerTeamNumber, int defenderTeamNumber)
        {
            int damage;
            Console.WriteLine();
            Console.WriteLine();
            //check if the target is being defended by another, then retarget the defender
            if (Program.Teams[defenderTeamNumber][target].defended)
            {
                Console.WriteLine("{0} ATTACKS {1}...", Program.Teams[attackerTeamNumber][attacker].statBrief(), Program.Teams[defenderTeamNumber][target].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(500);
                Console.WriteLine("BUT {0} INTERCEPTS THEIR ATTACK!", Program.Teams[defenderTeamNumber][Program.Teams[defenderTeamNumber][target].defender].statBrief());
                Console.WriteLine();
                System.Threading.Thread.Sleep(1000);
                target = Program.Teams[defenderTeamNumber][target].defender;
            }
            //otherwise continue as normal
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
                damage = damage * 3;
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
                        armour = 1 - ((Convert.ToDecimal(Program.Teams[defenderTeamNumber][target].armour) / 100) * 2);
                    }
                    damage = Convert.ToInt32(Convert.ToDecimal(damage) * armour);
                }
            }

            //the target receives the damage
            Program.Teams[defenderTeamNumber][target].takedamage(damage);

            if (isEveryoneDead(defenderTeamNumber))
            {
                Console.WriteLine();
                Console.WriteLine("{0} HAVE BEEN COMPLETEY DEFEATED BY {1}", Program.Teams[defenderTeamNumber][0].teamName, Program.Teams[attackerTeamNumber][0].teamName);
            }
        }

        /// <summary>
        /// apply healing to target
        /// </summary>
        void heal(int target, int healer, int healerTeam)
        {

            if (healer == target)
            {
                Console.WriteLine("{0} HEALS [SELF]", Program.Teams[healerTeam][healer].statBrief());
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("{0} HEALS {1}", Program.Teams[healerTeam][healer].statBrief(), Program.Teams[healerTeam][target].statBrief());
                Console.WriteLine();
            }
            System.Threading.Thread.Sleep(500);
            Program.Teams[healerTeam][target].receiveHeal(Program.Teams[healerTeam][healer].characterHeal());


        }

        /// <summary>
        /// Enter a defensive stance, Can defend self or stand infront of another character
        /// doing so reduces the defended character's damage output
        /// </summary>
        void defend(int defendTarget, int defender, int teamIndex)
        {
            //if they're already defended , then they're an invalid target, they can be defending themself though. You can't stack up defenders either.
            if (Program.Teams[teamIndex][defendTarget].defended == true || Program.Teams[teamIndex][defendTarget].defendingOther == true || Program.Teams[teamIndex][defender].defended == true)
            {
                Console.WriteLine("INVALID TARGET");
                Program.pressToContinue();
                playerOptions(defender);
                return;

            }
            else
            {
                if (defender == defendTarget)
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE", Program.Teams[teamIndex][defender].statBrief());
                    Program.Teams[teamIndex][defender].defending = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("{0} ENTERS A DEFENSIVE STANCE IN FRONT OF {1}", Program.Teams[teamIndex][defender].statBrief(), Program.Teams[teamIndex][defendTarget].statBrief());
                    Program.Teams[teamIndex][defender].defending = true;
                    Program.Teams[teamIndex][defender].defendedID = defendTarget;
                    Program.Teams[teamIndex][defender].defendingOther = true;
                    Program.Teams[teamIndex][defendTarget].defended = true;
                    Program.Teams[teamIndex][defendTarget].defender = defender;
                }
                System.Threading.Thread.Sleep(500);
            }

        }

        /// <summary>
        /// Scan Enemies and allies for detailed information, only applies to players
        /// </summary>
        /// <param name="playerIndex"></param>
        void scan(int playerIndex)
        {
            int selection;
            Console.WriteLine();
            Console.WriteLine("SELECT TARGET TO SCAN");
            Console.WriteLine();

            //lists allied team
            for (int i = 0; i < Program.Teams[0].Count; i++)
            {
                if (Program.Teams[0][i].isDead == false)
                {
                    //Specifies that the target is themself
                    if (i == playerIndex)
                    {
                        Console.WriteLine((i + 1) + ". [SELF]");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine((i + 1) + ". " + Program.Teams[0][i].statBrief());
                        Console.WriteLine();
                    }
                }
            }

            //lists enemy team.
            for (int i = 0; i < Program.Teams[1].Count; i++)
            {
                if (Program.Teams[1][i].isDead == false)
                {
                    Console.WriteLine((i + 1 + Program.Team1.Count) + ". " + Program.Teams[1][i].statBrief());
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
                //if the selection isn't invalid then the character's stats are printed
                else
                {
                    if (Program.Teams[1][selection - Program.Teams[1].Count].isDead == false)
                    {
                        Program.Teams[1][selection - Program.Teams[1].Count].PrintStats();
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
            Program.pressToContinue();
            Console.Clear();

        }

        /// <summary>
        /// Character Passes their turn until Last.
        /// </summary>
        /// <param name="playerIndex"></param>
        void hold(int playerIndex, int teamNumber)
        {
            List<int> tempTurns = new List<int>();
            Console.WriteLine();
            if (playerIndex > Program.numberOfPlayers - 1)
            {
                Console.WriteLine("{0} WAITS", Program.Teams[teamNumber][playerIndex - Program.numberOfPlayers].statBrief());
            }
            else
            {
                Console.WriteLine("{0} WAITS", Program.Teams[teamNumber][playerIndex].statBrief());
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
            for (int i = 0; i < availableTurns.Count; i++)
            {
                turns[i] = availableTurns[i];
            }
        }

        /// <summary>
        /// checks if the player made an invalid input when selecting a target for attacking or healing
        /// </summary>
        bool checkInvalidTarget(int actionInput, int targetTeam, int playerIndex)
        {
            bool invalidTarget = false;
            if (actionInput < 1 || actionInput > Program.Teams[targetTeam].Count)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                Program.pressToContinue();
                Console.Clear();
                invalidTarget = true;

            }
            else if (Program.Teams[targetTeam][actionInput - 1].isDead == true)
            {
                Console.WriteLine();
                Console.WriteLine("INVALID TARGET");
                Program.pressToContinue();
                Console.Clear();
                invalidTarget = true;
            }
            return invalidTarget;
        }


        /// <summary>
        /// advance the turns forward, plays ai code if the character is on the enemy team otherwise directs to the player options
        /// </summary>
        public void nextTurn(bool hold)
        {
            if (availableTurns.Count > 0)
            {

                if (turnCount > 1)
                {
                    // Console.WriteLine("TURN " + turnCount);
                    if (hold)
                    {

                    }
                    else
                    {
                        availableTurns.Remove(availableTurns[0]);
                    }
                }
                if (availableTurns.Count > 0)
                {
                    turnCount++;
                    turnOrderPrint();
                    if (availableTurns[0] < Program.numberOfPlayers)
                    {
                        playerOptions(availableTurns[0]);
                    }
                    else
                    {
                        AIcombat(availableTurns[0] - Program.numberOfPlayers);
                    }
                }

            }
            if (availableTurns.Count <= 0)
            {
                if (isEveryoneDead(0))
                {
                    program.battle(false, true);
                }
                if (isEveryoneDead(1))
                {
                    program.battle(true, false);
                }
                program.battle(false, false);
            }
            else if (isEveryoneDead(0))
            {
                program.battle(false, true);
            }
            else if (isEveryoneDead(1))
            {
                program.battle(true, false);
            }
        }


        /// <summary>
        /// List player allies
        /// </summary>
        void listAllies(int playerIndex)
        {
            for (int i = 0; i < Program.Teams[0].Count; i++)
            {

                if (Program.Teams[0][i].isDead == false)
                {
                    if (i == playerIndex)
                    {
                        Console.WriteLine((i + 1) + ". [SELF]");
                    }
                    else
                    {
                        Console.WriteLine((i + 1) + ". " + Program.Teams[0][i].statBrief());
                    }
                    Console.WriteLine();
                }

            }
        }


        /// <summary>
        /// Ai Code
        /// if they're a mage then they'll have a 30% chance to heal  the lowest health ally. They also have a 15% to pass their turn.
        /// if they're a tank then they'll have a 70% to defend for a rogue or mage.
        /// if they're a warrior then they have a 40% to defend for a rogue or mage.
        /// if they're a rogue then they'll attack regardless, rogues are pricks.
        /// default behavior is just to attack the lowest health target
        /// </summary>
        void AIcombat(int botID)
        {
            aiValues.Clear();
            aiIDValues.Clear();
            //turn off defending
            if (Program.Teams[1][botID].defending)
            {
                Console.WriteLine("{0} STOPPED DEFENDING", Program.Teams[1][botID].characterName);
            }
            Program.Teams[1][botID].defendingOther = false;
            Program.Teams[1][botID].defending = false;
            Program.Teams[1][Program.Teams[1][botID].defendedID].defended = false;
            bool holding = false;
            //clear the value lists so they can be reassinged*
            bool actionTaken = false;
            //generate the behavior value
            int aiBehavior = rng(1, 100);
            //the value used to check which behavior the ai should perform
            int aiBehaviorCheck;


            //tank specific
            if (Program.Teams[1][botID].Job == 0)
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
            if (Program.Teams[1][botID].Job == 1)
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
            if (Program.Teams[1][botID].Job == 3)
            {
                aiBehaviorCheck = 30;
                //heal lowest health ally
                if (aiBehavior < aiBehaviorCheck)
                {
                    for (int i = 0; i < Program.numberOfPlayers; i++)
                    {
                        aiValues.Add(Program.Teams[1][i].HP);
                    }
                    checkLowestValue();
                    heal(aiIDValues[0], botID, 1);
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
                for (int i = 0; i < Program.numberOfPlayers; i++)
                {
                    aiValues.Add(Program.Teams[0][i].HP);
                }
                checkLowestValue();
                attack(botID, aiIDValues[0], 1, 0);
            }
            Program.pressToContinue();
            Console.Clear();
            nextTurn(holding);
            return;
        }

        /// <summary>
        /// Used to find a target for the ai to block for
        /// </summary>
        /// <param name="botID"></param>
        bool blockForSquishy(int botID)
        {
            aiValues.Clear();
            aiIDValues.Clear();
            bool ok = false;
            bool noBlock = false;
            for (int i = 0; i < Program.Teams[1].Count; i++)
            {
                //assigns the job value of ai teammates to the list
                aiValues.Add(Program.Teams[1][i].Job);
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
                    if (Program.Teams[1][botID].defended == false)
                    {
                        if (Program.Teams[1][aiIDValues[k]].defended == false && Program.Teams[1][aiIDValues[k]].isDead == false)
                        {
                            defend(aiIDValues[k], botID, 1);
                            ok = true;
                        }
                    }
                    k++;
                    if (k == aiIDValues.Count - 1)
                    {
                        ok = true;
                        noBlock = true;
                    }
                }
            }
            if (noBlock)
            {
                ok = false;
            }
            return ok;
        }

        /// <summary>
        /// used by the ai to find  low health targets for both healing and damage dealing
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
            for (int i = 0; i < aiValues.Count; i++)
            {
                int temp;


                if (i + 1 < aiValues.Count)
                {
                    if (aiValues[i] > aiValues[i + 1])
                    {
                        temp = aiValues[i];
                        aiValues[i] = aiValues[i + 1];
                        aiValues[i + 1] = temp;
                        temp = aiIDValues[i];
                        aiIDValues[i] = aiIDValues[i + 1];
                        aiIDValues[i + 1] = temp;


                        if (i - 1 >= 0)
                        {
                            temp = aiValues[i - 1];
                            if (aiValues[i] < aiValues[i - 1])
                            {
                                aiValues[i - 1] = aiValues[i];
                                aiValues[i] = temp;
                                temp = aiIDValues[i - 1];
                                aiIDValues[i - 1] = aiIDValues[i];
                                aiIDValues[i] = temp;
                            }
                        }
                    }

                }
                //remove 0 values as it means the target is dead
                if (aiValues[0] == 0)
                {
                    aiValues.Remove(aiValues[0]);
                    aiIDValues.Remove(aiIDValues[0]);
                }

            }
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









    }
}

