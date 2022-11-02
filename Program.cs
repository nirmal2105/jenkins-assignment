using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Salary
{
    internal class Salary
    {
        private int calculateSalary(string[] arrivalTimes, string[] depTimes, int wage)
        {
            double totalWage = 0;
            for (int i = 0; i < arrivalTimes.Length; i++)
            {
                double wages = CalculateWage(arrivalTimes[i], depTimes[i], wage);
                totalWage += wages;
            }
            return (int)totalWage;
        }
        private double CalculateWage(string arrivalTime, string depTime, int wage)
        {
            double arrivalBasedOnHour = GetTimeBasedOnHour(arrivalTime);
            double depBasedOnHour = GetTimeBasedOnHour(depTime);
            double totalWage = 0;
            if (arrivalBasedOnHour >= 6 && arrivalBasedOnHour <= 18 && depBasedOnHour >= 6 && depBasedOnHour <= 18)
            {
                if (arrivalBasedOnHour < depBasedOnHour)
                {
                    totalWage = (depBasedOnHour - arrivalBasedOnHour) * wage;
                }
                else
                {
                    totalWage = (18 - arrivalBasedOnHour) * wage;
                    totalWage += 12 * 1.5 * wage;
                    totalWage += (depBasedOnHour - 6) * wage;
                }
            }
            else if ((arrivalBasedOnHour >= 18 || arrivalBasedOnHour <= 6) && (depBasedOnHour >= 18 || depBasedOnHour <= 6))
            {
                if (IsInBetween0To6(arrivalBasedOnHour))
                    arrivalBasedOnHour += 24;
                if (IsInBetween0To6(depBasedOnHour))
                    depBasedOnHour += 24;
                if (arrivalBasedOnHour < depBasedOnHour)
                {
                    totalWage = 1.5 * (depBasedOnHour - arrivalBasedOnHour) * wage;
                }
                else
                {
                    totalWage = (30 - arrivalBasedOnHour) * 1.5 * wage;
                    totalWage += 12 * wage;
                    totalWage += (depBasedOnHour - 18) * 1.5 * wage;
                }
            }
            else if ((arrivalBasedOnHour >= 18 || arrivalBasedOnHour <= 6) && depBasedOnHour >= 6 && depBasedOnHour <= 18)
            {
                if (IsInBetween0To6(arrivalBasedOnHour))
                    arrivalBasedOnHour += 24;
                totalWage = (30 - arrivalBasedOnHour) * 1.5 * wage;
                totalWage += (depBasedOnHour - 6) * wage;
            }
            else if (arrivalBasedOnHour <= 18 && arrivalBasedOnHour >= 6 && (depBasedOnHour >= 18 || depBasedOnHour <= 6))
            {
                if (IsInBetween0To6(depBasedOnHour))
                    depBasedOnHour += 24;
                totalWage = (depBasedOnHour - 18) * 1.5 * wage;
                totalWage += (18 - arrivalBasedOnHour) * wage;
            }
            return totalWage;
        }



        private bool IsInBetween0To6(double value)
        {
            return (value >= 0 && value < 6);
        }
        private double GetTimeBasedOnHour(string time)
        {
            string[] hourAndMinuteAndSec = time.Split(':');
            double hour = int.Parse(hourAndMinuteAndSec[0]);
            double minute = int.Parse(hourAndMinuteAndSec[1]);
            double second = int.Parse(hourAndMinuteAndSec[2]);
            return hour + minute / 60 + second / 3600;
        }
        static void Main(string[] args)
        {
            Salary salary = new Salary();
            String input = Console.ReadLine();
            do
            {
                string[] arrivalTimings = input.Split(',');
                string[] departureTimings = Console.ReadLine().Split(',');
                int wage = Int16.Parse(Console.ReadLine());



                Console.WriteLine(salary.calculateSalary(arrivalTimings, departureTimings, wage));
                input = Console.ReadLine();
            } while (input != "-1");
        }
    }
}
