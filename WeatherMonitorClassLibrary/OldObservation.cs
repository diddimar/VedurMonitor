using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using WeatherMonitorClassLibrary.Models;

namespace WeatherMonitorClassLibrary
{
    public class OldObservation
    {
        public List<String> OldGetObservation(string stationId, int langSel)
        {
            if (langSel == 1)
            {
                string islPath = @"http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=is&view=xml&ids=" + stationId;

                XmlDocument doc = new XmlDocument();
                doc.Load(islPath);
                XmlNodeList errorCheck = doc.GetElementsByTagName("err"),
                            stadur = doc.GetElementsByTagName("name"),
                            timi = doc.GetElementsByTagName("time"),
                            hiti = doc.GetElementsByTagName("T"),
                            vindstefna = doc.GetElementsByTagName("D"),
                            vindhradi = doc.GetElementsByTagName("F"),
                            vedurLysing = doc.GetElementsByTagName("W");

                string strStadur, strTimi, strHiti, strVindstefna, strVindhradi, strVerdurLysing, fullStrVindstefna = string.Empty, listVindstefna, fullStrVindhradi, fullStrHiti, fullStrTimi, useStrTimi, strErrorCheck;
                strErrorCheck = errorCheck.Count > 0 ? Convert.ToString(errorCheck[0].InnerXml) : string.Empty;
                strStadur = Convert.ToString(stadur[0].InnerXml);
                strTimi = Convert.ToString(timi[0].InnerXml);
                strHiti = Convert.ToString(hiti[0].InnerXml);
                strVindstefna = Convert.ToString(vindstefna[0].InnerXml);
                strVindhradi = Convert.ToString(vindhradi[0].InnerXml);
                strVerdurLysing = Convert.ToString(vedurLysing[0].InnerXml);
                fullStrTimi = strTimi.Substring(11);
                useStrTimi = fullStrTimi.Remove(5, 3);
                #region error handling

                if (strHiti == string.Empty)
                {
                    fullStrHiti = string.Empty;
                }
                else
                {
                    fullStrHiti = "Hitastig: " + strHiti + "°C";
                }
                if (strVindhradi == string.Empty)
                {
                    fullStrVindhradi = string.Empty;
                }
                else
                {
                    fullStrVindhradi = "Vindhraði: " + strVindhradi + " m/s";
                }

                #endregion

                #region(VindstefnuConverterÍslenska)
                if (strVindstefna != string.Empty)
                {
                    if (strVindstefna == "Logn")
                    {
                        fullStrVindstefna = "Logn";
                    }
                    else if (strVindstefna == "N")
                    {
                        fullStrVindstefna = "Norðanátt";
                    }
                    else if (strVindstefna == "NNA")
                    {
                        fullStrVindstefna = "Norð-norð-austanátt";
                    }
                    else if (strVindstefna == "ANA")
                    {
                        fullStrVindstefna = "Aust-norð-austanátt";
                    }
                    else if (strVindstefna == "A")
                    {
                        fullStrVindstefna = "Austanátt";
                    }
                    else if (strVindstefna == "ASA")
                    {
                        fullStrVindstefna = "Aust-suð-austanátt";
                    }
                    else if (strVindstefna == "SA")
                    {
                        fullStrVindstefna = "Suð-austanátt";
                    }
                    else if (strVindstefna == "SSA")
                    {
                        fullStrVindstefna = "Suð-suð-austanátt";
                    }
                    else if (strVindstefna == "S")
                    {
                        fullStrVindstefna = "Sunnanátt";
                    }
                    else if (strVindstefna == "SSV")
                    {
                        fullStrVindstefna = "Suð-suð-vestanátt";
                    }
                    else if (strVindstefna == "SV")
                    {
                        fullStrVindstefna = "Suð-vestanátt";
                    }
                    else if (strVindstefna == "VSV")
                    {
                        fullStrVindstefna = "Vest-suð-vestanátt";
                    }
                    else if (strVindstefna == "V")
                    {
                        fullStrVindstefna = "Vestanátt";
                    }
                    else if (strVindstefna == "VNV")
                    {
                        fullStrVindstefna = "Vest-norð-vestanátt";
                    }
                    else if (strVindstefna == "NV")
                    {
                        fullStrVindstefna = "Norð-vestanátt";
                    }
                    else if (strVindstefna == "NNV")
                    {
                        fullStrVindstefna = "Norð-norð-vestanátt";
                    }
                    else
                    {
                        fullStrVindstefna = string.Empty;
                    }
                }
                else
                {
                    fullStrVindstefna = string.Empty;
                }
                if (fullStrVindstefna != string.Empty)
                {
                    listVindstefna = "Vindátt: " + fullStrVindstefna;
                }
                else
                {
                    listVindstefna = string.Empty;
                }
                #endregion

                List<string> returnList = new List<string>();
                returnList.Add(strStadur);
                returnList.Add("Klukkan: " + useStrTimi);
                returnList.Add(fullStrHiti);
                returnList.Add(listVindstefna);
                returnList.Add(fullStrVindhradi);
                returnList.Add(strVerdurLysing);
                returnList.Add(strErrorCheck);

                return returnList;
            }

            else
            {
                string enPath = @"http://xmlweather.vedur.is/?op_w=xml&type=obs&lang=en&view=xml&ids=" + stationId;

                XmlDocument doc = new XmlDocument();
                doc.Load(enPath);

                XmlNodeList errorCheck = doc.GetElementsByTagName("err"),
                           stadur = doc.GetElementsByTagName("name"),
                           timi = doc.GetElementsByTagName("time"),
                           hiti = doc.GetElementsByTagName("T"),
                           vindstefna = doc.GetElementsByTagName("D"),
                           vindhradi = doc.GetElementsByTagName("F"),
                           vedurLysing = doc.GetElementsByTagName("W");


                string strStadur, strTimi, strHiti, strVindstefna, strVindhradi, strVerdurLysing, fullStrVindstefna = string.Empty, listVindstefna, fullStrVindhradi, fullStrHiti, fullStrTimi, useStrTimi, strErrorCheck;
                strErrorCheck = Convert.ToString(errorCheck[0].InnerXml);
                strStadur = Convert.ToString(stadur[0].InnerXml);
                strTimi = Convert.ToString(timi[0].InnerXml);
                strHiti = Convert.ToString(hiti[0].InnerXml);
                strVindstefna = Convert.ToString(vindstefna[0].InnerXml);
                strVindhradi = Convert.ToString(vindhradi[0].InnerXml);
                strVerdurLysing = Convert.ToString(vedurLysing[0].InnerXml);
                fullStrTimi = strTimi.Substring(11);
                useStrTimi = fullStrTimi.Remove(5, 3);
                #region error handling
                if (strHiti == string.Empty)
                {
                    fullStrHiti = string.Empty;
                }
                else
                {
                    fullStrHiti = "Temperature: " + strHiti + "°C";
                }
                if (strVindhradi == string.Empty)
                {
                    fullStrVindhradi = string.Empty;
                }
                else
                {
                    fullStrVindhradi = "Wind Speed: " + strVindhradi + " m/s";
                }
                #endregion

                #region(VindstefnuConverterEnglish)
                if (strVindstefna != string.Empty)
                {
                    if (strVindstefna == "Logn" || strVindstefna == "Calm")
                    {
                        fullStrVindstefna = "Calm";
                    }
                    else if (strVindstefna == "N")
                    {
                        fullStrVindstefna = "North";
                    }
                    else if (strVindstefna == "NNA" || strVindstefna == "NNE")
                    {
                        fullStrVindstefna = "North-north-east";
                    }
                    else if (strVindstefna == "ANA" || strVindstefna == "ENE")
                    {
                        fullStrVindstefna = "East-east-north";
                    }
                    else if (strVindstefna == "A" || strVindstefna == "E")
                    {
                        fullStrVindstefna = "East";
                    }
                    else if (strVindstefna == "ASA" || strVindstefna == "ESE")
                    {
                        fullStrVindstefna = "East-south-east";
                    }
                    else if (strVindstefna == "SA" || strVindstefna == "SE")
                    {
                        fullStrVindstefna = "South-east";
                    }
                    else if (strVindstefna == "SSA" || strVindstefna == "SSE")
                    {
                        fullStrVindstefna = "South-south-east";
                    }
                    else if (strVindstefna == "S")
                    {
                        fullStrVindstefna = "South";
                    }
                    else if (strVindstefna == "SSV" || strVindstefna == "SSW")
                    {
                        fullStrVindstefna = "South-south-west";
                    }
                    else if (strVindstefna == "SV" || strVindstefna == "SW")
                    {
                        fullStrVindstefna = "South-west";
                    }
                    else if (strVindstefna == "VSV" || strVindstefna == "WSW")
                    {
                        fullStrVindstefna = "West-south-west";
                    }
                    else if (strVindstefna == "V" || strVindstefna == "W")
                    {
                        fullStrVindstefna = "West";
                    }
                    else if (strVindstefna == "VNV" || strVindstefna == "WNW")
                    {
                        fullStrVindstefna = "West-north-west";
                    }
                    else if (strVindstefna == "NV" || strVindstefna == "NW")
                    {
                        fullStrVindstefna = "North-west";
                    }
                    else if (strVindstefna == "NNV" || strVindstefna == "NNW")
                    {
                        fullStrVindstefna = "North-north-west";
                    }
                    else
                    {
                        fullStrVindstefna = string.Empty;
                    }
                }
                else
                {
                    fullStrVindstefna = string.Empty;
                }
                if (fullStrVindstefna != string.Empty)
                {
                    listVindstefna = "Wind Direction: " + fullStrVindstefna;
                }
                else
                {
                    listVindstefna = string.Empty;
                }
                #endregion


                List<string> returnList = new List<string>();
                returnList.Add(strStadur);
                returnList.Add("Time: " + useStrTimi);
                returnList.Add(fullStrHiti);
                returnList.Add(listVindstefna);
                returnList.Add(fullStrVindhradi);
                returnList.Add(strVerdurLysing);
                returnList.Add(strErrorCheck);

                return returnList;
            }


        }

    }

}
