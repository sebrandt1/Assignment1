using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

/* ##Instruktioner##
 * 1. Regeln som ska användas för bedömningen är följande: om texten innehåller åtminstone en "svensk bokstav" (Å, Ä eller Ö) så är den på svenska, annars inte.
 * 2. Både små och stora bokstäver ska ingå i bedömningen.
 * 3. När bedömningen har gjorts ska programmet skriva ut resultatet: huruvida texten är på svenska eller inte.
 * 4. När bedömningen har gjorts ska även antalet svenska bokstäver i texten skrivas ut.
 * 
 * ##Pseudo##
 * 1.1. Skapa en integer swedishCount som plusas för varje svensk bokstav
 * 1.2. Skapa en bool isSwedish som är true om swedishCount >= 1
 * 2. Gör bara en if(x == 'x' || x = 'X')) xCount++; eftersom man INTE får använda sig av stringmetoder (exempelvis x.ToLower().Contains('x'))
 * 3. Ternary: string language = isSwedish ? "Texten är på svenska" : "Texten är INTE på svenska";
 * 3.2. Om man INTE får använda sig av ternary gör bara en enkel if(isSwedish) language = "Texten är på svenska"; else.....
 * 4. Kan använda dictionary<char, int> för att räkna ihop hur många gånger en bokstav visas i texten
 * 4.1. Kan också använda LINQ; int åCount = input.Count(x => x == 'å' || x == 'Å');
 * 4.2 Eftersom man inte får använda sig av strängmetoder så får vi helt enkelt räkna det med en variabel för varje svensk bokstav
 *      och en for loop + if-sats i for loopen
 *      
 *      Utökning 3: Statistik
 */
namespace Assignment1
{
    class Program
    {
        public static void Main()
        {
            int åCount = 0,
                äCount = 0,
                öCount = 0; //Icke engelska bokstäver är egentligen "bad practice" men det blir lättare så här
            int TotalCount() => //Deklarerar man totalCount = å + ä + ö så blir den 0 (då alla är deklarerade som 0)
                åCount + äCount + öCount; //På det här viset returnerar den alltid nuvarande värdena på å ä ö när man callar totalCount()
                                          //Alternativt kan man göra totalCount++ i ifsatsen för varje bokstav som dyker upp
            bool isSwedish() => TotalCount() >= 1; //Om totalCount returnerar 1 eller mer så är språket svenska

            string Language() => isSwedish() ? "Texten verkar vara på svenska" : "Texten verkar inte vara på svenska";
            string input;

            Console.WriteLine("Skriv in din text: ");
            input = Console.ReadLine(); //Input

            /*For loop som jämför index i i input med å ä ö och plusar på respektive xCount 
             * Computation */
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == 'å' || input[i] == 'Å')
                {
                    åCount++;
                }
                else if (input[i] == 'ä' || input[i] == 'Ä')
                {
                    äCount++;
                }
                else if (input[i] == 'ö' || input[i] == 'Ö')
                {
                    öCount++;
                }
            }

            //Output
            Console.WriteLine("{0}Antal svenska bokstäver: {1}Antal Å: {2}Antal Ä: {3}Antal Ö: {4}", new object[]
            {
                    Language() + Environment.NewLine,
                    TotalCount() + Environment.NewLine,
                    åCount + Environment.NewLine,
                    äCount + Environment.NewLine,
                    öCount
            });
            //Console.ReadKey();
        }
    }

    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void TestOne()
        {
            FakeConsole console = new FakeConsole("Sju sköna sjuksköterskor skötte sju sjuka sjömän");
            Program.Main();
            CollectionAssert.AreEqual(new[]
            {
                "Texten verkar vara på svenska",
                "Antal svenska bokstäver: 5",
                "Antal Å: 0",
                "Antal Ä: 1",
                "Antal Ö: 4"
            }, console.Lines);
            console.Dispose();
        }

        [TestMethod]
        public void TestTwo()
        {
            FakeConsole console = new FakeConsole("This is not Swedish");
            Program.Main();
            CollectionAssert.AreEqual(new[]
            {
                "Texten verkar inte vara på svenska",
                "Antal svenska bokstäver: 0",
                "Antal Å: 0",
                "Antal Ä: 0",
                "Antal Ö: 0"
            }, console.Lines);
            console.Dispose();
        }

        [TestMethod]
        public void TestThree()
        {
            FakeConsole console = new FakeConsole("Hur många ån finns det i en å");
            Program.Main();
            CollectionAssert.AreEqual(new[]
            {
                "Texten verkar vara på svenska",
                "Antal svenska bokstäver: 3",
                "Antal Å: 3",
                "Antal Ä: 0",
                "Antal Ö: 0"
            }, console.Lines);
            console.Dispose();
        }
        
        [TestMethod]
        public void TestFour()
        {
            FakeConsole console = new FakeConsole("!?¤=%*%#)(?:)");
            Program.Main();
            CollectionAssert.AreEqual(new[]
            {
                "Texten verkar inte vara på svenska",
                "Antal svenska bokstäver: 0",
                "Antal Å: 0",
                "Antal Ä: 0",
                "Antal Ö: 0"
            }, console.Lines);
            console.Dispose();
        }

        [TestMethod]
        public void TestFive()
        {
            FakeConsole console = new FakeConsole("Sju skÖna sjuksköterskor skÖtte sju sjuka sjömän");
            Program.Main();
            CollectionAssert.AreEqual(new[]
            {
                "Texten verkar vara på svenska",
                "Antal svenska bokstäver: 5",
                "Antal Å: 0",
                "Antal Ä: 1",
                "Antal Ö: 4"
            }, console.Lines);
            console.Dispose();
        }
    }
}
