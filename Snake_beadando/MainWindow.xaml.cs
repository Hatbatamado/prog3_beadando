﻿using System;
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
            vm.Jatekos.Move(dtJatekos, vm.Ellenseg, vm.Map);
        }

        private void JatekosStart()
        {
            if (dtJatekos == null || !dtJatekos.IsEnabled)
            {
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
                case Key.S:
                    JatekosStart();
                    break;
                case Key.Up:
                    vm.Jatekos.Direct = Direction.fel;
                    break;
                case Key.Down:
                    vm.Jatekos.Direct = Direction.le;
                    break;
                case Key.Left:
                    vm.Jatekos.Direct = Direction.balra;
                    break;
                case Key.Right:
                    vm.Jatekos.Direct = Direction.jobbra;
                    break;
            }
        }
    }
}