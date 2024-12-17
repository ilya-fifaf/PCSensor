using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Win32; 

namespace pc_Checker;

public class interfacer
{
    static public void Main()
    {
        Console.WriteLine(
            "\n \nd888888b d8888b. d8888b. d8888b.  .d88b.  d8888b. db   dD  .d8b.  \n`88' VP  `8D 88  `8D 88  `8D .8P  88. 88  `8D 88 ,8P' d8' `8b \n   88      oooY' 88oobY' 88oobY' 88  d'88 88oobY' 88,8P   88ooo88 \n   88      ~~~b. 88`8b   88`8b   88 d' 88 88`8b   88`8b   88~~~88 \n   88    db   8D 88 `88. 88 `88. `88  d8' 88 `88. 88 `88. 88   88 \n   YP    Y8888P' 88   YD 88   YD  `Y88P'  88   YD YP   YD YP   YP \n                                                                  \n   ");
        Console.WriteLine(
            "Приветствую! Выберите 1 из следующих пунктов. \n1. Анализ системы. \n2. Вывод текущего железа компьютера. \n3. Анализ дисков. \n4. Перезагрузка пк. \n5. Выключение пк. \n6. Soon... \n7. Выход из тула. \nV1 - PCSensor");

        while (true)
        {
            Console.WriteLine("\nОжидается ввод.");

            int a;
            {
                a = Convert.ToInt32(Console.ReadLine());
            }

            switch (a)
            {

                case 1:
                {
                    Console.WriteLine("Вызвана функция анализа системы.");
                    Thread.Sleep(2500);

                    ManagementObjectSearcher TeckUser = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher PCSystem = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
                    
                    foreach (ManagementObject obj in TeckUser.Get())
                    {
                        Console.WriteLine("Зарегистрированный пк : " + obj["Name"]);
                    }

                    foreach (ManagementObject obj in PCSystem.Get())
                    {
                        Console.WriteLine("ОС : " + obj["Caption"]);
                        Console.WriteLine("Серийный ключ : " + obj["SerialNumber"]);
                        Console.WriteLine("Имя текущего пользователя : " + obj["RegisteredUser"]);
                        Console.WriteLine("Мануфактура : " + obj["Manufacturer"]);
                        
                    }
                }
                    break;

                case 2:
                {
                    Console.WriteLine("Вызвана функция Технических характеристик. \n");
                    Thread.Sleep(2500);

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
                    ManagementObjectSearcher finder =
                        new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_UserAccount");
                    ManagementObjectSearcher searcher1 =
                        new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
                    ManagementObjectSearcher Operativka = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");

                    foreach (ManagementObject obj in finder.Get())
                    {
                        Console.WriteLine("Юзеры:  " + obj["Name"]);
                    }

                    foreach (ManagementObject obj in searcher1.Get())
                    {
                        Console.WriteLine("\n");
                        Console.WriteLine("Видеокарта: " + obj["Name"]);
                        Console.WriteLine("\n");
                    }

                    foreach (ManagementObject obj in searcher.Get())
                    {
                        Console.WriteLine("Процессор: " + obj["Name"]);
                        Console.WriteLine("Количество ядер: " + obj["NumberOfCores"]);
                        Console.WriteLine("Частота: " + obj["MaxClockSpeed"] + " МГц \n");
                    }
                    
                    foreach (ManagementObject obj in Operativka.Get())
                    {
                        object operativkaObj = obj["Capacity"];
                        long capacity = Convert.ToInt64(operativkaObj);
                        Console.WriteLine("Плашка ОЗУ : " + capacity / 1073741824);
                    }
                    
                    break;


                }

                case 3:
                    {
                        Console.WriteLine("Вызвана функция вывода информации о локальных дисках. \n");
                        Thread.Sleep(2500);
                        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
                        
                        foreach (ManagementObject obj in searcher.Get())
                        {
                            object stackMemory = Convert.ToInt64(obj["Size"]);
                            long StackMemory1 = Convert.ToInt64(stackMemory);
                            
                            Console.WriteLine("Жесткий диск :" + obj["Caption"]);
                            Console.WriteLine("Объём(gb) :" + StackMemory1 / 1073741824);
                        }
                        
                    }
                    break;

                    case 4:
                    {
                        Console.WriteLine("Перезагрузка произойдёт через 5 секунд.");
                        Thread.Sleep(5000);
                        Process.Start("ShutDown", "/r");
                    }
                        break;

                    case 5:
                    {
                        Console.WriteLine("Выключение пк произойдет через 5 секунд. ");
                        Thread.Sleep(5000);
                        Process.Start("ShutDown", "/s");
                    }
                        break;

                    default:
                    {
                        Console.WriteLine("stop");
                    }
                        break;
                        
            }
            if (Console.ReadLine() == "stop")
            {
                return;
            }    

            Console.ReadKey();
        }
    }
}
