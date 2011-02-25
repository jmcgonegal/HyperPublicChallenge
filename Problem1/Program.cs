using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HyperPublicProgrammingChallenge
{
/*
Hyperpublic Challenge - Problem 1

Hyperpublic users can add their friends by emailing a photo of them to add@hyperpublic.com. We want to determine a user’s influence on the system by determining how many users he is responsible for. A user’s influence is calculated by giving him 1 point for every user he’s added in addition to the sum of the influence scores of each user that he’s added.

Example: User 0 adds User 1 and User 2. User 1 adds User 3.

User 0’s influence score is 3. (He added two users and one of them added added a third user.)
User 1's is 1. 
User 2’s is 0. 
User 3’s is 0.
The above scenario is represented by the following input file. Line i is user ID i and position j within the line is marked with an X if user ID i added user ID j. Both row and column indicies are 0-based:

 OXXO
 OOOX
 OOOO
 OOOO
      
Use the input file here to determine what the highest influence score is among 100 random Hyperpublic users. To compute the answer to problem 1, append the top 3 influence totals together in descending order. (For example if they are 17, 5, and 3 then submit 1753)
*/
    class User
    {
        int influence = 0;
        public string line; // im lazy
        List<User> users = new List<User>(); // list of users that the users has added

        public User(string l)
        {
            line = l;
        }
        public void addUser(User u)
        {
            users.Add(u);
        }
        public int getInfluence()
        {
            return influence;
        }
        public static int calcInfulence(List<User> users)
        {
            int influence = 0;
            foreach (User u in users)
            {
                influence += calcInfulence(u.users);
            }
            return users.Count + influence;
        }
        public void calc()
        {
            influence = calcInfulence(this.users);
        }
        public static int CompareInfluence(User a, User b)
        {
            return b.getInfluence().CompareTo(a.getInfluence());
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo file = new FileInfo("challenge2input.txt");
            StreamReader fs = new StreamReader(file.OpenRead());

            string line;
            List<User> users = new List<User>();

            // read our file info and create 1 user for each line
            while ((line = fs.ReadLine()) != null)
            {
                users.Add(new User(line));
            }
            fs.Close();

            // read the line info and link users to each user they've added
            for (int i = 0; i < users.Count; i++)
            {
                char[] t = users[i].line.ToCharArray();
                for (int j = 0; j < t.Length; j++)
                {
                    if (t[j] == 'X')
                    {
                        users[i].addUser(users[j]);
                    }
                }
            }
            // calculate influence
            for (int i = 0; i < users.Count; i++)
            {
                users[i].calc();
            }
            // sort it
            users.Sort(User.CompareInfluence);

            
            // print the answer
            Console.Write("The answer is ");
            foreach (User u in users.GetRange(0,3))
            {
                Console.Write(u.getInfluence());
            }
            Console.ReadKey();
        }
    }
}
