﻿#pragma checksum "..\..\..\Componentes\PedidoFormUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A9EC11ED01B44816BA0EC4732333F62BF87662836D3FD1596592522906D33197"
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
    /// PedidoFormUC
    /// </summary>
    public partial class PedidoFormUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCancelar;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSalvar;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbTotal;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbStatus;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbFormaPag;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbHora;
        
        #line default
        #line hidden
        
        
        #line 60 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dtpData;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbCli;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\Componentes\PedidoFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFun;
        
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
            System.Uri resourceLocater = new System.Uri("/NovoTayUmDoce;component/componentes/pedidoformuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Componentes\PedidoFormUC.xaml"
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
            
            #line 23 "..\..\..\Componentes\PedidoFormUC.xaml"
            this.btCancelar.Click += new System.Windows.RoutedEventHandler(this.btCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btSalvar = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Componentes\PedidoFormUC.xaml"
            this.btSalvar.Click += new System.Windows.RoutedEventHandler(this.btSalvar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbTotal = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbStatus = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbFormaPag = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbHora = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.dtpData = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 8:
            this.cbCli = ((System.Windows.Controls.ComboBox)(target));
            
            #line 62 "..\..\..\Componentes\PedidoFormUC.xaml"
            this.cbCli.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbCli_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.cbFun = ((System.Windows.Controls.ComboBox)(target));
            
            #line 63 "..\..\..\Componentes\PedidoFormUC.xaml"
            this.cbFun.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbFun_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

