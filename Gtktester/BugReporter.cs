// /**
// * Author: Mike Santiago
// */
using System;
using System.Diagnostics;
using System.Net.Mail;
using Gtk;
using System.Net;
using System.Text;

namespace Gtktester
{
    internal class SystemInformation
    {
        public bool RunOnMono { get; set; }
        public bool CompiledOnMono {get;set;}
        public InternalOperatingSystem MonoDetectedOperatingSystem {get;set;}
        public Version CLRVersion {get;set;}
        public OperatingSystem OS {get;set;}
        public bool Is64Bit {get;set;}
        public string OSString {get;set;}

        public SystemInformation()
        {
            #if __MonoCS__
            this.CompiledOnMono = true;
            #else
            this.CompiledOnMono = false;
            #endif

            if (Type.GetType("Mono.Runtime") != null)
                RunOnMono = true;
            else
                RunOnMono = false;

            MonoDetectedOperatingSystem = Internals.CurrentOS;
            CLRVersion = Environment.Version;
            OS = Environment.OSVersion;
            Is64Bit = Environment.Is64BitOperatingSystem;
            OSString = OS.VersionString;
        }
    }

    public partial class BugReporter : Gtk.Window
    {
        private SystemInformation SysInfo;
        public BugReporter()
            : base(Gtk.WindowType.Toplevel)
        {
            SysInfo = new SystemInformation();
            this.Build();

            PopulateInfo();
        }

        public BugReporter(string additionalCommentsText) : base(Gtk.WindowType.Toplevel)
        {
            SysInfo = new SystemInformation();
            this.Build();

            PopulateInfo();
            textview2.Buffer.Text = additionalCommentsText;
        }

        public void SetAdditionalCommentsText(string text)
        {
            textview2.Buffer.Text = text;
        }

        private void PopulateInfo()
        {
            string InformationAsString = String.Format(
                "Running on Mono: {0}\n" +
                "Compiled on Mono: {1}\n" +
                "Mono Detected Operating System: {2}\n" +
                "CLRVersion: {3}\n" +
                "Platform: {4}\n" +
                "OS Version: {5}\n" +
                "64-Bit: {6}\n", SysInfo.RunOnMono, SysInfo.CompiledOnMono, SysInfo.MonoDetectedOperatingSystem.ToString(),
                SysInfo.CLRVersion.ToString(), SysInfo.OS.Platform, SysInfo.OS.Version.ToString(), SysInfo.Is64Bit
            );
            textview1.Buffer.Text = InformationAsString;
        }

        protected void OnButton16Clicked (object sender, EventArgs e)
        {
            Process.Start("http://www.github.com/Luigifan/LuaScriptsManager/issues");
        }

        protected void OnButton15Clicked (object sender, EventArgs e)
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.None, "Sending bug report..");
            md.Icon = Image.LoadFromResource("Gtktester.Icons.PNG.256.png").Pixbuf;
            md.WindowPosition = WindowPosition.Center;

            using (WebClient client = new WebClient())
            {
                Uri urlll = new Uri(Uri.EscapeUriString(
                                    String.Format(
                                        "http://mrmiketheripper.x10.mx/bugreports/luamodulemanager/?runningonmono={0}&compiledonmono={1}&clrversion={2}&platform={3}&osversion={4}&64bit={5}&additionalcomments={6}", 
                                        SysInfo.RunOnMono.ToString(), SysInfo.CompiledOnMono.ToString(), SysInfo.CLRVersion.ToString(), SysInfo.OS.Platform.ToString(),
                                        SysInfo.OS.Version.ToString(), SysInfo.Is64Bit.ToString(), textview2.Buffer.Text)
                                ));
                #if DEBUG
                Console.WriteLine(urlll);
                #endif
                string responseBody = client.DownloadString(urlll);

                if(responseBody == "1Sent")
                    md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Sent okay!\nResponse: {0}", responseBody);
                else
                    md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Error sending!\nResponse: {0}", responseBody);
                md.Run();
                md.Destroy();
            }

            this.Destroy();
        }

    }
}

