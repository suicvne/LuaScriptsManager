// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.IO.Compression;

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
        return File.Exists(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe");
    }

    private void DownloadUpdate()
    {
        try
        {
            File.Move(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe",
                Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager_old.exe");
        }
        catch(IOException)
        {
            Console.WriteLine("No LuaModuleManager.exe already exists");
        }

        using (WebClient wc = new WebClient())
        {
            wc.DownloadFile(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/LuaModuleManager.zip"), Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip");
            using (ZipArchive archive = ZipFile.Open(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip", ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    try
                    {
                        entry.ExtractToFile(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + entry.FullName, true);
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
                File.Delete(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.zip");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Couldn't remove zip!\n{0}", ex.Message);
            }
            Console.WriteLine("Done!");
            Process.Start(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe");
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
