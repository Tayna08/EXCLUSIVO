﻿#pragma checksum "..\..\..\Componentes\EstoqueFormUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B5F997857B89F6D009C5032F5053BC46D33228CFDDFD4D03AB3A7A228958BDC4"
//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

using NovoTayUmDoce.Componentes;
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


namespace NovoTayUmDoce.Componentes {
    
    
    /// <summary>
    /// EstoqueFormUC
    /// </summary>
    public partial class EstoqueFormUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCancelar;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSalvar;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNome;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbQuant;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpData;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpVali;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Componentes\EstoqueFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbPro;
        
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
            System.Uri resourceLocater = new System.Uri("/NovoTayUmDoce;component/componentes/estoqueformuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Componentes\EstoqueFormUC.xaml"
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
            this.btCancelar = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\Componentes\EstoqueFormUC.xaml"
            this.btCancelar.Click += new System.Windows.RoutedEventHandler(this.btCancelar_Click_1);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btSalvar = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Componentes\EstoqueFormUC.xaml"
            this.btSalvar.Click += new System.Windows.RoutedEventHandler(this.btSalvar_Click_1);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNome = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbQuant = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.dtpData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 6:
            this.dtpVali = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 7:
            this.cbPro = ((System.Windows.Controls.ComboBox)(target));
            
            #line 53 "..\..\..\Componentes\EstoqueFormUC.xaml"
            this.cbPro.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbPro_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

