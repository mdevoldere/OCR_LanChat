using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LanChatServer
{
    public partial class FormServer : Form
    {
        private UdpClient broadCaster;

        private bool _continue = true;

        private Thread thListener;

        /// <summary>
        /// Constructeur du formulaire Serveur.
        /// </summary>
        public FormServer()
        {
            InitializeComponent();
            
            broadCaster = new UdpClient() { EnableBroadcast = true };

            broadCaster.Connect(new IPEndPoint(IPAddress.Broadcast, 5053));
        }

        /// <summary>
        /// Start Server
        /// </summary>
        private void ServerStart()
        {
            //Ajoute des informations dans le log et modification des bouttons.
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            AddLog("Starting Server...");

            //Démarrage du thread d'écoute.
            _continue = true;
            thListener = new Thread(new ThreadStart(ListenNetwork));
            thListener.Start();
        }

        /// <summary>
        /// Méthode Arrêter qui sera appelée lorsqu'on aura besoin d'arrêter le serveur.
        /// </summary>
        /// <param name="_wait">Définie si on attend que les threads aient terminé pour
        /// continuer l'exécution du thread principal. True quand le programme se ferme.</param>
        private void ServerStop(bool _wait)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            AddLog("Serveur hors ligne");

            _continue = false;

            //On attend le thread d'écoute seulement si on le demande et si ce dernier était réellement en train de fonctionner.
            if (_wait && thListener != null && thListener.ThreadState == ThreadState.Running)
                thListener.Join();
        }

        /// <summary>
        /// Méthode qui écoute le réseau en permance en quête d'un message UDP sur le port 1523.
        /// </summary>
        private void ListenNetwork()
        {
            //Création d'un Socket qui servira de serveur de manière sécurisée.
            UdpClient localServer = null;
            bool localError = false;
            int attempts = 0;

            //J'essaie 3 fois car je veux éviter un plantage au serveur juste pour une question de millisecondes.
            do
            {
                try
                {
                    Thread.Sleep(600);
                    localServer = new UdpClient(1523);
                }
                catch
                {
                    localError = true;
                    attempts++;
                    //Thread.Sleep(500);
                }
            } while (localError && attempts < 4);

            //Si c'est vraiment impossible de se lier, on en informe le serveur et on quitte le thread.
            if (localServer == null)
            {
                this.Invoke(new Action<string>(AddLog), "Le port UPD 1523 est indisponible. Vérifiez la configuration réseau !");
                this.Invoke(new Action<bool>(ServerStop), false);
                return;
            }
            else
            {
                this.Invoke(new Action<string>(AddLog), "Serveur En ligne !");
            }

            localServer.Client.ReceiveTimeout = 1000;

            // Ecoute du réseau.
            while (_continue)
            {
                try
                {
                    IPEndPoint ip = null;
                    byte[] data = localServer.Receive(ref ip);

                    //Préparation des données à l'aide de la classe interne.
                    MessageData cd = new MessageData(ip, data);
                    //On lance un nouveau thread avec les données en paramètre.
                    new Thread(new ParameterizedThreadStart(ProcessMessage)).Start(cd);
                }
                catch // (Exception ex)
                {
                    //this.Invoke(new Action<string>(AjouterLog), ex.Message);
                }
            }

            localServer.Close();
        }

        /// <summary>
        /// Traitement d'un message entrant.
        /// </summary>
        /// <param name="messageArgs"></param>
        private void ProcessMessage(object messageArgs)
        {
            try
            {
                //On récupère les données entrantes et on les formatte comme il faut.
                MessageData data = messageArgs as MessageData;
                string message = string.Format("{0}:{1} > {2}", data.Client.Address.ToString(), data.Client.Port, Encoding.Default.GetString(data.Data));

                string clientMessage = string.Format("{0}", Encoding.Default.GetString(data.Data));
                
                //On renvoie le message formatté à travers le réseau.
                byte[] donnees = Encoding.Default.GetBytes(clientMessage);
                broadCaster.Send(donnees, donnees.Length);
                this.Invoke(new Action<string>(AddLog), message);
            }
            catch { }
        }

        /// <summary>
        /// Méthode en charge d'ajouter au Log les messages.
        /// </summary>
        /// <param name="message"></param>
        private void AddLog(string message)
        {
            txtLog.AppendText(DateTime.Now.ToUniversalTime() + ": " + message + "\r\n");
        }

        /// <summary>
        /// Gestion du bouton Arrêt.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStop_Click(object sender, EventArgs e)
        {
            byte[] donnees = Encoding.Default.GetBytes("Le serveur s'est arrêté");
            broadCaster.Send(donnees, donnees.Length);

            ServerStop(false);
        }

        /// <summary>
        /// Gestion du bouton Démarrer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStart_Click(object sender, EventArgs e)
        {
            ServerStart();
            byte[] donnees = Encoding.Default.GetBytes("Le serveur a démarré");
            broadCaster.Send(donnees, donnees.Length);
        }

        /// <summary>
        /// Gestion de la fermeture du formulaire.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmServer_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerStop(true);
        }
    }
}