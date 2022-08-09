using Census.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Census
{
    public partial class App : Application
    {
        public App()
        {
            DBUtil.Initialize();
            DBUtil.Execute("use CensusDB");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            DBUtil.DetachCurrentDatabase();
        }
    }
}
