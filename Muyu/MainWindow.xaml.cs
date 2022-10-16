using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Muyu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int count=0;
        int step = 1;
        string tip = "功德++";
        SoundPlayer player = new SoundPlayer();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            count = 0;
            player.Stream= Properties.Resources.muyu_sound;
            InitializeComponent();
            txt_tips.Text = tip;
            txt_step.Text = step.ToString();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            timer.Tick += timer_Tick;
            timer.IsEnabled = true;
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if(count>0)
            {
                if (count > step)
                {
                    count -= step;
                }
                else
                {
                    count--;
                }
                lbl_count.Content = "功德值:" + this.count;
            }
            else
            {
                timer.Stop();
                btn_laugh.IsEnabled = true;
            }
        }

        private void img_muyu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Click();
        }


        private void Click()
        {
            count+=step;
            lbl_count.Content = "功德值:" + this.count;
            player.Play();
        }

        private void btn_setting_MouseEnter(object sender, MouseEventArgs e)
        {
            btn_setting.BorderBrush = Brushes.White;
            btn_setting.BorderThickness = new Thickness(2.0);
            btn_setting.Opacity += 0.3;
        }

        private void btn_setting_MouseLeave(object sender, MouseEventArgs e)
        {
            btn_setting.BorderBrush = null;
            btn_setting.BorderThickness = new Thickness(0.0);
            btn_setting.Opacity -= 0.3;
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            if (grd_setting.Visibility == Visibility.Visible)
            {
                grd_setting.Visibility = Visibility.Hidden;
            }
            else
            {
                grd_setting.Visibility = Visibility.Visible;
            }
        }

        private void txt_tips_TextChanged(object sender, TextChangedEventArgs e)
        {
            tip = txt_tips.Text;
            lbl_tip.Content = tip;
        }

        private void txt_step_TextChanged(object sender, TextChangedEventArgs e)
        {
            step = Convert.ToInt32(txt_step.Text);
        }
        public void limitnumber(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void btn_laugh_Click(object sender, RoutedEventArgs e)
        {
            btn_laugh.IsEnabled = false;
            timer.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Space))
            {
                img_muyu_MouseDown(sender, new MouseButtonEventArgs(Mouse.PrimaryDevice,0,MouseButton.Left));
                img_muyu.BeginStoryboard(scaledown);
                img_muyu.BeginStoryboard(scaleup, HandoffBehavior.Compose);
            }else if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                btn_laugh_Click(sender, new RoutedEventArgs());
            }
        }
    }
}
