using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Xml;
using ImapX;
using ImapX.Enums;

namespace Marimba
{
    class email
    {
        ImapClient client;
        SmtpClient sendClient;
        string strAddress, strPassword;
        public static System.Drawing.Font unseen = new System.Drawing.Font("Quicksand Bold", 9);
        public static System.Drawing.Font seen = new System.Drawing.Font("Quicksand Book", 9);

        public bool loggedIn;
        public email(string strAddress, string strPassword, string imapServer, bool bImapSSL, string smtpServer, int smtpPort, bool bSmtpSSL)
        {
            this.strAddress = strAddress;
            this.strPassword = strPassword;

            client = new ImapClient(imapServer, bImapSSL);
            sendClient = new SmtpClient(smtpServer);

            client.Behavior.MessageFetchMode = ImapX.Enums.MessageFetchMode.Minimal;
            client.Behavior.AutoDownloadBodyOnAccess = false;
            client.Behavior.AutoPopulateFolderMessages = false;

            //set up the smtp email server ports

            sendClient.Port = smtpPort;
            sendClient.Credentials = new System.Net.NetworkCredential(strAddress, strPassword);
            sendClient.EnableSsl = bSmtpSSL;

            //no logging in yet, so mark as false
            loggedIn = false;
        }

        public bool login()
        {
            loggedIn = false;
            //don't even try if there is no password
            if(strPassword != "")
                if(client.Connect())
                {
                    if (client.Login(strAddress, strPassword))
                        loggedIn = true;
                }
            return loggedIn;
        }

        public void changePassword(string strPassword)
        {
            this.strPassword = strPassword;
        }

        public List<ListViewItem> folderItems()
        {
            try
            {
                //first, we need to figure out how many items there are
                //this downloads just the numbers
                client.Folders.Inbox.Messages.Download("ALL", MessageFetchMode.None);
                //defaults to inbox
                int max = Convert.ToInt32(client.Folders.Inbox.Exists);
                List<ListViewItem> output = new List<ListViewItem>();
                string[] header = new string[2];

                string strDisplayName;
                //int totalMessage = client.Folders.Inbox.Messages.Count();
                for (int i = max - 1; i >= max - 40; i--)
                {
                    client.Folders.Inbox.Messages[i].Download();
                    if (client.Folders.Inbox.Messages[i].Date != null)
                    {
                        //check for a display name
                        //if none is present, show the email address instead
                        if (String.IsNullOrEmpty(client.Folders.Inbox.Messages[i].From.DisplayName))
                            strDisplayName = client.Folders.Inbox.Messages[i].From.Address;
                        else
                            strDisplayName = client.Folders.Inbox.Messages[i].From.DisplayName;

                        if (client.Folders.Inbox.Messages[i].Seen)
                            output.Add(new ListViewItem(new string[5] { strDisplayName, client.Folders.Inbox.Messages[i].Subject,
                client.Folders.Inbox.Messages[i].Date.ToString(), formatFileSize(client.Folders.Inbox.Messages[i].Size), i.ToString()}, -1, System.Drawing.SystemColors.WindowText,
                    System.Drawing.SystemColors.Window, seen));
                        else
                            output.Add(new ListViewItem(new string[5] { strDisplayName, client.Folders.Inbox.Messages[i].Subject,
                client.Folders.Inbox.Messages[i].Date.ToString(), formatFileSize(client.Folders.Inbox.Messages[i].Size), i.ToString()}, -1, System.Drawing.SystemColors.WindowText,
                    System.Drawing.SystemColors.Window, unseen));
                    }
                }
                return output;
            }
            catch (ArgumentOutOfRangeException)
            {
                return new List<ListViewItem>();
            }
            catch
            {
                return new List<ListViewItem>();
            }
        }

        public List<ListViewItem> folderItems(int iStart, int iMore)
        {
            //first, we need to figure out how many items there are
            //this downloads just the numbers
            client.Folders.Inbox.Messages.Download("ALL", MessageFetchMode.None);
            //defaults to inbox
            int max = Convert.ToInt32(client.Folders.Inbox.Exists);
            List<ListViewItem> output = new List<ListViewItem>();
            string[] header = new string[2];

            string strDisplayName;

            for (int i = max - iStart - 1; i >= max - iStart - iMore; i--)
            {
                client.Folders.Inbox.Messages[i].Download();
                //check for a display name
                //if none is present, show the email address instead
                if (String.IsNullOrEmpty(client.Folders.Inbox.Messages[i].From.DisplayName))
                    strDisplayName = client.Folders.Inbox.Messages[i].From.Address;
                else
                    strDisplayName = client.Folders.Inbox.Messages[i].From.DisplayName;

                if(client.Folders.Inbox.Messages[i].Seen)
                    output.Add(new ListViewItem(new string[5] { strDisplayName, client.Folders.Inbox.Messages[i].Subject,
                client.Folders.Inbox.Messages[i].Date.ToString(), formatFileSize(client.Folders.Inbox.Messages[i].Size), i.ToString()}, -1, System.Drawing.SystemColors.WindowText,
                System.Drawing.SystemColors.Window, seen));
                else
                    output.Add(new ListViewItem(new string[5] { strDisplayName, client.Folders.Inbox.Messages[i].Subject,
                client.Folders.Inbox.Messages[i].Date.ToString(), formatFileSize(client.Folders.Inbox.Messages[i].Size), i.ToString()}, -1, System.Drawing.SystemColors.WindowText,
                System.Drawing.SystemColors.Window, unseen));
            }
            return output;
        }

        public ImapX.Message returnMessage(int messageIndex)
        {
            //download the entire message, then return it
            client.Folders.Inbox.Messages[messageIndex].Download(ImapX.Enums.MessageFetchMode.Basic);
            //mark the message as having been seen
            client.Folders.Inbox.Messages[messageIndex].Seen = true;
            return client.Folders.Inbox.Messages[messageIndex];
        }

        public bool sendMessage(string[] toAddress, string[] toName, string strSubject, string strHTML, emailBrowser.purpose purpose= emailBrowser.purpose.send)
        {
            //set up a try, catch thing... sending emails is tricky business
            try
            {
                MailMessage mail = new MailMessage();
                //Note to self: change this
                mail.From = new System.Net.Mail.MailAddress(strAddress,clsStorage.currentClub.strName);

                //check that the address and name arrays are the same
                int iLength = toAddress.Length;
                if (iLength != toName.Length)
                    return false;
                else
                {
                    //add the to names
                    //if bcc'ing, then only bcc
                    if(purpose == emailBrowser.purpose.send || purpose == emailBrowser.purpose.forward || purpose == emailBrowser.purpose.reply)
                        for (int i = 0; i < iLength; i++)
                            mail.To.Add(new System.Net.Mail.MailAddress(toAddress[i], toName[i]));
                    else
                        for (int i = 0; i < iLength; i++)
                            mail.Bcc.Add(new System.Net.Mail.MailAddress(toAddress[i], toName[i]));

                    //now set the meat of the message
                    mail.Subject = strSubject;
                    mail.IsBodyHtml = true;
                    mail.Body = strHTML;

                    //send it!
                    sendClient.Send(mail);
                    //success!
                    return true;
                }                   
                
            }
            catch
            {
                return false;
            }
        }

        public void logout()
        {
            try
            {
                if (loggedIn)
                    client.Logout();
            }
            catch
            {
                //if this fails, it is because of timeout issues
                //in my opinion, this is not an issue
            }
        }

        public static string formatFileSize(long bytes)
        {
            if (bytes < 1024)
                return bytes + " B";

            bytes = bytes / 1024;

            if (bytes < 1024)
                return bytes + " KB";

            bytes = bytes / 1024;


            return bytes + " MB";
        }

        /// <summary>
        /// Creates signature to attach at the end of the email
        /// </summary>
        /// <returns>HTML code of the signature</returns>
        public string createSignature()
        {
            string output = "<div><p class=\"ecxMsoNormal\"><span style=\"font-size:12.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D;\">--- <br><b>~" + Properties.Settings.Default.signatureName;
            if (!String.IsNullOrEmpty(Properties.Settings.Default.signaturePosition))
                output += "<br>" + Properties.Settings.Default.signaturePosition;
            output += "<br>UW Concert Band Club</b><br><a href=\"mailto:uwconcertbandclub@gmail.com\" target=\"_blank\">uwconcertbandclub@gmail.com</a><br><a href=\"http://uwcbc.uwaterloo.ca/\" target=\""+
                "_blank\">http://uwcbc.uwaterloo.ca</a><br><a href=\"http://tinyurl.com/uwcbc\" target=\"_blank\">Facebook</a><br><br></span><span style=\"font-size:7.5pt;font-family:&quot;Calibri&quot;,"+
                "&quot;sans-serif&quot;;color:#1F497D;\">UW Concert Band Club does not represent the <a href=\"http://feds.ca/\" target=\"_blank\">Federation of Students (FEDs)</a>.<br>To remove yourself "+
                "from this mailing list, <a href=\"mailto:uwconcertbandclub@gmail.com?subject=Unsubscribe\" target=\"_blank\">send an email to this account</a> with 'Unsubscribe' as the subject</span>"+
                "<span style=\"font-size:12.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;color:#1F497D;\">.</span></p></div>";
            return output;
        }

        public static string replyHeader(string strFromAddress, string strFromName, DateTime timeSent, string strTo, string strSubject, emailBrowser.purpose use)
        {
            //go through the various elements and implement them
            string output = "<p class=MsoNormal><span style='color:#1F497D'><o:p>&nbsp;</o:p></span></p><div><div style='border:none;border-top:solid #E1E1E1 1.0pt;padding:3.0pt 0cm 0cm 0cm'><p class=MsoNormal><b>";
            output += "<span lang=EN-US style='mso-fareast-language:EN-CA'>From:</span></b><span lang=EN-US style='mso-fareast-language:EN-CA'>";
            output += String.Format(" {0} [mailto:{1}] <br>", strFromName, strFromAddress);
            output += "<b>Sent:</b> " + timeSent.ToLongDateString() + " " + timeSent.ToLongTimeString() + "<br>";
            output += "<b>To:</b> " + strTo + "<br><b>Subject:</b> ";
            //correctly mark whether this is a reply or forwarding
            output += replySubject(strSubject, use);
            output += strSubject + "<o:p></o:p></span></p>";
            return output;
        }

        public static string replySubject(string strSubject, emailBrowser.purpose use)
        {
            if (use == emailBrowser.purpose.reply && !strSubject.StartsWith("re:", true, null))
                return "RE: " + strSubject;
            else if (use == emailBrowser.purpose.forward && !strSubject.StartsWith("fw:", true, null))
                return "FW: " + strSubject;
            else
                return strSubject;
        }
    }






    //Note: I borrowed the code to do this from here: http://code.msdn.microsoft.com/windowsdesktop/Converting-between-RTF-and-aaa02a6e/sourcecode?fileId=21412&pathId=403327065


     /// <summary> 
    /// RTFtoHTML is a static class that takes an HTML string 
    /// and converts it into XAML 
    /// </summary> 
    public static class RTFtoHTML
    { 
        // --------------------------------------------------------------------- 
        // 
        // Internal Methods 
        // 
        // --------------------------------------------------------------------- 
 
        #region Internal Methods 
 
        /// <summary> 
        /// Main entry point for Xaml-to-Html converter. 
        /// Converts a xaml string into html string. 
        /// </summary> 
        /// <param name="xamlString"> 
        /// Xaml strinng to convert. 
        /// </param> 
        /// <returns> 
        /// Html string produced from a source xaml. 
        /// </returns> 
        public static string ConvertXamlToHtml(string xamlString, bool asFullDocument) 
        { 
            XmlTextReader xamlReader; 
            StringBuilder htmlStringBuilder; 
            XmlTextWriter htmlWriter; 
 
            xamlReader = new XmlTextReader(new StringReader(xamlString)); 
 
            htmlStringBuilder = new StringBuilder(100); 
            htmlWriter = new XmlTextWriter(new StringWriter(htmlStringBuilder)); 
 
            if (!WriteFlowDocument(xamlReader, htmlWriter, asFullDocument)) 
            { 
                return ""; 
            } 
 
            string htmlString = htmlStringBuilder.ToString(); 
 
            return htmlString; 
        }

        public static string ConvertRtfToXaml(string rtfText)
        {
            var richTextBox = new System.Windows.Controls.RichTextBox();
            if (string.IsNullOrEmpty(rtfText)) return "";
            var textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
            using (var rtfMemoryStream = new MemoryStream())
            {
                using (var rtfStreamWriter = new StreamWriter(rtfMemoryStream))
                {
                    rtfStreamWriter.Write(rtfText);
                    rtfStreamWriter.Flush();
                    rtfMemoryStream.Seek(0, SeekOrigin.Begin);
                    textRange.Load(rtfMemoryStream, DataFormats.Rtf);
                }
            }
            using (var rtfMemoryStream = new MemoryStream())
            {
                textRange = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);
                textRange.Save(rtfMemoryStream, System.Windows.DataFormats.Xaml);
                rtfMemoryStream.Seek(0, SeekOrigin.Begin);
                using (var rtfStreamReader = new StreamReader(rtfMemoryStream))
                {
                    return rtfStreamReader.ReadToEnd();
                }
            }
        }

        private const string FlowDocumentFormat = "<FlowDocument>{0}</FlowDocument>";

        public static string ConvertRtfToHtml(string rtfText)
        {
            var xamlText = string.Format(FlowDocumentFormat, ConvertRtfToXaml(rtfText));

            return ConvertXamlToHtml(xamlText, false);
        } 
 
        #endregion Internal Methods 
 
        // --------------------------------------------------------------------- 
        // 
        // Private Methods 
        // 
        // --------------------------------------------------------------------- 
 
        #region Private Methods 
        /// <summary> 
        /// Processes a root level element of XAML (normally it's FlowDocument element). 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// XmlTextReader for a source xaml. 
        /// </param> 
        /// <param name="htmlWriter"> 
        /// XmlTextWriter producing resulting html 
        /// </param> 
        private static bool WriteFlowDocument(XmlTextReader xamlReader, XmlTextWriter htmlWriter, bool asFullDocument) 
        { 
            if (!ReadNextToken(xamlReader)) 
            { 
                // Xaml content is empty - nothing to convert 
                return false; 
            } 
 
            if (xamlReader.NodeType != XmlNodeType.Element || xamlReader.Name != "FlowDocument") 
            { 
                // Root FlowDocument elemet is missing 
                return false; 
            } 
 
            // Create a buffer StringBuilder for collecting css properties for inline STYLE attributes 
            // on every element level (it will be re-initialized on every level). 
            StringBuilder inlineStyle = new StringBuilder(); 
 
            if (asFullDocument) 
            { 
                htmlWriter.WriteStartElement("HTML"); 
                htmlWriter.WriteStartElement("BODY"); 
            } 
            WriteFormattingProperties(xamlReader, htmlWriter, inlineStyle); 
 
            WriteElementContent(xamlReader, htmlWriter, inlineStyle); 
 
            if (asFullDocument) 
            { 
                htmlWriter.WriteEndElement(); 
                htmlWriter.WriteEndElement(); 
            } 
            return true; 
        } 
 
        /// <summary> 
        /// Reads attributes of the current xaml element and converts 
        /// them into appropriate html attributes or css styles. 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// XmlTextReader which is expected to be at XmlNodeType.Element 
        /// (opening element tag) position. 
        /// The reader will remain at the same level after function complete. 
        /// </param> 
        /// <param name="htmlWriter"> 
        /// XmlTextWriter for output html, which is expected to be in 
        /// after WriteStartElement state. 
        /// </param> 
        /// <param name="inlineStyle"> 
        /// String builder for collecting css properties for inline STYLE attribute. 
        /// </param> 
        private static void WriteFormattingProperties(XmlTextReader xamlReader, XmlTextWriter htmlWriter, StringBuilder inlineStyle) 
        { 
            Debug.Assert(xamlReader.NodeType == XmlNodeType.Element); 
 
            // Clear string builder for the inline style 
            inlineStyle.Remove(0, inlineStyle.Length); 
 
            if (!xamlReader.HasAttributes) 
            { 
                return; 
            } 
 
            bool borderSet = false; 
 
            while (xamlReader.MoveToNextAttribute()) 
            { 
                string css = null; 
 
                switch (xamlReader.Name) 
                { 
                    // Character fomatting properties 
                    // ------------------------------ 
                    case "Background": 
                        css = "background-color:" + ParseXamlColor(xamlReader.Value) + ";"; 
                        break; 
                    case "FontFamily": 
                        css = "font-family:" + xamlReader.Value + ";"; 
                        break; 
                    case "FontStyle": 
                        css = "font-style:" + xamlReader.Value.ToLower() + ";"; 
                        break; 
                    case "FontWeight": 
                        css = "font-weight:" + xamlReader.Value.ToLower() + ";"; 
                        break; 
                    case "FontStretch": 
                        break; 
                    case "FontSize": 
                        css = "font-size:" + xamlReader.Value + ";"; 
                        break; 
                    case "Foreground": 
                        css = "color:" + ParseXamlColor(xamlReader.Value) + ";"; 
                        break; 
                    case "TextDecorations": 
                        if (xamlReader.Value.ToLower() == "strikethrough") 
                            css = "text-decoration:line-through;"; 
                        else 
                            css = "text-decoration:underline;"; 
                        break; 
                    case "TextEffects": 
                        break; 
                    case "Emphasis": 
                        break; 
                    case "StandardLigatures": 
                        break; 
                    case "Variants": 
                        break; 
                    case "Capitals": 
                        break; 
                    case "Fraction": 
                        break; 
 
                    // Paragraph formatting properties 
                    // ------------------------------- 
                    case "Padding": 
                        css = "padding:" + ParseXamlThickness(xamlReader.Value) + ";"; 
                        break; 
                    case "Margin": 
                        css = "margin:" + ParseXamlThickness(xamlReader.Value) + ";"; 
                        break; 
                    case "BorderThickness": 
                        css = "border-width:" + ParseXamlThickness(xamlReader.Value) + ";"; 
                        borderSet = true; 
                        break; 
                    case "BorderBrush": 
                        css = "border-color:" + ParseXamlColor(xamlReader.Value) + ";"; 
                        borderSet = true; 
                        break; 
                    case "LineHeight": 
                        break; 
                    case "TextIndent": 
                        css = "text-indent:" + xamlReader.Value + ";"; 
                        break; 
                    case "TextAlignment": 
                        css = "text-align:" + xamlReader.Value + ";"; 
                        break; 
                    case "IsKeptTogether": 
                        break; 
                    case "IsKeptWithNext": 
                        break; 
                    case "ColumnBreakBefore": 
                        break; 
                    case "PageBreakBefore": 
                        break; 
                    case "FlowDirection": 
                        break; 
 
                    // Table attributes 
                    // ---------------- 
                    case "Width": 
                        css = "width:" + xamlReader.Value + ";"; 
                        break; 
                    case "ColumnSpan": 
                        htmlWriter.WriteAttributeString("COLSPAN", xamlReader.Value); 
                        break; 
                    case "RowSpan": 
                        htmlWriter.WriteAttributeString("ROWSPAN", xamlReader.Value); 
                        break; 
 
                    // Hyperlink Attributes 
                    case "NavigateUri": 
                        htmlWriter.WriteAttributeString("HREF", xamlReader.Value); 
                        break; 
 
                    case "TargetName": 
                        htmlWriter.WriteAttributeString("TARGET", xamlReader.Value); 
                        break; 
                } 
 
                if (css != null) 
                { 
                    inlineStyle.Append(css); 
                } 
            } 
 
            if (borderSet) 
            { 
                inlineStyle.Append("border-style:solid;mso-element:para-border-div;"); 
            } 
 
            // Return the xamlReader back to element level 
            xamlReader.MoveToElement(); 
            Debug.Assert(xamlReader.NodeType == XmlNodeType.Element); 
        } 
 
        private static string ParseXamlColor(string color) 
        { 
            if (color.StartsWith("#")) 
            { 
                // Remove transparancy value 
                color = "#" + color.Substring(3); 
            } 
            return color; 
        } 
 
        private static string ParseXamlThickness(string thickness) 
        { 
            string[] values = thickness.Split(','); 
 
            for (int i = 0; i < values.Length; i++) 
            { 
                double value; 
                if (double.TryParse(values[i], out value)) 
                { 
                    values[i] = Math.Ceiling(value).ToString(); 
                } 
                else 
                { 
                    values[i] = "1"; 
                } 
            } 
 
            string cssThickness; 
            switch (values.Length) 
            { 
                case 1: 
                    cssThickness = thickness; 
                    break; 
                case 2: 
                    cssThickness = values[1] + " " + values[0]; 
                    break; 
                case 4: 
                    cssThickness = values[1] + " " + values[2] + " " + values[3] + " " + values[0]; 
                    break; 
                default: 
                    cssThickness = values[0]; 
                    break; 
            } 
 
            return cssThickness; 
        } 
 
        /// <summary> 
        /// Reads a content of current xaml element, converts it 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// XmlTextReader which is expected to be at XmlNodeType.Element 
        /// (opening element tag) position. 
        /// </param> 
        /// <param name="htmlWriter"> 
        /// May be null, in which case we are skipping the xaml element; 
        /// witout producing any output to html. 
        /// </param> 
        /// <param name="inlineStyle"> 
        /// StringBuilder used for collecting css properties for inline STYLE attribute. 
        /// </param> 
        private static void WriteElementContent(XmlTextReader xamlReader, XmlTextWriter htmlWriter, StringBuilder inlineStyle) 
        { 
            Debug.Assert(xamlReader.NodeType == XmlNodeType.Element); 
 
            bool elementContentStarted = false; 
 
            if (xamlReader.IsEmptyElement) 
            { 
                if (htmlWriter != null && !elementContentStarted && inlineStyle.Length > 0) 
                { 
                    // Output STYLE attribute and clear inlineStyle buffer. 
                    htmlWriter.WriteAttributeString("STYLE", inlineStyle.ToString()); 
                    inlineStyle.Remove(0, inlineStyle.Length); 
                } 
                elementContentStarted = true; 
            } 
            else 
            { 
                while (ReadNextToken(xamlReader) && xamlReader.NodeType != XmlNodeType.EndElement) 
                { 
                    switch (xamlReader.NodeType) 
                    { 
                        case XmlNodeType.Element: 
                            if (xamlReader.Name.Contains(".")) 
                            { 
                                AddComplexProperty(xamlReader, inlineStyle); 
                            } 
                            else 
                            { 
                                if (htmlWriter != null && !elementContentStarted && inlineStyle.Length > 0) 
                                { 
                                    // Output STYLE attribute and clear inlineStyle buffer. 
                                    htmlWriter.WriteAttributeString("STYLE", inlineStyle.ToString()); 
                                    inlineStyle.Remove(0, inlineStyle.Length); 
                                } 
                                elementContentStarted = true; 
                                WriteElement(xamlReader, htmlWriter, inlineStyle); 
                            } 
                            Debug.Assert(xamlReader.NodeType == XmlNodeType.EndElement || xamlReader.NodeType == XmlNodeType.Element && xamlReader.IsEmptyElement); 
                            break; 
                        case XmlNodeType.Comment: 
                            if (htmlWriter != null) 
                            { 
                                if (!elementContentStarted && inlineStyle.Length > 0) 
                                { 
                                    htmlWriter.WriteAttributeString("STYLE", inlineStyle.ToString()); 
                                } 
                                htmlWriter.WriteComment(xamlReader.Value); 
                            } 
                            elementContentStarted = true; 
                            break; 
                        case XmlNodeType.CDATA: 
                        case XmlNodeType.Text: 
                        case XmlNodeType.SignificantWhitespace: 
                            if (htmlWriter != null) 
                            { 
                                if (!elementContentStarted && inlineStyle.Length > 0) 
                                { 
                                    htmlWriter.WriteAttributeString("STYLE", inlineStyle.ToString()); 
                                } 
                                htmlWriter.WriteString(xamlReader.Value); 
                            } 
                            elementContentStarted = true; 
                            break; 
                    } 
                } 
 
                Debug.Assert(xamlReader.NodeType == XmlNodeType.EndElement); 
            } 
        } 
 
        /// <summary> 
        /// Conberts an element notation of complex property into 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// On entry this XmlTextReader must be on Element start tag; 
        /// on exit - on EndElement tag. 
        /// </param> 
        /// <param name="inlineStyle"> 
        /// StringBuilder containing a value for STYLE attribute. 
        /// </param> 
        private static void AddComplexProperty(XmlTextReader xamlReader, StringBuilder inlineStyle) 
        { 
            Debug.Assert(xamlReader.NodeType == XmlNodeType.Element); 
 
            if (inlineStyle != null && xamlReader.Name.EndsWith(".TextDecorations")) 
            { 
                inlineStyle.Append("text-decoration:underline;"); 
            } 
 
            // Skip the element representing the complex property 
            WriteElementContent(xamlReader, /*htmlWriter:*/null, /*inlineStyle:*/null); 
        } 
 
        /// <summary> 
        /// Converts a xaml element into an appropriate html element. 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// On entry this XmlTextReader must be on Element start tag; 
        /// on exit - on EndElement tag. 
        /// </param> 
        /// <param name="htmlWriter"> 
        /// May be null, in which case we are skipping xaml content 
        /// without producing any html output 
        /// </param> 
        /// <param name="inlineStyle"> 
        /// StringBuilder used for collecting css properties for inline STYLE attributes on every level. 
        /// </param> 
        private static void WriteElement(XmlTextReader xamlReader, XmlTextWriter htmlWriter, StringBuilder inlineStyle) 
        { 
            Debug.Assert(xamlReader.NodeType == XmlNodeType.Element); 
 
            if (htmlWriter == null) 
            { 
                // Skipping mode; recurse into the xaml element without any output 
                WriteElementContent(xamlReader, /*htmlWriter:*/null, null); 
            } 
            else 
            { 
                string htmlElementName = null; 
 
                switch (xamlReader.Name) 
                { 
                    case "Run" : 
                    case "Span": 
                        htmlElementName = "SPAN"; 
                        break; 
                    case "InlineUIContainer": 
                        htmlElementName = "SPAN"; 
                        break; 
                    case "Bold": 
                        htmlElementName = "B"; 
                        break; 
                    case "Italic" : 
                        htmlElementName = "I"; 
                        break; 
                    case "Paragraph" : 
                        htmlElementName = "P"; 
                        break; 
                    case "BlockUIContainer": 
                        htmlElementName = "DIV"; 
                        break; 
                    case "Section": 
                        htmlElementName = "DIV"; 
                        break; 
                    case "Table": 
                        htmlElementName = "TABLE"; 
                        break; 
                    case "TableColumn": 
                        htmlElementName = "COL"; 
                        break; 
                    case "TableRowGroup" : 
                        htmlElementName = "TBODY"; 
                        break; 
                    case "TableRow" : 
                        htmlElementName = "TR"; 
                        break; 
                    case "TableCell" : 
                        htmlElementName = "TD"; 
                        break; 
                    case "List" : 
                        string marker = xamlReader.GetAttribute("MarkerStyle"); 
                        if (marker == null || marker == "None" || marker == "Disc" || marker == "Circle" || marker == "Square" || marker == "Box") 
                        { 
                            htmlElementName = "UL"; 
                        } 
                        else 
                        { 
                            htmlElementName = "OL"; 
                        } 
                        break; 
                    case "ListItem" : 
                        htmlElementName = "LI"; 
                        break; 
                    case "Hyperlink": 
                        htmlElementName = "A"; 
                        break;
                    case "LineBreak":
                        htmlElementName = "BR";
                        break;
                    default : 
                        htmlElementName = null; // Ignore the element 
                        break; 
                } 
 
                if (htmlWriter != null && htmlElementName != null) 
                { 
                    htmlWriter.WriteStartElement(htmlElementName); 
 
                    WriteFormattingProperties(xamlReader, htmlWriter, inlineStyle); 
 
                    WriteElementContent(xamlReader, htmlWriter, inlineStyle); 
 
                    htmlWriter.WriteEndElement(); 
                } 
                else 
                { 
                    // Skip this unrecognized xaml element 
                    WriteElementContent(xamlReader, /*htmlWriter:*/null, null); 
                } 
            } 
        } 
 
        // Reader advance helpers 
        // ---------------------- 
                  
        /// <summary> 
        /// Reads several items from xamlReader skipping all non-significant stuff. 
        /// </summary> 
        /// <param name="xamlReader"> 
        /// XmlTextReader from tokens are being read. 
        /// </param> 
        /// <returns> 
        /// True if new token is available; false if end of stream reached. 
        /// </returns> 
        private static bool ReadNextToken(XmlReader xamlReader) 
        { 
            while (xamlReader.Read()) 
            { 
                Debug.Assert(xamlReader.ReadState == ReadState.Interactive, "Reader is expected to be in Interactive state (" + xamlReader.ReadState + ")"); 
                switch (xamlReader.NodeType) 
                { 
                    case XmlNodeType.Element:  
                    case XmlNodeType.EndElement: 
                    case XmlNodeType.None: 
                    case XmlNodeType.CDATA: 
                    case XmlNodeType.Text: 
                    case XmlNodeType.SignificantWhitespace: 
                        return true; 
 
                    case XmlNodeType.Whitespace: 
                        if (xamlReader.XmlSpace == XmlSpace.Preserve) 
                        { 
                            return true; 
                        } 
                        // ignore insignificant whitespace 
                        break; 
 
                    case XmlNodeType.EndEntity: 
                    case XmlNodeType.EntityReference: 
                        //  Implement entity reading 
                        //xamlReader.ResolveEntity(); 
                        //xamlReader.Read(); 
                        //ReadChildNodes( parent, parentBaseUri, xamlReader, positionInfo); 
                        break; // for now we ignore entities as insignificant stuff 
 
                    case XmlNodeType.Comment: 
                        return true; 
                    case XmlNodeType.ProcessingInstruction: 
                    case XmlNodeType.DocumentType: 
                    case XmlNodeType.XmlDeclaration: 
                    default: 
                        // Ignorable stuff 
                        break; 
                } 
            } 
            return false; 
        } 
 
        #endregion Private Methods 
 
        // --------------------------------------------------------------------- 
        // 
        // Private Fields 
        // 
        // --------------------------------------------------------------------- 
 
        #region Private Fields 
 
        #endregion Private Fields 
    }

    public interface IMarkupConverter
    {
        string ConvertXamlToHtml(string xamlText);
        string ConvertHtmlToXaml(string htmlText);
        string ConvertRtfToHtml(string rtfText);
        string ConvertHtmlToRtf(string htmlText);
    }

    /*
    public class MarkupConverter : IMarkupConverter
    {
        public string ConvertXamlToHtml(string xamlText)
        {
            return RTFtoHTML.ConvertXamlToHtml(xamlText, false);
        }

        public string ConvertRtfToHtml(string rtfText)
        {
            return RTFtoHTML.ConvertRtfToHtml(rtfText);
        }
    }*/
} 
