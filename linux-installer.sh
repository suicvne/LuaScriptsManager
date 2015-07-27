#!/bin/bash

DESKTOPLAUNCHERURL="http://mrmiketheripper.x10.mx/luamodulemanager/linux/"
LAUNCHSCRIPTURL="http://mrmiketheripper.x10.mx/luamodulemanager/linux/launch.sh"
LATESTJARURL="http://mrmiketheripper.x10.mx/luamodulemanager/LuaModuleManager.exe.zip"
ICONURL="http://mrmiketheripper.x10.mx/luamodulemanager/linux/512.png"

#0. Check for depencies
#1. Check if the dir ~/.mcmodinstaller exists
#2. Make if necessary
#3. wget the 3 necessary files
#4. Copy the .desktop to ~/.local/share/applications/

MONOVERSIONOUTPUT=`mono --version`
if [[ $MONOVERSIONOUTPUT == *"4.0"* ]] 
then
	echo Mono 4.0.2 installed!
else
  echo "Need to install latest Mono from repositories."
  sudo apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
  echo "deb http://download.mono-project.com/repo/debian wheezy main" | sudo tee /etc/apt/sources.list.d/mono-xamarin.list
  sudo apt-get update
  sudo apt-get install mono-complete
  
  echo "If any of these failed, you can try running sudo apt-get dist-upgrade"
  echo "Press enter to continue installation"
  echo -n1
fi

echo "Making necessary directories.."

if [ ! -d "$HOME/.luamodulemanager" ]; then
	mkdir "$HOME/.luamodulemanager"
fi

#echo "Checking for Java.."
#check if java even exists
echo "Retrieving files.."
#download
wget -q -O "$HOME/.luamodulemanager/LuaModuleManager.zip" $LATESTJARURL
#wget -q -O "$HOME/.local/share/applications/LunaLua Module Manager.desktop" $DESKTOPLAUNCHERURL
wget -q -O "$HOME/.luamodulemanager/icon.png" $ICONURL
wget -q -O "$HOME/.luamodulemanager/launch.sh" $LAUNCHSCRIPTURL

echo "Extracting.."
unzip "$HOME/.luamodulemanager/LuaModuleManager.zip" -d "$HOME/.luamodulemanager" 

echo "Writing .desktop.."
#
echo -e "#!/usr/bin/env xdg-open\n[Desktop Entry]\nVersion=1.0\nType=Application\nTerminal=false\nName=LunaLua Module Manager\nComment=\"Manager for LunaLua API scripts/modules\"\nExec=mono $HOME/.luamodulemanager/LuaModuleManager.exe\nIcon=$HOME/.luamodulemanager/icon.png" | tee "$HOME/.local/share/applications/luamodulemanager.desktop"
#
echo "Setting permissions.."
chmod +x "$HOME/.luamodulemanager/launch.sh"
chmod +x "$HOME/.local/share/applications/luamodulemanager.desktop"
#
echo "All files seem to be installed correctly!\n\n"
#
echo "Would you like us to make a script into your bin directory?"
echo "This process requires root access"
echo "It will write a simple bash script to your /usr/bin directory under the name \"luamodulemanager\" for access in Terminal"
echo "(y/n)"
read RESPONSE

if [[ $RESPONSE == *"y"* ]]
then
	echo -e "#!/bin/sh\n\nmono $HOME/.luamodulemanager/LuaModuleManager.exe" | sudo tee /usr/bin/luamodulemanager
	sudo chmod +x /usr/bin/luamodulemanager
	echo -e "All done!"
fi

echo "Press enter to exit.."
read -n1



