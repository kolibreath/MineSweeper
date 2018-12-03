using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace MineSweeper.Utils
{
    static class UserAccountHelper
    {
        private const string USER_ACCOUNT_LIST_FILE_NAME =
            "accountlist.txt";
        private  static  string _accountListPath =
            //commbine two paths together
            Path.Combine(ApplicationData.Current.LocalFolder.Path, USER_ACCOUNT_LIST_FILE_NAME);

        public static List<UserLogin> UserAccounts = new List<UserLogin>();
        /// <summary>
        /// return the xml formatted string representing the list
        /// 
        /// make a type cast when cast into 
        /// </summary>
        /// <returns></returns>
        public static string SerializeAccountToXml()
        {
            XmlSerializer xmlizer = new XmlSerializer(typeof(List<UserLogin>));
            StringWriter writer = new StringWriter();
            xmlizer.Serialize(writer, UserAccounts);

            return writer.ToString();
        }

        /// <summary>
        /// 将已经序列化完成xml格式的东西写入当前目录下面的account中
        /// </summary>
        public static async void SaveAccountListAsync()
        {
            string accountXml = SerializeAccountToXml();
            Debug.WriteLine(_accountListPath);
            if (File.Exists(_accountListPath))
            {
                StorageFile accountFile = await StorageFile.GetFileFromPathAsync(_accountListPath);
                await FileIO.WriteTextAsync(accountFile, accountXml);
            }
            else
            {
                StorageFile accountFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(USER_ACCOUNT_LIST_FILE_NAME);
                await FileIO.WriteTextAsync(accountFile, accountXml);
            }
        }

        /// <summary>
        /// 反序列化xml格式的文档
        /// </summary>
        /// <param name="listAsXml"></param>
        /// <returns></returns>
        public static List<UserLogin> DeserializeXmlToAccountList(string listAsXml)
        {
            XmlSerializer xmlizer = new XmlSerializer(typeof(List<UserLogin>));
            TextReader reader = new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(listAsXml)));

            return (xmlizer.Deserialize(reader)) as List<UserLogin>;
        }

        /// <summary>
        /// 异步加载xml formatted 信息
        /// </summary>
        /// <returns></returns>
        public static async Task<List<UserLogin>> LoadAccountListAsync()
        {
            if (File.Exists(_accountListPath))
            {
                StorageFile accountFile = await StorageFile.GetFileFromPathAsync(_accountListPath);

                string accountsXml = await FileIO.ReadTextAsync(accountFile);
                UserAccounts = 
                    DeserializeXmlToAccountList(accountsXml);
            }

            return UserAccounts;
        }

        /// <summary>
        /// 保存用户 
        /// </summary>
        /// <param name="user"></param>
        public static void AddAccount(UserLogin user)
        {
            UserAccounts.Add(user);
            SaveAccountListAsync();

        }

        public static void RemoveAccount(UserLogin user)
        {
            UserAccounts.Remove(user);
            SaveAccountListAsync();
        }


        public static bool CheckCredential(UserLogin user)
        {
            if (user.Email == null)
            {
                return false;
            }
            else if (user.Email.Contains("@"))
            {
                return true;
            }
            
              return true;
        }
    }
}
