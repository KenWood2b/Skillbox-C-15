using System;
using System.Data.SqlClient;
using System.Data.OleDb; 
using System.Threading.Tasks;
using System.Windows;

namespace ADOWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Строка подключения для MSSQLLocalDB
        private readonly string sqlConnectionString = @"Server=desktop-f55hp68\kenwood2b;Database=MSSQLLocalDemo;Integrated Security=True;";

        // Строка подключения для MS Access
        private readonly string accessConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\C#\ADOWpfApp\ADOWpfApp\bin\Debug";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CheckConnections_Click(object sender, RoutedEventArgs e)
        {
            SqlStatusTextBox.Text = "Проверка...";
            AccessStatusTextBox.Text = "Проверка...";

            var sqlTask = CheckSqlConnectionAsync();
            var accessTask = CheckAccessConnectionAsync();

            var sqlStatus = await sqlTask;
            var accessStatus = await accessTask;

            SqlStatusTextBox.Text = sqlStatus;
            AccessStatusTextBox.Text = accessStatus;
        }

        private async Task<string> CheckSqlConnectionAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new SqlConnection(sqlConnectionString))
                    {
                        connection.Open();
                        return "Подключение успешно!";
                    }
                }
                catch (Exception ex)
                {
                    return $"Ошибка подключения: {ex.Message}";
                }
            });
        }

        private async Task<string> CheckAccessConnectionAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (var connection = new OleDbConnection(accessConnectionString))
                    {
                        connection.Open();
                        return "Подключение успешно!";
                    }
                }
                catch (Exception ex)
                {
                    return $"Ошибка подключения: {ex.Message}";
                }
            });
        }
    }
}

