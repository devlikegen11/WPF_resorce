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

namespace dbwpfprject
{
    public partial class DBWPF : Page, INotifyPropertyChanged
    {
        private static System.Timers.Timer _timer;  
        private bool _startFlag = false;
        private bool logFlag = false;
        private ChartValues<int> _cpuValues;
        private ChartValues<int> _memValues;
        private ChartValues<int> _diskValues;
        public ChartValues<int> CpuValues
        {
            get { return _cpuValues; }  // 값 반환
            set
            {
                _cpuValues = value;     // 새로운 값 저장
                OnProChan(nameof(CpuValues)); // 속성 값이 변경되었음을 알리기 위해 OnProChan 메서드를 호출.
            }
        }
        public ChartValues<int> MemValues
        {
            get { return _memValues; }
            set
            {
                _memValues = value;
                OnProChan(nameof(MemValues));
            }
        }
        public ChartValues<int> DiskValues
        {
            get { return _diskValues; }
            set
            {
                _diskValues = value;
                OnProChan(nameof(DiskValues));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;   //값이 변경될때 발생하는 이벤트

        protected virtual void OnProChan(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  //PropertyChanged가 null이 아닐때 이벤트 발생
        }

        public DBWPF()
        {
            InitializeComponent();
            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += Ontimedevent;
            _timer.AutoReset = true;

            CpuValues = new ChartValues<int> { };
            MemValues = new ChartValues<int> { };
            DiskValues = new ChartValues<int> { };

            DataContext = this;
        }
        public static int cpuValue;
        public static int memValue;
        public static int diskValue;
        private async void Ontimedevent(object source, ElapsedEventArgs e)
        {
            cpuValue = await GetCpuValue();
            memValue = await GetMemValue();
            diskValue = (int)GetDiskValue();

            //Application.Current.Dispatcher.Invoke(() =>
            Dispatcher.Invoke(() =>
            {
                diskguage.Value = cpuValue;
                memguage.Value = memValue;
                cpugauge.Value = diskValue;
                CpuValues.Add(cpuValue); 
                MemValues.Add(memValue);
                DiskValues.Add(diskValue);
            });
        }

        private async Task<int> GetCpuValue()
        {
            var CpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            CpuCounter.NextValue();
            await Task.Delay(1000);
            int returnValue = (int)CpuCounter.NextValue();
            return returnValue;
        }

        private async Task<int> GetMemValue()
        {
            var MemCounter = new PerformanceCounter("Memory", "% Committed Bytes in Use");
            MemCounter.NextValue();
            await Task.Delay(1000);
            int returnValue = (int)MemCounter.NextValue();
            return returnValue;
        }

        private double GetDiskValue()
        {
            double TotalDiskSize = 0;
            double AvailableDiskSize = 0;
            double UsedDiskSize = 0;
            double UsedDiskSizePercent = 0;
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    TotalDiskSize += drive.TotalSize;
                    AvailableDiskSize += drive.AvailableFreeSpace;
                }
            }
            UsedDiskSize = TotalDiskSize - AvailableDiskSize;
            UsedDiskSizePercent = (UsedDiskSize / TotalDiskSize) * 100;

            return UsedDiskSizePercent;
        }

        Action DBthread = async () =>
        {
            if (cpuValue != 0 && memValue != 0 && diskValue != 0)
            {
                MySqlConnection conn = DBDB.connect();

                try
                {
                    await conn.OpenAsync();
                    string query = $"INSERT INTO project VALUES (NOW(),{cpuValue}, {memValue}, {diskValue});";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Error : {ex.Message}");
                }
                finally
                {
                    conn.Close();
                }
            }
            else
            {
                DateTime nowtime = DateTime.Now;
                string errtime = nowtime.ToString("yyyy-MM-dd_HH-mm-ss");
                using (StreamWriter writer = File.CreateText(@"C:\Users\LMS\source\repos\dbwpfprject\Errfile\ERRtext "+ errtime + ".txt"))
                {
                    string cpuerr = cpuValue.ToString();
                    string memerr = memValue.ToString();
                    string diskerr = diskValue.ToString();
                    await writer.WriteLineAsync ("오류발생");
                    await writer.WriteLineAsync ("cpu : " + cpuerr);
                    await writer.WriteLineAsync ("memory : " + memerr);
                    await writer.WriteLineAsync ("disk : " + diskerr);
                }
            }
        };
        private async void startbut_Click(object sender, RoutedEventArgs e)
        {
            if (_startFlag == false)
            {
                _startFlag = true;
                _timer.Enabled = true;
                startbut.Content = "Stop";
            }
            else
            {
                _startFlag = false;
                _timer.Enabled = false;
                startbut.Content = "Start";
            }
            while(_startFlag)
            {
                Task backDB = Task.Run(DBthread);
                await Task.Delay(1000);
                await backDB;
            }
        }
        private Window pastlogg;
        private void pastlog_Click(object sender, RoutedEventArgs e)
        {
            if (logFlag == false)
            {
                pastlogg = new pastlog()
                {
                    Height = 450,
                    Width = 800
                };
                pastlogg.Show();
                logFlag = true;
                pastlog.Content = "조회 종료";
            }
            else
            {
                pastlogg.Close();
                pastlogg = null;
                logFlag = false;
                pastlog.Content = "조  회";
            }
        }
    }
}