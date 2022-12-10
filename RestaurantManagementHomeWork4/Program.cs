using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RestaurantManagementHomeWork4
{
    class Program
    {
        public static List<reservation> reservations = new List<reservation>();
        static void Main(string[] args)
        {
            // welcome title including today's date
            DateTime today = DateTime.Today;
            string s = "Restaurant Management System";
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s + " Today's date " + today.ToString("dd/MM/yyyy") + "\n \n");

            while (true)
            {
                // List of services, to get user input
                string s1 = "- Press 'L' To List daily menu \n";
                string s2 = "- Press 'C' To Check aviliable Tables \n";
                string s3 = "- Press 'T' To List Reserved Tables' Details \n";
                string s4 = "- Press 'R' For New Reservation \n";
                Console.WriteLine(" Hi, how can I help you? \n" + s1 + s2 + s3 + s4 + "*****Then press Enter***** \n");

                string readInput = Console.ReadLine().ToLower();
                if (readInput == "r") //List menu in case input is "R"
                    reserve();

                else if (readInput == "l") //List menu in case input is "L" 
                {
                    // List daily menu Task: Read menu from .txt file called "menu.txt" to list daily menu
                    StreamReader sr = new StreamReader("../../menu.txt");
                    string data = sr.ReadLine();
                    while (data != null)
                    {
                        Console.WriteLine(data);
                        data = sr.ReadLine();
                    }
                    Console.WriteLine("\n");
                }
                else if (readInput == "c") // check table is avaliable in a certain time
                {
                    Console.WriteLine("Please Enter Expected time of Arrival \nEnter The time in Format hh:mm 24-h format");
                    TimeSpan timestart = new TimeSpan();
                    TimeSpan.TryParse(Console.ReadLine(), out timestart);
                    checkAvailability(timestart);
                }
                else if (readInput == "t") // list reserved tables
                {
                    if ((reservations != null) && (!reservations.Any())) // if there is no reservation yet!
                    {
                        Console.WriteLine("There is no reservation saved for today");
                    }
                    else
                    { 
                        foreach (var item in reservations)
                        {
                            string s9 = "List of Reserved Tables";
                            Console.SetCursorPosition((Console.WindowWidth - s9.Length) / 2, Console.CursorTop);
                            Console.WriteLine(s9);
                            string s10 = $" \nTable: {item.Table.tableNumber} \n";
                            string s11 = $"Guest Name:{item.person.personName} \n";
                            string s12 = $"Phone No: { item.person.phoneNumber} \n";
                            string s13 = $"Total Number of Companion {item.person.noOfCompanions} \n";
                            string s14 = $"Check-in Schedule: {item.eventStartTime} \n";
                            string s15 = $"Leave Schedule: {item.eventEndTime} \n \n";
                            Console.Write(s10 + s11 + s12 + s13 + s14 + s15);
                        }
                    }

                }
            }
            

        }
        public static void reserve()
        {
            // get reservation data from the guest
            Console.WriteLine("Enter Table Number (T1, T2, T3, T4, T5, T6, T7)");
            string tableNo = Console.ReadLine();
            Console.WriteLine("Enter Guest Name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Guest Telephone Number");
            string telephone = Console.ReadLine();
            Console.WriteLine("Enter Number of Chairs Per Table");
            int No = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Start Event Time (hh:mm 24-h format)");
            TimeSpan startTime = TimeSpan.Parse(Console.ReadLine());
            Console.WriteLine("Enter End Event Time (hh:mm 24-h format)");
            TimeSpan endTime = TimeSpan.Parse(Console.ReadLine());
            RestaurantManagementHomeWork4.availabletables MyStatus = (RestaurantManagementHomeWork4.availabletables)Enum.Parse(typeof(RestaurantManagementHomeWork4.availabletables), tableNo);
            table tab = new table() { tableNumber = MyStatus };
            person per = new person() { personName = name, phoneNumber = telephone, noOfCompanions = No };
            // add reservation
            reservations.Add(new reservation()
            {
    
                eventStartTime = startTime,
                eventEndTime = endTime,
                Table = tab,
                person = per
            });
            Console.WriteLine("\n ****** Table is Booked Successfuly ****** \n");
        }
        public static void checkAvailability(TimeSpan arrival)
        {
            List<availabletables> reservedList = new List<availabletables>();
            foreach (var arrItem in reservations)
            {
                reservations.OrderBy(n => n.Table.tableNumber).ThenBy(n => n.eventStartTime).GroupBy(n => n.Table.tableNumber).ToList();
                if (!(arrival < arrItem.eventStartTime || arrival >= arrItem.eventEndTime))
                {
                    reservedList.Add(arrItem.Table.tableNumber);
                }
            }
            foreach (availabletables val in Enum.GetValues(typeof(availabletables)))
            {
                if (!(reservedList.Contains(val)))
                    Console.WriteLine(val);
            }
        }
    }
}
