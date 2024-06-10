using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2City.Core.Convertors
{
    public class TimeUtility
    {
        /// <summary>
        /// Ex: 10 روز پیش
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetTimeName(DateTime dt)
        {
            TimeSpan t = (DateTime.Now - dt);
            
            if(t.Days>=365)
            {
                return ((int)(t.Days / 365))+" سال پیش ";
            }

            if (t.Days >= 30)
            {
                return ((int)(t.Days / 30))+" ماه پیش ";
            }

            if (t.Days < 30 & t.Days>0)
            {
                return ((int)(t.Days)) + " روز پیش ";
            }

            if (t.Hours>0)
            {
                return ((int)(t.Hours)) + " ساعت پیش ";
            }

            if (t.Minutes > 0)
            {
                return ((int)(t.Minutes)) + " دقیقه پیش ";
            }


            return t.Seconds + " ثانیه پیش ";

        }

        public string GetDateName(DateTime dt)
        {
            int Year = dt.Year;
            int Month = dt.Month;
            int Day = dt.Day;

            string[] MonthName = {"فروردین","اردیبهشت","خرداد","تیر","مرداد","شهریور","مهر","آبان","آذر","دی","بهمن","اسفند" };

            return Day + " " + MonthName[Month - 1] + " " + Year;

        }
    }
}
