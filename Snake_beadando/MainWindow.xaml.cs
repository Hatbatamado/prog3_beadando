using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake_beadando
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel vm;
        DispatcherTimer dtJatekos;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new ViewModel((int)this.ActualWidth, (int)this.ActualHeight);
            
            this.DataContext = vm;
            
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (vm.Jatekos.Move(dtJatekos, vm.Jatekos, vm.Ellenseg, vm.Map))
            {
                vm.GameOver(vm.Jatekos, Player.jatekos);
            }
        }

        private void JatekosStart()
        {
            if (dtJatekos == null || !dtJatekos.IsEnabled)
            {
                vm.JatekosInit();
                vm.JatekosUzenet = "";

                dtJatekos = new DispatcherTimer();
                dtJatekos.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dtJatekos.Tick += Dt_Tick;
                dtJatekos.Start();
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.F:
                    JatekosStart();
                    break;
                case Key.W:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.Direct != Direction.le)
                        vm.Jatekos.Direct = Direction.fel;
                    break;
                case Key.S:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.Direct != Direction.fel)
                        vm.Jatekos.Direct = Direction.le;
                    break;
                case Key.A:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.Direct != Direction.jobbra)
                        vm.Jatekos.Direct = Direction.balra;
                    break;
                case Key.D:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.Direct != Direction.balra)
                        vm.Jatekos.Direct = Direction.jobbra;
                    break;
            }
        }
    }
}
