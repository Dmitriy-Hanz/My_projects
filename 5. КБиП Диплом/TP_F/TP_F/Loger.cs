using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TP_F
{
    public class Loger
    {
        private static StreamWriter sw;
        public static void AppendLogEvent(string activity, Facility target)
        {
            using (sw = new StreamWriter($"{Environment.CurrentDirectory}\\FacilityLog.txt", true,Encoding.GetEncoding(1251)))
            {
                if (target is Komp == true)
                {
                    sw.WriteLine(DateTime.Now + " | " + activity + $" [Тип: Компьютер; Идентификационный номер: {target.uIN}]");
                }
                if (target is Monitor == true)
                {
                    sw.WriteLine(DateTime.Now + " | " + activity + $" [Тип: Монитор; Идентификационный номер: {target.uIN}]");
                }
            }
        }
        public static void AppendLogEvent(string activity, Facility target,Kabinet k, WorkPlace wp)
        {
            using (sw = new StreamWriter($"{Environment.CurrentDirectory}\\FacilityLog.txt", true, Encoding.GetEncoding(1251)))
            {
                if (target is Komp == true)
                {
                    sw.WriteLine(DateTime.Now + " | " + activity + $" [Тип: Компьютер; Идентификационный номер: {target.uIN}; Название кабинета: {k.name}; Название рабочего места: {wp.name}");
                }
                if (target is Monitor == true)
                {
                    sw.WriteLine(DateTime.Now + " | " + activity + $" [Тип: Монитор; Идентификационный номер: {target.uIN}; Название кабинета: {k.name}; Название рабочего места: {wp.name}");
                }
            }
        }
    }
}
