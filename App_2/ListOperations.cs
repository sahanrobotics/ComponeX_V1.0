using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Diagnostics;

class Node
{
    public int Data1;
    public int Data2;
    public int id;
    public string Text;
    public Node Next;
   

    public Node(int data1, int data2, string text, int iD)
    {
        Data1 = data1;
        Data2 = data2;
        Text = text;
        id = iD;
        Next = null;
        
    }
}

class LinkedList
{
    private Node head;
    public long timeE;

    // Add node to the end of the list
    public void AddToLinkedList(string text, int data1, int data2)
    {
        int ID = 0;
        Node newNode = new Node(data1, data2, text , ID);
        if (head == null)
        {
            head = newNode;
            return;
        }
        Node temp = head;
        while (temp.Next != null)
        {
            temp = temp.Next;
        }
        temp.Next = newNode;
    }


    public void AddToLinkedList(string text, int data1, int data2 , int ID)
    {
        
        Node newNode = new Node(data1, data2, text, ID);
        if (head == null)
        {
            head = newNode;
            return;
        }
        Node temp = head;
        while (temp.Next != null)
        {
            temp = temp.Next;
        }
        temp.Next = newNode;
    }


    public bool UpdateNode(string text, int newData1, int newData2)
    {
        Node current = head;
        while (current != null)
        {
            if (current.Text == text) // Check if text matches
            {
                current.Data1 = newData1;
                current.Data2 = newData2;
                return true; // Successfully updated
            }
            current = current.Next;
        }
        return false; // Node not found
    }



    public List<Node> SearchByText(string searchText)
    {
        List<Node> results = new List<Node>();

        // Handle empty or null search text
        if (string.IsNullOrEmpty(searchText))
        {
            return results;  // Return an empty list if the search text is invalid
        }

        Node temp = head;

        while (temp != null)
        {
            // Case-insensitive search
            if (temp.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(temp);
            }
            temp = temp.Next;
        }

        return results;
    }


    public List<Node> SearchByFlexibleText(string searchText)
    {
        List<Node> results = new List<Node>();

        if (string.IsNullOrEmpty(searchText))
        {
            return results; // Return empty list if input is null or empty
        }

        bool isNumericSearch = int.TryParse(searchText, out int searchNumber);

        Node temp = head;

        while (temp != null)
        {
            // Check for exact match (case-insensitive for text)
            if (temp.Text.Equals(searchText, StringComparison.OrdinalIgnoreCase))
            {
                results.Add(temp);
            }
            // If input is numeric, also check if stored text can be interpreted as the same number
            else if (isNumericSearch && int.TryParse(temp.Text, out int nodeNumber) && nodeNumber == searchNumber)
            {
                results.Add(temp);
            }

            temp = temp.Next;
        }

        return results;
    }





    // Search for nodes by Data1 or Data2 and display exact or nearest matches
    public void SearchByParameter(string parameter, int value, DataGridView searchView)
    {
        // Ensure columns exist in the DataGridView
        if (searchView.Columns.Count == 0)
        {
            searchView.Columns.Add("PartNumber", "Part Number");
            searchView.Columns.Add("IMax", "I Max");
            searchView.Columns.Add("JunctionVoltage", "Junction Voltage");
            searchView.Columns.Add("MatchType", "Match Type");
        }

        // Clear previous search results
        searchView.Rows.Clear();

        List<(Node node, int distance)> results = new List<(Node, int)>();

        Node temp = head;
        while (temp != null)
        {
            int nodeValue = (parameter == "Data1") ? temp.Data1 : temp.Data2;
            int distance = Math.Abs(nodeValue - value); // Calculate distance

            results.Add((temp, distance));
            temp = temp.Next;
        }

        // Sort results by distance
        results.Sort((a, b) => a.distance.CompareTo(b.distance));

        // Display results
        bool exactMatchFound = false;

        foreach (var (node, distance) in results)
        {
            string matchType = (distance == 0) ? "Exact Match" : "Nearest Match";

            // Add row to DataGridView
            searchView.Rows.Add(node.Text, node.Data1, node.Data2, matchType);

            if (distance == 0) exactMatchFound = true;
        }

        if (!exactMatchFound && results.Count == 0)
        {
            MessageBox.Show("No matches found!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }




    public void DisplaySortedListSummry(int algoSelect ,int component , bool dataSelect, bool isAscending, DataGridView dataGridView)
    {
        // Step 1: Extract nodes from the linked list into a List<Node>
        List<Node> nodeList = new List<Node>();
        Node current = head;
        while (current != null)
        {
            nodeList.Add(current);
            current = current.Next;
        }
        List<Node> sortedNodeList;
        // Step 2: Sort the List<Node> using the BubbleSortNodes method

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();  // Start measuring time

        if (algoSelect == 0)
        {
            sortedNodeList = BubbleSortNodes(nodeList, dataSelect, isAscending);
            stopwatch.Stop();  // Stop measuring time
            timeE = stopwatch.ElapsedMilliseconds;  // Get elapsed time in millisecon
            DisplaySortedNodes(sortedNodeList, dataGridView, component);
            MessageBox.Show("Bubble Sort completed!", "Sorting Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (algoSelect == 1)
        {
            sortedNodeList = QuickSortNodes(nodeList, dataSelect, isAscending);
            stopwatch.Stop();  // Stop measuring time
            timeE = stopwatch.ElapsedMilliseconds;  // Get elapsed time in millisecon
            DisplaySortedNodes(sortedNodeList, dataGridView, component);
            MessageBox.Show("Quick Sort completed!", "Sorting Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else if (algoSelect == 2)
        {
            sortedNodeList = MergeSortNodes(nodeList, dataSelect, isAscending);
            stopwatch.Stop();  // Stop measuring time
            timeE = stopwatch.ElapsedMilliseconds;  // Get elapsed time in millisecon
            DisplaySortedNodes(sortedNodeList, dataGridView, component);
            MessageBox.Show("Merge Sort completed!", "Sorting Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        else
        {
            MessageBox.Show("Please select a sorting algorithm.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }

   
       

    }

    public long GetTime()
    {
        return timeE;
    }


    public void DisplaySortedNodes(List<Node> sortedNodeList, DataGridView dataGridView, int component)
    {
        dataGridView.DataSource = null;
        dataGridView.Rows.Clear();
        dataGridView.Columns.Clear();
        if (component == 0)
        {
            dataGridView.Columns.Add("PartNumber", "Diode");
            dataGridView.Columns.Add("IMax", "I Max");
            dataGridView.Columns.Add("JunctionVoltage", "Junction Voltage");
        }
        else
        {
            if (component == 1)
            {
                dataGridView.Columns.Add("PartNumber", "Transistor");
                dataGridView.Columns.Add("IMax", "Ic Max");
                dataGridView.Columns.Add("JunctionVoltage", "Current Gain");
            }
            else
            {
                dataGridView.Columns.Add("PartNumber", "Resistance");
                dataGridView.Columns.Add("IMax", "Wattage");
                dataGridView.Columns.Add("JunctionVoltage", "Tolarance %");
            }

        }

        foreach (var node in sortedNodeList)
        {
            dataGridView.Rows.Add(node.Text, node.Data1, node.Data2, node.id);
        }

    }

    public List<Node> BubbleSortNodes(List<Node> nodeList, bool dataSelect, bool isAscending)
    {
        bool swapped;
        do
        {
            swapped = false;
            for (int i = 0; i < nodeList.Count - 1; i++)
            {
                int currentData = dataSelect ? nodeList[i].Data1 : nodeList[i].Data2;
                int nextData = dataSelect ? nodeList[i + 1].Data1 : nodeList[i + 1].Data2;

                bool shouldSwap = isAscending ? currentData > nextData : currentData < nextData;

                if (shouldSwap)
                {
                    // Swap the nodes in the list
                    Node temp = nodeList[i];
                    nodeList[i] = nodeList[i + 1];
                    nodeList[i + 1] = temp;

                    swapped = true;
                }
            }
        } while (swapped); // Continue until no more swaps are made

        return nodeList; // Return the sorted list
    }



    public List<Node> MergeSortNodes(List<Node> nodeList, bool dataSelect, bool isAscending)
    {
        if (nodeList.Count <= 1)
            return nodeList;

        int mid = nodeList.Count / 2;
        List<Node> left = MergeSortNodes(nodeList.GetRange(0, mid), dataSelect, isAscending);
        List<Node> right = MergeSortNodes(nodeList.GetRange(mid, nodeList.Count - mid), dataSelect, isAscending);

        return Merge(left, right, dataSelect, isAscending);
    }

    private List<Node> Merge(List<Node> left, List<Node> right, bool dataSelect, bool isAscending)
    {
        List<Node> result = new List<Node>();
        int i = 0, j = 0;

        while (i < left.Count && j < right.Count)
        {
            int leftData = dataSelect ? left[i].Data1 : left[i].Data2;
            int rightData = dataSelect ? right[j].Data1 : right[j].Data2;

            bool shouldTakeLeft = isAscending ? leftData <= rightData : leftData >= rightData;

            if (shouldTakeLeft)
                result.Add(left[i++]);
            else
                result.Add(right[j++]);
        }

        result.AddRange(left.GetRange(i, left.Count - i));
        result.AddRange(right.GetRange(j, right.Count - j));

        return result;
    }

    public List<Node> QuickSortNodes(List<Node> nodeList, bool dataSelect, bool isAscending)
    {
        if (nodeList.Count <= 1)
            return nodeList;

        Node pivot = nodeList[nodeList.Count / 2];
        List<Node> less = new List<Node>();
        List<Node> equal = new List<Node>();
        List<Node> greater = new List<Node>();

        int pivotData = dataSelect ? pivot.Data1 : pivot.Data2;

        foreach (var node in nodeList)
        {
            int nodeData = dataSelect ? node.Data1 : node.Data2;

            if (nodeData < pivotData)
                (isAscending ? less : greater).Add(node);
            else if (nodeData > pivotData)
                (isAscending ? greater : less).Add(node);
            else
                equal.Add(node);
        }

        List<Node> sortedList = new List<Node>();
        sortedList.AddRange(QuickSortNodes(less, dataSelect, isAscending));
        sortedList.AddRange(equal);
        sortedList.AddRange(QuickSortNodes(greater, dataSelect, isAscending));

        return sortedList;
    }





    public void SearchByAllParameters(string searchText, int value1, int value2, DataGridView searchView)
    {
        // Ensure columns exist in the DataGridView
        if (searchView.Columns.Count == 0)
        {
            searchView.Columns.Add("PartNumber", "Resistor Value Ohms");
            searchView.Columns.Add("IMax", "Wattage mW");
            searchView.Columns.Add("JunctionVoltage", "Tolerance %");
            searchView.Columns.Add("MatchType", "Match Type");
        }

        // Clear previous search results
        searchView.Rows.Clear();

        List<(Node node, int primaryScore, int secondaryScore)> results = new List<(Node, int, int)>();

        Node temp = head;
        while (temp != null)
        {
            if (!int.TryParse(temp.Text, out int nodeTextValue) || !int.TryParse(searchText, out int searchTextValue))
            {
                temp = temp.Next;
                continue;
            }

            // PRIMARY SCORE: Resistor Value Match
            int primaryScore = Math.Abs(nodeTextValue - searchTextValue); // Lower is better

            // SECONDARY SCORE: Data1 & Data2
            int secondaryScore = (Math.Abs(temp.Data1 - value1) * 2) + Math.Abs(temp.Data2 - value2); // Lower is better

            results.Add((temp, primaryScore, secondaryScore));
            temp = temp.Next;
        }

        // Apply Bubble Sort
        BubbleSort(results);

        // Display results
        bool exactMatchFound = false;
        foreach (var (node, primaryScore, secondaryScore) in results)
        {
            string matchType;
            if (primaryScore == 0 && secondaryScore == 0) matchType = "Exact Match";
            else if (primaryScore < 10) matchType = "Nearest Match";
            else matchType = "Others";

            // Add row to DataGridView
            searchView.Rows.Add(node.Text, node.Data1, node.Data2, matchType);

            if (primaryScore == 0) exactMatchFound = true;
        }

        if (!exactMatchFound && results.Count == 0)
        {
            MessageBox.Show("No matches found!", "Search Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    private void BubbleSort(List<(Node node, int primaryScore, int secondaryScore)> results)
    {
        int n = results.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                // Compare based on primaryScore first (resistor value match), then secondaryScore (Data1, Data2)
                if (results[j].primaryScore > results[j + 1].primaryScore ||
                   (results[j].primaryScore == results[j + 1].primaryScore && results[j].secondaryScore > results[j + 1].secondaryScore))
                {
                    // Swap elements
                    (results[j], results[j + 1]) = (results[j + 1], results[j]);
                }
            }
        }
    }

    // Bubble Sort implementation for sorting the results by distance
    private void BubbleSort(List<(Node node, int distance)> results)
    {
        int n = results.Count;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (results[j].distance > results[j + 1].distance)
                {
                    // Swap the elements
                    var temp = results[j];
                    results[j] = results[j + 1];
                    results[j + 1] = temp;
                }
            }
        }
    }

    // Delete node by position
    public void DeleteByPosition(int position)
    {
        if (head == null) return;

        if (position == 0)
        {
            head = head.Next;
            return;
        }

        Node temp = head;
        for (int i = 0; temp != null && i < position - 1; i++)
        {
            temp = temp.Next;
        }

        if (temp == null || temp.Next == null) return;

        temp.Next = temp.Next.Next;
    }

    // Delete node by text
    public void DeleteByText(string text)
    {
        if (head == null) return;

        if (head.Text == text)
        {
            head = head.Next;
            return;
        }

        Node temp = head;
        while (temp.Next != null && temp.Next.Text != text)
        {
            temp = temp.Next;
        }

        if (temp.Next == null) return;

        temp.Next = temp.Next.Next;
    }

    // Convert LinkedList to DataTable
    public DataTable ConvertLinkedListToDataTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Part Number", typeof(string));
        table.Columns.Add("I Max", typeof(int));
        table.Columns.Add("Junction Voltage", typeof(int));

        Node temp = head;
        while (temp != null)
        {
            table.Rows.Add(temp.Text, temp.Data1, temp.Data2);
            temp = temp.Next;
        }

        return table;
    }

    // Display the list
    public void Display()
    {
        Node temp = head;
        string message = "Stored Data:\n";

        while (temp != null)
        {
            message += $"({temp.Data1}, {temp.Data2}, {temp.Text})\n";
            temp = temp.Next;
        }

        MessageBox.Show(message, "Data List", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Convert the LinkedList to a JSON string
    public string ConvertToJson()
    {
        List<Node> nodeList = new List<Node>();
        Node temp = head;

        while (temp != null)
        {
            nodeList.Add(temp);
            temp = temp.Next;
        }

        return JsonConvert.SerializeObject(nodeList, Formatting.Indented);
    }

    // Load the LinkedList from a JSON string
    public void LoadFromJson(string json)
    {
        List<Node> nodeList = JsonConvert.DeserializeObject<List<Node>>(json);

        if (nodeList != null)
        {
            foreach (var node in nodeList)
            {
                AddToLinkedList(node.Text, node.Data1, node.Data2);
            }
        }
    }

    // Save the LinkedList to a file in JSON format
    public void SaveToFile(string filePath)
    {
        List<Node> existingNodes = new List<Node>();

        // Check if the file exists and load existing data
        if (File.Exists(filePath))
        {
            string existingJson = File.ReadAllText(filePath);
            if (!string.IsNullOrWhiteSpace(existingJson))
            {
                existingNodes = JsonConvert.DeserializeObject<List<Node>>(existingJson) ?? new List<Node>();
            }
        }

        // Convert current LinkedList to a list
        List<Node> currentNodes = new List<Node>();
        Node temp = head;
        while (temp != null)
        {
            currentNodes.Add(temp);
            temp = temp.Next;
        }

        // Remove nodes from the JSON file that are no longer in the current LinkedList
        existingNodes.RemoveAll(existingNode =>
            !currentNodes.Any(currentNode => currentNode.Text == existingNode.Text && currentNode.Data1 == existingNode.Data1 && currentNode.Data2 == existingNode.Data2)
        );

        // Now update or add the current nodes
        foreach (var newNode in currentNodes)
        {
            var existingNode = existingNodes.FirstOrDefault(n => n.Text == newNode.Text && n.Data1 == newNode.Data1 && n.Data2 == newNode.Data2);
            if (existingNode != null)
            {
                // Update existing record
                existingNode.Data1 = newNode.Data1;
                existingNode.Data2 = newNode.Data2;
            }
            else
            {
                // Add new record
                existingNodes.Add(newNode);
            }
        }

        // Write the updated list back to the file
        string json = JsonConvert.SerializeObject(existingNodes, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }



    // Load the LinkedList from a file in JSON format
    public void LoadFromFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            LoadFromJson(json);
        }
    }
}
