using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem2
{
/*
Hyperpublic has an internal karma system to determine which users are the most involved in the ecosystem. Users earn points for the following tasks.

2 Points – Add Place
3 Points – Add Thing
17 Points – Tag Object
23 Points – Upload Photo
42 Points – Twitter Share
98 Points – Facebook Share
Being addicted to their own product, the Hyperpublic staff has racked up some big karma. The members of the team have the following karma totals:

Doug – 2349 points
Jordan – 2102 points
Eric – 2001 points
Jonathan – 1747 points
Amazingly, they've all accomplished these totals in the minimum number of tasks possible in order to reach each amount. For example, if their total was 6 points they would have accomplished this in just 2 tasks (2 "Add Thing" tasks), as opposed to accomplishing it by 3 "Add Place" tasks. Your job is to compute how many total tasks each user has completed. After you've done so, find the answer to Problem 2 using the following formula:

Problem 2 Answer = Doug's total tasks * Jordan's total tasks * Eric's total tasks * Jonathan's total tasks
*/
    class User
    {
        string name;
        int points;
        int tasks;
        public User(string n, int p)
        {
            name = n;
            points = p;
            tasks = calcTasks(points); // calcGreedyTasks(points);
        }
        private static int calcTask(int points, int value)
        {
            if (points < value) return 0;
            else if (points > value) return points / value;
            else return 1;
        }

        public int calcTasks(int points)
        {
            int max_f = calcTask(points, 2);
            int lowest = max_f;
            // I could assign weights and dynamic program but brute force seems faster
            for (int f = 0; f <= 5; f++) 
            {
                for (int e = 0; e <= 5; e++)
                {
                    for (int d = 0; d <= 5; d++)
                    {
                        for (int c = 0; c <= 5; c++)
                        {
                            for (int b = 0; b <= 5; b++)
                            {
                                for (int a = 0; a <= calcTask(points, 98); a++)
                                {
                                    if (points - (f * 2) - (e * 3) - (d * 17) - (c * 23) - (b * 42) - (a * 98) == 0)
                                    {
                                        int total = a + b + c + d + e + f;
                                        if (total < lowest) lowest = total;
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
            return lowest;
        }

        // Originally wrote this beast, it didn't yield the results I was looking for
        public int calcGreedyTasks(int points)
        {
            int facebook = calcTask(points, 98); // facebook share
            int twitter = calcTask(points - (facebook * 98), 42);
            int tag = calcTask(points - (facebook * 98) - (twitter * 42), 23);
            int photo = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23), 17);
            int thing = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17), 3);
            int place = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17) - (thing * 3), 2);
            int rem = points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17) - (thing * 3) - (place * 2);
            if (rem != 0)
            {
                if (thing > 0)
                {
                    thing--;
                    place = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17) - (thing * 3), 2);
                }
                else if (photo > 0)
                {
                    photo--;
                    thing = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17), 3);
                    place = calcTask(points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17) - (thing * 3), 2);
                }
                rem = points - (facebook * 98) - (twitter * 42) - (tag * 23) - (photo * 17) - (thing * 3) - (place * 2);
            }

            return facebook + twitter + tag + photo + thing + place;
        }
        public int getTasks()
        {
            return tasks;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            
            users.Add(new User("Doug", 2349)); // 28
            users.Add(new User("Jordan", 2102)); // 23 
            users.Add(new User("Eric", 2001)); // 25
            users.Add(new User("Johnathan", 1747)); // 22

            int answer = 1;
            foreach (User u in users)
            {
                Console.WriteLine(u.getTasks());
                answer *= u.getTasks();
            }
            Console.WriteLine("Answer = " + answer); // 354200 = 28 * 23 * 25 * 22
            Console.ReadKey();
        }
    }
}
