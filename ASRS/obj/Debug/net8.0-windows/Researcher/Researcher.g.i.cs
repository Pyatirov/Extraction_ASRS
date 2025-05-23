﻿#pragma checksum "..\..\..\..\Researcher\Researcher.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3F42DE425E8D6284F3C2DE576F473CC34EC61D7D"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ASRS;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace ASRS {
    
    
    /// <summary>
    /// Researcher
    /// </summary>
    public partial class Researcher : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 30 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Mechanisms_Experiment;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.UniformGrid ug_Constants_Inputs_Panel;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbLog;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton rbExplicit;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dg_Mechanisms;
        
        #line default
        #line hidden
        
        
        #line 79 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Mechanisms_Points;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\Researcher\Researcher.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel pointInputsPanel;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ASRS;component/researcher/researcher.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Researcher\Researcher.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 8 "..\..\..\..\Researcher\Researcher.xaml"
            ((ASRS.Researcher)(target)).Closed += new System.EventHandler(this.Researcher_Closed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cb_Mechanisms_Experiment = ((System.Windows.Controls.ComboBox)(target));
            
            #line 30 "..\..\..\..\Researcher\Researcher.xaml"
            this.cb_Mechanisms_Experiment.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cb_Mechanisms_Experiment_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ug_Constants_Inputs_Panel = ((System.Windows.Controls.Primitives.UniformGrid)(target));
            return;
            case 4:
            this.rbLog = ((System.Windows.Controls.RadioButton)(target));
            
            #line 37 "..\..\..\..\Researcher\Researcher.xaml"
            this.rbLog.Checked += new System.Windows.RoutedEventHandler(this.rb_Checked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rbExplicit = ((System.Windows.Controls.RadioButton)(target));
            
            #line 38 "..\..\..\..\Researcher\Researcher.xaml"
            this.rbExplicit.Checked += new System.Windows.RoutedEventHandler(this.rb_Checked);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 41 "..\..\..\..\Researcher\Researcher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Calculate_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 42 "..\..\..\..\Researcher\Researcher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Show_Component_Matrix_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.dg_Mechanisms = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 10:
            
            #line 66 "..\..\..\..\Researcher\Researcher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Add_Mechanism_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.cb_Mechanisms_Points = ((System.Windows.Controls.ComboBox)(target));
            
            #line 79 "..\..\..\..\Researcher\Researcher.xaml"
            this.cb_Mechanisms_Points.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cb_Mechanisms_Points_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 12:
            this.pointInputsPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 13:
            
            #line 85 "..\..\..\..\Researcher\Researcher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Add_Experimental_Point_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.10.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 60 "..\..\..\..\Researcher\Researcher.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.bt_Delete_Mechanism_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

