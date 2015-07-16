using System;
using System.Collections.Generic;

class Program
{

    Random r = new Random();
   public static List<Character> Team1 = new List<Character>();
   public static List<Character> Team2 = new List<Character>();
   public string team1Name;
   public string team2Name;
   int? numberOfPlayers;


    //testchange2
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

    int? checkNumberAnswer()
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
        return number;
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
    /// Create characters for the player to use.
    /// </summary>
    void createPlayerCharacter()
    {
        
        int counter= 1;
        bool valid = false;

        while (valid == false)
        {
            Console.WriteLine("Name Your Team...");
            team1Name = Console.ReadLine();
            Console.WriteLine("So you're {0}? Y/N", team1Name);
            valid = yesOrNo(Console.ReadLine());
        }
        valid = false;

        while (counter <= (numberOfPlayers-1))
        {
            Team1.Add(new Character());
            valid = false;
            Team1[counter].teamName = team1Name;
            while (valid == false)
            {
                Console.WriteLine("Enter the name for {1}'s Fighter {0}", counter,team1Name);
                Team1[counter].characterName = Console.ReadLine();
                Console.WriteLine(Team1[counter].characterName+", is this name correct? Y/N");

                valid = yesOrNo(Console.ReadLine());

            }
            valid = false;
            while (valid == false)
            {
                Console.WriteLine("Choose their job...");
                Console.WriteLine("0. Paladin    1. Warrior   2. Rogue   3. Mage");
                Team1[counter].Job = Convert.ToInt32(checkNumberAnswer());
                if (Team1[counter].Job > 3)
                {
                    Team1[counter].Job = 0;
                }
                Console.WriteLine(Team1[counter].jobNames[Team1[counter].Job] + ", is this job correct? Y/N");
                valid = yesOrNo(Console.ReadLine());
            }
            valid = false;

            while (valid == false)
            {
                Console.Clear();
                Console.WriteLine("Generating Character...");
                Team1[counter].level = 0;
                Team1[counter].XP += 100;
                Team1[counter].Levelup();
                Team1[counter].PrintStats();
                Console.WriteLine();
                Console.WriteLine("Are these stats Satisfactory?");
                valid = yesOrNo(Console.ReadLine());
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

        Console.WriteLine("How many players on each team?");
        numberOfPlayers = checkNumberAnswer();
        if (numberOfPlayers < 0 || numberOfPlayers > 5)
        {
            Console.WriteLine("Invalid Number of team members (1-5)");
            choosePlayerNumber();
            return;
        }
        Console.WriteLine("{0} players per team", numberOfPlayers);
        Team1.Add(new Character() {characterName ="Zero" });
        Team2.Add(new Character() { characterName ="Zero"});
        numberOfPlayers++;
    }







    ///<summary>
    ///Main Logic
    ///</summary>
    public void Run()
    {
        string[] randomName = new string[4] { "Dick", "Dickinson", "Dickus", "John" };
        string[] randomTitle = new string[4] { "The Killer", "The Dick", "The Reckless", "The Faggot" };

        //job names, should put this under the character class


        //intro text
        Console.WriteLine("Welcome challangers to Cyber Arena 20XX 2");
        Console.WriteLine("The intense team battle combat game");

        choosePlayerNumber();
       

        Console.WriteLine("Create your Fighters");
        Console.ReadKey();
        Console.Clear();

        createPlayerCharacter();




        for (int count = 1; count < numberOfPlayers; count++)
        {
            //create random character
            Team2.Add(new Character() { characterName = (randomName[rng(0, (randomName.Length))] + " " + randomTitle[rng(0, (randomTitle.Length))]), Job = rng(0, 4) });
            Team2[count].XP += rng(100, 10000);
            Team2[count].Levelup();

            //announce them
            Team2[count].PrintStats();




        }
        Console.ReadKey();
        Console.Clear();
        Run();
    }

}
