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
    public partial class WorkplaceEditorWin : Window
    {
        private static DataTable tempDT;
        public static WorkPlace tempWP;
        public static WorkPlace originalSource;
        public WorkplaceEditorWin()
        {
            InitializeComponent();
            tempWP = new WorkPlace();
            DataContext = tempWP;
        }
        public WorkplaceEditorWin(WorkPlace target)
        {
            InitializeComponent();
            Title = "Редактирование рабочего места";
            Header_L.Content = "Редактирование рабочего места";
            tempWP = new WorkPlace { name = target.name };
            originalSource = target;
            DataContext = tempWP;
        }

        private void Save_B_Click(object sender, RoutedEventArgs e)
        {
            if (tempWP.name == "") { MessageBox.Show("Нужно указать название рабочего места", "Сообщение", MessageBoxButton.OK); return; }
            if (tempWP.name.Length > 25) { MessageBox.Show("Название рабочего места не должно содержать больше 25 символов", "Сообщение", MessageBoxButton.OK); return; }
            tempDT = new DataTable();
            if (originalSource == null)
            {
                SysHelperDB.ExecCom($"INSERT INTO РабочееМесто VALUES (NULL,NULL,{FACEHandlers.selectedK.kabinetID},'{tempWP.name}') {Environment.NewLine} SELECT @@IDENTITY", tempDT);
                tempWP.workPlaceID = int.Parse(tempDT.Rows[0][0].ToString());
                App.sysH.kabinets[App.sysH.kabinets.IndexOf(FACEHandlers.selectedK)].workPlaces.Add(tempWP);

                FACECreator.CreateWorkPlaceMP(MainWindow.Auditories_W.WorkPlacesList_WP, $"workPlaces[{FACEHandlers.selectedK.workPlaces.Count - 1}]");
                App.sysH.kabinets[App.sysH.kabinets.IndexOf(FACEHandlers.selectedK)].workPlaces[FACEHandlers.selectedK.workPlaces.Count - 1].UpdateFACE();
                MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = null;
                MainWindow.Auditories_W.WorkPlacesList_WP.DataContext = FACEHandlers.selectedK;
            }
            else
            {
                SysHelperDB.ExecCom($"UPDATE РабочееМесто SET название = '{tempWP.name}' WHERE ID_РабМеста_P = {originalSource.workPlaceID}");
                originalSource.name = tempWP.name;
            }
            Close();
        }

        private void Cancel_B_Click(object sender, RoutedEventArgs e) => Close();
    }
}
