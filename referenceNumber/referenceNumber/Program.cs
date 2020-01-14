using System;
using System.IO;

namespace referenceNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            string choice = null;
            string path = @"C:\TEMP\referencenumber.txt";
            string message = "";

            do
            {
                choice = UserInterface();
                switch (choice.ToUpper())
                {
                    case "1":
                        if (CheckRefNumber())
                        {
                            message = "\nSyöttämäsi viitenumero on oikein.";
                        }
                        else
                        {
                            message = "\nSyöttämäsi viitenumero on väärin.";
                        }
                        break;

                    case "2":
                        int length = LengthSpec();
                        CreateRefNum(length);
                        message = "\nOhjelma on päättynyt.";
                        break;

                    case "3":
                        string refNumbers = "";
                        Console.WriteLine("Montako viitenumeroa tarvitset?");
                        string refCount = Console.ReadLine();

                        if (refCount[0] == '0' || refCount[0] == '-')
                        {
                            Console.WriteLine("Syöttämäsi määrä ei ole mahdollinen. Ohjelman on keskeytetty.");
                            break;
                        }

                        else if (int.TryParse(refCount, out int howManyInNumerics))
                        {
                            int refLengt = LengthSpec();
                            for (int i = 0; i < howManyInNumerics; i++)
                            {
                                refNumbers += $"{i + 1}.\t {CreateRefNum(refLengt)}\r\n";
                            }

                            WriteToFile(path, refNumbers);
                            message = "\nTarvitsemasi määrä viitenumeroita on luotu löydät ne tallennettuna tiedostoon kansiossa C:/TEMP/referenceNumber.";
                        }


                        else
                        {
                            Console.WriteLine("Syötteesi ei ollut luku. Ohjelma on keskeytetty.");
                        }

                        break;

                    case "X":
                        message = "\nOhjelma on päättynyt. Paina enter.";
                        break;

                    default:
                        message = "Väärä syöte. Valitse 1, 2, 3 tai X poistuaksesi ohjelmasta";
                        break;
                }

                Console.WriteLine(message);
                Console.ReadLine();
                Console.Clear();
            }

            while (choice.ToUpper() != "X");

        }

        static string UserInterface()
        {
            Console.WriteLine("Tämä on viitenumero-ohjelma Syötä haluamasi vaihtoehto.");
            Console.WriteLine("( 1 ) Tarkasta viitenumero");
            Console.WriteLine("( 2 ) Luo viitenumero");
            Console.WriteLine("( 3 ) Luo viitenumeroja ja tallenna ne tiedostoon");
            Console.WriteLine("( X ) Lopeta ohjelma");
            Console.WriteLine();
            Console.Write("Valintasi? ");

            return Console.ReadLine();
        }

        static bool CheckRefNumber()
        {
            Console.WriteLine("Syötä viitenumero.");
            string refNumber = Console.ReadLine();

            if (refNumber[0] == '0')
            {
                Console.WriteLine("Viitenumeron ensimmäinen luku ei voi olla nolla.");
                return false;
            }


            if (int.TryParse(refNumber, out int numerics))
            {
                string multiplier = "731";
                int jumpUp = 0;
                int checkNumber;
                int nextTen;

                for (int i = 0; i < refNumber.Length - 1; i++)
                {
                    jumpUp += int.Parse(refNumber[refNumber.Length - 2 - i].ToString()) * int.Parse(multiplier[i % 3].ToString());
                    ;
                }

                nextTen = (int)Math.Ceiling((decimal)jumpUp / 10) * 10;
                checkNumber = nextTen - jumpUp;

                if (checkNumber == 10)
                {
                    checkNumber = 0;
                }

                if (refNumber[refNumber.Length - 1].ToString() == checkNumber.ToString())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                Console.Write($"Syötteesi oli: {refNumber}, on väärin kirjoitettu .Suorita ohjelma uudelleen.");
                return false;
            }
        }

        static int LengthSpec()
        {
            while (true)
            {
                Console.WriteLine("Valitse viitenumeron pituus väliltä 3-19.");
                string refNumLength = Console.ReadLine();

                if (int.TryParse(refNumLength, out int numerics))
                {
                    if (numerics >= 3 && numerics <= 19)
                    {
                        return numerics;
                    }
                    else
                    {
                        Console.WriteLine("Pituus on väärä Syötä väliltä 3-19.");
                    }
                }
                else
                {
                    Console.WriteLine($"Syötteesi: {refNumLength}, ei ole sallittu.");
                }
            }
        }

        static string CreateRefNum(int length)
        {
            var numbers = "0123456789";
            string multiplier = "731";
            int jumpUp = 0;
            int nextTen;
            int checkNumber;

            var refNumbers = new char[length];
            Random aNumber = new Random();
            for (int i = 0; i < length; i++)
            {
                refNumbers[i] = numbers[aNumber.Next(10)];
                if (refNumbers[0] == '0')
                {
                    i--;
                }
            }

            var finalRefNumbers = new String(refNumbers);

            for (int i = 0; i < length; i++)
            {
                jumpUp += int.Parse(finalRefNumbers[length - 1 - i].ToString()) * int.Parse(multiplier[i % 3].ToString());
                ;
            }

            nextTen = (int)Math.Ceiling((decimal)jumpUp / 10) * 10;
            checkNumber = nextTen - jumpUp;

            if (checkNumber == 10)
            {
                checkNumber = 0;
            }

            string refNumWithCheck = finalRefNumbers + checkNumber;

            for (int i = 0; i < refNumWithCheck.Length; i += 5)
            {
                refNumWithCheck = refNumWithCheck.Insert(refNumWithCheck.Length - i, " ");
                i++;
            }

            Console.WriteLine($"Uusi viitenumero: {refNumWithCheck}");
            return refNumWithCheck;
        }

        static void WriteToFile(string filePath, string saveNumbers)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine("Kotimaiset viitenumerot:");
                sw.WriteLine($"{saveNumbers}");
            }
        }
    }
}