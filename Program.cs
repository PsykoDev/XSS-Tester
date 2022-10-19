using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Net;

namespace XSS_Tester
{
    internal class Program
    {

        static string uriTest = "https://xss-quiz.int21h.jp/";

        static void Main(string[] args)
        {
            foreach( string arg in args)
            {
                uriTest = arg;
            }

            if(uriTest != string.Empty)
            {
                TestStart(uriTest);
            }
        }

        static async void TestStart(string uriTest)
        {
            string[] lines = File.ReadAllLines("wordlist/xss-payload-list.txt");

            foreach (string line in lines)
            {
                Console.WriteLine("Test: "+uriTest + line);
                await RequestTest(uriTest + line);
                
            }

        }

        static Task RequestTest(string v)
        {
            try
            {

                ChromeOptions options = new ChromeOptions();
                options.PageLoadStrategy = PageLoadStrategy.Normal;
                ChromeDriverService driverService = ChromeDriverService.CreateDefaultService();
                driverService.HideCommandPromptWindow = true;
                driverService.EnableVerboseLogging = false;
                driverService.SuppressInitialDiagnosticInformation = true;
                driverService.EnableAppendLog = false;

                ChromeDriver driver = new ChromeDriver(driverService, options);
                driver.Navigate().GoToUrl(v);

                var alert = Alert.WaitGetAlert(driver, 0);
                if (alert != null)
                {
                    Console.WriteLine("!!!!Found: " +v);
                    Console.WriteLine(alert.Text);
                }

                driver.Quit();

            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR:\n"+e);
            }

            return Task.CompletedTask;
        }

    }
}