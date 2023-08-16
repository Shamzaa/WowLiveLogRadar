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
            var playersToRender = state.GetPlayersToRender();
            var worldMarkersToRender = state.GetWorldMarkersToRender();
            var debuffDropLocationsToRender = state.GetIndicatorsToRender();

            foreach (var indicatorEntity in debuffDropLocationsToRender) {
                DrawIndicator(indicatorEntity, e);
            }

            foreach (var playerEntity in playersToRender) {
                DrawPlayer(playerEntity, e);
            }

            foreach (var worldMarkerEntity in worldMarkersToRender) {
                DrawWorldMarker(worldMarkerEntity.X, worldMarkerEntity.Y, worldMarkerEntity.RenderIdentifier, worldMarkerEntity.IsOnField, e);
            }

        }
        private void DrawIndicator(LogHook.Entity indicator, PaintEventArgs e) {
            if (!indicator.IsOnField) return;

            float x1 = ((indicator.X + 2520) / (-2440 + 2520)) * 800;
            float y1 = ((2460 - indicator.Y) / (2460 - 2380)) * 800;

            var graphics = e.Graphics;

            if (indicator.IsHighlighted) {

                var highLightPath = new System.Drawing.Drawing2D.GraphicsPath();
                highLightPath.AddEllipse(x1 - 10, y1 - 10, 40, 40);
                var highLightRegion = new Region(highLightPath);

                var h = indicator.HighlightColour;
                var highlightBrush = new SolidBrush(Color.FromArgb(180, h.R, h.G, h.B));
                graphics.FillRegion(highlightBrush, highLightRegion);
            }

        }

        private void DrawPlayer(LogHook.Entity player, PaintEventArgs e) {
            if (!player.IsOnField) return;

            float x1 = ((player.X + 2520) / (-2440 + 2520))*800;
            float y1 = ((2460 - player.Y) / (2460 - 2380))*800;
            
            var graphics = e.Graphics;

            if(player.IsHighlighted) {

                var highLightPath= new System.Drawing.Drawing2D.GraphicsPath();
                highLightPath.AddEllipse(x1 - 10, y1 - 10, 40, 40);
                var highLightRegion = new Region(highLightPath);

                var h = player.HighlightColour;
                var highlightBrush = new SolidBrush(Color.FromArgb(180, h.R, h.G, h.B));
                graphics.FillRegion(highlightBrush, highLightRegion);
            }

            var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(x1, y1, 20, 20);
            var region = new Region(path);
            // change colour with class colour
            var c = GetRGBFromPlayerClass(player.RenderIdentifier);
            var brush = new SolidBrush(Color.FromArgb(255, c.R, c.G, c.B));
            graphics.FillRegion(brush, region);

        }

        private (int R, int G, int B) GetRGBFromPlayerClass(string playerClass) {
            switch (playerClass) {
                case "Death Knight":
                    return (196, 30, 58);
                case "Demon Hunter":
                    return (163, 48, 201);
                case "Druid":
                    return (255, 124, 10);
                case "Evoker":
                    return (51, 147, 127);
                case "Hunter":
                    return (170, 211, 114);
                case "Mage":
                    return (63, 199, 234);
                case "Monk":
                    return (0, 255, 152);
                case "Paladin":
                    return (244, 140, 186);
                case "Priest":
                    return (255, 255, 255);
                case "Rogue":
                    return (255, 244, 104);
                case "Shaman":
                    return (0, 112, 221);
                case "Warlock":
                    return (135, 136, 238);
                case "Warrior":
                    return (198, 155, 109);
                default:
                    throw new Exception($"unhandled class {playerClass}");
            }
        }

        private void DrawWorldMarker(float x, float y, string markerIdentifier, bool onField, PaintEventArgs e) {
            if (!onField) return;

            // todo: change with icons
            //var path = new System.Drawing.Drawing2D.GraphicsPath();
            float x1 = ((x + 2520) / (-2440 + 2520)) * 800;
            float y1 = ((2460 - y) / (2460 - 2380)) * 800;
            //path.AddEllipse(x1, y1, 10, 10);
            //var region = new Region(path);
            var graphics = e.Graphics;
            var image = GetImageFromWorldMarkerName(markerIdentifier);
            graphics.DrawImage(image, x1, y1, 30, 30);
            //graphics.FillRegion(Brushes.White, region);

        }

        private Image GetImageFromWorldMarkerName(string worldMarkerName) {
            switch (worldMarkerName) {
                case "blue":
                    return Properties.Resources.blue;
                case "green":
                    return Properties.Resources.green;
                case "purple":
                    return Properties.Resources.purple;
                case "red":
                    return Properties.Resources.red;
                case "yellow":
                    return Properties.Resources.yellow;
                case "orange":
                    return Properties.Resources.orange;
                case "silver":
                    return Properties.Resources.silver;
                case "skull":
                    return Properties.Resources.skull;
                default:
                    throw new Exception($"unknown marker name {worldMarkerName}");
            }
        }

        private void StartHook() {
            RunHookAsync();
        }

        private async void RunHookAsync() {
            await Task.Run(() => {
                LogHook.LogHook.ReadFile("C:\\Users\\Shamzaa\\scripts\\WowLiveLogRadar\\CombatLogEmulator\\outtest.txt");
                //LogHook.LogHook.ReadFile("C:\\Program Files (x86)\\World of Warcraft\\_retail_\\Logs\\WoWCombatLog-081523_222432.txt");
            });
        }

    }
}
