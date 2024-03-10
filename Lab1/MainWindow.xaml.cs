using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ArrayOperations
{
    public partial class MainWindow : Window
    {
        // Список для зберігання користувацьких чисел
        private List<long> customNumbers = new List<long>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обробник події для додавання користувацького числа до списку.
        /// </summary>
        private void AddNumber_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка чи введене значення є коректним числом
            if (long.TryParse(txtCustomNumber.Text, out long number))
            {
                // Додавання числа до списку користувацьких чисел
                customNumbers.Add(number);
                // Оновлення елементів у списку користувацьких чисел на формі
                listBoxNumbers.Items.Add(number);
                // Очищення поля для введення нового числа
                txtCustomNumber.Clear();
            }
            else
            {
                // Повідомлення про помилку у разі некоректного введення
                MessageBox.Show("Будь ласка, введіть коректне число.");
            }
        }

        /// <summary>
        /// Обробник події для видалення вибраного числа зі списку.
        /// </summary>
        private void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка чи є вибраний елемент у списку користувацьких чисел
            if (listBoxNumbers.SelectedIndex != -1)
            {
                // Видалення числа зі списку користувацьких чисел
                customNumbers.RemoveAt(listBoxNumbers.SelectedIndex);
                // Видалення вибраного елемента зі списку на формі
                listBoxNumbers.Items.RemoveAt(listBoxNumbers.SelectedIndex);
            }
            else
            {
                // Повідомлення про помилку у разі відсутності вибраного елемента для видалення
                MessageBox.Show("Будь ласка, виберіть число для видалення.");
            }
        }

        /// <summary>
        /// Обробник події для заповнення списку заздалегідь визначеними числами.
        /// </summary>
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            // Виклик методу для заповнення списку числами
            FillArray();
        }

        /// <summary>
        /// Обробник події для очищення списку користувацьких чисел.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Очищення списку користувацьких чисел
            customNumbers.Clear();
            // Очищення списку на формі
            listBoxNumbers.Items.Clear();
            // Очищення результатів
            listBoxResult.Items.Clear();
        }

        /// <summary>
        /// Обробник події для виконання обчислень на списку користувацьких чисел.
        /// </summary>
        private void Run_Click(object sender, RoutedEventArgs e)
        {
            // Перевірка на коректність введених значень b та c
            if (!long.TryParse(txtB.Text, out long b) || !long.TryParse(txtC.Text, out long c))
            {
                // Повідомлення про помилку, якщо введені значення b та c некоректні
                MessageBox.Show("Будь ласка, введіть коректні числа для b та c.");
                return;
            }

            // Створення списку для зберігання чисел, які потрапляють у заданий діапазон та чисел, що не потрапляють
            List<long> inRange = new List<long>();
            List<long> outOfRange = new List<long>();

            // Перебір усіх користувацьких чисел для визначення того, чи вони потрапляють у заданий діапазон
            foreach (long num in customNumbers)
            {
                if (num > b && num <= c)
                {
                    // Якщо число потрапляє у діапазон, додати його до списку inRange
                    inRange.Add(num);
                }
                else
                {
                    // Інакше додати його до списку outOfRange
                    outOfRange.Add(num);
                }
            }

            // Обчислення суми, кількості та середнього значення чисел у діапазоні
            long sum = inRange.Sum();
            int count = inRange.Count;
            double average = count == 0 ? 0 : (double)sum / count;

            // Сортування списку outOfRange у спадному порядку
            outOfRange.Sort((a, b) => b.CompareTo(a));

            // Відображення результатів у списку listBoxResult
            listBoxResult.Items.Add($"Середнє значення всіх елементів у діапазоні ([{b},{c}]");
            listBoxResult.Items.Add(average);

            listBoxResult.Items.Add("Елементи, що не потрапляють у діапазон:");
            foreach (long num in outOfRange)
            {
                listBoxResult.Items.Add(num);
            }
        }

        /// <summary>
        /// Метод для заповнення списку користувацьких чисел заздалегідь визначеними значеннями.
        /// </summary>
        private void FillArray()
        {
            // Очищення списку перед заповненням
            customNumbers.Clear();
            // Додавання заздалегідь визначених чисел до списку
            customNumbers.Add(-4);
            customNumbers.Add(3);

            // Поки список не досягне довжини 10, генерувати наступне число за формулою
            int n = customNumbers.Count;
            while (n < 10)
            {
                long nextNumber = customNumbers[n - 1] * customNumbers[n - 1] + 2 * customNumbers[n - 2];
                customNumbers.Add(nextNumber);
                n++;
            }

            // Оновлення елементів у списку на формі
            listBoxNumbers.Items.Clear();
            foreach (long num in customNumbers)
            {
                listBoxNumbers.Items.Add(num);
            }
        }
    }
}
