using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Marimba
{
    public partial class sendElectionEmail : Form
    {
        //the from and to email addresses
        MailAddress fromAddress, toAddress;
        //the message to be sent
        MailMessage message;
        //smtp is used to connect to gmail
        SmtpClient smtp;
        string subject, body, password;
        string[] strRecipients, strEmails;
        bool sendCode;
        int iRecipients;
        public sendElectionEmail(string strSubject, string strBody, string[] strRecipients, string[] strEmails, bool sendCode)
        {
            InitializeComponent();
            txtSubject.Text = strSubject;
            txtBody.Text = strBody;
            iRecipients = strRecipients.Length;
            this.strRecipients = strRecipients;
            this.strEmails = strEmails;
            this.sendCode = sendCode;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (sendable())
            {
                fromAddress = new MailAddress(txtEmail.Text, clsStorage.currentClub.strName);
                password = txtPassword.Text;
                subject = txtSubject.Text;
                body = txtBody.Text;
                //set up connection to gmail account
                smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, password),
                    Timeout = 20000
                };
                if (sendMail(strEmails, strRecipients))
                {
                    if (Properties.Settings.Default.playSounds)
                        sound.success.Play();
                    MessageBox.Show("E-Mail Successfully Sent.");
                    clsStorage.currentClub.addHistory("electors", history.changeType.sentEmail);
                }
            }
            else
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("E-Mail was not sent. Please confirm all information has been added.");
            }
        }

        bool sendable()
        {
            return (txtEmail.Text != "" && txtBody.Text != "" && txtPassword.Text != "" && txtSubject.Text != "");
        }

        bool sendMail(string[] mailinglist, string[] names)
        {
            try
            {
                for (int i = 0; i < iRecipients; i++)
                {
                    //attach the code if this isn't a reminder e-mail
                    if (sendCode)
                        subject += "/r/nCode: " + clsStorage.currentClub.currentElection.electorList[i].strCode;
                    toAddress = new MailAddress(mailinglist[i], names[i]);
                    using (message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                //we made it here, so the e-mail was sent.
                return true;
            }
            catch (SmtpException)
            {
                if (Properties.Settings.Default.playSounds)
                    sound.error.Play();
                MessageBox.Show("Unable to authenticate club account. Please confirm the correct password was entered.");
                return false;
            }
        }
    }
}
