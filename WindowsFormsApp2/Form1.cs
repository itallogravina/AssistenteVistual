using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Speech.Recognition;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
         
        private SpeechRecognitionEngine engine;//reconhecimento
        private bool isJarvisListening = true;

        public Form1()
        {
            InitializeComponent();
            
        }


        private void LoadSpeech()
        {
            try
            {
                engine = new SpeechRecognitionEngine();
                engine.SetInputToDefaultAudioDevice();

                Choices c_commandsOfSysten = new Choices();
                c_commandsOfSysten.Add(GrammarWord.WhatTimeIs.ToArray());//Que horas sao
                c_commandsOfSysten.Add(GrammarWord.WhatDataIs.ToArray());//Qual a data
                c_commandsOfSysten.Add(GrammarWord.OpenBrowser.ToArray());//Abir browser
                c_commandsOfSysten.Add(GrammarWord.AbrirNotas.ToArray());//Abrir Notas
                c_commandsOfSysten.Add(GrammarWord.JarvisSopListening.ToArray());//Para de ouvir
                c_commandsOfSysten.Add(GrammarWord.JarvisStartListening.ToArray());//escutar
                c_commandsOfSysten.Add(GrammarWord.MinimizarWindow.ToArray());
                c_commandsOfSysten.Add(GrammarWord.MaximizarWindow.ToArray());
                c_commandsOfSysten.Add(GrammarWord.NormalWindow.ToArray());

                c_commandsOfSysten.Add(GrammarWord.PesquisaGoogle.ToArray());
                c_commandsOfSysten.Add(GrammarWord.FecharGoogle.ToArray());

           
                c_commandsOfSysten.Add(GrammarWord.FecharNotas.ToArray());

                GrammarBuilder gb_commandOfSystem = new GrammarBuilder();
                gb_commandOfSystem.Append(c_commandsOfSysten);

                Grammar g_commandOfSystem = new Grammar(gb_commandOfSystem);
                g_commandOfSystem.Name = "sys";

                engine.LoadGrammar(g_commandOfSystem);//carregar gramatica

                //carregando gramatica
                //engine.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(word))));

                engine.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(rec);
                engine.AudioLevelUpdated += new EventHandler<AudioLevelUpdatedEventArgs>(AudioLevel);

                engine.RecognizeAsync(RecognizeMode.Multiple);//iniciar reconhecimento 
                Speaker.Speak("Olá");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ococrreu um erro no LoadSpeech" + ex.Message);
            }
        }

        private void Engine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSpeech();
            Speaker.Speak("Diga o que quer!","Como posso ajudar");
            
        }

        //metodo para o reconhecimento q
        private void rec(object s, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            float conf = e.Result.Confidence;
            
            if(conf> 0.35f)
            {
                if (GrammarWord.JarvisSopListening.Any(x=>x==speech))
                {
                    Speaker.Speak("Qual quer coisa me chame");
                    isJarvisListening = false;
                }
                else if (GrammarWord.JarvisStartListening.Any(x=>x==speech))
                {
                    isJarvisListening = true;
                    Speaker.Speak("Estou ouvindo","Diga o que quer ");
                }
                if (isJarvisListening == true) {
                    switch (e.Result.Grammar.Name)
                    {
                        case "sys":
                            //compara com a tabela 
                            if (GrammarWord.WhatTimeIs.Any(x => x == speech))
                            {
                                Runner.WhatTimesIs();
                            }
                            else if (GrammarWord.OpenBrowser.Any(x => x == speech))
                            {
                                Runner.OpenBrowser();
                            }
                            else if (GrammarWord.WhatDataIs.Any(x => x == speech))
                            {
                                Runner.WhatDataIs();
                            }
                            else if (GrammarWord.AbrirNotas.Any(x => x == speech))
                            {
                                Runner.AbirNota();
                            }
                            else if (GrammarWord.FecharNotas.Any(x => x == speech))
                            {
                                Runner.FecharNotas();
                            }
                            else if (GrammarWord.MinimizarWindow.Any(x => x == speech))
                            {
                                MinimizeWindow();
                            }
                            else if (GrammarWord.MaximizarWindow.Any(x => x == speech))
                            {
                                MaximizarWindow();
                            }
                            else if (GrammarWord.NormalWindow.Any(x => x == speech))
                            {
                                NormalWindow();
                            }
                            else if (GrammarWord.PesquisaGoogle.Any(x => x == speech))
                            {
                                Runner.PesquisaGoogle();
                            }
                            else if (GrammarWord.FecharGoogle.Any(x => x == speech))
                            {
                                Speaker.Speak("Fechando");
                                Runner.FecharGoogle();
                            }

                            break;
                    }
                }
            }
        }

        private void AudioLevel(object s, AudioLevelUpdatedEventArgs e)
        {
            this.progressBar1.Maximum = 100;
            this.progressBar1.Value = e.AudioLevel;
        }

        private void MinimizeWindow()
        {
            if (this.WindowState == FormWindowState.Normal||this.WindowState == FormWindowState.Maximized )
            {
                this.WindowState = FormWindowState.Minimized;
                Speaker.Speak("minimizado");
            }
            else 
            {
                Speaker.Speak("Já está minimizado");
            }
        }

        private void MaximizarWindow()
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                Speaker.Speak("Maximizado");
            }
            else
            {
                Speaker.Speak("Já está maximizado");
            }
        }

        private void NormalWindow()
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                Speaker.Speak("Pronto");
            }
            else
            {
                Speaker.Speak("Já está no estado que deseja");
            }
        }
    }
}
