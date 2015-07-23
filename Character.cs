using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


   public class Character
    {

       //random number used for rng
       Random r = new Random();


       //the character's ID number, this can be 1-10
       public int characterID;
       public int TeamID;
       //Health variables
       public int maxHealth;
       public int HP;

       // Level Variables
       public int level= 0;
       public int XP = 0;

        //base health for each class, not growth. used for balance purposes.
       int[] basehealth = new int[4] { 800, 400, 200, 300 };

        //1)Paladin   2)Warrior  3)Rogue  4) Mage
        //add additional class by increasing array sizes, might put these into their own class later and make a list
        //stats used when leveling up, these are affected by character job, 
        int[] strgrowth= new int[4]   {3,20,8,0};
        int[] stamgrowth = new int[4] {25,10,5,9};
        int[] dexgrowth = new int[4]  {5,9,25,6};
        int[] intelgrowth = new int[4]{2,1,9,26};

        //base values for critical strike, armour(damage reduction) and block, these are then altered slightly based on character level and on dexterity/intellect, stamina/strength and srength/dexterity stats repsectively
        int[] critbase = new int[4]   { 0, 3, 50, 20};
        int[] blockbase = new int[4]  { 4, 0, 1, 0 };
        int[] armourbase = new int[4] { 20, 3, 2, 0 };
        
        //character stats
       public int str = 0;
       public int stam = 0;
       public int dex = 0;
       public int intel = 0;

        // combat stats, critchance gives a chance to do 3x damage, blackchance gives the chance to block all damage, armour reduces damage taken by a percent
       public int critchance;
       public  int blockchance;
       public  int armour;
         
        
       public int Job;
       public string characterName;
       public string teamName;
       public string[] jobNames = new string[4] { "[BORG TANK]", "[MECHA GALDIATOR]", "[WARP SLICER]", "[CYBER CASTER]" };

       //combat variables to determine whether this character is dead
       public bool isDead = false;

       //combat variables, used for whether their in a  defensive postion or wheather another player is defending them
       //defensive stance lasts until this character's next turn
       int defenceArmour;
       int defenceBlock;
       public bool defendingOther = false;
       public bool defending = false;
       public bool defended = false;
       public int defender;
       public int defendedID;


       //random number generator
       int rng(int min, int max)
       {
           int number = r.Next(min, max);
           return number;
           
       }

       public string statBrief()
       {
           string brief;
           brief = characterName + " THE " + jobNames[Job] + " " + HP + "/" + maxHealth;
           if (defending == true)
           {
               brief = brief + " <DEFENDING>";
           }
           if (isDead)
           {
               brief = brief + " <DEAD>";
           }
           return brief; 
       }


       /// <summary>
       /// print character Stats, shows if they're being defended
       /// </summary>
       public void PrintStats()
       {
           Console.WriteLine("    "+teamName);
           Console.WriteLine("----------------------------");
           Console.WriteLine("        {0}",characterName);
           Console.WriteLine("----------------------------");
           Console.WriteLine("      LVL {0} {1}     ",level,jobNames[Job]);
           Console.WriteLine("----------------------------");
           if (isDead == false)
           {
           Console.WriteLine("      HP {0}/{1}", HP, maxHealth);
           }
           else
           {
           Console.WriteLine("          --DEAD--");
           }
           Console.WriteLine("----------------------------");
           Console.WriteLine("       STRENGTH " + str);
           Console.WriteLine("        STAMINA " + stam);
           Console.WriteLine("      DEXTERITY " + dex);
           Console.WriteLine("      INTELLECT " + intel);
           if (defending == true)
           {
               defenceCalc();
               Console.WriteLine("----------------------------");
               Console.WriteLine("      --DEFENDING--");
               Console.WriteLine("----------------------------");
               Console.WriteLine("CRIT {0}% BLOCK {1}% ARMOUR {2}%", critchance, defenceBlock, defenceArmour);
               Console.WriteLine("----------------------------");
               Console.WriteLine();
           }
           else
           {
               Console.WriteLine("----------------------------");
               Console.WriteLine("CRIT {0}% BLOCK {1}% ARMOUR {2}%", critchance, blockchance, armour);
               Console.WriteLine("----------------------------");
               Console.WriteLine();
           }

           //if they're being defended by somone then print their stats too
           if (defended == true)
               //makes sure they're not defending themself
               if (defender != characterID - 1)
               {
                   {
                       Console.Write("DEFENDED BY ");
                       Console.Write(Program.Team1[defender].statBrief());
                       Console.WriteLine();
                       Console.WriteLine();
                   }
               }



       }
       /// <summary>
       /// calculate the stats for a character whilest they defend
       /// these are only used to print the values at the moment
       /// </summary>
       void defenceCalc()
       {
           defenceArmour = armour * 2;
           defenceBlock = blockchance * 2;
               if (defenceBlock > 80)
               {
                   defenceBlock = 80;
               }
               if (defenceArmour > 90)
               {
                   defenceArmour = 90;
               }
       }

       /// <summary>
       /// Receive healing, if they're a tank then they receive less
       /// </summary>
       /// <param name="healAmount"></param>
       public void receiveHeal(int healAmount)
       {
           if (Job == 2){
               healAmount /= 2;
           }
           HP += healAmount;
           if (HP > maxHealth)
           {
               HP = maxHealth;
           }
           Console.WriteLine("{0} RECEIVES {1} HEALTH",characterName,healAmount);
           Console.WriteLine();
           System.Threading.Thread.Sleep(500);
       }


       /// <summary>
       /// calculates healing based on a random value based on their intellect plus a base heal of their intellectx3
       /// Tanks can't heal well, otherwise they'd be overpowered
       /// </summary>
       /// <returns></returns>
       public int characterHeal()
       {
           int heal;
           heal = rng(1, intel) + (intel * 3);
           if (Job == 0)
           {
               heal /= 2;
           }
           return heal;
       }

       /// <summary>
       /// calculate base damage by multiplying strength by 4. seperates mages from other classes, as they use their int stat to deal damage
       /// if the character is being defended then their damage is halved
       /// </summary>
       /// <returns></returns>
       public int characterDamage()
       {
           int damage;
           if (Job == 3)
           {
               damage = intel * 5;
           }
           else
           {
               damage = str * 9;
           }
           if (defended)
           {
               damage /= 2;
           }
           return damage;
       }

        /// <summary>
        /// recieve damage, if their health is 0 then they're decalred dead
        /// </summary>
        /// <param name="damage"></param>
       public void takedamage(int damage)
       {
           HP -= damage;
           Console.WriteLine("{0} RECEIVES {1} DAMAGE!", characterName, damage);
           Console.WriteLine();
           if (HP <= 0)
           {
               HP = 0;
               System.Threading.Thread.Sleep(500);
               defending = false;
               defendingOther = false;
               defended = false;
               Program.Teams[TeamID][defender].defending = false;
               Program.Teams[TeamID][defender].defendingOther = false;
               Program.Teams[TeamID][defendedID].defended = false;
               
               

               Console.Write(characterName + " HAS BEEN DESTROYED!");
               Console.WriteLine();
               isDead = true;
               Console.WriteLine();
           }
       }

       public bool dodge()
       {
           bool dodge= false;
           int hitchance = rng(1, 100);
           int dodgechance = rng(1, 5) + (dex /( 6*level));
           if (dodgechance > 75)
           {
               dodgechance = 75;
           }
           if (dodgechance > hitchance)
           {
               dodge = true;
           }


           return dodge;
       }
       public bool block()
       {
           bool block = false;
           int hitchance = rng(1, 100);
           if (defending == true) 
           {
               blockchance *= 2;
           }
           if (blockchance > 90)
           {
               blockchance = 90;
           }
           if (blockchance > hitchance)
           {
               block = true;
           }


           return block;
       }

       public bool crit()
       {
           bool crit = false;

           int hitchance = rng(1, 100);
           if (critchance > hitchance)
           {
               crit = true;
           }


           return crit;
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
                critchance = critbase[Job] + (dex / (2 * level) + (intel / (3* level)));
                blockchance = blockbase[Job] + (str / (5 * level))+ (dex / (5 * level)+ (stam / (5 * level)));
                armour = armourbase[Job] + (stam / (3 * level)) + (str / (5 * level));
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

