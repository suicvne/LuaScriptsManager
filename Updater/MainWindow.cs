﻿// /**
// * Author: Mike Santiago
// */
using System;
using Gtk;
using System.IO;
using System.Net;
using System.Diagnostics;

public partial class MainWindow: Gtk.Window
{
    public MainWindow()
        : base(Gtk.WindowType.Toplevel)
    {
        Build();
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
            wc.DownloadDataCompleted += (object sender, DownloadDataCompletedEventArgs e) => 
                {
                    if(!e.Cancelled)
                    {                    
                        Console.WriteLine("Done!");
                        Process.Start(Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe");
                    }
                    this.Destroy();
                };

            wc.DownloadFileAsync(new Uri("http://mrmiketheripper.x10.mx/luamodulemanager/LuaModuleManager.exe"), Environment.CurrentDirectory + System.IO.Path.DirectorySeparatorChar + "LuaModuleManager.exe");

        }

    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }
}