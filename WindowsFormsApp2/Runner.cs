using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Diagnostics.Eventing;

namespace WindowsFormsApp2
{
   public class Runner
    {
        public static void WhatTimesIs()
        {
            Speaker.Speak(DateTime.Now.ToShortTimeString());
        }
        public static void WhatDataIs()
        {
            Speaker.Speak(DateTime.Now.ToShortDateString());
        }
        //=======================NOtas=======================
        public static void AbirNota()
        {
            Speaker.Speak("Abrindo");
            System.Diagnostics.Process.Start("notepad.exe");
        }
        public static void FecharNotas()
        {
            foreach (var process in Process.GetProcessesByName("notepad"))
            {
                process.CloseMainWindow();
            Speaker.Speak("fechado");

            }


        }
        //=====================internet
        public static void OpenBrowser()
        {
            Speaker.Speak("Abrindo");
            System.Diagnostics.Process.Start("chrome.exe");
            
        }
        public static void PesquisaGoogle()
        {
            Speaker.Speak("Abrindo");
            System.Diagnostics.Process.Start("https://www.google.com/");

        }  
        public static void FecharGoogle()
        {
          foreach(var process in Process.GetProcessesByName("chrome"))
            {
                process.CloseMainWindow();
            }
                    
           
        }


    }
}
