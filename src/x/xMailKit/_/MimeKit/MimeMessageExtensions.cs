﻿using MailKit;
using MimeKit;
using MimeKit.Text;
using System.Text;

namespace MimeKit;
public static class MimeMessageExtensions {

  public static MimeMessage From(this MimeMessage message, string address) {
    message.From.Add(MailboxAddress.Parse(address));
    return message;
  }

  public static MimeMessage From(this MimeMessage message, string name, string address) {
    message.From.Add(new MailboxAddress(name, address));
    return message;
  }

  public static MimeMessage To(this MimeMessage message, string address) {
    message.To.Add(MailboxAddress.Parse(address));
    return message;
  }

  public static MimeMessage To(this MimeMessage message, string name, string address) {
    message.To.Add(new MailboxAddress(name, address));
    return message;
  }

  public static MimeMessage Cc(this MimeMessage message, string address) {
    message.Cc.Add(MailboxAddress.Parse(address));
    return message;
  }

  public static MimeMessage Cc(this MimeMessage message, string name, string address) {
    message.Cc.Add(new MailboxAddress(name, address));
    return message;
  }

  public static MimeMessage Bcc(this MimeMessage message, string address) {
    message.Bcc.Add(MailboxAddress.Parse(address));
    return message;
  }

  public static MimeMessage Bcc(this MimeMessage message, string name, string address) {
    message.Bcc.Add(new MailboxAddress(name, address));
    return message;
  }
  public static MimeMessage Subject(this MimeMessage message, string subject) {
    message.Subject = subject;
    return message;
  }

  public static MimeMessage Body(this MimeMessage message, string body, TextFormat textFormat = TextFormat.Plain) {
    message.Body = new TextPart(textFormat) { Text = body };
    return message;
  }

  public static MimeMessage Body(this MimeMessage message, BodyBuilder bodyBuilder) {
    message.Body = bodyBuilder.ToMessageBody();
    return message;
  }

  public static MimeMessage BodyFromFile(this MimeMessage message, string fileName, TextFormat textFormat = TextFormat.Plain, Func<string, string>? fileContentsFormatter = null) {
    var body = File.ReadAllText(fileName);
    //var body = "";
    // using (var reader = new StreamReader(File.OpenRead(fileName))) {
    //   body = reader.ReadToEnd();
    // }
    return message.Body(fileContentsFormatter is not null ? fileContentsFormatter(body) : body, textFormat);
  }

  //public static ITemplateRenderer DefaultRenderer = new ReplaceRenderer();

  //public static MimeMessage UsingTemplateFromFile<T>(this MimeMessage message, string filename, T model, bool isHtml = true) {
  //  var template = "";
  //  using (var reader = new StreamReader(File.OpenRead(filename))) {
  //    template = reader.ReadToEnd();
  //  }
  //  var result = Renderer.Parse(template, model, isHtml);
  //  message.Body( result, isHtml? TextFormat.Html : TextFormat.Plain);
  //  return message;
  //}


  public static MimeMessage HtmlBody(this MimeMessage message, string body) => message.Body(body, TextFormat.Html);
  public static MimeMessage HtmlBody(this MimeMessage message, StringBuilder sb) => message.HtmlBody(sb.ToString());
  public static MimeMessage HtmlBodyFromFile(this MimeMessage message, string fileName, Func<string, string>? fileContentsFormatter = null) => message.BodyFromFile(fileName, TextFormat.Html, fileContentsFormatter);

  public static void SendUsingSmptClient(this MimeMessage message, IMailKitAppSettings settings) => settings.SendUsingSmptClient(message);
  public static void SendUsingSmptClient(this IEnumerable<MimeMessage> messages, IMailKitAppSettings settings) => settings.SendUsingSmptClient(messages.ToArray());
  public async static Task SendUsingSmptClientAsync(this MimeMessage message, IMailKitAppSettings settings) => await settings.SendUsingSmptClientAsync(message);
  public async static Task SendUsingSmptClientAsync(this IEnumerable<MimeMessage> messages, IMailKitAppSettings settings) => await settings.SendUsingSmptClientAsync(messages.ToArray());

  #region Send



  //public static void Send(this MimeMessage msg, BodyBuilder body) {
  //  msg.Body = body.ToMessageBody();
  //  msg.Send();
  //}

  //public static void Send<T>(this MimeMessage msg, string subjectSufix, string htmlBody) => msg.Send(typeof(T), subjectSufix, htmlBody);
  //public static void Send(this MimeMessage msg, Type debugType, string subjectSufix, string htmlBody) => msg.Send(debugType.FullName + ": " + subjectSufix, htmlBody);
  ////public static void Send(this MimeMessage msg, object debugObject, string subjectSufix, string htmlBody) => msg.Send(debugObject.GetType(), subjectSufix, htmlBody);

  //public static void Send<T>(this MimeMessage msg, string subjectSuffix, Dictionary<string, string> body) {
  //  var sb = new StringBuilder();
  //  foreach (var dic in body) {
  //    sb.AppendFormat("<tr><th>{0}</th><td>{1}</td></tr>", dic.Key, dic.Value);
  //  }
  //  msg.Send<T>(subjectSuffix, $"<style>\r\ntable{{ border-collapse:collapse}}\r\nth{{background:#eee;border:1px solid #ccc;font-weight:bold;padding:.5em;text-align:left;}}\r\ntd{{ border:1px solid #ccc;padding:.5em;}}\r\n</style>\r\n<table><tbody>{sb}</tbody></table>");
  //}

  public static void SetMailMessageAlternateViews(this MimeMessage msg, BodyBuilder htmlBody, MimeEntity[] linkedResources) {
    var alternateViews = new MultipartAlternative();
    var htmlView = new MultipartAlternative {
      new TextPart(TextFormat.Html) {
        Text = htmlBody.HtmlBody
      }
    };
    foreach (var lr in linkedResources)
      htmlView.Add(lr);
    alternateViews.Add(htmlView);
    var plainView = new MultipartAlternative {
      new TextPart(TextFormat.Plain) {
        Text = htmlBody.TextBody
      }
    };
    alternateViews.Add(plainView);
    msg.Body = alternateViews;
  }

  //public static void SetMailMessageAlternateViews(MimeMessage msg, string htmlbody) {
  //  var alternative = new MultipartAlternative();
  //  if (msg.Body != null)
  //    alternative.Add(msg.Body);
  //  foreach (var le in msg.Attachments) {
  //    alternative.Add(le);
  //  }
  //}
  //public static void SetMailMessageAlternateViews(MimeMessage msg, string htmlbody) {
  //  var htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null/* TODO Change to default(_) if this is not a reference type */, System.Net.Mime.MediaTypeNames.Text.Html);
  //  foreach (var lr in LinkedResources)
  //    htmlView.LinkedResources.Add(lr);
  //  msg.AlternateViews.Add(htmlView);
  //  var plainView = AlternateView.CreateAlternateViewFromString(HtmlToText.ConvertHtml(htmlBody), null/* TODO Change to default(_) if this is not a reference type */, System.Net.Mime.MediaTypeNames.Text.Plain);
  //  msg.AlternateViews.Add(plainView);
  //}

  #endregion

  public static MimeMessage ApplySettings(this MimeMessage message, IMailKitAppSettings settings) {
    if (message.From.Count < 1) {
      message.From.Add(settings.DefaultFrom);
    }
    if ((message.To.Count + message.Cc.Count + message.Bcc.Count) < 1) {
      message.To.Add(settings.DefaultTo);
    }
    return message;
  }

  //public string FilePath { get; } = "\\Logs\\emails.log";

  //public string FormatMessage(string to, string subject, string message) => $"Date:{DateTime.Now}\nTo: {to}\nSubject: {subject}\nMessage: {message}\n\n";

  //public static Task AppendFileAsync(this IFileMessageService service, string to, string subject, string message) {
  //  var emailMessage = service.FormatMessage(to, subject, message);
  //  File.AppendAllText(service.FilePath, emailMessage);
  //  return Task.FromResult(0);
  //}

  //public static void SaveToPickupDirectory(this MimeMessage message, string pickupDirectory) {
  //  // Generate a random file name to save the message to.
  //  var path = Path.Combine(pickupDirectory, Guid.NewGuid().ToString() + ".eml");
  //  Stream stream;

  //  try {
  //    // Attempt to create the new file.
  //    stream = File.Open(path, FileMode.CreateNew);
  //  } catch (IOException) {
  //    // If the file already exists, try again with a new Guid.
  //    if (File.Exists(path))
  //    // Otherwise, fail immediately since it probably means that there is
  //    // no graceful way to recover from this error.
  //    throw;
  //  }

  //  try {
  //    using (stream) {
  //      using (var filtered = new FilteredStream(stream)) {
  //        filtered.Add(new SmtpDataFilter());
  //        // Make sure to write the message in DOS (<CR><LF>) format.
  //        var options = FormatOptions.Default.Clone();
  //        options.NewLineFormat = NewLineFormat.Dos;
  //        message.WriteTo(options, filtered);
  //        filtered.Flush();
  //        return;
  //      }
  //    }
  //  } catch {
  //    File.Delete(path);
  //    throw;
  //  }
  //}


}