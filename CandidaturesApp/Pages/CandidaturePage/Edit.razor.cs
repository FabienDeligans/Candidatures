using System;
using System.Diagnostics;
using System.Linq;
using Blazored.Modal;
using CandidaturesCore.Controller;
using CandidaturesCore.Model;
using Microsoft.AspNetCore.Components;

namespace CandidaturesApp.Pages.CandidaturePage
{
    public partial class Edit
    {

        [Parameter]
        public string CandidatureId { get; set; }

        public Candidature Candidature { get; set; }
        private bool InEditMode { get; set; }

        [CascadingParameter]
        public BlazoredModalInstance BlazoredModal { get; set; }
        public string FileName { get; set; }


        protected override void OnInitialized()
        {
            var controller = new BaseController<Candidature>();

            if (CandidatureId != null)
            {
                Candidature = controller.QueryCollection().FirstOrDefault(v => v.Id == CandidatureId);
                InEditMode = true;
            }
            else
            {
                Candidature = new Candidature();
                InEditMode = false;
                Candidature.Date = DateTime.Now;
                Candidature.DateEtat = DateTime.Now;
            }
        }

        public void Save()
        {
            var controller = new BaseController<Candidature>();

            Candidature.Date = Candidature.Date.Date.ToUniversalTime();
            Candidature.DateEtat = Candidature.DateEtat.Date.ToUniversalTime();
            Candidature.Entreprise = Candidature.Entreprise.ToUpper();

            if (InEditMode)
            {
                controller.ReplaceOne(Candidature);
            }
            else
            {
                controller.Insert(Candidature);
            }

            BlazoredModal.Close();

        }

        public void Cancel()
        {
            BlazoredModal.Cancel();
        }

        public void OpenUrl(string url)
        {
            if (url == null || !url.Contains(@$"C:\Users\delig\Google Drive\CV\Annonces\")) return;
            Process p = new Process
            {
                StartInfo =
                {
                    UseShellExecute = true,
                    FileName = url,
                    Verb = "Open"
                }
            };
            p.Start();
        }

        public void GetUrl()
        {
            if (FileName == null) return;
            var name = FileName.Replace(@$"C:\fakepath\", "");
            Candidature.Annonce = @$"C:\Users\delig\Google Drive\CV\Annonces\{name}";
        }
    }
}
