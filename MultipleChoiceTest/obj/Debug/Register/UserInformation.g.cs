#pragma checksum "..\..\..\Register\UserInformation.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "58E493D792781ACC56B813BE650BD37FA4D520D3497C7EC7904CFBC7B98EA6AC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MultipleChoiceTest.Register;
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


namespace MultipleChoiceTest.Register {
    
    
    /// <summary>
    /// UserInformation
    /// </summary>
    public partial class UserInformation : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbExcludedModule;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddModule;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbIncludedModule;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveModule;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFirstName;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSurname;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Register\UserInformation.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSubmit;
        
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
            System.Uri resourceLocater = new System.Uri("/MultipleChoiceTest;component/register/userinformation.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Register\UserInformation.xaml"
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
            
            #line 12 "..\..\..\Register\UserInformation.xaml"
            ((MultipleChoiceTest.Register.UserInformation)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\Register\UserInformation.xaml"
            ((MultipleChoiceTest.Register.UserInformation)(target)).Closed += new System.EventHandler(this.Window_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbExcludedModule = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.btnAddModule = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\Register\UserInformation.xaml"
            this.btnAddModule.Click += new System.Windows.RoutedEventHandler(this.BtnAddModule_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbIncludedModule = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btnRemoveModule = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\Register\UserInformation.xaml"
            this.btnRemoveModule.Click += new System.Windows.RoutedEventHandler(this.BtnRemoveModule_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txtFirstName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.txtSurname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.btnSubmit = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\Register\UserInformation.xaml"
            this.btnSubmit.Click += new System.Windows.RoutedEventHandler(this.BtnSubmit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

