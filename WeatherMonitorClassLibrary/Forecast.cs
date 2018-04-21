using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace WeatherMonitorClassLibrary
{
    public class Forecast
    {
        public List<string> GetForecast(string selectionId, int langSel)
        {
            string path = @"http://xmlweather.vedur.is/?op_w=xml&type=txt&lang=is&view=xml&ids=" + selectionId;

            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList description = doc.GetElementsByTagName("content"),
                        descriptionCreation = doc.GetElementsByTagName("creation"),
                        descriptionValidFrom = doc.GetElementsByTagName("valid_from"),
                        descriptionValidTo = doc.GetElementsByTagName("valid_to");

            string strDescription = null, strCreate, strValidFrom, strValidTo;

            try
            {
                strDescription = Convert.ToString(description[0].InnerXml);
                if (strDescription == string.Empty)
                {
                    if (langSel == 1)
                    {
                        strDescription = "Engar upplýsingar í augnablikinu";
                    }
                    if (langSel == 2)
                    {
                        strDescription = "No information at the moment";
                    }
                    else
                    {
                        strDescription = "Engar upplýsingar í augnablikinu";
                    }
                }
                else
                {
                    StringBuilder sb = new StringBuilder(strDescription);       //bæta við Veðurhorfur næstu daga inn í Íslenska fyrst þetta virkar!
                    sb.Replace("<br /><br />", "\n");
                    sb.Replace("<br />", "");
                    strDescription = sb.ToString();
                }

                strCreate = Convert.ToString(descriptionCreation[0].InnerXml);
                strValidFrom = Convert.ToString(descriptionValidFrom[0].InnerXml);
                strValidTo = Convert.ToString(descriptionValidTo[0].InnerXml);
            }
            catch (NullReferenceException)
            {
                if (langSel == 1)
                {
                    strDescription = "Engar upplýsingar í augnablikinu";
                }
                if (langSel == 2)
                {
                    strDescription = "No information at the moment";
                }
                else
                {
                    strDescription = "Engar upplýsingar í augnablikinu";
                }
                strCreate = DateTime.Now.ToString();
                strValidFrom = strCreate;
                strValidTo = strCreate;
            }

            List<string> returnList = new List<string>();
            returnList.Add(strDescription);
            returnList.Add(strCreate);
            returnList.Add(strValidFrom);
            returnList.Add(strValidTo);

            return returnList;
        }
    }
}
