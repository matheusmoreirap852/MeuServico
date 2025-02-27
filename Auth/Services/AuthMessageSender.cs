using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Net.Mail;
using System.Net;
using Auth.Models;

namespace Auth.Services;

public class AuthMessageSender : IEmailSender
{
    private EmailSettings _emailSettings;

    public AuthMessageSender(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public Task SendEmailAsync(string email, string subject, string message, List<IFormFile> arquivos)
    {
        try
        {
            Execute(email, subject, message, arquivos).Wait();
            return Task.FromResult(0);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task ExecuteOuvidoria(string email, string subject, string message, ArrayList anexos)
    {
        try
        {
            string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.UsernameEmail, "E-mail")
            };

            mail.To.Add(new MailAddress(toEmail));
            mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

            mail.Subject = "Recuperar senha Sesc " + subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
            {
                //smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                smtp.UseDefaultCredentials = false; // Use local IP, no credentials needed
                smtp.EnableSsl = false; // Ativar o TLS
                smtp.Timeout = 10000;
                await smtp.SendMailAsync(mail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public async Task Execute(string email, string subject, string message, List<IFormFile> arquivos)
    {

        using (SmtpClient smtpCliente = new SmtpClient())
        {
            try
            {
                string nomeArquivo = "";
                if (email == null || email == "")
                {
                    email = _emailSettings.ToEmail;
                }

                MailMessage mail = new MailMessage();
                // Obtem os anexos contidos em um arquivo arraylist e inclui na mensagem

                mail.From = new MailAddress(_emailSettings.FromEmail);
                mail.To.Add(new MailAddress(_emailSettings.ToEmail));
                mail.CC.Add(new MailAddress(email));
                mail.Subject = "Recuperar senha Portal";
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail, _emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
