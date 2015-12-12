#! /bin/sh

echo 'Downloading from http://netstorage.unity3d.com/unity/2524e04062b4/MacEditorInstaller/Unity-5.3.0f4.pkg: '
curl -o Unity.pkg http://netstorage.unity3d.com/unity/2524e04062b4/MacEditorInstaller/Unity-5.3.0f4.pkg

echo 'Installing Unity.pkg'
sudo installer -dumplog -package Unity.pkg -target /
