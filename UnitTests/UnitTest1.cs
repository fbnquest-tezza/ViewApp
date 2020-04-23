using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGenerateSessionID()
        {
            var session = GenerateSessionID();
            if (session != null)
            {
                Assert.IsTrue(true, session);
                Console.WriteLine("Test Session ID generated successfully");
            }
            else
            {
                Assert.Fail();
                Console.WriteLine("Test Session ID generation failed");
            }
        }
        public static string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
        public static string GenerateSessionID()
        {
            string SessionID = "000000000000";
            DateTime thisDay = DateTime.Now;
            string dateString = thisDay.ToString("yyMMddHHmmss");
            string serial = Convert.ToString(RandomDigits(12));

            SessionID = dateString + serial;

            return SessionID;

        }
    }
}
