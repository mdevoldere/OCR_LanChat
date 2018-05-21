//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Net;
//using System.Net.Sockets;

//namespace LanChatServer
//{
//    class ServerUdp
//    {
//        private UdpClient _broadcaster;

//        private Thread _thEcouteur;

//        private bool _continuer;

//        public ServerUdp()
//        {
//            _continuer = false;
//            _broadcaster = new UdpClient();
//            _broadcaster.EnableBroadcast = true;
//            _broadcaster.Connect(new IPEndPoint(IPAddress.Broadcast, 5053));
//        }

//        /// <summary>
//        /// Start Server.
//        /// </summary>
//        private void Start()
//        {
//            //Ajoute des informations dans le log et modification des bouttons.
//            /*btnStart.Enabled = false;
//            btnStop.Enabled = true;
//            AjouterLog("Server Start");*/

//            //Démarrage du thread d'écoute.
//            //_continuer = true;
//            //_thEcouteur = new Thread(new ThreadStart(EcouterReseau));
//            //_thEcouteur.Start();
//        }

//        /// <summary>
//        /// Stop Server.
//        /// </summary>
//        /// <param name="attendre">Définie si on attend que les threads aient terminé pour
//        /// continuer l'exécution du thread principal. True quand le programme se ferme.</param>
//        private void Stop(bool attendre)
//        {
//            /*
//            btnStart.Enabled = true;
//            btnStop.Enabled = false;
//            AjouterLog("Arrêt du serveur...");
//            */
//            _continuer = false;

//            // On attend le thread d'écoute seulement si on le demande et si ce dernier était réellement en train de fonctionner.
//            if (attendre && _thEcouteur != null && _thEcouteur.ThreadState == ThreadState.Running)
//                _thEcouteur.Join();
//        }


//        /// <summary>
//        /// Listen local network
//        /// </summary>
//        private void Listen()
//        {
//            //Création d'un Socket qui servira de serveur de manière sécurisée.
//            UdpClient updServer = null;

//            bool error = false;

//            int attempts = 0;

//            // 3 essais (éviter un plantage au serveur juste pour une question de millisecondes).
//            do
//            {
//                try
//                {
//                    updServer = new UdpClient(1523);
//                }
//                catch
//                {
//                    error = true;
//                    attempts++;
//                    Thread.Sleep(400);
//                }
//            } while (error && attempts < 4);

//            //Si c'est vraiment impossible de se lier, on en informe le serveur et on quitte le thread.
//            if (updServer == null)
//            {
//                this.Invoke(new Action<string>(AjouterLog), "Il est impossible de se lier au port 1523. Vérifiez votre configuration réseau.");
//                this.Invoke(new Action<bool>(ServerStop), false);
//                return;
//            }

//            updServer.Client.ReceiveTimeout = 1000;

//            //Boucle infinie d'écoute du réseau.
//            while (_continuer)
//            {
//                try
//                {
//                    IPEndPoint ip = null;
//                    byte[] data = updServer.Receive(ref ip);

//                    //Préparation des données à l'aide de la classe interne.
//                    CommunicationData cd = new CommunicationData(ip, data);
//                    //On lance un nouveau thread avec les données en paramètre.
//                    new Thread(new ParameterizedThreadStart(TraiterMessage)).Start(cd);
//                }
//                catch
//                {

//                }
//            }

//            updServer.Close();
//        }



//    }
//}
