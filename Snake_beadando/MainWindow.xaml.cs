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
        DispatcherTimer dtEllenseg;
        bool jatekosReady;
        bool ellensegReady;

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
            if (vm.Jatekos.Move(dtJatekos, dtEllenseg, vm.Jatekos, vm.Ellenseg, vm.Map))
            {
                vm.GameOver(vm.Jatekos, Player.jatekos);
            }
        }

        private void JatekosStart()
        {
            if (!jatekosReady)
                jatekosReady = true;

            if (ellensegReady && (dtJatekos == null || !dtJatekos.IsEnabled))
            {              
                vm.JatekosInit();
                vm.JatekosUzenet = "";                

                dtJatekos = new DispatcherTimer();
                dtJatekos.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dtJatekos.Tick += Dt_Tick;
                dtJatekos.Start();

                if (dtEllenseg == null || !dtEllenseg.IsEnabled)
                    EllensegStart();
                jatekosReady = false;                
            }
            else
                vm.JatekosUzenet = "Várakozás a másik játékosra";
        }

        private void EllensegStart()
        {
            if (!ellensegReady)
                ellensegReady = true;

            if (jatekosReady && (dtEllenseg == null || !dtEllenseg.IsEnabled))
            {
                vm.EllensegInit();
                vm.EllensegUzenet = "";                

                dtEllenseg = new DispatcherTimer();
                dtEllenseg.Interval = new TimeSpan(0, 0, 0, 0, 300);
                dtEllenseg.Tick += DtEllenseg_Tick;
                dtEllenseg.Start();

                if (dtJatekos == null || !dtJatekos.IsEnabled)
                    JatekosStart();
                ellensegReady = false;                
            }
            else
                vm.EllensegUzenet= "Várakozás a másik játékosra";
        }

        private void DtEllenseg_Tick(object sender, EventArgs e)
        {
            if (vm.Ellenseg.Move(dtEllenseg, dtJatekos, vm.Ellenseg, vm.Jatekos, vm.Map))
            {
                vm.GameOver(vm.Ellenseg, Player.ellenseg);
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
                case Key.NumPad0:
                    EllensegStart();
                    break;
                case Key.Up:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.Direct != Direction.le)
                        vm.Ellenseg.Direct = Direction.fel;
                    break;
                case Key.Down:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.Direct != Direction.fel)
                        vm.Ellenseg.Direct = Direction.le;
                    break;
                case Key.Left:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.Direct != Direction.jobbra)
                        vm.Ellenseg.Direct = Direction.balra;
                    break;
                case Key.Right:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.Direct != Direction.balra)
                        vm.Ellenseg.Direct = Direction.jobbra;
                    break;
            }
        }
    }
}
