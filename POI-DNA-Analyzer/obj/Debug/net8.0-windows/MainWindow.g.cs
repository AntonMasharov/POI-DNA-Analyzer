﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D96A44FAD016A2C0B2A94D23D712AD01643ED28A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using OxyPlot.Wpf;
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


namespace POI_DNA_Analyzer {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem SequenceFinderTab;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PromptField;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EnterPrompt;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ResultText;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveFileButton;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClearButton;
        
        #line default
        #line hidden
        
        
        #line 85 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox List;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem DinucleotidesAnalyzerTab;
        
        #line default
        #line hidden
        
        
        #line 107 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ChunkSize;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ChunkSizeTextBox;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox EnableSliderCheckBox;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SimilaritySlider;
        
        #line default
        #line hidden
        
        
        #line 202 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartButton;
        
        #line default
        #line hidden
        
        
        #line 210 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveDinucleotidesAnalyzer;
        
        #line default
        #line hidden
        
        
        #line 220 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal OxyPlot.Wpf.PlotView OxyPlot;
        
        #line default
        #line hidden
        
        
        #line 223 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ScrollBar HorizontalScrollBar;
        
        #line default
        #line hidden
        
        
        #line 234 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OpenFileButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/POI-DNA-Analyzer;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetRULang);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 17 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.SetENLang);
            
            #line default
            #line hidden
            return;
            case 3:
            this.SequenceFinderTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 4:
            this.PromptField = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.EnterPrompt = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\MainWindow.xaml"
            this.EnterPrompt.Click += new System.Windows.RoutedEventHandler(this.EnterPromptButtonClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ResultText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.SaveFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 70 "..\..\..\MainWindow.xaml"
            this.SaveFileButton.Click += new System.Windows.RoutedEventHandler(this.SaveFileButtonClick);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ClearButton = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\..\MainWindow.xaml"
            this.ClearButton.Click += new System.Windows.RoutedEventHandler(this.ClearResultButtonClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.List = ((System.Windows.Controls.ListBox)(target));
            return;
            case 10:
            this.DinucleotidesAnalyzerTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 11:
            this.ChunkSize = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.ChunkSizeTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 13:
            this.EnableSliderCheckBox = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 14:
            this.SimilaritySlider = ((System.Windows.Controls.Slider)(target));
            return;
            case 15:
            
            #line 173 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 174 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 175 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 176 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 177 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 178 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 179 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 180 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 181 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 182 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 25:
            
            #line 183 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 26:
            
            #line 184 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 185 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 186 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 187 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 188 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 189 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 32:
            
            #line 190 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 33:
            
            #line 191 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 34:
            
            #line 192 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ShowGraph);
            
            #line default
            #line hidden
            return;
            case 35:
            this.StartButton = ((System.Windows.Controls.Button)(target));
            
            #line 205 "..\..\..\MainWindow.xaml"
            this.StartButton.Click += new System.Windows.RoutedEventHandler(this.StartDinucleotidesAnalyzerButtonClick);
            
            #line default
            #line hidden
            return;
            case 36:
            this.SaveDinucleotidesAnalyzer = ((System.Windows.Controls.Button)(target));
            
            #line 211 "..\..\..\MainWindow.xaml"
            this.SaveDinucleotidesAnalyzer.Click += new System.Windows.RoutedEventHandler(this.SaveDinucleotidesAnalyzerButtonClick);
            
            #line default
            #line hidden
            return;
            case 37:
            this.OxyPlot = ((OxyPlot.Wpf.PlotView)(target));
            return;
            case 38:
            this.HorizontalScrollBar = ((System.Windows.Controls.Primitives.ScrollBar)(target));
            
            #line 225 "..\..\..\MainWindow.xaml"
            this.HorizontalScrollBar.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.HorizontalScrollBar_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 39:
            this.OpenFileButton = ((System.Windows.Controls.Button)(target));
            
            #line 236 "..\..\..\MainWindow.xaml"
            this.OpenFileButton.Click += new System.Windows.RoutedEventHandler(this.OpenFileButtonClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

