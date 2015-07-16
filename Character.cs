using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Character
    {
        Random r = new Random();

        int rng(int min, int max)
        {
            int number = r.Next(min, max);
            return number;
        }

        int maxHealth;
       public int HP;
       public int level= 0;
       public int XP = 0;
        //base health for each class
       int[] basehealth = new int[4] { 500, 400, 250, 300 };

        //1)Paladin   2)Warrior  3)Rogue  4) Mage
        //add additional class by increasing array sizes, might put these into their own class later and make a list
        //stats used when leveling up, these are affected by character job, 
        int[] strgrowth= new int[4]{10,13,5,3};
        int[] stamgrowth = new int[4]{15,10,7,8};
        int[] dexgrowth = new int[4]{5,9,15,6};
        int[] intelgrowth = new int[4]{5,1,6,17};

        //base values for critical strike, armour(damage reduction) and block, these are then altered slightly based character level and on dexterity, stamina and srength stats repsectively
        int[] critbase = new int[4] {1,4,20,10};
        int[] blockbase = new int[4] { 20, 7, 2, 1 };
        int[] armourbase = new int[4] { 20, 20, 5, 0 };
        
        //character stats
        int str = 0;
        int stam = 0;
        int dex = 0;
        int intel = 0;

        //
        int critchance;
        int blockchance;
        int armour;
        
        
       public int Job;
       public string characterName;
       public string teamName;
       public string[] jobNames = new string[4] { "PALADIN", "WARRIOR", "ROGUE", "MAGE" };



        //combat variables, used for whether their in a  defensive postion and the player who's defending them, defensive stance lasts until this character's next turn
       public bool defending;
       public bool defended;
       public int defender;






       public void PrintStats()
       {
           Console.WriteLine(teamName);
           Console.WriteLine("----------------------------");
           Console.WriteLine("    "+characterName);
           Console.WriteLine("----------------------------");
           Console.WriteLine("      LVL {0} {1}     ",level,jobNames[Job]);
           Console.WriteLine("----------------------------");
           Console.WriteLine("      HP {0}/{1}",HP,maxHealth);
           Console.WriteLine("----------------------------");
           Console.WriteLine("       STRENGTH " + str);
           Console.WriteLine("        STAMINA " + stam);
           Console.WriteLine("      DEXTERITY " + dex);
           Console.WriteLine("      INTELLECT " + intel);
           Console.WriteLine("----------------------------");
           Console.WriteLine("CRIT {0}% BLOCK {1}% ARMOUR {2}%",critchance,blockchance,armour);
           Console.WriteLine("----------------------------");
           Console.WriteLine();

           //if they're being defended by somone then print their stats too
           if (defended == true)
           {
               Console.WriteLine();
               Console.WriteLine("DEFENDED BY...");
               Program.Team1[defender].PrintStats();
           }



       }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
       void takedamage(int damage)
       {

       }




        /// <summary>
        /// Begin levelup sequence for characters, converts 100xp into a level then adds stats based on character job 
        /// </summary>
        public void Levelup()
        {

            int newLevels=0;

            while (XP > 100 || XP == 100)
            {
                XP -= 100;
                level++;
                newLevels++;
            }

            //add to stats
            while (newLevels > 0)
            {


                //stats
                str += strgrowth[Job]+(rng(0,strgrowth[Job]));
                stam += stamgrowth[Job] + (rng(0, stamgrowth[Job]));
                dex += dexgrowth[Job] + (rng(0, dexgrowth[Job]));
                intel += intelgrowth[Job] + (rng(0, intelgrowth[Job]));

                // alter block and crit chance
                critchance = critbase[Job] + (dex / (2 * level));
                blockchance = blockbase[Job] + (str / (2 * level));
                armour = armourbase[Job] + (stam / (2 * level));
                newLevels--;
            }
            // restore and alter health based on satmina
            maxHealth = basehealth[Job] + (stam * 10);
            HP = maxHealth;
        }



        /// <summary>
        /// used to clear unwanted stats
        /// </summary>
        public void clearStats()
        {
            stam = 0;
            str = 0;
            dex = 0;
            intel = 0;

        }




    }

