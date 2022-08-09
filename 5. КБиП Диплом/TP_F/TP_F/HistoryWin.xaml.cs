using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.IO;
using System.Data;

namespace TP_F
{
    public partial class HistoryWin : Window
    {
        public HistoryWin()
        {
            InitializeComponent();
            string tempStr;
            DataTable dt = new DataTable();
            dt.Columns.Add("Дата и время записи", Type.GetType("System.String"));
            dt.Columns.Add("Действие", Type.GetType("System.String"));
            using (StreamReader sr = new StreamReader($"{Environment.CurrentDirectory}\\FacilityLog.txt",Encoding.GetEncoding(1251)))
            {
                while (!sr.EndOfStream)
                {
                    tempStr = sr.ReadLine();
                    HistoryList_LB.Items.Add(tempStr);
                }
            }
        }

        private void ClearHistory_B_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить все записи?", "Предупреждение", MessageBoxButton.YesNo) == MessageBoxResult.No) { return; }
            HistoryList_LB.Items.Clear();
            File.Delete($"{Environment.CurrentDirectory}\\FacilityLog.txt");
        }

        private void Exit_B_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
