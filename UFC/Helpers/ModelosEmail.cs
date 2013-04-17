using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpers;

namespace UFC.Helpers
{    
    public static class ModelosEmail
    {
        private static string remetente = "ctracker@celtica.com.br";
        private static string nomeremetente = "BolaoOSS";

        public static void EsqueciSenha(string destino, string senha) {
            try
            {
                string corpo = "Sua nova senha é: " + senha;
                SendMail oSendMail = new SendMail();
                oSendMail.setAssunto("Nova senha");
                oSendMail.setCorpo(corpo);
                oSendMail.setRemetente(remetente, nomeremetente);
                oSendMail.setDestinatario(destino);
                oSendMail.sendEmail();            
            }
            catch (Exception)
            {
            }
        }        
        public static void ConfirmaCadastro(string destino,string url) {
            try
            {
                SendMail oSendMail = new SendMail();
                oSendMail.setAssunto("Confirmação de cadastro");
                oSendMail.setCorpo(url);
                oSendMail.setRemetente(remetente, nomeremetente);
                oSendMail.setDestinatario(destino);
                oSendMail.sendEmail();
            }
            catch (Exception)
            {
            }
        }
    }
}