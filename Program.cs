﻿using System;
using System.Collections.Generic;

class Program
{

   Random r = new Random();
   public static List<Character> Team1 = new List<Character>();
   public static List<Character> Team2 = new List<Character>();
   public static List<Character>[] Teams = new List<Character>[2]{Team1,Team2};  
   public string team1Name;
   public string team2Name;
   int assignID = 1;
   public static int numberOfPlayers;



    ///<summary>
    ///random number generator
    ///</summary>

    int rng(int min, int max)
    {
        int number = r.Next(min, max);
        return number;
    }



    ///<summary>
    ///Check answer for number, then return that number
    ///</summary>

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



    bool yesOrNo(string input)
    {
        bool valid = false;
            if (input == "y" || input == "Yes")
            {
                valid = true;
            }
            else if (input == "n" || input == "No")
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
    /// Create enemies
    /// </summary>
    void createEnemy()
    {
        string[] randomName = new string[10] { "DICK", "DIRTY", "CASH", "JOHN", "JONATHAN","DIO","SMOKEY","BRUCE","BIG","LITTLE" };
        string[] randomTitle = new string[10] { "DOE", "BADDY", "DAN", "ZEPPELI","JOESTAR","BRANDO","MONEY","CENA","JOHNSON","WAYNE" };
        string[] randomTeamName = new string[12] { "KILLER", "SANDY", "SALTY", "SEXUAL", "ANGRY", "SILENT", "MEN", "KILLERS", "RASCALS", "GENTLEMEN", "FREAKS", "BOYS" };

        team2Name = "[THE " + randomTeamName[rng(0, randomTeamName.Length / 2)] + " " + randomTeamName[rng(randomTeamName.Length/2, randomTeamName.Length)]+"]";
        for (int count = 0; count < numberOfPlayers; count++)
        {
            //create random character
            Teams[1].Add(new Character() { characterName = "["+(randomName[rng(0, (randomName.Length))] + " " + randomTitle[rng(0, (randomTitle.Length))]+"]"), Job = rng(0, 4) });
            Teams[1][count].teamName = team2Name;
            Teams[1][count].characterID = assignID;
            assignID++;
            Teams[1][count].XP = rng(1,100)+(Team1[count].level*100);
            Teams[1][count].Levelup();

            //announce them
            Teams[1][count].PrintStats();
        }

    }



    /// <summary>
    /// Create characters for the player to use.
    /// </summary>
    void createPlayerCharacter()
    {
        
        int counter= 0;
        bool valid = false;

        //Asks the player to name the team
        while (valid == false)
        {
            Console.WriteLine("TEAM NAME:");
            team1Name = ("TEAM "+ Console.ReadLine());
            Console.WriteLine("SO YOUR TEAM IS CALLED {0}? Y/N", team1Name);
            valid = yesOrNo(Console.ReadLine());
        }
        valid = false;


        //Creates the new character, then assigns the team name to them
        while (counter <= (numberOfPlayers-1))
        {
            Team1.Add(new Character());
            valid = false;
            Team1[counter].teamName = "["+team1Name+"]";
            Team1[counter].characterID = assignID;
            assignID++;

            //asks for their name
            while (valid == false)
            {
                Console.WriteLine("ENTER THE NAME FOR {1}, FIGHTER {0}:", Team1[counter].characterID,team1Name);
                Team1[counter].characterName = Console.ReadLine();
                Console.WriteLine(Team1[counter].characterName+", REGISTERED. PROCEED? Y/N");

                valid = yesOrNo(Console.ReadLine());

            }
            valid = false;

            //Asks the player to choose the character's job
            while (valid == false)
            {
                Console.WriteLine("CHOOSE "+ Team1[counter].characterName+"'S JOB");
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
                Console.WriteLine("...GENERATING FIGHTER...");
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
    /// choose number of members  for each team
    /// </summary>

    void choosePlayerNumber()
    {

        Console.WriteLine("HOW MANY PLAYERS PER TEAM? (2-5)");
        numberOfPlayers = checkNumberAnswer();
        if (numberOfPlayers < 2 || numberOfPlayers > 5)
        {
            Console.WriteLine();
            Console.WriteLine("INVALID NUMBER (2-5)");
            choosePlayerNumber();
            return;
        }
        Console.WriteLine();
        Console.WriteLine("{0} PLAYERS PER TEAM", numberOfPlayers);
        Console.WriteLine();
    }







    ///<summary>
    ///Main Logic
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
        Console.WriteLine("[PRESS ANY KEY TO CONTINUE]");
        Console.ReadKey();
        Console.Clear();

        createPlayerCharacter();
        createEnemy();

        Console.ReadKey();
        Combat Combatphase = new Combat();
        Combatphase.turnOrder();
        Console.ReadKey();
        Console.Clear();
        Run();
    }

    public void Run()
    {
        Console.WriteLine("TEST GAME");
        pressToContinue();
        testMen();
        createEnemy();
        Combat Combatphase = new Combat();
        Console.WriteLine(" TURN ORDER TEST");
        Combatphase.turnOrder();
        Combatphase.turnOrderFill();
        Combatphase.turnOrderPrint();
        Console.WriteLine(" --DEATH TEST--");
        pressToContinue();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            Teams[0][i].isDead = true;
        }
        Teams[0][1].isDead = false;
        Teams[0][3].isDead = false;
        Teams[1][2].defending = true;
        Teams[1][1].defender = 2;
        Teams[1][1].defended = true;
        Combatphase.turnOrderPrint();
        pressToContinue();
        Console.Clear();
        Combatphase.playerOptions(1);
        pressToContinue();
        Console.WriteLine(" --BATTLE TEST--");
        Console.WriteLine();
        //combat tests
        for (int i = 0; i < 50; i++)
        {
            Combatphase.dealdamage( rng(1,numberOfPlayers), rng(1, numberOfPlayers),0,1);
            Combatphase.heal( rng(0, Convert.ToInt32(numberOfPlayers)), rng(0, Convert.ToInt32(numberOfPlayers)),1);
            Combatphase.dealdamage(rng(1, numberOfPlayers), rng(1, numberOfPlayers), 1, 0);
            Combatphase.heal(rng(0, Convert.ToInt32(numberOfPlayers)), rng(0, Convert.ToInt32(numberOfPlayers)), 0);
            
        }

    }

    void testMen()
    {
        int testXP = 1000;
        numberOfPlayers = 4;
        team1Name = "[TEAM TESTER]";
        Team1.Add(new Character() { characterID = 1, characterName = "[TEST TANK]", Job = 0, XP = testXP, teamName = team1Name  });
        Team1.Add(new Character() { characterID = 2, characterName = "[TEST WARRIOR]", Job = 1, XP = testXP, teamName = team1Name});
        Team1.Add(new Character() { characterID = 3, characterName = "[TEST ROGUE]", Job = 2, XP = testXP, teamName = team1Name });
        Team1.Add(new Character() { characterID = 4, characterName = "[TEST MAGE]", Job = 3, XP = testXP, teamName = team1Name, defender = 0, defended= true });
        Team1[0].Levelup();
        Team1[1].Levelup();
        Team1[2].Levelup();
        Team1[3].Levelup();
        Team1[0].PrintStats();
        Team1[1].PrintStats();
        Team1[2].PrintStats();
        Team1[3].PrintStats();
    }

    static public void pressToContinue()
    {
        Console.WriteLine();
        Console.WriteLine("[PRESS ANY KEY TO CONTINUE]");
        Console.WriteLine();
        Console.ReadKey();
    }

}
