using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using System.IO;
using System.Threading;

namespace Drift_City_Tools
{
    class DriftCityToolsLibrary
    {
        // Get Path of Documents Folder
        public string GetFolderPath(string FolderName)
        {
            string UserName = Environment.UserName;
            string StoringFolderPath = "C:\\Users\\" + UserName + "\\Documents" + "\\" + FolderName;

            return StoringFolderPath;
        }

        // Get Path of Documents
        public string GetFilePath(string FolderName, string FileName)
        {
            string UserName = Environment.UserName;
            string StoringFilePath = "C:\\Users\\" + UserName + "\\Documents" + "\\" + FolderName + "\\" + FileName;

            return StoringFilePath;
        }

        // Will Return The Mito Needed To Upgrade Car's V Level
        public int GetVLevelUpgradePrice(int IndexFrom, int IndexTo)
        {
            int[] VLevelPrice = { 200, 300, 550, 800, 1300, 1900, 2600, 4900 };
            const int MitoToGCoinRatio = 750;
            int TotalPrice = 0;
            for (int i = IndexFrom; i <= IndexTo; i++)
            {
                TotalPrice = TotalPrice + VLevelPrice[i];
            }

            return TotalPrice * MitoToGCoinRatio;
        }

        // Will Create Dealership's Selling Cars' Stats
        public void CreateXMLDealershipList(string CarName, string BaseVLevel, string Price, bool Coupon)
        {
            string FileName = "Dealership.xml";
            string StoringFolderPath = GetFolderPath("Drift City Tools");
            string StoringFilePath = GetFilePath("Drift City Tools", FileName);

            XmlWriterSettings Settings = new XmlWriterSettings();
            Settings.Indent = true;
            Settings.OmitXmlDeclaration = true;
            Settings.NewLineOnAttributes = true;
            Settings.Encoding = Encoding.UTF8;

            if (Directory.Exists(StoringFolderPath))
            {
                if (File.Exists(StoringFilePath))
                {
                    try
                    {
                        XmlDocument DealershipList = new XmlDocument();
                        DealershipList.Load(StoringFilePath);
                        XmlNode _Car = DealershipList.CreateElement("Car");
                        XmlAttribute _CarName = DealershipList.CreateAttribute("CarName");
                        XmlNode _CarBaseVLevel = DealershipList.CreateElement("BaseVLevel");
                        XmlNode _CarPrice = DealershipList.CreateElement("Price");
                        XmlNode _CarCoupon = DealershipList.CreateElement("Coupon");

                        _CarName.Value = CarName;
                        _CarBaseVLevel.InnerText = BaseVLevel;
                        _CarPrice.InnerText = Price;
                        _CarCoupon.InnerText = Coupon.ToString();

                        _Car.Attributes.Append(_CarName);
                        _Car.AppendChild(_CarBaseVLevel);
                        _Car.AppendChild(_CarPrice);
                        _Car.AppendChild(_CarCoupon);
                        // Create Speed Point to Manual Edit
                        for (int j = 1; j <= 9; j++)
                        {
                            XmlNode Speed = DealershipList.CreateElement("SpeedPointV" + j);
                            Speed.InnerText = "123";
                            _Car.AppendChild(Speed);
                        }
                        // Create Acceleration Point to Manual Edit
                        for (int j = 1; j <= 9; j++)
                        {
                            XmlNode Accel = DealershipList.CreateElement("AccelerationPointV" + j);
                            Accel.InnerText = "123";
                            _Car.AppendChild(Accel);
                        }
                        // Create Durability Point to Manual Edit
                        for (int j = 1; j <= 9; j++)
                        {
                            XmlNode Dura = DealershipList.CreateElement("DurabilityPointV" + j);
                            Dura.InnerText = "123";
                            _Car.AppendChild(Dura);
                        }
                        // Create Booster Point to Manual Edit
                        for (int j = 1; j <= 9; j++)
                        {
                            XmlNode Booster = DealershipList.CreateElement("BoosterPointV" + j);
                            Booster.InnerText = "123";
                            _Car.AppendChild(Booster);
                        }
                        DealershipList.DocumentElement.AppendChild(_Car);
                        DealershipList.Save(StoringFilePath);
                    }
                    catch (Exception)
                    {
                        using (XmlWriter AuctionHouseList = XmlWriter.Create(StoringFilePath))
                        {
                            AuctionHouseList.WriteStartElement("DriftCityRemasteredDealershipXML");
                            AuctionHouseList.WriteEndElement();
                            AuctionHouseList.Flush();
                            AuctionHouseList.Close();
                        }
                        // Re-run This Method
                        CreateXMLDealershipList(CarName, BaseVLevel, Price, Coupon);
                        //MessageBox.Show("Opps, something went wrong.\n\nTry delete " + FileName + " at " + StoringFolderPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    using (XmlWriter AuctionHouseList = XmlWriter.Create(StoringFilePath))
                    {
                        AuctionHouseList.WriteStartElement("DriftCityRemasteredDealershipXML");
                        AuctionHouseList.WriteEndElement();
                        AuctionHouseList.Flush();
                        AuctionHouseList.Close();
                    }
                    // Re-run This Method
                    CreateXMLDealershipList(CarName, BaseVLevel, Price, Coupon);
                }
            }
            else
            {
                Directory.CreateDirectory(StoringFolderPath);
                // Re-run This Method
                CreateXMLDealershipList(CarName, BaseVLevel, Price, Coupon);
            }
        }

        // Will Create Auction House Selling List
        public void CreateXMLAuctionHouseList(string Name, string Price, string Quantity, string Cost)
        {
            string FileName = "AuctionHouse.xml";
            string StoringFolderPath = GetFolderPath("Drift City Tools");
            string StoringFilePath = GetFilePath("Drift City Tools", FileName);

            XmlWriterSettings Settings = new XmlWriterSettings();
            Settings.Indent = true;
            Settings.OmitXmlDeclaration = true;
            Settings.NewLineOnAttributes = true;
            Settings.Encoding = Encoding.UTF8;

            if (Directory.Exists(StoringFolderPath))
            {
                if (File.Exists(StoringFilePath))
                {
                    try
                    {
                        XmlDocument AuctionHouseList = new XmlDocument();
                        AuctionHouseList.Load(StoringFilePath);
                        XmlNode _SellingItem = AuctionHouseList.CreateElement("SellingItem");
                        XmlAttribute _ItemName = AuctionHouseList.CreateAttribute("ItemName");
                        XmlNode _ItemPrice = AuctionHouseList.CreateElement("ItemPrice");
                        XmlNode _ItemQuantity = AuctionHouseList.CreateElement("ItemQuantity");
                        XmlNode _ItemCost = AuctionHouseList.CreateElement("ItemCost");

                        _ItemName.Value = Name;
                        _ItemPrice.InnerText = Price;
                        _ItemQuantity.InnerText = Quantity;
                        _ItemCost.InnerText = Cost;

                        _SellingItem.Attributes.Append(_ItemName);
                        _SellingItem.AppendChild(_ItemPrice);
                        _SellingItem.AppendChild(_ItemQuantity);
                        _SellingItem.AppendChild(_ItemCost);
                        AuctionHouseList.DocumentElement.AppendChild(_SellingItem);
                        AuctionHouseList.Save(StoringFilePath);
                    }
                    catch (Exception)
                    {
                        using (XmlWriter AuctionHouseList = XmlWriter.Create(StoringFilePath))
                        {
                            AuctionHouseList.WriteStartElement("DriftCityRemasteredAuctionHouseXML");
                            AuctionHouseList.WriteEndElement();
                            AuctionHouseList.Flush();
                            AuctionHouseList.Close();
                        }
                        // Re-run This Method
                        CreateXMLAuctionHouseList(Name, Price, Quantity, Cost);
                        //MessageBox.Show("Opps, something went wrong.\n\nTry delete " + FileName + " at " + StoringFolderPath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    using (XmlWriter AuctionHouseList = XmlWriter.Create(StoringFilePath))
                    {
                        AuctionHouseList.WriteStartElement("DriftCityRemasteredAuctionHouseXML");
                        AuctionHouseList.WriteEndElement();
                        AuctionHouseList.Flush();
                        AuctionHouseList.Close();
                    }
                    // Re-run This Method
                    CreateXMLAuctionHouseList(Name, Price, Quantity, Cost);
                }
            }
            else
            {
                Directory.CreateDirectory(StoringFolderPath);
                // Re-run This Method
                CreateXMLAuctionHouseList(Name, Price, Quantity, Cost);
            }
        }
    }
}
