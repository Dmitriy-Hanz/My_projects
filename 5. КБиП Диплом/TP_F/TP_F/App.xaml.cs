using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TP_F
{
    public partial class App : Application
    {
        public static SysHelperDB sysH;
        public static FacilityEditorWin FacilityEditorW;
        public static FinderWin Finder_W;

        private void Application_Exit(object sender, ExitEventArgs e)
        {

        }
    }
}
