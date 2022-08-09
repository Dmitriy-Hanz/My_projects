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
using System.Data;

namespace TP_F
{
    public partial class KabinetEditorWin : Window
    {
        private static DataTable tempDT;
        public static Kabinet tempKab;
        public static Kabinet originalSource;
        public KabinetEditorWin()
        {
            InitializeComponent();
            tempKab = new Kabinet();
            DataContext = tempKab;
        }
        public KabinetEditorWin(Kabinet target)
        {
            InitializeComponent();
            Title = "Редактирование кабинета";
            Header_L.Content = "Редактирование кабинета";
            tempKab = new Kabinet {name = target.name };
            originalSource = target;
            DataContext = tempKab;
        }

        private void Save_B_Click(object sender, RoutedEventArgs e)
        {
            if (tempKab.name == "") { MessageBox.Show("Нужно указать название кабинета", "Сообщение", MessageBoxButton.OK); return; }
            if (tempKab.name.Length > 25) { MessageBox.Show("Название кабинета не должно содержать больше 25 символов", "Сообщение", MessageBoxButton.OK); return; }
            tempDT = new DataTable();
            if (originalSource == null)
            {
                SysHelperDB.ExecCom($"INSERT INTO Кабинет VALUES({0},'{tempKab.name}') {Environment.NewLine} SELECT @@IDENTITY", tempDT);
                tempKab.kabinetID = tempDT.Rows[0][0].ToString();
                App.sysH.kabinets.Add(tempKab);
                FACECreator.CreateKabinetMP(MainWindow.Auditories_W.AuditoriesList_WP, $"kabinets[{App.sysH.kabinets.Count-1}]");
            }
            else
            {
                SysHelperDB.ExecCom($"UPDATE Кабинет SET название = '{tempKab.name}' WHERE ID_Кабинета_P = {originalSource.kabinetID}");
                originalSource.name = tempKab.name;
            }
            Close();
        }
        private void Cancel_B_Click(object sender, RoutedEventArgs e) => Close();
    }
}
