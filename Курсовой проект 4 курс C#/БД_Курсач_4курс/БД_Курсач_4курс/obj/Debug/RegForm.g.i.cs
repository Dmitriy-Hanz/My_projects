﻿#pragma checksum "..\..\RegForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4C86E5E18D9629D4108F50597BABF2719788A5CDDB9FE534D607C879A9E372A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using БД_Курсач_4курс;


namespace БД_Курсач_4курс {
    
    
    /// <summary>
    /// RegForm
    /// </summary>
    public partial class RegForm : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YName_TB;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YSurname_TB;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YFathername_TB;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YPassportSer_TB;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YPhoneNum_TB;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox YPassword_TB;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel_B;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\RegForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Reg_B;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/БД_Курсач_4курс;component/regform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegForm.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.YName_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.YSurname_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.YFathername_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.YPassportSer_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.YPhoneNum_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.YPassword_TB = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.Cancel_B = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\RegForm.xaml"
            this.Cancel_B.Click += new System.Windows.RoutedEventHandler(this.Cancel_B_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Reg_B = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\RegForm.xaml"
            this.Reg_B.Click += new System.Windows.RoutedEventHandler(this.Reg_B_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

