using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;
using teambot.Models;

namespace teambot.Controllers
{
    public class xmlController : ApiController
    {
        public string GetPhoneNumber(string eneity)
        {
            bool isTrue = false;
            string returnString = "";
            XmlDocument xml = new XmlDocument();
            //获取或设置包含该应用程序的目录的名称(获取项目路径)。
            string projectURL = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string url = projectURL + @"\XML\memberInfo.xml";
            xml.Load(url);
            //获取根节点  
            XmlElement rootElement = xml.DocumentElement;
            foreach (XmlElement childElement in rootElement)
            {
                string attri = childElement.GetAttribute("username");
                if (attri.Contains(eneity.Trim()))
                {
                    XmlNodeList grandsonNodes = childElement.ChildNodes;
                    foreach (XmlNode grandsonNode in grandsonNodes)
                    {
                        if (grandsonNode.Name == "name")
                        {
                            eneity = grandsonNode.InnerText;
                        }
                        if (grandsonNode.Name == "phoneNumber")
                        {
                            returnString = eneity + "的电话是: " + grandsonNode.InnerText; ;
                        }
                    }
                    break;
                }
                else continue;

            }
            if (returnString == "" || returnString == null)
            {
                //没有此数据
                if(!isTrue)
                {
                    string[] defaultReturnArray = { "请检查一下被查询人的姓名", "请检查姓名是否正确", "您所查询的可能不是实验室成员", "查无此人", "实验室没有该成员" };
                    Random random = new Random();
                    int index = random.Next(0, defaultReturnArray.Length);
                    returnString = defaultReturnArray[index];
                }
                else
                    returnString = "信息可能尚未收录";
            }
               
            return returnString;
        }
        public string getEmail(string eneity)
        {
            bool isTrue = false;
            string returnString = "";
            XmlDocument xml = new XmlDocument();
            //获取或设置包含该应用程序的目录的名称(获取项目路径)。
            string projectURL = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string url = projectURL + @"\XML\memberInfo.xml";
            xml.Load(url);

            //获取根节点  
            XmlElement rootElement = xml.DocumentElement;
            //遍历孩子节点
            foreach (XmlElement childElement in rootElement)
            {
                string attri=childElement.GetAttribute("username");
                if (attri.Contains(eneity.Trim()))
                {
                    XmlNodeList grandsonNodes = childElement.ChildNodes;
                    foreach (XmlNode grandsonNode in grandsonNodes)
                    {
                        if (grandsonNode.Name == "name")
                        {
                            eneity = grandsonNode.InnerText;
                        }
                        if (grandsonNode.Name == "email")
                        {
                            returnString = eneity + "的邮箱是: " + grandsonNode.InnerText; ;
                        }
                    }
                    break;
                }
                else continue;

            }
            if (returnString == "" || returnString == null)
            {
                //没有此数据
                if (!isTrue)
                {
                    string[] defaultReturnArray = { "请检查一下被查询人的姓名", "请检查姓名是否正确", "您所查询的可能不是实验室成员", "查无此人", "实验室没有该成员" };
                    Random random = new Random();
                    int index = random.Next(0, defaultReturnArray.Length);
                    returnString = defaultReturnArray[index];
                }
                else
                    returnString = "信息可能尚未收录";
            }
            return returnString;
        }
        public string getAllInfo(string eneity)
        {
            bool isTrue = false;
            string returnString = "";
            XmlDocument xml = new XmlDocument();
            //获取或设置包含该应用程序的目录的名称(获取项目路径)。
            string projectURL = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string url = projectURL + @"\XML\memberInfo.xml";
            xml.Load(url);

            //获取根节点  
            XmlElement rootElement = xml.DocumentElement;
            foreach (XmlElement childElement in rootElement)
            {
                string attri = childElement.GetAttribute("username");
                if (attri.Contains(eneity.Trim()))
                {
                    XmlNodeList grandsonNodes = childElement.ChildNodes;
                    foreach (XmlNode grandsonNode in grandsonNodes)
                    {
                        if (grandsonNode.Name == "name")
                        {
                            eneity = grandsonNode.InnerText;
                        }
                        if (grandsonNode.Name == "phoneNumber")
                        {
                            returnString += "电话是: " + grandsonNode.InnerText+" 、"; 
                        }
                        if (grandsonNode.Name == "email")
                        {
                            returnString +="邮箱地址是: " + grandsonNode.InnerText + " 、";
                        }
                    }
                    returnString = eneity + "的" + returnString.TrimEnd('、');
                    break;
                }
                else continue;

            }
            if (returnString == "" || returnString == null)
            {
                //没有此数据
                if (!isTrue)
                {
                    string[] defaultReturnArray = { "实验室没有该成员","请检查一下被查询人的姓名", "请检查姓名是否正确", "您所查询的可能不是实验室成员", "查无此人" };
                    Random random = new Random();
                    int index = random.Next(0, defaultReturnArray.Length);
                    returnString = defaultReturnArray[index];
                }
                else
                    returnString = "信息可能尚未收录";
            }
            return returnString;
        }
    }
}

