using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using ZXing;

namespace NovoTayUmDoce.Componentes
{
    public partial class QrCodeWindow : Window
    {
        MainWindow _context;
        public QrCodeWindow(MainWindow context)
        {
            InitializeComponent();
            _context = context;
        }
      
            
        
    }
   
}
    



