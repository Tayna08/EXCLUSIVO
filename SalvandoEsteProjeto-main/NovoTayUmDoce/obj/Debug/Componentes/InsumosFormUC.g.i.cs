﻿#pragma checksum "..\..\..\Componentes\InsumosFormUC.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C5D3AE655194F0E3FE40AFF30267639641A83F14CE8C9F775D84481D4D44717A"
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
    /// InsumosFormUC
    /// </summary>
    public partial class InsumosFormUC : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btCancelar;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btSalvar;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbNOme;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbValorGas;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEstMedio;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbEstMaximo;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPeso;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\Componentes\InsumosFormUC.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbFor;
        
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
            System.Uri resourceLocater = new System.Uri("/NovoTayUmDoce;component/componentes/insumosformuc.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Componentes\InsumosFormUC.xaml"
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
            
            #line 26 "..\..\..\Componentes\InsumosFormUC.xaml"
            this.btCancelar.Click += new System.Windows.RoutedEventHandler(this.btCancelar_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btSalvar = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Componentes\InsumosFormUC.xaml"
            this.btSalvar.Click += new System.Windows.RoutedEventHandler(this.btSalvar_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbNOme = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tbValorGas = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbEstMedio = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.tbEstMaximo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.tbPeso = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.cbFor = ((System.Windows.Controls.ComboBox)(target));
            
            #line 83 "..\..\..\Componentes\InsumosFormUC.xaml"
            this.cbFor.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbFor_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

