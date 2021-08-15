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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        // Mathod Below
        // ----------------------------------------- //
        // Will Calculate The Result From User's Input
        private void GetAuctionHouseCalculationResult()
        {
            int Price, Quantity, Cost, Tax, Profit, Total;
            FontFamily LabelFont = new FontFamily("Arial");

            try
            {
                // Convert Section
                Price = Convert.ToInt32(textBoxPriceInput.Text);
                Quantity = Convert.ToInt32(textBoxQuantityInput.Text);
                Cost = Convert.ToInt32(textBoxCostInput.Text);
                Tax = Convert.ToInt32(textBoxTaxInput.Text);

                // The Profit Calculation
                Profit = (Price - Cost - (Price * Tax / 100)) * Quantity;
                Total = Price * Quantity;
                if (Profit > 0)
                {
                    labelProfitOutput.Font = new Font(LabelFont, 11);
                    labelTotalEarn.Font = new Font(LabelFont, 11);
                    // Format The String from 10000 to 10,000
                    labelProfitOutput.Text = Profit.ToString("N0", new CultureInfo("en-US")) + " Mito";
                    labelTotalEarn.Text = Total.ToString("N0", new CultureInfo("en-US")) + " Mito";
                }
                else
                {
                    labelProfitOutput.Font = new Font(LabelFont, 9);
                    labelProfitOutput.Text = "You are not gonna\nmake any profit from this.";
                }
                labelError.Text = "All looks just fine.";
            }
            catch (Exception)
            {
                labelError.Text = "Please make sure  Price, Quantity, and Cost are inserted with integer.";
            }
        }

        // Just Clear Textbox 
        private void ClearTextbox()
        {
            textBoxPriceInput.Text = "";
            textBoxQuantityInput.Text = "";
            textBoxCostInput.Text = "";
            textBoxItemNameInput.Text = "";
        }

        // Will Refresh and Display Selling Item on Listbox
        private void DisplaySellingList()
        {
            listBoxItems.Items.Clear();
            DriftCityToolsLibrary Path = new DriftCityToolsLibrary();
            string StoringFolderPath = Path.GetFolderPath("Drift City Tools");
            string StoringFilePath = Path.GetFilePath("Drift City Tools", "AuctionHouse.xml");

            try
            {
                XmlDocument SellingList = new XmlDocument();
                SellingList.Load(StoringFilePath);
                foreach (XmlNode Node in SellingList.SelectSingleNode("DriftCityRemasteredAuctionHouseXML"))
                {
                    listBoxItems.Items.Add(
                        Node.Attributes[0].Value + " / " +
                        Node.SelectSingleNode("ItemPrice").InnerText + " / " +
                        Node.SelectSingleNode("ItemQuantity").InnerText + " / " +
                        Node.SelectSingleNode("ItemCost").InnerText);
                }
            }
            catch (Exception)
            {
                labelError.Text = "Something went wrong.";
            }
        }

        // Add ComboBox Item(Car Stats)
        private void AddComboboxCarStatsItem()
        {
            DriftCityToolsLibrary Path = new DriftCityToolsLibrary();
            string StoringFolderPath = Path.GetFolderPath("Drift City Tools");
            string StoringFilePath = Path.GetFilePath("Drift City Tools", "Dealership.xml");

            try
            {
                XmlDocument ComboboxCarList = new XmlDocument();
                ComboboxCarList.Load(StoringFilePath);
                foreach (XmlNode Node in ComboboxCarList.SelectSingleNode("DriftCityRemasteredDealershipXML"))
                {
                    comboBoxDealershipName.Items.Add(Node.Attributes[0].Value.ToString() + " V" + Node.SelectSingleNode("BaseVLevel").InnerText.ToString());
                }
            }
            catch (Exception)
            {
            }
        }

        // Will Display Car Stats Info
        private void DisplayCarStats()
        {
            panelCarStatsPoints.Visible = false;
            panelCarStatsBasic.Visible = false;
            DriftCityToolsLibrary Path = new DriftCityToolsLibrary();
            string StoringFolderPath = Path.GetFolderPath("Drift City Tools");
            string StoringFilePath = Path.GetFilePath("Drift City Tools", "Dealership.xml");
            string SelectedCarName = comboBoxDealershipName.SelectedItem.ToString();
            SelectedCarName = SelectedCarName.Remove(SelectedCarName.Length - 3);

            try
            {
                XmlDocument CarList = new XmlDocument();
                CarList.Load(StoringFilePath);

                foreach (XmlNode Node in CarList.SelectNodes("DriftCityRemasteredDealershipXML/Car"))
                {
                    if (Node.Attributes[0].Value.ToString() == SelectedCarName)
                    {
                        labelCarNameOutput.Text = Node.Attributes[0].Value.ToString();
                        labelCarPriceOutput.Text = Convert.ToInt32(Node.SelectSingleNode("Price").InnerText).ToString("N0", new CultureInfo("en-US")) + " Mito";
                        if (Node.SelectSingleNode("Coupon").InnerText.ToString() == "True" || Node.SelectSingleNode("Coupon").InnerText.ToString() == "true")
                        {
                            labelCarCouponOutput.Text = "Coupon Needed";
                        }
                        else
                        {
                            labelCarCouponOutput.Text = "Coupon Not Needed";
                        }

                        labelCarStatsPointSpeedV1.Text = Node.SelectSingleNode("SpeedPointV1").InnerText.ToString();
                        labelCarStatsPointSpeedV2.Text = Node.SelectSingleNode("SpeedPointV2").InnerText.ToString();
                        labelCarStatsPointSpeedV3.Text = Node.SelectSingleNode("SpeedPointV3").InnerText.ToString();
                        labelCarStatsPointSpeedV4.Text = Node.SelectSingleNode("SpeedPointV4").InnerText.ToString();
                        labelCarStatsPointSpeedV5.Text = Node.SelectSingleNode("SpeedPointV5").InnerText.ToString();
                        labelCarStatsPointSpeedV6.Text = Node.SelectSingleNode("SpeedPointV6").InnerText.ToString();
                        labelCarStatsPointSpeedV7.Text = Node.SelectSingleNode("SpeedPointV7").InnerText.ToString();
                        labelCarStatsPointSpeedV8.Text = Node.SelectSingleNode("SpeedPointV8").InnerText.ToString();
                        labelCarStatsPointSpeedV9.Text = Node.SelectSingleNode("SpeedPointV9").InnerText.ToString();

                        labelCarStatsPointAccelerationV1.Text = Node.SelectSingleNode("AccelerationPointV1").InnerText.ToString();
                        labelCarStatsPointAccelerationV2.Text = Node.SelectSingleNode("AccelerationPointV2").InnerText.ToString();
                        labelCarStatsPointAccelerationV3.Text = Node.SelectSingleNode("AccelerationPointV3").InnerText.ToString();
                        labelCarStatsPointAccelerationV4.Text = Node.SelectSingleNode("AccelerationPointV4").InnerText.ToString();
                        labelCarStatsPointAccelerationV5.Text = Node.SelectSingleNode("AccelerationPointV5").InnerText.ToString();
                        labelCarStatsPointAccelerationV6.Text = Node.SelectSingleNode("AccelerationPointV6").InnerText.ToString();
                        labelCarStatsPointAccelerationV7.Text = Node.SelectSingleNode("AccelerationPointV7").InnerText.ToString();
                        labelCarStatsPointAccelerationV8.Text = Node.SelectSingleNode("AccelerationPointV8").InnerText.ToString();
                        labelCarStatsPointAccelerationV9.Text = Node.SelectSingleNode("AccelerationPointV9").InnerText.ToString();

                        labelCarStatsPointDurabilityV1.Text = Node.SelectSingleNode("DurabilityPointV1").InnerText.ToString();
                        labelCarStatsPointDurabilityV2.Text = Node.SelectSingleNode("DurabilityPointV2").InnerText.ToString();
                        labelCarStatsPointDurabilityV3.Text = Node.SelectSingleNode("DurabilityPointV3").InnerText.ToString();
                        labelCarStatsPointDurabilityV4.Text = Node.SelectSingleNode("DurabilityPointV4").InnerText.ToString();
                        labelCarStatsPointDurabilityV5.Text = Node.SelectSingleNode("DurabilityPointV5").InnerText.ToString();
                        labelCarStatsPointDurabilityV6.Text = Node.SelectSingleNode("DurabilityPointV6").InnerText.ToString();
                        labelCarStatsPointDurabilityV7.Text = Node.SelectSingleNode("DurabilityPointV7").InnerText.ToString();
                        labelCarStatsPointDurabilityV8.Text = Node.SelectSingleNode("DurabilityPointV8").InnerText.ToString();
                        labelCarStatsPointDurabilityV9.Text = Node.SelectSingleNode("DurabilityPointV9").InnerText.ToString();

                        labelCarStatsPointBoosterV1.Text = Node.SelectSingleNode("BoosterPointV1").InnerText.ToString();
                        labelCarStatsPointBoosterV2.Text = Node.SelectSingleNode("BoosterPointV2").InnerText.ToString();
                        labelCarStatsPointBoosterV3.Text = Node.SelectSingleNode("BoosterPointV3").InnerText.ToString();
                        labelCarStatsPointBoosterV4.Text = Node.SelectSingleNode("BoosterPointV4").InnerText.ToString();
                        labelCarStatsPointBoosterV5.Text = Node.SelectSingleNode("BoosterPointV5").InnerText.ToString();
                        labelCarStatsPointBoosterV6.Text = Node.SelectSingleNode("BoosterPointV6").InnerText.ToString();
                        labelCarStatsPointBoosterV7.Text = Node.SelectSingleNode("BoosterPointV7").InnerText.ToString();
                        labelCarStatsPointBoosterV8.Text = Node.SelectSingleNode("BoosterPointV8").InnerText.ToString();
                        labelCarStatsPointBoosterV9.Text = Node.SelectSingleNode("BoosterPointV9").InnerText.ToString();
                    }
                }
            }
            catch (Exception)
            {
                EdsomeLibrary Error = new EdsomeLibrary();
                Error.MsgError("Something went wrong.");
            }

            panelCarStatsPoints.Visible = true;
            panelCarStatsBasic.Visible = true;
        }

        // Will Display The Amount of Mito Needed to Upgrade Car's V Level
        private void VLevelSelectedIndexChanged()
        {
            DriftCityToolsLibrary VLevel = new DriftCityToolsLibrary();
            labelVLevelPriceOutput.Text = VLevel.GetVLevelUpgradePrice(
                comboBoxVLevelFrom.SelectedIndex,
                comboBoxVLevelTo.SelectedIndex).ToString("N0", new CultureInfo("en-US")) + " Mito.";
        }

        // Create XML File for Dealership
        private void StartCreateXMLDealershipList()
        {
            DriftCityToolsLibrary CarStatsGenerator = new DriftCityToolsLibrary();
            CarStatsGenerator.CreateXMLDealershipList(
                textBoxDSSGCarName.Text,
                textBoxDSSGBaseVLevel.Text,
                textBoxDSSGPrice.Text,
                checkBoxDSSGCoupon.Checked);
        }

        // Create XML File for Auction House
        private void StartCreateXMLAuctionHouseList()
        {
            if (textBoxItemNameInput.Text != "")
            {
                DriftCityToolsLibrary AuctionHouseList = new DriftCityToolsLibrary();
                AuctionHouseList.CreateXMLAuctionHouseList(
                    textBoxItemNameInput.Text,
                    textBoxPriceInput.Text,
                    textBoxQuantityInput.Text,
                    textBoxCostInput.Text);
                ClearTextbox();
            }
            else
            {
                MessageBox.Show("You need to fill the Item Name to complete this action.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Multi-Threadings Below
        // ----------------------------------------- //

        // Events Below
        // ----------------------------------------- //
        // Basic Setting When Start This Program
        private void FormMain_Load(object sender, EventArgs e)
        {
            checkBoxLockTax.Checked = true;
            textBoxTaxInput.Enabled = false;
            //buttonAddItem.Enabled = true;
            //buttonDeleteItem.Enabled = true;

            // Setting TabIndex
            textBoxItemNameInput.TabIndex = 0;
            textBoxPriceInput.TabIndex = 1;
            textBoxQuantityInput.TabIndex = 2;
            textBoxCostInput.TabIndex = 3;
            buttonAddItem.TabIndex = 5;
            buttonDeleteItem.TabIndex = 6;
            checkBoxLockTax.TabIndex = 7;
            if (textBoxTaxInput.Enabled == true)
            {
                textBoxTaxInput.TabIndex = 4;
            }

            panelCarStatsPoints.Visible = false;
            panelCarStatsBasic.Visible = false;

            // Add ComboBox Item(Car's V Leveling)
            for (int i = 1; i < 9; i++)
            {
                comboBoxVLevelFrom.Items.Add("V" + i);
            }
            for (int i = 2; i < 10; i++)
            {
                comboBoxVLevelTo.Items.Add("V" + i);
            }

            AddComboboxCarStatsItem();
            DisplaySellingList();
        }

        // Checkboxes
        // ----------------------------------------- //
        // Decide if The Tax are Fixed
        private void checkBoxLockTax_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxLockTax.Checked == true)
            {
                textBoxTaxInput.Enabled = false;
                textBoxTaxInput.Text = "5";
            }
            else
            {
                textBoxTaxInput.Enabled = true;
            }
        }

        // Textboxes
        // ----------------------------------------- //
        // Will Display The Amount of Profit from The Auction House
        private void textBoxPriceInput_TextChanged(object sender, EventArgs e)
        {
            GetAuctionHouseCalculationResult();
        }

        private void textBoxCostInput_TextChanged(object sender, EventArgs e)
        {
            GetAuctionHouseCalculationResult();
        }

        private void textBoxQuantityInput_TextChanged(object sender, EventArgs e)
        {
            GetAuctionHouseCalculationResult();
        }

        private void textBoxTaxInput_TextChanged(object sender, EventArgs e)
        {
            GetAuctionHouseCalculationResult();
        }

        // ComboBoxes
        // ----------------------------------------- //
        // Will Display The Amount of Mito Needed to Upgrade Car's V Level
        private void comboBoxVLevelFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            VLevelSelectedIndexChanged();
        }

        private void comboBoxVLevelTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            VLevelSelectedIndexChanged();
        }

        // Will Display Car Stats Info
        private void comboBoxDealershipName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCarStats();
        }

        // Will Display Car Stats Info
        private void comboBoxDealershipVLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayCarStats();
        }

        // Buttons
        // ----------------------------------------- //
        // Generate XML File and Add Selling Item to Selling List
        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            StartCreateXMLAuctionHouseList();
            DisplaySellingList();
        }

        // To Delete Selling Item from Selling List
        private void buttonDeleteItem_Click(object sender, EventArgs e)
        {

        }

        // To Open Selling List's Folder
        private void buttonOpenSellingListFolder_Click(object sender, EventArgs e)
        {
            DriftCityToolsLibrary OpenFolder = new DriftCityToolsLibrary();
            EdsomeLibrary OpenFileManager = new EdsomeLibrary();

            if (Directory.Exists(OpenFolder.GetFolderPath("Drift City Tools")))
            {
                try
                {
                    OpenFileManager.OpenApplication(OpenFolder.GetFolderPath("Drift City Tools"));
                }
                catch (Exception)
                {
                    OpenFileManager.MsgError("Failed to open the folder, please try again.");
                }
            }
            else
            {
                OpenFileManager.MsgError("The target folder does not exists.\nYou might need to create one using button \"Add To Selling List\".");
            }
        }

        // To Update Selling List
        private void buttonRefreshSellingList_Click(object sender, EventArgs e)
        {
            DisplaySellingList();
        }

        // Open Windows Calculator Application
        private void buttonOpenCalc_Click(object sender, EventArgs e)
        {
            EdsomeLibrary OpenApp = new EdsomeLibrary();
            OpenApp.OpenApplication("calc");
        }

        // Open Drift City Remastered Application
        private void buttonOpenDCR_Click(object sender, EventArgs e)
        {
            EdsomeLibrary OpenApp = new EdsomeLibrary();
            OpenApp.OpenApplication("RemasteredLauncher.exe");
        }

        // Generate XML File
        private void buttonDSSGGenerate_Click(object sender, EventArgs e)
        {
            StartCreateXMLDealershipList();
        }
    }
}