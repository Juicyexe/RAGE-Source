﻿using System;
using System.Threading;
using System.Windows.Forms;

namespace RAGE_UI
{
    class Functions
    {
        public static string exploitdllname = "RAGE.dll";
        public static void Inject()
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe exist
            {
                MessageBox.Show("Already injected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);//if the pipe exist that's mean that we don't need to inject
                return;
            }
            else if (!NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe don't exist
            {
                switch (Injector.DllInjector.GetInstance.Inject("RobloxPlayerBeta", AppDomain.CurrentDomain.BaseDirectory + exploitdllname))//Process name and dll directory
                {
                    case Injector.DllInjectionResult.DllNotFound://if can't find the dll
                        MessageBox.Show($"Couldn't find {exploitdllname}", "Dll was not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that dll was not found
                        return;
                    case Injector.DllInjectionResult.GameProcessNotFound://if can't find the process
                        MessageBox.Show("Couldn't find RobloxPlayerBeta.exe!", "Target process was not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that proccess was not found
                        return;
                    case Injector.DllInjectionResult.InjectionFailed://if injection fails(this don't work or only on special cases)
                        MessageBox.Show("Injection Failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that injection failed
                        return;
                }
                Thread.Sleep(3000);//pause the ui for 3 seconds
                if (!NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe dont exist
                {
                    MessageBox.Show("Injection Failed!\nPipe not found!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display that the pipe was not found so the injection was unsuccessful
                }
            }
        }
    }
}
