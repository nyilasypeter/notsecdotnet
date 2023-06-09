﻿using NotSecDotNet.Data;
using NotSecDotNet.Dto;
using NotSecDotNet.Model;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace NotSecDotNet.Services
{
    public class RemotePasswrodChangeService
    {
        MovieDbContext movieDbContext;

        public RemotePasswrodChangeService(MovieDbContext movieDbContext)
        {
            this.movieDbContext = movieDbContext;
        }


        public String ChangePassword(String pwdChangeXML)
        {
            try
            {  
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(pwdChangeXML);
                String name = doc.SelectSingleNode("user/userName").InnerText;
                String newPwd = doc.SelectSingleNode("user/pwd").InnerText;
                User usr = movieDbContext.Users.Single<User>(u => u.Name == name);
                usr.Password = newPwd;
                movieDbContext.SaveChanges();
                return "Passwrod changed";
            }
            catch (Exception e)
            {
                return "No such user";
            }

        }

        public static String ReadFileContent(String fileName)
        {
            return File.ReadAllText(@"C:\Users\john\tmp" + fileName);
        }

    }
}
