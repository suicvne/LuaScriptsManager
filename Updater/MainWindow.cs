// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;
using Updater;

public partial class MainWindow: Gtk.Window
{
    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
        DoUpdateEvents();
    }

    private void DoUpdateEvents()
    {
        if (CheckIfCurExe())
        {
            DownloadUpdate();
        }
        else
        {
            MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok,
                                   "ERROR:\nCould not find your LuaModuleManager.exe.");
            md.Run();
            md.Destroy();
            this.Destroy();
            Application.Quit();
            Environment.Exit(-1);
        }
    }

    private bool CheckIfCurExe()
    {
        return File.Exists(Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe");
    }

    private void DownloadUpdate()
    {
        try
        {
            File.Move(Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe",
                Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager_old.exe");
        }
        catch(IOException)
        {
            Console.WriteLine("No LuaModuleManager.exe already exists");
        }

        using (WebClient wc = new WebClient())
        {
            wc.DownloadFile(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/LuaModuleManager.zip"), Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip");
            using (ZipArchive archive = ZipFile.Open(Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip", ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    try
                    {
                        entry.ExtractToFile(Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + entry.FullName, true);
                    }
                    catch(System.IO.IOException ioex)
                    {
                        if (ioex.Message.Contains("another process"))
                            Console.WriteLine("Skipping file");
                    }
                }
            }

            try
            {
                File.Delete(Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't remove zip!\n{0}", ex.Message);
            }
            Console.WriteLine("Done!");
            Process p = new Process();
            p.StartInfo.Verb = "runas";
            p.StartInfo.Arguments = Environment.CurrentDirectory;
            p.StartInfo.FileName = Program.ManagerDir + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe";
            p.Start();
            this.Destroy();
            Environment.Exit(0);
        }

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
        Environment.Exit(-1);
    }
    protected void OnDestroyEvent(object sender, DestroyEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
        Environment.Exit(-1);
    }
}
