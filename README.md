# ComponeX - Electronic Component Search Engine

![ComponeX Banner](assets/banner.png)

## 📌 Project Overview
**ComponeX** is a smart electronic component search engine that allows users to efficiently manage and search for electronic components based on various parameters. It provides sorting options, searching functionalities, and performance analysis for different sorting algorithms.

## 📂 Features
✔️ **Add New Component**  
✔️ **Edit Existing Component**  
✔️ **Delete Components**  
✔️ **Search Components** (with nearest parameter matching)  
✔️ **Sort and View Entire Dataset**  
✔️ **Sorting Algorithms Time Comparison**

## 🚀 Technologies Used
- **C#** (Windows Forms Application)
- **SQLite** (Database)
- **Python** (Dataset Generation)
- **MATLAB** (Execution Time Analysis)
- **JSON** (Data Storage)

## 📊 Sorting Algorithms Used
ComponeX allows users to select the best sorting algorithm based on dataset size and efficiency.

| Algorithm    | Best Case | Average Case | Worst Case | Suitable For |
|-------------|----------|--------------|------------|--------------|
| **Bubble Sort** | O(n)  | O(n²)  | O(n²)  | Small datasets |
| **Merge Sort**  | O(n log n) | O(n log n) | O(n log n) | Large, stable sorting |
| **Quick Sort**  | O(n log n) | O(n log n) | O(n²) | Fast performance in large datasets |

## 📈 Performance Comparison
We measured sorting algorithm execution times using **C#'s Stopwatch**. The dataset (ranging from 10 to 1,000,000 transistors) was generated in **Python** and stored as JSON.

**Execution Time Analysis:**
- Stopwatch was used to measure sorting times.
- MATLAB was used for **theoretical analysis**, plotting results on a log scale.

![Performance Graph](assets/performance_chart.png)

## 🎥 System Demonstration
🔹 [Watch the Demo Video](https://example.com/demo-video)  

## 👥 Team Members
| Name | Registration No. | Department |
|------|----------------|------------|
| **DEIE Manodya G.P.** | EG/2022/5186 | DEIE |
| **COM Dassanayake D.M.B.C.** | EG/2022/4984 | COM |
| **DEIE Manthreerathnasekara H.A.S.S.** | EG/2022/5187 | DEIE |

## 📜 License
This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.

## 📧 Contact
For any inquiries, feel free to reach out:  
📩 Email: example@email.com  
🌐 Website: [www.componeX.com](https://example.com)

---

> _Developed by Group No. 34 - University of Ruhuna_

