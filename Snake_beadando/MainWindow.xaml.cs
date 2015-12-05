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
        DispatcherTimer dtKaja;
        bool jatekosReady;
        bool ellensegReady;
        static Random R;

        public MainWindow()
        {
            R = new Random();
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            vm = ViewModel.Get((int)this.ActualWidth, (int)this.ActualHeight);
            
            this.DataContext = vm;
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            if (!vm.Jatekos.ChangeAble)
                vm.Jatekos.ChangeAble = true;

            Status statusz = vm.Jatekos.Move(vm.Jatekos, vm.Ellenseg);
            if (statusz == Status.gameover)
            {
                vm.GameOver(vm.Jatekos, Player.jatekos, dtJatekos, dtEllenseg, dtKaja);
            }
        }

        private void KajaStart()
        {
            vm.Kaja = new Food(gridImg);
            vm.Rocket = new Rocket(gridImg);
            dtKaja = new DispatcherTimer();
            dtKaja.Interval = new TimeSpan(0, 0, 0, 0, R.Next(1500, 3500));
            dtKaja.Tick += DtKaja_Tick;
            dtKaja.Start();
        }

        private void DtKaja_Tick(object sender, EventArgs e)
        {
            dtKaja.Stop();
            if (R.Next(1, 101) <= 20)
                vm.Rocket.AddFood(R, vm.Jatekos, vm.Ellenseg);
            else
                vm.Kaja.AddFood(R, vm.Jatekos, vm.Ellenseg);
            dtKaja.Interval = new TimeSpan(0, 0, 0, 0, R.Next(1500, 3500));
            dtKaja.Start();
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

                KajaStart();
            }
            else if (dtJatekos == null || !dtJatekos.IsEnabled)
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
            else if (dtEllenseg == null || !dtEllenseg.IsEnabled)
                vm.EllensegUzenet = "Várakozás a másik játékosra";
        }

        private void DtEllenseg_Tick(object sender, EventArgs e)
        {
            if (!vm.Ellenseg.ChangeAble)
                vm.Ellenseg.ChangeAble = true;

            Status statusz = vm.Ellenseg.Move(vm.Ellenseg, vm.Jatekos);
            if (statusz == Status.gameover)
            {
                vm.GameOver(vm.Ellenseg, Player.ellenseg, dtJatekos, dtEllenseg, dtKaja);
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
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.ChangeAble && vm.Jatekos.Direct != Direction.le)
                    {
                        vm.Jatekos.Direct = Direction.fel;
                        vm.Jatekos.ChangeAble = false;
                    }
                    break;
                case Key.S:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.ChangeAble && vm.Jatekos.Direct != Direction.fel)
                    {
                        vm.Jatekos.Direct = Direction.le;
                        vm.Jatekos.ChangeAble = false;
                    }
                        break;
                case Key.A:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.ChangeAble && vm.Jatekos.Direct != Direction.jobbra)
                    {
                        vm.Jatekos.Direct = Direction.balra;
                        vm.Jatekos.ChangeAble = false;
                    }
                        break;
                case Key.D:
                    if (dtJatekos != null && dtJatekos.IsEnabled && vm.Jatekos.ChangeAble && vm.Jatekos.Direct != Direction.balra)
                    {
                        vm.Jatekos.Direct = Direction.jobbra;
                        vm.Jatekos.ChangeAble = false;
                    }
                    break;
                case Key.NumPad0:
                    EllensegStart();
                    break;
                case Key.Up:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.ChangeAble && vm.Ellenseg.Direct != Direction.le)
                    {
                        vm.Ellenseg.Direct = Direction.fel;
                        vm.Ellenseg.ChangeAble = false;
                    }
                    break;
                case Key.Down:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.ChangeAble && vm.Ellenseg.Direct != Direction.fel)
                    {
                        vm.Ellenseg.Direct = Direction.le;
                        vm.Ellenseg.ChangeAble = false;
                    }
                    break;
                case Key.Left:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.ChangeAble && vm.Ellenseg.Direct != Direction.jobbra)
                    {
                        vm.Ellenseg.Direct = Direction.balra;
                        vm.Ellenseg.ChangeAble = false;
                    }
                    break;
                case Key.Right:
                    if (dtEllenseg != null && dtEllenseg.IsEnabled && vm.Ellenseg.ChangeAble && vm.Ellenseg.Direct != Direction.balra)
                    {
                        vm.Ellenseg.Direct = Direction.jobbra;
                        vm.Ellenseg.ChangeAble = false;
                    }
                    break;
                case Key.Space:
                    vm.Jatekos.UseRocket(vm.Jatekos, vm.Ellenseg, vm, dtJatekos, dtEllenseg, dtKaja);
                    break;
                case Key.RightCtrl:
                    vm.Ellenseg.UseRocket(vm.Ellenseg, vm.Jatekos, vm, dtJatekos, dtEllenseg, dtKaja);
                    break;
            }
        }
    }
}
