using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

namespace ASPWebFormsTest._06_WorkingWithDatabase
{
    public partial class _09_WorkingWithDatabase : System.Web.UI.Page
    {
        SqlConnection _connection;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Чтение значения строки подключения из web.config из секции <connectionStrings>
            string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;
            // Настройка объекта подключения к базе и открытие подключения.
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            // При выгрузке страницы из памяти сервера закрываем подключение к базе данных.
            if (_connection != null && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
        }

        // Чтение одного значения из базы данных.
        protected void ReadOneValueButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Создание объекта запроса.
                SqlCommand command = new SqlCommand("SELECT Login FROM Users WHERE ID=1", _connection);

                // Чтение первой строки и первой колонки (одного значения) из результата который возвращает запрос.
                object result = command.ExecuteScalar();
                string login = Convert.ToString(result);

                ReadOneValueOutput.Text = login;
            }
            catch (Exception ex)
            {
                ReadOneValueOutput.Text = "Error:<br />" + ex.Message;
                ReadOneValueOutput.ForeColor = Color.Red;
            }
        }

        // Чтение нескольких строк из базы данных.
        protected void ReadAllButton_Click(object sender, EventArgs e)
        {
            SqlDataReader reader = null;
            try
            {
                // Создание объекта запроса.
                SqlCommand command = new SqlCommand("SELECT * FROM Users", _connection);

                string result = string.Empty;

                // Создание объекта для построчного считывания данных из базы.
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // Получение данных из колонок.
                    result += "ID = " + Convert.ToString(reader["ID"]) + "; ";
                    result += "Login = " + Convert.ToString(reader["Login"]) + "; ";
                    result += "Password = " + Convert.ToString(reader["Password"]);
                    result += "<br />";
                }
                ReadAllOutput.Text = result;
            }
            catch (Exception ex)
            {
                ReadAllOutput.Text = "Error:<br />" + ex.Message;
                ReadAllOutput.ForeColor = Color.Red;
            }
            finally
            {
                // Если reader был открыт освобождаем память, закрывая объект.
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        // Добавление новой записи в базу.
        protected void AddNewEntryButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("INSERT INTO Users (Login, Password, Email)VALUES(@Login, @Password, @Email)", _connection);
                // Инициализация переменных в запросе.
                command.Parameters.AddWithValue("Login", LoginTextBox.Text);
                command.Parameters.AddWithValue("Password", PasswordTextBox.Text);
                command.Parameters.AddWithValue("Email", EmailTextBox.Text);

                // Выполнение запроса.
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorOutput.Text = "Error:<br />" + ex.Message;
                ErrorOutput.ForeColor = Color.Red;
            }
        }

        // Удаление записи из базы данных.
        protected void RemoveByIdButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE ID=@UserId", _connection);

                // Инициализация переменных в запросе.
                command.Parameters.AddWithValue("UserID", IdToRemoveTextBox.Text);

                // Выполнение запроса. Метод ExecuteNonQuery возвращает количество удаленных в базе строк.
                int affectedRows = command.ExecuteNonQuery();
                
                // Добавление в ответ пользователю тега скрипт, в котором с помощью javascript функции alert выводиться сообщение.
                string script = string.Format("alert('Удалено {0} строк');", affectedRows);
                ClientScript.RegisterClientScriptBlock(this.GetType(), "MessageBox", script, true);
            }
            catch (Exception ex)
            {
                ErrorOutput2.Text = "Error:<br />" + ex.Message;
                ErrorOutput2.ForeColor = Color.Red;
            }
        }
    }
}