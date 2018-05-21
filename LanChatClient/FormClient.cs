using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace LanChatClient
{
    public partial class FormClient : Form
    {
        private StringBuilder error;

        private FormChatConnection formConnect;

        
        private UdpClient _client;

        private bool _continuer;

        private Thread _thEcouteur;

        /// <summary>
        /// Constructeur de la classe FormClient.
        /// </summary>
        public FormClient()
        {
            InitializeComponent();
            error = new StringBuilder();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            txtMessage.Text = " s'est connecté !";
            BtnSend_Click(new object(), new EventArgs());
        }

        public void Connect()
        {
            //On crée automatiquement le client qui sera en charge d'envoyer les messages au serveur.
            _client = new UdpClient();
            _client.Connect(formConnect.txtServer.Text, 1523);

            //Initialisation des objets nécessaires au client. On lance également le thread qui en charge d'écouter.
            _continuer = true;
            _thEcouteur = new Thread(new ThreadStart(ThreadEcouteur));
            _thEcouteur.Start();
            
        }

        public void SetCredentials(FormChatConnection _form)
        {
            formConnect = _form;

            txtResultat.Text = "Nom d'utilisateur: " + formConnect.txtUsername.Text;
            txtResultat.Text += "\nConnexion au serveur " + formConnect.txtServer.Text;
            
            Connect();
        }

        private void DisplayError(string _message, Exception ex)
        {
            error.Clear();
            error.AppendLine(_message);
            error.AppendLine(ex.Message);
            error.AppendLine(ex.StackTrace);
            MessageBox.Show(error.ToString(), ex.Source);
        }

        /// <summary>
        /// Gestion de l'envoi d'un message. Pas besoin d'un thread séparé pour cela, les données sont
        /// trop légères pour que ça en vaille la peine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSend_Click(object sender, EventArgs e)
        {
            txtMessage.Text = txtMessage.Text.Trim();

            if(txtMessage.Text.Length < 1)
            {
                return;
            }

            try
            {
                byte[] data = Encoding.Default.GetBytes(formConnect.txtUsername.Text + ": " + txtMessage.Text);
                
                _client.Send(data, data.Length);

                txtMessage.Clear();
                txtMessage.Focus();
            }
            catch(Exception ex)
            {
                DisplayError("Le message n'a pas été envoyé", ex);
            }

            
        }

        /// <summary>
        /// Fermeture du formulaire. Ecoute stoppée + attente fermeture des threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                formConnect.Close();
                _continuer = false;
                _client.Close();
                _thEcouteur.Join();
            }
            catch(Exception ex)
            {
                DisplayError("Impossible de fermer la connexion !", ex);
            }
            
        }

        /// <summary>
        /// Fonction en charge d'écouter les communications réseau.
        /// </summary>
        private void ThreadEcouteur()
        {
            //Déclaration du Socket d'écoute.
            UdpClient ecouteur = null;

            //Création sécurisée du Socket.
            try
            {
                ecouteur = new UdpClient(5053);
            }
            catch(Exception ex)
            {
                DisplayError("Impossible de se lier au port UDP 5053. Configuration réseau à vérifier.", ex);
                return;
            }

            //Définition du Timeout.
            ecouteur.Client.ReceiveTimeout = 1000;

            //Bouclage infini d'écoute de port.
            while (_continuer)
            {
                try
                {
                    IPEndPoint ip = null;
                    byte[] data = ecouteur.Receive(ref ip);

                    //Invocation du la méthode AjouterLog afin que les données soient inscrites dans
                    //la TextBox.
                    this.Invoke(new Action<string>(AjouterLog), Encoding.Default.GetString(data));
                }
                catch
                {
                    
                }
            }

            ecouteur.Close();
        }

        /// <summary>
        /// Ajouter un message à la console du Client.
        /// </summary>
        /// <param name="data"></param>
        private void AjouterLog(string data)
        {
            txtResultat.AppendText("\r\n" + data);
        }

        
    }
}