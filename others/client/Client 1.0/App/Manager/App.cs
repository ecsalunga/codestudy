using Genting.Infrastructure.CommonServices.Client.Manager;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Genting.Infrastructure.CommonServices.Client
{
    public partial class App : Form
    {
        public NotifyIcon Taskbar { get; set; }

        public ManagerCore Core { get; set; }

        public App()
        {
            InitializeComponent();

            this.Taskbar = new NotifyIcon();
            this.Taskbar.Icon = new Icon("Main.ico");
            this.Taskbar.Visible = true;
            this.Taskbar.DoubleClick += (sender, args) =>
            {
                this.show();
            };

            var menu = new ContextMenu();
            MenuItem mnuShow = new MenuItem("Show...");
            MenuItem mnuClose = new MenuItem("Close");

            menu.MenuItems.Add(mnuShow);
            menu.MenuItems.Add(mnuClose);
            mnuShow.Click += (sender, e) =>
            {
                this.show();
            };

            mnuClose.Click += (sender, e) =>
            {
                this.Close();
            };

            this.Taskbar.ContextMenu = menu;
            this.Core = new ManagerCore();
            this.Core.Init();
        }

        private void show()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.Taskbar.Visible = false;
        }

        private void App_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.TopMost = false;
                this.Taskbar.Visible = true;
            }
        }
    }
}
