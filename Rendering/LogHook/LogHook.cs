using LogHook.EventHandling;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace LogHook
{
    public static class LogHook
    {
        public static void ReadFile(string sourcePath) {

            var handler = new CombatLogEventHandler();

            var wh = new AutoResetEvent(false);
            var fsw = new FileSystemWatcher(".");
            fsw.Filter = sourcePath;
            fsw.EnableRaisingEvents = true;
            fsw.Changed += (s, e) => wh.Set();

            var fs = new FileStream(sourcePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(fs)) {

                Console.WriteLine("Going to end of file...");

                // jump to end of file
                if (sr.BaseStream.Length > 1024) {
                    sr.BaseStream.Seek(0, SeekOrigin.End);
                }

                Console.WriteLine("Hooked!");
                var s = "";
                string lastLine = "";
                while (true) {
                    s = sr.ReadToEnd();
                    if (!string.IsNullOrEmpty(s)) {

                        // if we read in the middle of a write, we might get an incomplete line at the end. To combat this we always store the last line.
                        // If the first line of the next batch isn't a start of a combatlog event, we merge previous and first, so it should be a complete line.
                        var lines = Regex.Split(s, "[\r\n]+").Where(l => !string.IsNullOrEmpty(l)).ToArray();
                        if (!IsStartOfCombatLogEvent(lines[0])) {
                            lines[0] = lastLine + lines[0];
                        }

                        lastLine = lines.Last();
                        lines = lines.Take(lines.Count() - 1).ToArray();
                        foreach (var line in lines) {
                            var regx = new Regex(',' + "(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            //var eventArguments = regx.Split(line);
                            string argumentsString = line.Split(new string[] { "  " }, StringSplitOptions.None)[1];
                            //var eventArguments = argumentsString.Split(',');
                            var eventArguments = regx.Split(argumentsString);
                            var eventType = eventArguments[0];

                            handler.Handle(eventType, eventArguments);

                        }
                    } else
                        // just so we don't continiously read the file without needing to
                        wh.WaitOne(100);
                }
            }


            bool IsStartOfCombatLogEvent(string line) {
                return Regex.IsMatch(line, "^[1-12]/[1-31]");
            }

        }
    }
}
