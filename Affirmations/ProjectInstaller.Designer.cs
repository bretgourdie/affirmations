namespace Affirmations
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.affirmationProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.affirmationInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // affirmationProcessInstaller1
            // 
            this.affirmationProcessInstaller1.Password = null;
            this.affirmationProcessInstaller1.Username = null;
            // 
            // affirmationInstaller
            // 
            this.affirmationInstaller.Description = "Service to randomly encourage employees based on computer heuristics.";
            this.affirmationInstaller.DisplayName = "SDI Periodic Encouragement Service";
            this.affirmationInstaller.ServiceName = "Affirmations";
            this.affirmationInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.affirmationProcessInstaller1,
            this.affirmationInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller affirmationProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller affirmationInstaller;
    }
}