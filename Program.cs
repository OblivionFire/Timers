using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
/*
 * work needs to be done on the 'next timer selection' option when a timer finishes. The code works up until you have to actually pick the next timer
 * I would also like to add a option to the quickstart the next timer in the path
 */
namespace Timer_app_V3._0._0
{
    class Program
    {
        public static pathManager pathStorage = new pathManager();
        public static bool waitForTimer;

        public static void BL()
        {
            Console.WriteLine("  ");
        }
        static void Main(string[] args)
        {
            bool exit = false;
            Program.waitForTimer = false;
            char option = '1';

            do
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Add a path");
                Console.WriteLine("2: Add a timer to an existing path");
                Console.WriteLine("3: Start a timer");
                Console.WriteLine("4: Wait for timer to complete");
                Console.WriteLine("5: Exit");
                BL();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                option = Console.ReadKey().KeyChar;
                BL();
                Console.ForegroundColor = ConsoleColor.White;
                BL();
                switch (option)
                {
                    case '1':
                        {
                            Console.WriteLine("Option 1: Choosen. Adding a Path");
                            string pathName;
                            BL();
                            Console.WriteLine("What would you like the name of this path to be?");
                            BL();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            pathName = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            BL();
                            pathStorage.newPath(pathName);
                            break;
                        }

                    case '2':
                        {
                            BL();
                            Console.WriteLine("Which path would you like to add a timer to?");
                            BL();
                            pathStorage.getPathnames();
                            BL();
                            int pathChoice = 0;
                            BL();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            pathChoice = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.White;
                            BL();
                            BL();
                            Console.WriteLine("What would you like to name the timer");
                            BL();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            string TimerName = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            BL();
                            Console.WriteLine("How long would you like the timer to be?");
                            Console.WriteLine("Please use three messurments Hours, Minutes, Seconds, in this order");
                            Console.WriteLine("HH -enter-");
                            Console.WriteLine("MM -enter-");
                            Console.WriteLine("S -enter-");
                            BL();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            int HoursToAdd = Convert.ToInt32(Console.ReadLine());
                            BL();
                            int MinutesToAdd = Convert.ToInt32(Console.ReadLine());
                            BL();
                            int SecondsToAdd = Convert.ToInt32(Console.ReadLine());
                            BL();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine(string.Format(("Adding a timer named '{0}' for {1} Hours, {2} Minutes, {3} Seconds"), TimerName, HoursToAdd, MinutesToAdd, SecondsToAdd));
                            BL();
                            pathStorage.addATimer(TimerName, pathChoice, HoursToAdd, MinutesToAdd, SecondsToAdd);
                            BL();
                            break;
                        }

                    case '3':
                        {
                            BL();
                            Console.WriteLine("Which Path is the timer you want to start in");
                            BL();
                            pathStorage.getPathNamesAndTimers();
                            BL();
                            int pathChoice = 0;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            pathChoice = Convert.ToInt32(Console.ReadLine());
                            BL();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("And What is the timer you would like to Start. Please type the name in");
                            BL();
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            string timerChoice = Console.ReadLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            BL();
                            pathStorage.startATimer(timerChoice, pathChoice);
                            BL();
                            break;
                        }

                    case '4':
                        {
                            Console.WriteLine("Waiting for timer to complete. Menu will display again after timer is done");
                            waitForTimer = true;
                            break;
                        }
                    case '5':
                        {
                            BL();
                            Console.WriteLine("Are you sure you want to exit? This will delete all you current paths and timers.");
                            Console.WriteLine("0: No");
                            Console.WriteLine("1: Yes");
                            int choice = 0;
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            choice = Convert.ToInt32(Console.ReadLine());
                            Console.ForegroundColor = ConsoleColor.White;
                            BL();
                            if (choice == 0)
                            {
                                Console.WriteLine("The program will not close");
                                BL();
                            }
                            else
                            {
                                Console.WriteLine("Exiting");
                                exit = true;
                                BL();
                            }
                            break;
                        }

                    default:
                        {
                            BL();
                            Console.WriteLine("That is not a valid option please choose again");
                            BL();
                            break;
                        }
                }
            } while (exit == false );

        }
    }

    class pathManager
    {
        List<path> pathHolding = new List<path>();
        public pathManager()
        {

        }


        public void newPath(string pathName)
        {
            pathHolding.Add(new path(pathName));

            Console.WriteLine(string.Format("The Path '{0}' has been added", pathName));
            Program.BL();

        }

        public void getPathnames()
        {
            for (int i = 0; i < pathHolding.Count; i++)
            {
                Console.WriteLine(string.Format("{0}: {1}", i, pathHolding[i].getPathName()));
            }
        }

        public void getPathNamesAndTimers()
        {
            for (int i = 0; i < pathHolding.Count; i++)
            {
                Console.WriteLine(string.Format("{0}: {1}", i, pathHolding[i].getPathName()));
                pathHolding[i].getTimerNames();

            }
        }

        public void addATimer(string timerName, int pathToAddTo, int Hours, int Minutes, int seconds)//will have a print out of paths and a indexing next to them so pathToAdd to is int
        {
            int totalTime = 0;
            Minutes += (Hours * 60);
            totalTime += (Minutes * 60);
            totalTime += seconds;
            if (pathToAddTo <= pathHolding.Count)
            {
                pathHolding[pathToAddTo].addTimer(timerName, totalTime);

                Console.WriteLine("the requested timer was added");
            }
            else
            {
                Console.WriteLine("The timer could not be added due to the path requested not existing");
            }
        }

        public void startATimer(string timerNameX, int pathIndexX)//same as pathToAddTo user will select using a index off of a print out to screen not path name
        {

            pathHolding[pathIndexX].runTimer(timerNameX, pathIndexX);
        }

        public void timerDone(int pathIndex, string nameX)
        {
            pathHolding[pathIndex].timerFinished(nameX);
        }

    }
    class path
    {
        string name;
        Dictionary<String, int> timers;

        public string getPathName()
        {
            return name;
        }

        public void getTimerNames()
        {
            foreach (var pair in timers)
            {
                Console.WriteLine(string.Format("   1: {0}", pair.Key));
            }
        }

        public path(String nameX)
        {
            timers = new Dictionary<String, int>();
            name = nameX;
        }

        public void addTimer(string name, int secondsToRun)
        {
            timers.Add(name, secondsToRun);
        }

        public void runTimer(string timerToRun, int pathIndex)
        {
            int timerTime = 0;

            if (timers.TryGetValue(timerToRun, out timerTime))
            {
                Timer time = new Timer(timerTime, pathIndex, timerToRun);
                Console.WriteLine("the timer has been started");
            }

            else
            {
                Console.WriteLine("The timer you entered does not exist in this path");
            }

        }

        public void timerFinished(string name)
        {
            Console.WriteLine(string.Format("Timer '{0}' has finished running", name));

            int itemsCount = timers.Count;//This is code taken off of stackoverflow
            bool isLast = false;
            List<string> keyList = new List<string>(this.timers.Keys);
            if (keyList[keyList.Count - 1].Equals(name))
            {
                isLast = true;
            }

            if (isLast)//Last on in dictonary
            {
                timers.Remove(name);
                Console.WriteLine("This is the last timer you have in the path.");// I would like this to delete the path at some point ATM it is more truble then it is worth
                Program.waitForTimer = false;
            }

            else//not last
            {
                timers.Remove(name);
                Console.WriteLine("There are more timers in this path, these are what they are");
                foreach (KeyValuePair<string, int> pair in timers)
                {
                    Console.WriteLine(string.Format("Name: {0}", pair.Key));
                    Console.WriteLine(string.Format("Time: {0}", pair.Value));
                }

                Console.WriteLine("Would you like the start another timer?");
                Console.WriteLine("0: No");
                Console.WriteLine("1: Yes");
                int answer = 0;
                answer = Convert.ToInt32(Console.ReadLine());
                if (answer == 0)
                {
                    Console.WriteLine("Not staring another timer send you back to the menu");
                    Program.waitForTimer = false;
                }

                else if (answer == 1)
                { 
                        Console.WriteLine("Which timer would you like to start?");
                        Console.WriteLine("You may type 'next' to start the next timer");
                        foreach (KeyValuePair<string, int> pair in timers)
                        {
                            Console.WriteLine(string.Format("Name: {0}      Time: {1}", pair.Key, pair.Value));
                        }
                        string nextTimer;
                        nextTimer = Console.ReadLine();
                        int timerTime; // used to hold the output value from testing for a key

                    if (timers.TryGetValue(nextTimer, out timerTime))
                    {
                        runTimer(nextTimer, timerTime);
                        Console.WriteLine("Starting that timer");
                        Program.waitForTimer = false;
                    }
                    else
                    {
                        Console.WriteLine("That timer does not exist");
                        Program.waitForTimer = false;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid answer returning to the menu");
                    Program.waitForTimer = false;
                }
            }
        }


    }

    class Timer
    {
        int totalSeconds;
        int pathIndex;
        string timerName;
        public Timer(int timeToRun, int PathIndexX, string Name)
        {
            totalSeconds = timeToRun;
            pathIndex = PathIndexX;
            timerName = Name;
            TimeSpan runTimeX = TimeSpan.FromSeconds(totalSeconds);//Turns a Int total seconds into a TimeSpan
            Thread burner = new Thread(() => timerCallBack(runTimeX));//Lambda thread?? Don't ask me I asked the internet and it works okay, leave off
            burner.Start();//Intalization
        }

        public void timerCallBack(TimeSpan runTime)
        {
            DateTime start = DateTime.Now;//Used to find delta
            TimeSpan timeRemaining = TimeSpan.FromSeconds(0);//used for while loop testing

            do
            {
                TimeSpan Delta = DateTime.Now - start;//Used to find out how much time is left on the timer
                timeRemaining = runTime - Delta;//Time remaining
            } while (timeRemaining.TotalSeconds > 0);

            pathManager Temp = Program.pathStorage;
            Temp.timerDone(pathIndex, timerName); //Finished timer

        }

    }
}