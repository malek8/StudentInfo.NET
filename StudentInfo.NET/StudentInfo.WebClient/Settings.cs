using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace StudentInfo.WebClient
{
    public class Settings
    {
        public static string MailerKey
        {
            get { return ReadKeyValue("MailerKey"); }
        }

        public static string ConfirmationEmailFrom
        {
            get { return ReadKeyValue("ConfirmationEmailFrom"); }
        }

        public static string SystemName
        {
            get { return ReadKeyValue("SystemName"); }
        }

        private static string ReadKeyValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}