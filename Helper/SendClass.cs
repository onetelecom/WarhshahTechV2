using System;
using System.Collections.Generic;
using System.Text;

namespace Taqnyat.JSONBody
{
    public class SendClass
    {
        private string _recipients;

        private string _body;

        private string _sender;

        private string _scheduledDatetime;

        private string _smsId;

        public string recipients
        {
            get
            {
                return _recipients;
            }
            set
            {
                _recipients = value;
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

        public string sender
        {
            get
            {
                return _sender;
            }
            set
            {
                _sender = value;
            }
        }

        public string scheduledDatetime
        {
            get
            {
                return _scheduledDatetime;
            }
            set
            {
                _scheduledDatetime = value;
            }
        }

        public string smsId
        {
            get
            {
                return _smsId;
            }
            set
            {
                _smsId = value;
            }
        }

        public SendClass()
        {
            _recipients = string.Empty;
            _body = string.Empty;
            _sender = string.Empty;
            _scheduledDatetime = string.Empty;
            _smsId = string.Empty;
        }
    }
}
