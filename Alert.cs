using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSS_Tester
{
    static class Alert
    {
        public static IAlert WaitGetAlert(this IWebDriver driver, int waitTimeInSeconds = 5)
        {
            IAlert alert = null;

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds));

            try
            {
                IAlert? alert1 = wait.Until(d =>
                                {
                                    try
                                    {
                                        return driver.SwitchTo().Alert();
                                    }
                                    catch (NoAlertPresentException)
                                    {
                                        return null;
                                    }
                                });
                alert = alert1;
            }
            catch (WebDriverTimeoutException) { alert = null; }

            return alert;
        }
    }
}
