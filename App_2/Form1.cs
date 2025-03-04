
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.Globalization;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace App_2

{
    public partial class Form1 : Form
    {
        LinkedList diodeData = new LinkedList();
        LinkedList TransData = new LinkedList();
        LinkedList ResistData = new LinkedList();
        public Form1()
        {
            InitializeComponent();
            diodeData.LoadFromFile("diodeData.json");
            TransData.LoadFromFile("TransistorData.json");
            ResistData.LoadFromFile("ResistorData.json");
            DisplayData();



        }

        // Initialize the columns for SearchView if they haven't been set already
        private void InitializeSearchViewColumns()
        {
            if (SearchView.Columns.Count == 0)  // If columns are not set yet
            {
                SearchView.Columns.Add("PartNumber", "Part Number");
                SearchView.Columns.Add("IMax", "I Max");
                SearchView.Columns.Add("JunctionVoltage", "Junction Voltage");
            }
        }


        private void InitializeSearchViewColumnUpdate()
        {
            try
            {
                // Validate if there are any existing columns
                if (searchViewUp.Columns.Count == 0)  // If columns are not set yet
                {
                    if (diodeRb1.Checked)
                    {
                        searchViewUp.Columns.Add("PartNumber", "Diode");
                        searchViewUp.Columns.Add("IMax", "I Max");
                        searchViewUp.Columns.Add("JunctionVoltage", "Junction Voltage");
                    }
                    else if (TrRb1.Checked)
                    {
                        searchViewUp.Columns.Add("PartNumber", "Transistor");
                        searchViewUp.Columns.Add("IMax", "Current Gain");
                        searchViewUp.Columns.Add("JunctionVoltage", "Collector Current");
                    }
                    else if (ResistRb1.Checked)
                    {
                        searchViewUp.Columns.Add("PartNumber", "Resistor Value");
                        searchViewUp.Columns.Add("IMax", "Wattage");
                        searchViewUp.Columns.Add("JunctionVoltage", "Tolerance %");
                    }
                    else
                    {
                        MessageBox.Show("No component type selected. Please choose a component type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred while initializing columns: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Method to load data to the DataGridView

        private void LoadDataToSearchView(List<Node> nodes)
        {
            try
            {
                SearchView.DataSource = null;  // Unbind the data source temporarily



                // Clear previous rows before loading new data
                SearchView.Rows.Clear();

                SearchView.Columns.Clear();

                // Ensure columns are set before adding rows
                InitializeSearchViewColumns();

                // Add new rows based on the node data
                foreach (var node in nodes)
                {
                    SearchView.Rows.Add(node.Text, node.Data1, node.Data2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDataToSearchViewUpdate(List<Node> nodes)
        {
            try
            {
                searchViewUp.DataSource = null;  // Unbind the data source temporarily



                // Clear previous rows before loading new data
                searchViewUp.Rows.Clear();

                searchViewUp.Columns.Clear();

                // Ensure columns are set before adding rows
                InitializeSearchViewColumnUpdate();

                // Add new rows based on the node data
                foreach (var node in nodes)
                {
                    searchViewUp.Rows.Add(node.Text, node.Data1, node.Data2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DisplayData()
        {

            //  SearchView.DataSource = null;  // Unbind the data source temporarily

            // Clear previous rows before loading new data
            SearchView.Rows.Clear();

            DataTable table = new DataTable();


            // Convert the linked list data into a DataTable
            table = diodeData.ConvertLinkedListToDataTable();

            // Bind the DataTable to the DataGridView
            SearchView.DataSource = table;




        }
        private bool ValidateInputs(string text, string para1, string para2, out int data1, out int data2)
        {
            // Initialize output variables
            data1 = 0;
            data2 = 0;

            // Validate text input (Part Number)
            if (string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show("Part number cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate if data1 is a valid integer
            if (!int.TryParse(para1, out data1))
            {
                MessageBox.Show("Parameters must be a valid integers!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Validate if data2 is a valid integer
            if (!int.TryParse(para2, out data2))
            {
                MessageBox.Show("Parameters must be a valid integers!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // If all validations pass, return true
            return true;
        }

        private void metroControlBox1_Click(object sender, EventArgs e)
        {

        }
        public void loadPage(object F)
        {





        }
        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void aloneTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }



        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string text = DPartNumber.Text.Trim();
            string para1 = DPara1.Text.Trim();
            string para2 = DPara2.Text.Trim();

            // Call the validation function
            if (ValidateInputs(text, para1, para2, out int data1, out int data2))
            {
                // If validation passes, add to the linked list
                diodeData.AddToLinkedList(text, data1, data2);
                MessageBox.Show("Node added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                diodeData.SaveToFile("diodeData.json");
                // Show stored data
                diodeData.Display();
                //DisplayData();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = TPartNumber.Text.Trim();
            string para1 = TPara1.Text.Trim();
            string para2 = TPara2.Text.Trim();

            // Call the validation function
            if (ValidateInputs(text, para1, para2, out int data1, out int data2))
            {
                // If validation passes, add to the linked list
                TransData.AddToLinkedList(text, data1, data2);
                MessageBox.Show("Node added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TransData.SaveToFile("TransistorData.json");
                // Show stored data
                TransData.Display();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string text = RPartNumber.Text.Trim();
            string para1 = RPara1.Text.Trim();
            string para2 = RPara2.Text.Trim();

            // Call the validation function
            if (ValidateInputs(text, para1, para2, out int data1, out int data2))
            {
                // If validation passes, add to the linked list
                ResistData.AddToLinkedList(text, data1, data2);
                MessageBox.Show("Node added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResistData.SaveToFile("ResistorData.json");
                // Show stored data
                ResistData.Display();
            }
        }

        private void Search_Delete_Click(object sender, EventArgs e)
        {
            searchFunction(true);

        }

        public void searchFunction(bool alertEnabled)
        {
            string searchText = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                List<Node> searchResults = new List<Node>();

                if (diodeRb.Checked)
                {
                    searchResults = diodeData.SearchByText(searchText);
                }
                else if (TrRb.Checked)
                {
                    searchResults = TransData.SearchByText(searchText);
                }
                else if (ResistRb.Checked)
                {
                    searchResults = ResistData.SearchByText(searchText);
                }

                // Debugging: Check if results are found
                Console.WriteLine($"Search Text: {searchText}, Results Found: {searchResults.Count}");

                if (alertEnabled)  // Only show alert if true
                {
                    if (searchResults.Count > 0)
                    {
                        string resultMessage = "Search Results Found:\n";

                        foreach (var node in searchResults)
                        {
                            resultMessage += $"Part Number: {node.Text}, I Max: {node.Data1}, Junction Voltage: {node.Data2}\n";
                        }

                        MessageBox.Show(resultMessage, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No results found!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Load results to the DataGridView
                LoadDataToSearchView(searchResults);
            }
            else
            {
                if (alertEnabled)  // Show alert only if enabled
                {
                    MessageBox.Show("Please enter a part number to search.");
                }
            }
        }
        public void searchFunctionForUpdate(bool alertEnabled)
        {
            string searchText = UpSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchText))
            {
                List<Node> searchResults = new List<Node>();

                if (diodeRb1.Checked)
                {
                    searchResults = diodeData.SearchByText(searchText);
                }
                else if (TrRb1.Checked)
                {
                    searchResults = TransData.SearchByText(searchText);
                }
                else if (ResistRb1.Checked)
                {
                    searchResults = ResistData.SearchByFlexibleText(searchText);
                }

                // Debugging: Check if results are found
                Console.WriteLine($"Search Text: {searchText}, Results Found: {searchResults.Count}");

                if (alertEnabled)  // Only show alert if true
                {
                    if (searchResults.Count > 0)
                    {
                        string resultMessage = "Search Results Found:\n";

                        foreach (var node in searchResults)
                        {
                            resultMessage += $"Part Number: {node.Text}, I Max: {node.Data1}, Junction Voltage: {node.Data2}\n";
                        }

                        MessageBox.Show(resultMessage, "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No results found!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Load results to the DataGridView
                LoadDataToSearchViewUpdate(searchResults);
            }
            else
            {
                if (alertEnabled)  // Show alert only if enabled
                {
                    MessageBox.Show("Please enter a part number to search.");
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (diodeRb.Checked)
            {
                if (SearchView.SelectedRows.Count > 0)
                {
                    try
                    {
                        // Collect the part numbers to delete
                        List<string> partNumbersToDelete = new List<string>();

                        foreach (DataGridViewRow row in SearchView.SelectedRows)
                        {
                            if (row.Cells[0].Value != null) // Ensure it's not null
                            {
                                string partNumberToDelete = row.Cells[0].Value.ToString();
                                partNumbersToDelete.Add(partNumberToDelete);
                            }
                        }

                        // Delete from the linked list
                        foreach (var partNumber in partNumbersToDelete)
                        {
                            diodeData.DeleteByText(partNumber);
                        }

                        //  Instead of clearing rows directly, reload the updated data from the linked list
                        SearchView.DataSource = null;
                        SearchView.DataSource = diodeData.ConvertLinkedListToDataTable();
                        diodeData.SaveToFile("diodeData.json");
                        searchFunction(false);
                        MessageBox.Show("Selected rows have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row to delete.");
                }
            }
            else
            {
                if (TrRb.Checked)
                {
                    if (SearchView.SelectedRows.Count > 0)
                    {
                        try
                        {
                            // Collect the part numbers to delete
                            List<string> partNumbersToDelete = new List<string>();

                            foreach (DataGridViewRow row in SearchView.SelectedRows)
                            {
                                if (row.Cells[0].Value != null) // Ensure it's not null
                                {
                                    string partNumberToDelete = row.Cells[0].Value.ToString();
                                    partNumbersToDelete.Add(partNumberToDelete);
                                }
                            }

                            // Delete from the linked list
                            foreach (var partNumber in partNumbersToDelete)
                            {
                                TransData.DeleteByText(partNumber);
                            }

                            //  Instead of clearing rows directly, reload the updated data from the linked list
                            SearchView.DataSource = null;
                            SearchView.DataSource = TransData.ConvertLinkedListToDataTable();
                            TransData.SaveToFile("TransistorData.json");
                            searchFunction(false);
                            MessageBox.Show("Selected rows have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a row to delete.");
                    }
                }
                else
                {
                    if (ResistRb.Checked)
                    {
                        if (SearchView.SelectedRows.Count > 0)
                        {
                            try
                            {
                                // Collect the part numbers to delete
                                List<string> partNumbersToDelete = new List<string>();
                                foreach (DataGridViewRow row in SearchView.SelectedRows)
                                {
                                    if (row.Cells[0].Value != null) // Ensure it's not null
                                    {
                                        string partNumberToDelete = row.Cells[0].Value.ToString();
                                        partNumbersToDelete.Add(partNumberToDelete);
                                    }
                                }
                                // Delete from the linked list
                                foreach (var partNumber in partNumbersToDelete)
                                {
                                    ResistData.DeleteByText(partNumber);
                                }
                                //  Instead of clearing rows directly, reload the updated data from the linked list
                                SearchView.DataSource = null;
                                SearchView.DataSource = ResistData.ConvertLinkedListToDataTable();
                                ResistData.SaveToFile("ResistorData.json");
                                searchFunction(false);
                                MessageBox.Show("Selected rows have been deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select a row to delete.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please select a component type (Diode, Transistor, or Resistor).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }


                }
            }

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (diodeSw1.Checked)
            {
                int value = int.Parse(paraD.Text.Trim());
                diodeData.SearchByParameter("Data1", value, NearestViewD);
            }
            else
            {
                if (diodeSw2.Checked)
                {
                    int value = int.Parse(paraD.Text.Trim());
                    diodeData.SearchByParameter("Data2", value, NearestViewD);
                }
                else
                {
                    MessageBox.Show("Please select a parameter to search by.");
                }
            }

        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            if (TrSw1.Checked)
            {
                int value = int.Parse(paraT.Text.Trim());
                TransData.SearchByParameter("Data1", value, NearestViewT);
            }
            else
            {
                if (TrSw2.Checked)
                {
                    int value = int.Parse(paraT.Text.Trim());
                    TransData.SearchByParameter("Data2", value, NearestViewT);
                }
                else
                {
                    MessageBox.Show("Please select a parameter to search by.");
                }
            }
        }

        private void TrSw1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Validate inputs before processing
            if (string.IsNullOrWhiteSpace(paraR1.Text) ||
                string.IsNullOrWhiteSpace(paraR2.Text) ||
                string.IsNullOrWhiteSpace(paraR3.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string value1 = paraR1.Text.Trim();

            if (!int.TryParse(paraR2.Text.Trim(), out int value2) ||
                !int.TryParse(paraR3.Text.Trim(), out int value3))
            {
                MessageBox.Show("Invalid numeric input. Please enter valid numbers for the parameters.",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ResistData.SearchByAllParameters(value1, value2, value3, NearestResist);


        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            searchFunctionForUpdate(true);

            if (diodeRb1.Checked)
            {
                tl1.Text = "Diode";
                tl2.Text = "I Max";
                tl3.Text = "Junction Voltage";
            }
            else if (TrRb1.Checked)
            {
                tl1.Text = "Transistor";
                tl2.Text = "Current Gain";
                tl3.Text = "Collector Current";
            }
            else if (ResistRb1.Checked)
            {
                tl1.Text = "Resistance";
                tl2.Text = "Wattage";
                tl3.Text = "Tolerance %";
            }


        }

        private void searchViewUp_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (searchViewUp.CurrentRow != null) // Check if there's a selected row
                {
                    t1.Text = searchViewUp.CurrentRow.Cells[0].Value?.ToString() ?? string.Empty;
                    t2.Text = searchViewUp.CurrentRow.Cells[1].Value?.ToString() ?? string.Empty;
                    t3.Text = searchViewUp.CurrentRow.Cells[2].Value?.ToString() ?? string.Empty;
                }
                else
                {
                    MessageBox.Show("No row selected!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ResistRb1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(t1.Text) || string.IsNullOrWhiteSpace(t2.Text) || string.IsNullOrWhiteSpace(t3.Text))
                {
                    MessageBox.Show("Please fill in all fields before updating.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int data1, data2;

                if (!int.TryParse(t2.Text, out data1) || !int.TryParse(t3.Text, out data2))
                {
                    MessageBox.Show("Invalid input! Please enter valid  integer numbers for Parametrs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                bool isUpdated = false;

                if (diodeRb1.Checked)
                {
                    isUpdated = diodeData.UpdateNode(t1.Text, data1, data2);
                    diodeData.SaveToFile("DiodeData.json");

                }
                else if (TrRb1.Checked)
                {
                    isUpdated = TransData.UpdateNode(t1.Text, data1, data2);
                    TransData.SaveToFile("TransistorData.json");
                }
                else if (ResistRb1.Checked)
                {
                    isUpdated = ResistData.UpdateNode(t1.Text, data1, data2);
                    ResistData.SaveToFile("ResistorData.json");
                }
                else
                {
                    MessageBox.Show("Please select a component type (Diode, Transistor, or Resistor).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (isUpdated)
                {
                    searchFunctionForUpdate(false);
                    MessageBox.Show("Node updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Update failed! No matching node found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void label30_Click(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void paraD_TextChanged(object sender, EventArgs e)
        {

        }


        private void InitializeSortViewColumns()
        {
            if (SearchView.Columns.Count == 0)  // If columns are not set yet
            {
                summrySort.Columns.Add("PartNumber", "Part Number");
                summrySort.Columns.Add("IMax", "I Max");
                summrySort.Columns.Add("JunctionVoltage", "Junction Voltage");
            }
        }


        private void button8_Click(object sender, EventArgs e)
        {
            if (s1.SelectedIndex == 0)
            {
                if (s3.SelectedIndex == 0)
                {
                    if (s2.SelectedIndex == 0)
                    {
                        diodeData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, true, summrySort);
                    }
                    else
                    {
                        diodeData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, false, summrySort);
                    }
                }
                else
                {
                    if (s2.SelectedIndex == 0)
                    {
                        diodeData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, true, summrySort);
                    }
                    else
                    {
                        diodeData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, false, summrySort);
                    }
                }

                if (timer == null)
                {
                    MessageBox.Show("Timer label is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    long elapsedTimeMs = diodeData.GetTime(); // Assuming it returns time in milliseconds

                    // Convert milliseconds to TimeSpan
                    TimeSpan timeSpan = TimeSpan.FromMilliseconds(elapsedTimeMs);

                    // Format time as HH:MM:SS.ms
                    string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                        timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

                    timer.Text = $"{formattedTime}";
                }




            }
            else
            {
                if (s1.SelectedIndex == 1)
                {
                    if (s3.SelectedIndex == 0)
                    {
                        if (s2.SelectedIndex == 0)
                        {
                            TransData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, true, summrySort);
                        }
                        else
                        {
                            TransData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, false, summrySort);
                        }
                    }
                    else
                    {
                        if (s2.SelectedIndex == 0)
                        {
                            TransData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, true, summrySort);
                        }
                        else
                        {
                            TransData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, false, summrySort);
                        }
                    }

                    if (timer == null)
                    {
                        MessageBox.Show("Timer label is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        long elapsedTimeMs = TransData.GetTime(); // Assuming it returns time in milliseconds

                        // Convert milliseconds to TimeSpan
                        TimeSpan timeSpan = TimeSpan.FromMilliseconds(elapsedTimeMs);

                        // Format time as HH:MM:SS.ms
                        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

                        timer.Text = $"{formattedTime}";
                    }


                }
                else
                {
                    if (s1.SelectedIndex == 2)
                    {
                        if (s3.SelectedIndex == 0)
                        {
                            if (s2.SelectedIndex == 0)
                            {
                                ResistData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, true, summrySort);
                            }
                            else
                            {
                                ResistData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, true, false, summrySort);
                            }
                        }
                        else
                        {
                            if (s2.SelectedIndex == 0)
                            {
                                ResistData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, true, summrySort);
                            }
                            else
                            {
                                ResistData.DisplaySortedListSummry(s4.SelectedIndex, s1.SelectedIndex, false, false, summrySort);
                            }
                        }

                        if (timer == null)
                        {
                            MessageBox.Show("Timer label is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            long elapsedTimeMs = ResistData.GetTime(); // Assuming it returns time in milliseconds

                            // Convert milliseconds to TimeSpan
                            TimeSpan timeSpan = TimeSpan.FromMilliseconds(elapsedTimeMs);

                            // Format time as HH:MM:SS.ms
                            string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D2}.{3:D3}",
                                timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);

                            timer.Text = $"{formattedTime}";
                        }

                    }
                    else
                    {

                        MessageBox.Show("Please select a component type (Diode, Transistor, or Resistor).", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
            }
        }

        private void timer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
