﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deliverance.OXMSG.Properties;
using Deliverance.OXCMSG.Properties;

namespace Deliverance.OXMSG
{
    //See https://msdn.microsoft.com/en-us/library/ee124371(v=exchg.80).aspx
    /// <summary>
    /// The type of email recipient
    /// </summary>
    public enum RecipientType
    {
        Originator,
        To,
        CC,
        BCC
    }
    /// <summary>
    /// This class represents an email recipient
    /// </summary>
    public class Recipient
    {
        internal PropertyStream PropertyStream { private get; set; }

        /// <summary>
        /// Returns the email address of the recipient
        /// </summary>
        public string EmailAddress
        {
            get
            {
                return PropertyStream.Data.OfType<VariableLengthPropertyEntry>()
                    .First(x => x.PropertyTag.ID == RecipientProperties.PidTagSmtpAddress).VariableLengthData.ToString();
            }
        }

        /// <summary>
        /// Returns the display name of the recipient
        /// </summary>
        public string DisplayName
        {
            get
            {
                return PropertyStream.Data.OfType<VariableLengthPropertyEntry>()
                    .First(x => x.PropertyTag.ID == RecipientProperties.PidTagDisplayName).VariableLengthData.ToString();
            }
        }

        /// <summary>
        /// Returns the type of recipient
        /// </summary>
        public RecipientType Type
        {
            get
            {
                return (RecipientType)BitConverter.ToInt32(PropertyStream.Data.First(x => x.PropertyTag.ID == RecipientProperties.PidTagRecipientType).Value, 0);
            }
        }
    }
}
