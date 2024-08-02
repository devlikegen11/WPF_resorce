using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;
using MySql.Data.MySqlClient;
using System.Threading;
using Google.Protobuf.WellKnownTypes;
using dbwpfprject.DB;
using System.Data.SqlClient;
using Mysqlx.Session;
using System.Configuration;
using System.Data;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Remoting.Messaging;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using System.Xml.Linq;

namespace dbwpfprject
{
    /// <summary>
    /// pastlog.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    public class LogEntry
    {
        public DateTime LogTime { get; set; }
        public string LogData { get; set; }
    }
    public partial class pastlog : Window
    {
        public ChartValues<double> CPUlog { get; set; }
        public ChartValues<double> MEMlog { get; set; }
        public ChartValues<double> DISKlog { get; set; }
        public pastlog()
        {
            InitializeComponent();
        }

        private async void CPULOG_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn = DBDB.connect();
            try
            {
                await conn.OpenAsync();
                string query = "SELECT date_time, CPU FROM project;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet(); // 데이터를 저장할 Dataset 객체 ds 생성.
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                List<LogEntry> logEntries = new List<LogEntry>();
                foreach (DataRow row in dt.Rows)
                {
                    LogEntry entry = new LogEntry
                    {
                        LogTime = Convert.ToDateTime(row["date_time"]),
                        LogData = row["CPU"].ToString()
                    };
                    logEntries.Add(entry);
                }
                logview.ItemsSource = logEntries;

                CPUlog = new ChartValues<double>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    CPUlog.Add(Convert.ToDouble(row["CPU"]));
                }
                logchart.DataContext = this;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        private async void MEMLOG_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn3 = DBDB.connect();
            try
            {
                await conn3.OpenAsync();
                string query = "SELECT date_time, MEMORY FROM project;";
                MySqlCommand cmd = new MySqlCommand(query, conn3);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet(); // 데이터를 저장할 Dataset 객체 ds 생성.
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                List<LogEntry> logEntries = new List<LogEntry>();
                foreach (DataRow row in dt.Rows)
                {
                    LogEntry entry = new LogEntry
                    {
                        LogTime = Convert.ToDateTime(row["date_time"]),
                        LogData = row["MEMORY"].ToString()
                    };
                    logEntries.Add(entry);
                }
                logview.ItemsSource = logEntries;

                MEMlog = new ChartValues<double>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    MEMlog.Add(Convert.ToDouble(row["MEMORY"]));
                }
                logchart.DataContext = this;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn3.Close();
            }
        }

        private async void DISKLOG_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection conn2 = DBDB.connect();
            try
            {
                await conn2.OpenAsync();
                string query = "SELECT date_time, DISK FROM project;";
                MySqlCommand cmd = new MySqlCommand(query, conn2);
                MySqlDataAdapter adp = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet(); // 데이터를 저장할 Dataset 객체 ds 생성.
                adp.Fill(ds);

                DataTable dt = ds.Tables[0];
                List<LogEntry> logEntries = new List<LogEntry>();
                foreach (DataRow row in dt.Rows)
                {
                    LogEntry entry = new LogEntry
                    {
                        LogTime = Convert.ToDateTime(row["date_time"]),
                        LogData = row["DISK"].ToString()
                    };
                    logEntries.Add(entry);
                }
                logview.ItemsSource = logEntries;

                DISKlog = new ChartValues<double>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    DISKlog.Add(Convert.ToDouble(row["DISK"]));
                }
                logchart.DataContext = this;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn2.Close();
            }
        }
    }
}