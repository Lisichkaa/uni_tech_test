using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uni_tech_test
{
    public class UsersMenu
    {
        public int ShowMenu()
        {            
            Console.WriteLine("\n[1] Searching Removable Device\n" +
                                "[2] Exit\n");
            Console.Write("Enter your choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            return choice;
        }
    }
}
