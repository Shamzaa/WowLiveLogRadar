using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Rendering
{
    public partial class AnimatedMap : Form
    {
        public AnimatedMap() {
            InitializeComponent();
        }

        private Timer tmr = new Timer();
        private void LoadEvent(object sender, EventArgs e) {
            tmr.Interval = 100;
            tmr.Tick += new EventHandler(tmr_Tick);
            tmr.Start();
            // doubt this is how you're supposed to do it but xdd
            StartHook();
        }
        private void tmr_Tick(object sender, EventArgs e) {
            var debug = LogHook.EntityStateMaster.Instance.DebugEntityPositions();
            debugWindow.Text = debug;
            this.Invalidate();
        }
        private void DrawEvent(object sender, PaintEventArgs e) {
            var state = LogHook.EntityStateMaster.Instance;
            var entitiesToRender = state.GetEntitiesToRender();


            foreach (var entity in entitiesToRender) {
                DrawPlayer(entity.X, entity.Y, entity.IsOnField, e);
            }
        }

        private void DrawPlayer(float x, float y, bool onField, PaintEventArgs e) {
            if (!onField) return;

            var path = new System.Drawing.Drawing2D.GraphicsPath();
            float x1 = ((x + 2520) / (-2440 + 2520))*800;
            float y1 = ((2460 - y) / (2460 - 2380))*800;
            path.AddEllipse(x1, y1, 10, 10);
            var region = new System.Drawing.Region(path);
            var graphics = e.Graphics;
            graphics.FillRegion(Brushes.Red, region);

        }

        private void StartHook() {
            RunHookAsync();
        }

        private async void RunHookAsync() {
            await Task.Run(() => {
                LogHook.LogHook.ReadFile("C:\\Users\\Shamzaa\\scripts\\WowLiveLogRadar\\CombatLogEmulator\\outtest.txt");
            });
        }

    }
}
