using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;


namespace GTSoft.CoreDotNet
{
    public class SMTP
    {
        #region Class Member Declarations

        protected int _port;
        protected string _server, _from, _from_display, _subject, _body;
        protected string[] _to_emails, _cc_emails;

        #endregion




        #region Contsructor

        public SMTP()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #endregion




        #region Public Methods

        public void Send_Mail()
        {
            try
            {
                SmtpClient client = new SmtpClient(_server, _port);
                MailAddress from = new MailAddress(_from, _from_display, System.Text.Encoding.UTF8);
                MailMessage message = new MailMessage();
                string to_email, cc_email;

                message.From = from;

                if (_to_emails != null)
                {
                    for (int i = 0; i <= _to_emails.GetUpperBound(0); i++)
                    {
                        to_email = _to_emails[i].ToString();

                        if (to_email.Trim() != "")
                            message.To.Add(to_email);
                    }
                }

                if (_cc_emails != null)
                {
                    for (int i = 0; i <= _cc_emails.GetUpperBound(0); i++)
                    {
                        cc_email = _cc_emails[i].ToString();

                        if(cc_email.Trim() !="")
                            message.CC.Add(cc_email);
                    }
                }

                message.Body = _body;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = _subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion




        #region Private Methods

        #endregion




        #region Class Property Declarations

        public int port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public string server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public string from
        {
            get
            {
                return _from;
            }
            set
            {
                _from = value;
            }
        }

        public string from_display
        {
            get
            {
                return _from_display;
            }
            set
            {
                _from_display = value;
            }
        }

        public string[] to_emails
        {
            get
            {
                return _to_emails;
            }
            set
            {
                _to_emails = value;
            }
        }

        public string[] cc_emails
        {
            get
            {
                return _cc_emails;
            }
            set
            {
                _cc_emails = value;
            }
        }

        public string subject
        {
            get
            {
                return _subject;
            }
            set
            {
                _subject = value;
            }
        }

        public string body
        {
            get
            {
                return _body;
            }
            set
            {
                _body = value;
            }
        }

        #endregion




    }
}
